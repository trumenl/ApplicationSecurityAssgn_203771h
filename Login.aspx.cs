using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Security.Cryptography;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Net.Mail;

namespace ApplicationSecurityAssgn_203771h
{
    public partial class Login : System.Web.UI.Page
    {

        string MYDBConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MYDBConnection"].ConnectionString;

        public string Action = null;

        static string RandomNumber;

       




        protected void Page_Load(object sender, EventArgs e)
        {

        }



        public class MyObject
        {
            public string success { get; set; }

            public List<string> ErrorMessage { get; set; }
        }






        



        protected void LoginMe(object sender, EventArgs e)
        {

            
            string userid = HttpUtility.HtmlEncode(tb_userid.Text.ToString().Trim());
            string pwd = HttpUtility.HtmlEncode(tb_pwd.Text.ToString().Trim());
            SHA512Managed hashing = new SHA512Managed();
            string dbHash = getDBHash(userid);
            string dbSalt = getDBSalt(userid);

            try
            {
                //if (ValidateCaptcha())
                //{


                    if (dbSalt != null && dbSalt.Length > 0 && dbHash != null && dbHash.Length > 0)
                    {

                 
                    string pwdWithSalt = pwd + dbSalt;
                        byte[] hashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwdWithSalt));
                        string userHash = Convert.ToBase64String(hashWithSalt);

                        if (userHash.Equals(dbHash))
                        {

                        Action = " Has logged in successfully";
                        createLog();
                        Session["LoggedIn"] = HttpUtility.HtmlEncode(tb_userid.Text.Trim());
                            //Response.Redirect("HomePage.aspx", false);

                        


                        // Creates a new GUID ( a unique value & almost impossible to guess) and save it as a new seesion variable
                        //called AuthToken. This same GUID is then saved into a cookie named Authtoken
                        string guid = Guid.NewGuid().ToString();
                            Session["AuthToken"] = guid;

                            Response.Cookies.Add(new HttpCookie("AuthToken", guid));

                        Random random = new Random();
                        RandomNumber = random.Next(000000, 999999).ToString();

                        CreateOTP(userid, RandomNumber);

                        verificationcode(RandomNumber);


                        Response.Redirect("Verification.aspx", false);



                    };


                      




                    }

                else
                {
                    lblMessage.Text = "Email or password is not valid. Please try again .";
                    //Response.Redirect("Login.aspx");
                }


                //}


               //else
               // {
                    //lbl_gScore.Text = "Email or password is not valid. Please try againnn.";
                    //Response.Redirect("Login.aspx");
               // }



                    



            }
            catch (Exception ex) { throw new Exception(ex.ToString());}
            finally { }

        }


  





        public bool ValidateCaptcha()
        {
            bool result = true;

            string captchaResponse = Request.Form["g-recaptcha-response"];
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create
                ("https://www.google.com/recaptcha/api/siteverify?secret=6Lf6a2UeAAAAAAXT-U8zE3af5llG8UcsWeB4zsZe &response" + captchaResponse);

            try
            {
                using (WebResponse wResponse = req.GetResponse())
                {
                    using (StreamReader readStream = new StreamReader(wResponse.GetResponseStream()))
                    {
                        string jsonResponse = readStream.ReadToEnd();
                        lbl_gScore.Text = jsonResponse.ToString();
                        JavaScriptSerializer js = new JavaScriptSerializer();
                        MyObject jsonObject = js.Deserialize<MyObject>(jsonResponse);
                        result = Convert.ToBoolean(jsonObject.success);

                    }
                }
                return result;
            }
            catch (WebException ex)
            {
                throw ex;
            }


        }





        // Checks username and password (hardcoded for now)
        //if (tb_userid.Text.Trim().Equals("Trumenlim") && tb_pwd.Text.Trim().Equals("Rebelt7i@123"))
        //{






        // Response.Redirect("Homepage.aspx", false);
        //}



        // else
        //{
        //    lblMessage.Text = "Wrong username or password";

        //}
        //}
        //}





        protected string getDBHash(string userid)
        {
            string h = null;
            SqlConnection connection = new SqlConnection(MYDBConnectionString);
            string sql = "select PasswordHash FROM Account WHERE Email=@USERID";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@USERID", userid);
            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        if (reader["PasswordHash"] != null)
                        {
                            if (reader["PasswordHash"] != DBNull.Value)
                            {
                                h = reader["PasswordHash"].ToString();
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { connection.Close(); }
            return h;
        }




        protected string getDBSalt(string userid)
        {
            string s = null;
            SqlConnection connection = new SqlConnection(MYDBConnectionString);
            string sql = "select PASSWORDSALT FROM ACCOUNT WHERE Email=@USERID";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@USERID", userid);
            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["PASSWORDSALT"] != null)
                        {
                            if (reader["PASSWORDSALT"] != DBNull.Value)
                            {
                                s = reader["PASSWORDSALT"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { connection.Close(); }
            return s;
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
                            cmd.Parameters.AddWithValue("@Email", HttpUtility.HtmlEncode(tb_userid.Text.Trim()));
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


        protected string CreateOTP (string userid, string RandomNumber)
        {
            string otp = null;
            SqlConnection con = new SqlConnection(MYDBConnectionString);
            string sql = "update Account set Verification = @Verification where Email=@Email";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@Verification", RandomNumber);
            cmd.Parameters.AddWithValue("@Email", userid);

            try
            {
                con.Open();
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["Verification"]!= null)
                        {
                            otp = reader["Verification"].ToString();
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                throw new Exception(ex.ToString());

            }
            
            finally { con.Close(); }

            return otp;
        }

        protected string verificationcode(string verifycode)
        {
            string senderAddress = "Trumen <limtrum1@gmail.com>";

            string str = null;

            
            
            
           var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("limtrum1@gmail.com", "Rebelt7i@123"),
             EnableSsl = true,
            };

            var messageSend = new MailMessage
            {
                Subject = "Verification",
                Body = "This is your verification code " + verifycode + " for SITCONNECT, Thank you!"
            };

            messageSend.To.Add(tb_userid.Text.ToString());
            messageSend.From = new MailAddress(senderAddress);

            try
            {
                smtpClient.Send(messageSend);
                return str;
            }

            catch
            {
                throw;
            }


        }
      






    }
}