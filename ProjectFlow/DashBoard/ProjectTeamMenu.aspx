<%@ Page Title="" Language="C#" MasterPageFile="~/ServicesWithContent.Master" AutoEventWireup="true" CodeBehind="ProjectTeamMenu.aspx.cs" Inherits="ProjectFlow.DashBoard.ProjectTeamMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style8 {
            width: 147px;
        }
        .auto-style9 {
            width: 111px;
        }
        .auto-style10 {
            width: 111px;
            height: 42px;
        }
        .auto-style12 {
            width: 147px;
            height: 42px;
        }
        .auto-style13 {
            width: 231px;
        }
        .auto-style14 {
            width: 231px;
            height: 42px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="modal fade" id="CreateTeam" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <asp:Label ID="Label4" runat="server" Text="Create Team"></asp:Label>
                </div>
                <div class="modal-body">                    
                    <table class="auto-style1">
                        <tr>
                            <td class="auto-style9">
                                <asp:Label ID="Label1" runat="server" Text="Team Name"></asp:Label>
                                &nbsp;<br />
                                <br />
                            </td>
                            <td class="auto-style13">
                                <asp:TextBox ID="NameTB" CssClass="form-control" runat="server" Width="223px"></asp:TextBox>
                                <br />
                            </td>
                            <td class="auto-style8">
                                &nbsp;
                                <asp:RequiredFieldValidator ID="nameRequiredValidator" runat="server" ControlToValidate="NameTB" ErrorMessage="*" ForeColor="Red" Font-Size="Large"></asp:RequiredFieldValidator>
                                &nbsp;<asp:RegularExpressionValidator ID="nameRegexValidator" runat="server" validationexpression="^.{1,255}$" ControlToValidate="NameTB" ErrorMessage="max 255 characters!" Font-Size="Small" ForeColor="Red"></asp:RegularExpressionValidator>
                                <br />
                                <br />
                            </td>
                        </tr>                        
                        <tr>
                            <td class="auto-style10">
                                <asp:Label ID="Label2" runat="server" Text="Description"></asp:Label>
                                <br />
                                <br />
                            </td>
                            <td class="auto-style14">
                                <asp:TextBox ID="DescTB" CssClass="form-control" runat="server" Width="222px"></asp:TextBox>
                                <br />
                            </td>
                            <td class="auto-style12">
                                &nbsp;

                                &nbsp;<br />
                                <br />

                            </td>
                        </tr>

                        <tr>
                            <td class="auto-style9">
                                <br />
                                <br />
                            </td>
                            <td class="auto-style13">
                                <asp:Button ID="CreateBtn" CssClass="btn btn-success" runat="server" Text="Create" OnClick="CreateBtn_Click" />
                                <br />
                            </td>
                            <td class="auto-style8">
                                &nbsp;

                                &nbsp;&nbsp;<br />
                                <br />

                            </td>
                        </tr>                       
                    </table>                   
                </div>
                <div class="modal-footer">
                                <asp:Label ID="errorLabel" runat="server" ForeColor="Red"></asp:Label>
                </div>
            </div>
        </div>
    </div>
    <div class="container">
        <div>                        
            <asp:Button ID="CreateTeamBtn" CssClass="btn btn-primary" runat="server" OnClientClick="myfunction(); return false;" UseSubmitBehavior="false" data-toggle="modal" data-target="#CreateTeam" Text="Create Team" />           
        </div>
        <br>
        <div>
            
            <asp:GridView ID="TeamGV" CssClass="table table-dark table-hover table-bordered" runat="server" AutoGenerateColumns="False" Width="1320px" OnRowCancelingEdit="TeamGV_RowCancelingEdit" OnRowEditing="TeamGV_RowEditing" OnRowUpdating="TeamGV_RowUpdating" OnSelectedIndexChanged="TeamGV_SelectedIndexChanged">
                <Columns>
                    <asp:BoundField DataField="teamID" HeaderText="ID" />
                    <asp:BoundField DataField="teamName" HeaderText="Team Name" />
                    <asp:BoundField DataField="teamDescription" HeaderText="Description" />
                    <asp:CommandField ButtonType="Button" SelectText="Open" ShowSelectButton="True">
                        <ControlStyle CssClass="btn btn-primary"/>
                    </asp:CommandField>
                    <asp:CommandField ButtonType="Button" ShowEditButton="True">
                        <ControlStyle CssClass="btn btn-primary"/>
                    </asp:CommandField>
                </Columns>
            </asp:GridView>
            
        </div>
    </div>
    <script>
        $(document).ready(function () {
            hideSidebar();    
            $('#CreateProject').modal('hide');
        });      
    </script>
</asp:Content>
