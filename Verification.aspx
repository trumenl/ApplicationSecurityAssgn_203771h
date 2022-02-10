<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Verification.aspx.cs" Inherits="ApplicationSecurityAssgn_203771h.Verification" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
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
