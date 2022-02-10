﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ApplicationSecurityAssgn_203771h
{
    public partial class Verification : System.Web.UI.Page
    {
        string MYDBConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoggedIn"] != null && Session["AuthToken"] != null && Request.Cookies["AuthToken"] != null)
            {
                if (!Session["AuthToken"].ToString().Equals(Request.Cookies["AuthToken"].Value))
                {
                    Response.Redirect("Login.aspx", false);
                }
            }

        }

        protected string vCodeOTP(string email)
        {
            string otp = null;
            SqlConnection con = new SqlConnection(MYDBConnectionString);
            string sql = "select Verification from Account where Email = @EMAIL";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@EMAIL", email);
            try
            {
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        otp = reader["Verification"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                con.Close();
            }
            return otp;
        }

        protected void VerifyCode(object sender, EventArgs e)
        {
            if (HttpUtility.HtmlEncode(verification_code.Text.ToString()) == vCodeOTP(Session["LoggedIn"].ToString()))
            {
                

                Response.Redirect("HomePage.aspx", false);

            }
            else
            {
                lbl_message.Text = "Incorrect verification code!";
            }
        }

       
    }
}