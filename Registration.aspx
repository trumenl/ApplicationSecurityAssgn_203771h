<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="ApplicationSecurityAssgn_203771h.Registration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script type="text/javascript">
        function validate() {
            var str = document.getElementById('<%=tb_password.ClientID %>').value;

            if (str.length < 12) {
                document.getElementById("lbl_pwdchecker").innerHTML = "Password Length Must be at least 12 Characters";
                document.getElementById("lbl_pwdchecker").style.color = "Red";
                return ("Ptoo_short");
            }

            else if (str.search(/[0-9]/) == -1) {
                document.getElementById("lbl_pwdchecker").innerHTML = "Password require at least 1 number";
                document.getElementById("lbl_pwdchecker").style.color = "Red";
                return ("no_number");

            }

            else if (str.search(/[a-z]/) == -1) {
                document.getElementById("lbl_suggest").innerHTML = "Password requires at least 1 lowercase letter"
                document.getElementById("lbl_suggest").style.color = "Red";
                return ("need_lowercase");
            }
            else if (str.search(/[A-Z]/) == -1) {
                document.getElementById("lbl_suggest").innerHTML = "Password requires at least 1 uppercase letter"
                document.getElementById("lbl_suggest").style.color = "Red";
                return ("need_uppercase");
            }
            else if (str.search(/[!@#$%&?]/) == -1) {
                document.getElementById("lbl_suggest").innerHTML = "Password requires at least 1 special charcater"
                document.getElementById("lbl_suggest").style.color = "Red";
                return ("need_special");
            }
           
            document.getElementById("lbl_pwdchecker").innerHTML = "Excellent!";
            document.getElementById("lbl_pwdchecker").style.color = "Green";
            

        }
    </script>




</head>
<body>

        <div class="topnav">
  <a href="HomePage.aspx">SITCONNECT</a>
  <a href="Login.aspx">Login</a>
  <a class="active" href="Registration.aspx">Register</a>
</div>



    <form id="form1" runat="server">
        <div>
            Registration<br />
            <br />
            First Name:&nbsp;
            <asp:TextBox ID="first_name" runat="server"></asp:TextBox>
            <br />
            <br />
            Last Name:&nbsp;
            <asp:TextBox ID="last_name" runat="server"></asp:TextBox>
            <br />
            <br />
            Credit Card Number:&nbsp;
            <asp:TextBox ID="cc_number" runat="server"></asp:TextBox>
            <br />
            <br />
            Credit Card Date of expiry:&nbsp;
            <asp:TextBox ID="cc_date" runat="server" type="date"></asp:TextBox>
            <br />
            <br />
            Credit Card CVV:&nbsp; <asp:TextBox ID="cc_cvv" runat="server"></asp:TextBox>
            <br />
            <br />
            Email Address:&nbsp;
            <asp:TextBox ID="email_address" runat="server" OnTextChanged="TextBox4_TextChanged" Wrap="False"></asp:TextBox>
            <br />
            <br />
            Password:&nbsp;&nbsp;&nbsp; <asp:TextBox ID="tb_password" runat="server" type="password" Width="209px" ></asp:TextBox>
            <asp:Label ID="lbl_pwdchecker" runat="server" Text="pwdchecker"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button1" runat="server" Text="Check Password" Width="201px" onClick="btn_checkPassword_Click"/>
            <br />
        </div>
        <br />
        Date of Birth:&nbsp;
        <asp:TextBox ID="date_of_birth" runat="server"  type="date"></asp:TextBox>
        <br />
        <br />
        Photo:&nbsp;
        <asp:TextBox ID="photo" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="Button2" runat="server" Text="Submit" Width="223px" OnClick="btn_Submit_Click"/>
    </form>
</body>
</html>










<style>
body {
  margin: 0;
  font-family: Arial, Helvetica, sans-serif;
}

.topnav {
  overflow: hidden;
  background-color: #333;
}

.topnav a {
  float: left;
  color: #f2f2f2;
  text-align: center;
  padding: 14px 16px;
  text-decoration: none;
  font-size: 17px;
}

.topnav a:hover {
  background-color: #ddd;
  color: black;
}

.topnav a.active {
  background-color: #04AA6D;
  color: white;
}
</style>
