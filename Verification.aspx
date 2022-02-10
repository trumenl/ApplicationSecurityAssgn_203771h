<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Verification.aspx.cs" Inherits="ApplicationSecurityAssgn_203771h.Verification" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>

    <div class="topnav">
  <a href="HomePage.aspx">SITCONNECT</a>
  <a href="Login.aspx">Login</a>
  <a href="Registration.aspx">Register</a>
</div>


    <p>
        <br />
    </p>
    <p>
        &nbsp;</p>
    <form id="form1" runat="server">
        <p>
            Enter verification code:
            <asp:TextBox ID="verification_code" runat="server"></asp:TextBox>
&nbsp;&nbsp;
            <asp:Button ID="verify_button" runat="server" Text="Verify" OnClick="VerifyCode" />
        </p>
        <p>
            &nbsp;</p>
        <p>
            <asp:Label ID="lbl_message" runat="server"></asp:Label>
        </p>
        <div>
        </div>
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
