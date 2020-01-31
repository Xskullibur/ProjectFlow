<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmailTest.aspx.cs" Inherits="ProjectFlow.Tasks.WebForm1" Async="True" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

        <i></i>

        <div>
            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Button" />
        </div>
        <p>
            <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
        </p>
        <p>
            <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Button" />
        </p>
    </form>
</body>
</html>
