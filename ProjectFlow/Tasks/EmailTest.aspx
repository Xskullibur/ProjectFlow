<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmailTest.aspx.cs" Inherits="ProjectFlow.Tasks.WebForm1" Async="True" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_ClickAsync" />
        </div>
    </form>
</body>
</html>
