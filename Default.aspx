<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ProjectFlow.Default" %>

<!DOCTYPE html>
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style3 {
            width: 152px;
        }
        .auto-style4 {
            width: 152px;
            height: 37px;
        }
        .auto-style5 {
            height: 37px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table style="width:100%;">
                <tr>
                    <td colspan="2">
                        <asp:Label ID="Label3" runat="server" Text="Login"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">
                        <asp:Label ID="Label1" runat="server" Text="Username:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="usernameTxtBox" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style4">
                        <asp:Label ID="Label2" runat="server" Text="Password:"></asp:Label>
                    </td>
                    <td class="auto-style5">
                        <asp:TextBox ID="passwordTxtBox" runat="server" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">
                        &nbsp;</td>
                    <td>
                        <asp:Button ID="Button1" runat="server" OnClick="BasicLoginEvent" Text="Login" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
