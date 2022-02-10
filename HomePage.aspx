<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="ApplicationSecurityAssgn_203771h.HomePage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>

     <div class="topnav">
  <a class="active" href="HomePage.aspx">SITCONNECT</a>
  
</div>


    <form id="form1" runat="server">
        <div>
            <fieldset>
                <legend> HomePage</legend>

                <br />

                <asp:Label ID="lblMessage" runat="server" EnableViewState="false" />



                <br />
                <br />



                <t1> User profile:<br />
                name:
                <asp:Label ID="name" runat="server"></asp:Label>
                <br />
                email: </t1>
                <asp:Label ID="email" runat="server"></asp:Label>





                <br />
                <br />

                <asp:Button ID="btnLogout" runat="server" Text="Logout" OnClick="LogoutMe" Visible="false" />

                <br />
                <br />
                <br />
                <br />
                <br />
                Password:
                <asp:TextBox ID="ChangePasswd" runat="server"></asp:TextBox>
&nbsp;<asp:Button ID="ChangePasswordButton" runat="server" OnClick="ChangePassword" Text="Change Password" />
                <br />
                <br />
                <br />

                <asp:Label ID="password_message" runat="server"></asp:Label>

                <p/>

            </fieldset>
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
