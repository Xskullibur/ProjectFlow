<%@ Page Title="" Language="C#" MasterPageFile="~/ServicesWithContent.Master" AutoEventWireup="true" CodeBehind="ProjectMenu.aspx.cs" Inherits="ProjectFlow.DashBoard.ProjectMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <title></title>
        <style type="text/css">
            .auto-style1 {
                width: 100%;
                height: 124px;
            }

            .auto-style2 {
                width: 307px;
            }
            .auto-style6 {
                width: 307px;
                height: 28px;
            }
            .auto-style7 {
                height: 28px;
                width: 1525px;
            }
            .auto-style8 {
                width: 1525px;
            }
            .auto-style9 {
                width: 307px;
                height: 30px;
            }
            .auto-style10 {
                width: 1037px;
                height: 30px;
            }
            .auto-style13 {
                width: 1525px;
                height: 30px;
            }           
        </style>       
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="modal fade" id="CreateProject" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <asp:Label ID="Label4" runat="server" Text="Create Project"></asp:Label>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                      <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">                    
                    <table class="auto-style1">
                        <tr>
                            <td class="auto-style9">
                                <asp:Label ID="Label1" runat="server" Text="Name"></asp:Label>
                                <br />
                                <br />
                            </td>
                            <td class="auto-style13">
                                <asp:TextBox ID="NameTB" CssClass="form-control" placeholder="Required, max 255" runat="server"></asp:TextBox>
                                <br />
                            </td>
                            <td class="auto-style10">
                                &nbsp;
                                <asp:RequiredFieldValidator ID="nameRequiredValidator" runat="server" ControlToValidate="NameTB" ErrorMessage="*" ForeColor="Red" Font-Size="Large" ValidationGroup="modelValidation"></asp:RequiredFieldValidator>
                                &nbsp;<asp:RegularExpressionValidator ID="nameRegexValidator" runat="server" ValidationGroup="modelValidation" validationexpression="^.{1,255}$" ControlToValidate="NameTB" ErrorMessage="max 255 characters!" Font-Size="Small" ForeColor="Red"></asp:RegularExpressionValidator>
                                <br />
                                <br />
                            </td>
                        </tr>                        
                        <tr>
                            <td class="auto-style9">
                                <asp:Label ID="Label2" runat="server" Text="Description"></asp:Label>
                                <br />
                                <br />
                            </td>
                            <td class="auto-style13">
                                <asp:TextBox ID="DescTB" CssClass="form-control" placeholder="Optional, max 255" runat="server"></asp:TextBox>
                                <br />
                            </td>
                            <td class="auto-style10">
                                &nbsp;

                                &nbsp;<asp:RegularExpressionValidator ID="descRegexValidator" runat="server" ValidationGroup="modelValidation" validationexpression="^.{1,255}$" ErrorMessage="max 255 characters!" Font-Size="Small" ForeColor="Red" ControlToValidate="DescTB"></asp:RegularExpressionValidator>
                                <br />
                                <br />

                            </td>
                        </tr>

                        <tr>
                            <td class="auto-style6">
                                <asp:Label ID="Label5" runat="server" Text="Project ID"></asp:Label>
                                <br />
                                <br />
                            </td>
                            <td class="auto-style7">
                                <asp:TextBox ID="ProjectIdTB" placeholder="Required, exactly 6 character" CssClass="form-control" runat="server"></asp:TextBox>
                                <br />
                            </td>
                            <td class="auto-style10">
                                &nbsp;

                                &nbsp;<asp:RequiredFieldValidator ID="IdRequirdValidator" runat="server" ValidationGroup="modelValidation" ControlToValidate="ProjectIdTB" ErrorMessage="*" ForeColor="Red" Font-Size="Large"></asp:RequiredFieldValidator>
                                &nbsp;<asp:RegularExpressionValidator ID="projectIdRegexValidator" runat="server" ValidationGroup="modelValidation" validationexpression="^[a-zA-Z0-9]{6}$" ControlToValidate="ProjectIdTB" ErrorMessage="6 characters!" Font-Size="Small" ForeColor="Red"></asp:RegularExpressionValidator>

                                <br />
                                <br />

                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style2">&nbsp;</td>
                            <td class="auto-style8">
                            &nbsp;&nbsp;&nbsp;
                                <asp:Button ID="CreateBtn" CssClass="btn btn-success" runat="server" Text="Create" OnClick="CreateBtn_Click" ValidationGroup="modelValidation"/>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="createAnotherBtn" runat="server" CssClass="btn btn-success" OnClick="createAnotherBtn_Click" Text="Create And Add Another" ValidationGroup="modelValidation" />
                                </td>
                            <td class="auto-style2">&nbsp;</td>
                        </tr>
                    </table>                   
                </div>
                <div class="modal-footer">
                                <asp:Label ID="errorLabel" runat="server" ForeColor="Red"></asp:Label>
                </div>
            </div>
        </div>
    </div>
    <div class="card card-body projectflow-card-shadow container">
        <div class="container">        
        <br>
        <div class="row">
            <div class="col-2">
                <asp:Button ID="newProjectBtn" CssClass="btn btn-primary" runat="server" OnClientClick="myfunction(); return false;" UseSubmitBehavior="false" data-toggle="modal" data-target="#CreateProject" Text="New Project" OnClick="newProjectBtn_Click" />
            </div>
            <div class="col-1">
            </div>
            <div class="col-3">
                <asp:DropDownList ID="PageSelectDP" CssClass="form-control border border-dark" runat="server" AutoPostBack="True" OnSelectedIndexChanged="PageSelectDP_SelectedIndexChanged">
                    <asp:ListItem Value="0">Avaliable</asp:ListItem>
                    <asp:ListItem Value="1">Deleted</asp:ListItem>
                </asp:DropDownList>
            </div>
             <div class="col-3">
                <asp:TextBox ID="SearchTB" CssClass="form-control" placeholder="Project Name" runat="server"></asp:TextBox>
            </div>
            <div class="col-1">
                <asp:Button ID="searchBtn" CssClass="btn btn-primary" runat="server" Text="Search" OnClick="searchBtn_Click"/>
            </div> 
            <div class="col-1">
                <asp:Button ID="showAllBtn" runat="server" CssClass="btn btn-primary" OnClick="showAllBtn_Click" Text="Show All" />
            </div>
        </div>
        <br>
        <div class="row">
            <div class="col">
                <div style="overflow-x: auto;">
                    <asp:GridView ID="projectGV" CssClass="table table-bordered projectflow-table table-striped" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="projectGV_SelectedIndexChanged" Width="1056px" OnRowCancelingEdit="projectGV_RowCancelingEdit" OnRowEditing="projectGV_RowEditing" OnRowUpdating="projectGV_RowUpdating" AllowPaging="True" PageSize="4" OnPageIndexChanging="projectGV_PageIndexChanging" OnRowDeleting="projectGV_RowDeleting">
                        <HeaderStyle CssClass="thead-light"/>
                        <Columns>
                            <asp:BoundField DataField="projectID" HeaderText="Module ID" ReadOnly="true"/>    
                            
                            <asp:TemplateField  HeaderText="Module Name">
                                <EditItemTemplate>
                                    <asp:TextBox ID="editNameTB" CssClass="form-control" runat="server" Text='<%# Bind("projectName") %>'></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="editNameRequiredValidator" runat="server" ControlToValidate="editNameTB" ErrorMessage="*" ForeColor="Red" Font-Size="Large" ValidationGroup="tableValidation"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="editNameRegexValidator" runat="server" ValidationGroup="tableValidation" validationexpression="^.{1,255}$" ControlToValidate="editNameTB" ErrorMessage="max 255 characters!" Font-Size="Small" ForeColor="Red"></asp:RegularExpressionValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="nameLabel" runat="server" Text='<%# Bind("projectName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField  HeaderText="Description">
                                <EditItemTemplate>
                                    <asp:TextBox ID="editDescTB" CssClass="form-control" runat="server" Text='<%# Bind("projectDescription") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="descLabel" runat="server" Text='<%# Bind("projectDescription") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                          
                            <asp:BoundField DataField="createDate" DataFormatString="{0:dd/MM/yyyy}" ReadOnly="true" HeaderText="Date Created" />
                          
                            <asp:CommandField SelectText="View Team" ShowSelectButton="True" ButtonType="Button">
                                <ControlStyle CssClass="btn btn-primary" />
                            </asp:CommandField>
                           
                            <asp:TemplateField>
                                <ItemTemplate>                     
                                   <asp:Button Text="Edit Module" CssClass="btn btn-primary" CommandName="Edit" runat="server" />
                                </ItemTemplate>
                                <edititemtemplate>
					                <asp:Button id="btnUpdate" CssClass="btn btn-sm btn-primary mb-2" runat="server" commandname="Update" text="Update" ValidationGroup="tableValidation"/>
                                    </br>
					                <asp:Button id="btnCancel" CssClass="btn btn-sm btn-outline-danger" runat="server" commandname="Cancel" text="Cancel" />
				               </edititemtemplate>             
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <ItemTemplate>                     
                                    <asp:Button ID="deleteBtn" CssClass="btn btn-danger" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete" OnClientClick="return confirm('Are you sure to delete project?');"></asp:Button>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                           <div class="jumbotron jumbotron-fluid">
                                <div class="container">
                                    <h1 class="display-4">Seem Empty, add some projects now!</h1>                                          
                                    <p>Projects not showing?    <asp:HyperLink ID="emailLink" Text="click here!" NavigateUrl="mailto:projectflow.nyp.eadp@gmail.com" runat="server"></asp:HyperLink></p>
                                </div>
                           </div>
                       </EmptyDataTemplate>
                    </asp:GridView>
                   </div>
                </div>
            </div>
             <br>
            <div class="row mb-3">
                <div class="col">
                    <asp:Button ID="refreshBtn" CssClass="btn btn-primary" runat="server" Text="Refresh" OnClick="refreshBtn_Click" />
                </div>
            </div>
        </div>
    </div> 
    <script>
        hideSidebar(); 
        $(document).ready(function () {
            $('#CreateProject').modal('hide');
        });      
    </script>
</asp:Content>
