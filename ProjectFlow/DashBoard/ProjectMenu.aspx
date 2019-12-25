<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectMenu.aspx.cs" Inherits="ProjectFlow.ProjectMenu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>   
    <style type="text/css">
        .auto-style1 {
            width: 100%;
            height: 124px;
        }
        .auto-style2 {
            width: 234px;
        }
    </style>
    
</head>
<body>
    <form id="form1" runat="server">
        <div class="modal fade" id="CreateProject" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <asp:Label ID="Label4" runat="server" Text="Create Project"></asp:Label>
                    </div>
                    <div class="modal-body">
                        <table class="auto-style1">
                            <tr>
                                <td class="auto-style2">
                                    <asp:Label ID="Label1" runat="server" Text="Name"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="NameTB" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style2">
                                    <asp:Label ID="Label2" runat="server" Text="Description"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="DescTB" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style2">
                                    <asp:Label ID="Label5" runat="server" Text="Project ID"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="ProjectIdTB" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style2">
                                    &nbsp;</td>
                                <td>
                                    <asp:Button ID="CreateBtn" runat="server" Text="Create" OnClick="CreateBtn_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="modal-footer">
                    </div>
                </div>
            </div>
        </div>
    <div>
        <button type="button" class="btn btn-info btn-lg" data-toggle="modal" data-target="#CreateProject">Open Modal</button>
    </div>
    <div>
        
        <asp:GridView ID="projectGV" CssClass="table table-hover table-bordered" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="projectGV_SelectedIndexChanged" Width="531px">
            <Columns>
                <asp:BoundField DataField="projectID" HeaderText="Project ID" />
                <asp:BoundField DataField="projectName" HeaderText="Name" />
                <asp:BoundField DataField="projectDescription" HeaderText="Description" />
                <asp:CommandField SelectText="Open" ShowSelectButton="True" />
            </Columns>
        </asp:GridView>
        
    </div>

    </form>

    <script>
        $('#CreateProject').on('show.bs.modal', function (event) {
          var button = $(event.relatedTarget)              
          var modal = $(this)         
        })
    </script>
</body>
</html>
