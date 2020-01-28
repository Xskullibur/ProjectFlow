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
            width: 371px;
        }
        .auto-style14 {
            width: 371px;
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
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                      <span aria-hidden="true">&times;</span>
                    </button>
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
                                <asp:TextBox ID="NameTB" CssClass="form-control" placeholder="Required, max 255" runat="server" Width="223px"></asp:TextBox>
                                <br />
                            </td>
                            <td class="auto-style8">
                                &nbsp;
                                <asp:RequiredFieldValidator ID="nameRequiredValidator" runat="server" ValidationGroup="modelValidation" ControlToValidate="NameTB" ErrorMessage="*" ForeColor="Red" Font-Size="Large"></asp:RequiredFieldValidator>
                                &nbsp;<asp:RegularExpressionValidator ID="nameRegexValidator" runat="server" ValidationGroup="modelValidation" validationexpression="^.{1,255}$" ControlToValidate="NameTB" ErrorMessage="max 255 characters!" Font-Size="Small" ForeColor="Red"></asp:RegularExpressionValidator>
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
                                <asp:TextBox ID="DescTB" CssClass="form-control" placeholder="Optional, max 255" runat="server" Width="222px"></asp:TextBox>
                                <br />
                            </td>
                            <td class="auto-style12">
                                &nbsp;

                                &nbsp;<asp:RegularExpressionValidator ID="descRegexValidator" runat="server" ValidationGroup="modelValidation" validationexpression="^.{1,255}$" ControlToValidate="DescTB" ErrorMessage="max 255 characters!" Font-Size="Small" ForeColor="Red"></asp:RegularExpressionValidator>
                                <br />
                                <br />

                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style10">
                                <asp:Label ID="Label5" runat="server" Text="Description"></asp:Label>
                                <br />
                                <br />
                            </td>
                            <td class="auto-style14">
                                <asp:DropDownList ID="GroupDP" CssClass="form-control border border-dark" runat="server" AutoPostBack="False">
                                        <asp:ListItem Value="1">1</asp:ListItem>
                                        <asp:ListItem Value="2">2</asp:ListItem>
                                        <asp:ListItem Value="3">3</asp:ListItem>
                                        <asp:ListItem Value="4">4</asp:ListItem>
                                        <asp:ListItem Value="5">5</asp:ListItem>
                                        <asp:ListItem Value="6">6</asp:ListItem>
                                        <asp:ListItem Value="7">7</asp:ListItem>
                                        <asp:ListItem Value="8">8</asp:ListItem>
                                        <asp:ListItem Value="9">9</asp:ListItem>
                                        <asp:ListItem Value="10">10</asp:ListItem>
                                    </asp:DropDownList> 
                                <br />
                            </td>
                            <td class="auto-style12">
                              
                            </td>
                        </tr>

                        <tr>
                            <td class="auto-style9">
                                <br />
                                <br />
                            </td>
                            <td class="auto-style13">
                                <asp:Button ID="CreateBtn" ValidationGroup="modelValidation" CssClass="btn btn-success" runat="server" Text="Create" OnClick="CreateBtn_Click" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="createAnotherBtn" runat="server" CssClass="btn btn-success" OnClick="createAnotherBtn_Click" Text="Create Another" ValidationGroup="modelValidation" Width="160px" />
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
        <div class="row">
            <div class="col">
                <h1>
                    <asp:Label ID="Label3" runat="server" Font-Size="XX-Large" Text="Team Select"></asp:Label>
                </h1>
            </div>            
        </div>
        <br>
        <div class="row">
            <div class="col">
                <h3>
                    <asp:Label ID="InfoLabel" runat="server" Font-Size="Medium"></asp:Label>
                </h3>
            </div>            
        </div>
        <br>
        <div class="row"> 
            <div class="col-2">
                <asp:Button ID="CreateTeamBtn" CssClass="btn btn-primary" runat="server" OnClientClick="myfunction(); return false;" UseSubmitBehavior="false" data-toggle="modal" data-target="#CreateTeam" Text="Create Team" />
            </div>           
            <div class="col-3">
                 <asp:DropDownList ID="PageSelectDP" CssClass="form-control border border-dark" runat="server" AutoPostBack="True" OnSelectedIndexChanged="PageSelectDP_SelectedIndexChanged">
                    <asp:ListItem Value="0">Avaliable</asp:ListItem>
                    <asp:ListItem Value="1">Deleted</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-3">
            </div>           
            <div class="col-2">
                <asp:DropDownList ID="sortGroupDP" CssClass="form-control border border-dark" runat="server" AutoPostBack="True" OnSelectedIndexChanged="sortGroupDP_SelectedIndexChanged">
                    <asp:ListItem Value="0">Show All</asp:ListItem>
                    <asp:ListItem Value="1">1</asp:ListItem>
                    <asp:ListItem Value="2">2</asp:ListItem>
                    <asp:ListItem Value="3">3</asp:ListItem>
                    <asp:ListItem Value="4">4</asp:ListItem>
                    <asp:ListItem Value="5">5</asp:ListItem>
                    <asp:ListItem Value="6">6</asp:ListItem>
                    <asp:ListItem Value="7">7</asp:ListItem>
                    <asp:ListItem Value="8">8</asp:ListItem>
                    <asp:ListItem Value="9">9</asp:ListItem>
                    <asp:ListItem Value="10">10</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <br>
        <div class="row">
            <div class="col">
                <div style="overflow-x: auto;">
                    <asp:GridView ID="TeamGV" CssClass="table table-bordered" runat="server" AutoGenerateColumns="False" Width="1056px" OnRowCancelingEdit="TeamGV_RowCancelingEdit" OnRowEditing="TeamGV_RowEditing" OnRowUpdating="TeamGV_RowUpdating" OnSelectedIndexChanged="TeamGV_SelectedIndexChanged" OnPageIndexChanging="TeamGV_PageIndexChanging" AllowPaging="True" PageSize="4" OnRowDeleting="TeamGV_RowDeleting">
                        <HeaderStyle CssClass="thead-light"/>
                        <Columns>
                            <asp:BoundField DataField="teamID" HeaderText="ID" ReadOnly="true"/>

                            <asp:TemplateField HeaderText="Team Name">
                                <EditItemTemplate>
                                    <asp:TextBox ID="editNameTB" CssClass="form-control" runat="server" Text='<%# Bind("teamName") %>'></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="editNameRequiredValidator" runat="server" ValidationGroup="tableValidation" ControlToValidate="editNameTB" ErrorMessage="*" ForeColor="Red" Font-Size="Large"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="editNameRegexValidator" runat="server" ValidationGroup="tableValidation" validationexpression="^.{1,255}$" ControlToValidate="editNameTB" ErrorMessage="max 255 characters!" Font-Size="Small" ForeColor="Red"></asp:RegularExpressionValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="nameLabel" runat="server" Text='<%# Bind("teamName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Description">
                                <EditItemTemplate>
                                    <asp:TextBox ID="editDescTB" CssClass="form-control" runat="server" Text='<%# Bind("teamDescription") %>'></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="editDescRegexValidator" runat="server" ValidationGroup="tableValidation" validationexpression="^.{1,255}$" ControlToValidate="editDescTB" ErrorMessage="max 255 characters!" Font-Size="Small" ForeColor="Red"></asp:RegularExpressionValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="descLabel" runat="server" Text='<%# Bind("teamDescription") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Group">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="editGroupDP" CssClass="form-control border border-dark" Text='<%# Bind("group") %>' runat="server" AutoPostBack="False">
                                        <asp:ListItem Value="1">1</asp:ListItem>
                                        <asp:ListItem Value="2">2</asp:ListItem>
                                        <asp:ListItem Value="3">3</asp:ListItem>
                                        <asp:ListItem Value="4">4</asp:ListItem>
                                        <asp:ListItem Value="5">5</asp:ListItem>
                                        <asp:ListItem Value="6">6</asp:ListItem>
                                        <asp:ListItem Value="7">7</asp:ListItem>
                                        <asp:ListItem Value="8">8</asp:ListItem>
                                        <asp:ListItem Value="9">9</asp:ListItem>
                                        <asp:ListItem Value="10">10</asp:ListItem>
                                    </asp:DropDownList> 
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="descLabel" runat="server" Text='<%# Bind("group") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:CommandField ButtonType="Button" SelectText="View Member" ShowSelectButton="True">
                                <ControlStyle CssClass="btn btn-success" />
                            </asp:CommandField>
                            <asp:CommandField ButtonType="Button" ShowEditButton="True" ValidationGroup="tableValidation">
                                <ControlStyle CssClass="btn btn-warning" />
                            </asp:CommandField> 
                            
                            <asp:TemplateField>
                                <ItemTemplate>                     
                                    <asp:Button ID="deleteBtn" CssClass="btn btn-danger" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete" OnClientClick="return confirm('Are you sure to delete team?');"></asp:Button>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                           <div class="jumbotron jumbotron-fluid">
                                <div class="container">
                                    <h1 class="display-4">Seem Empty, create a team now!</h1>                                          
                                    <p>Team not showing?    <asp:HyperLink ID="emailLink" Text="click here!" NavigateUrl="mailto:projectflow.nyp.eadp@gmail.com" runat="server"></asp:HyperLink></p>
                                </div>
                           </div>
                       </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </div>
        </div>
         <br>
        <div class="row">
            <div class="col">
                <asp:Button ID="refreshBtn" CssClass="btn btn-primary" runat="server" Text="Refresh" OnClick="refreshBtn_Click" />
            </div>
        </div>
    </div>
    <script>
        $(document).ready(function () {
            hideSidebar();    
            $('#CreateProject').modal('hide');
        });      
    </script>
</asp:Content>
