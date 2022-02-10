<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ApplicationSecurityAssgn_203771h.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="https://www.google.com/recaptcha/api.js?render=6Lf6a2UeAAAAACuA7vNy4tSK7-UATamkzImMW-0I"></script>
</head>
<body>

    <div class="topnav">
  <a href="HomePage.aspx">SITCONNECT</a>
  <a class="active" href="Login.aspx">Login</a>
  <a href="Registration.aspx">Register</a>
</div>




    <form id="form1" runat="server">
        <div>

            <fieldset>
                <legend>Login</legend>

                

                <p>Email : <asp:TextBox ID="tb_userid" runat="server" Height="25px" Width="137px" /> </p>
                <p>Password : <asp:TextBox ID="tb_pwd" runat="server" type="password" Height="24px" Width="137px" /> </p>
                <asp:Label runat="server" ID="lbl_gScore"></asp:Label>
                
                <p><asp:Button ID="btnSubmit" runat="server" Text="Login" OnClick="LoginMe" Height="27px" Width="133px" />
                <br />
                <br />

                 <asp:Label ID="lblMessage" runat="server" EnableViewState="False" > </asp:Label>

                         

                </p>
                <p>&nbsp;</p>
            </fieldset>
        </div>
        <input type="hidden" id="g-recaptcha-response" name="g-recaptcha-response"/>
    </form>



   



</body>


     <script>
         grecaptcha.ready(function () {
             grecaptcha.execute('6Lf6a2UeAAAAACuA7vNy4tSK7-UATamkzImMW-0I', { action: 'Login' }).then(function (token) {
                 document.getElementById("g-recaptcha-response").value = token;
             });
         });
     </script>


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
