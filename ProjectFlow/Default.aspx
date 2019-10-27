<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ProjectFlow.Default" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title></title>
    </head>
<body>
    <form id="form1" runat="server">
        <div>
            <table style="width:100%;">
                <tr>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="Login"></asp:Label>
                        <br />
                        <asp:Login ID="LoginForm" runat="server" BackColor="#F7F7DE" BorderColor="#CCCC99" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="10pt" Height="462px" OnAuthenticate="BasicLoginEvent" Width="646px">
                            <TitleTextStyle BackColor="#6B696B" Font-Bold="True" ForeColor="#FFFFFF" />
                        </asp:Login>
                    </td>
                </tr>
                </table>
        </div>
    </form>
</body>
</html>
