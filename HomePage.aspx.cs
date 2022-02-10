using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ApplicationSecurityAssgn_203771h
{
    public partial class HomePage : System.Web.UI.Page
    {
        

        string MYDBConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MYDBConnection"].ConnectionString;
        byte[] Key;
        byte[] IV;
        string userID = null;
        public string Action = null;
        static string finalHash;
        static string salt;
        




        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoggedIn"] != null && Session["AuthToken"] != null && Request.Cookies["AuthToken"] != null)
            {
                if (!Session["AuthToken"].ToString().Equals(Request.Cookies["AuthToken"].Value))
                {
                    Response.Redirect("Login.aspx", false);
                }
                else
                {
                    lblMessage.Text = "You have successfully logged in";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    btnLogout.Visible = true;
                }
            }
            else
            {
                Response.Redirect("Login.aspx", false);
            }


            if (Session["LoggedIn"] != null)
            {
                userID = (string)Session["LoggedIn"];

                displayUserProfile(userID);
            }



        }


        protected void displayUserProfile(string userid)
        {
            SqlConnection connection = new SqlConnection(MYDBConnectionString);
            string sql = "select * FROM Account WHERE Email=@USERID";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@USERID", userid);
            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["Fname"] != DBNull.Value)
                        {
                            name.Text = reader["fname"].ToString();
                        }

                        if (reader["Email"] != DBNull.Value)
                        {
                            email.Text = reader["Email"].ToString();
                        }
                        if (reader["IV"] != DBNull.Value)
                        {
                            IV = Convert.FromBase64String(reader["IV"].ToString());
                        }
                        if (reader["Key"] != DBNull.Value)
                        {
                            Key = Convert.FromBase64String(reader["Key"].ToString());
                        }
                    }

                }
            }//try
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                connection.Close();
            }
        }


        protected void ChangePassword(object sender, EventArgs e)
        {


            //string MYDBConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MYDBConnection"].ConnectionString;

            //SqlConnection connection = new SqlConnection(MYDBConnectionString);

            string sql = "UPDATE [Account] SET [PasswordHash] = @PasswordHash, [PasswordSalt] = @PasswordSalt WHERE Email = @USERID";
            //SqlCommand command = new SqlCommand(sql, connection);

            using (SqlConnection con = new SqlConnection(MYDBConnectionString))
            {

                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {

                        //string pwd = get value from your Textbox
                        string pwd = ChangePasswd.Text.ToString().Trim();
                        //Generate random "salt"
                        RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                        byte[] saltByte = new byte[8];
                        //Fills array of bytes with a cryptographically strong sequence of random values.
                        rng.GetBytes(saltByte);
                        salt = Convert.ToBase64String(saltByte);
                        SHA512Managed hashing = new SHA512Managed();
                        string pwdWithSalt = pwd + salt;
                        byte[] plainHash = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwd));
                        byte[] hashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwdWithSalt));
                        finalHash = Convert.ToBase64String(hashWithSalt);
                        RijndaelManaged cipher = new RijndaelManaged();
                        cipher.GenerateKey();
                        Key = cipher.Key;
                        IV = cipher.IV;




                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@PasswordHash", finalHash);
                        cmd.Parameters.AddWithValue("@PasswordSalt", salt);
                        cmd.Parameters.AddWithValue("@USERID", Session["LoggedIn"].ToString());

                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                    }
                }
            }

            password_message.Text = "Your password have been changed.";



        }


        protected void LogoutMe(object sender, EventArgs e)
        {

            Action = "Has logged out successfully";
            createLog();
            //Clear() removes all variables stored in session and
            //if user try to browse the site, same sessionID which was previously assigned to him will be used.
            Session.Clear();
            //Abandon() removes all variables stored in session, fire session_end event, and
            //if  user try to browse the site, a new sessionID will be assigned to it.
            Session.Abandon();
            Session.RemoveAll();
            Action = "Has logged out successfully";
            

            Response.Redirect("Login.aspx", false);


            if (Request.Cookies["ASP.NET_SessionId"] != null)
            {
                Response.Cookies["ASP.NET_SessionId"].Value = string.Empty;
                Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddMonths(-20);

            }

            if (Request.Cookies["AuthToken"] != null)
            {
                Response.Cookies["AuthToken"].Value = string.Empty;
                Response.Cookies["AuthToken"].Expires = DateTime.Now.AddMonths(-20);
            }
        }




        protected void createLog()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(MYDBConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO AuditLog VALUES (@ActionLog, @Email, @DateTime)"))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@Email", userID);
                            cmd.Parameters.AddWithValue("@DateTime", DateTime.Now);
                            cmd.Parameters.AddWithValue("@ActionLog", Action);
                            cmd.Connection = con;
                           con.Open();
                            cmd.ExecuteNonQuery();
                           con.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
          }
        }







    }
}