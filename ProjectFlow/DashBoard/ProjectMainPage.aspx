<%@ Page Title="" Language="C#" MasterPageFile="~/ServicesWithContent.Master" AutoEventWireup="true" CodeBehind="ProjectMainPage.aspx.cs" Inherits="ProjectFlow.ProjectMainPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style6 {
            height: 42px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="modal fade" id="CreateMember" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <asp:Label ID="Label4" runat="server" Text="Add Member"></asp:Label>
                </div>
                <div class="modal-body">                    
                    <table class="auto-style1">
                        <tr>
                            <td class="auto-style9">
                                <asp:Label ID="Label1" runat="server" Text="Student ID"></asp:Label>
                                &nbsp;<br />
                                <br />
                            </td>
                            <td class="auto-style13">
                                <asp:TextBox ID="studentIDTB" CssClass="form-control" runat="server" Width="223px"></asp:TextBox>
                                <br />
                            </td>
                            <td class="auto-style8">
                                &nbsp;
                                <asp:RequiredFieldValidator ID="studentRequiredValidator" runat="server" ValidationGroup="modelValidation" ControlToValidate="studentIDTB" ErrorMessage="*" ForeColor="Red" Font-Size="Large"></asp:RequiredFieldValidator>
                                &nbsp;<asp:RegularExpressionValidator ID="studentRegexValidator" runat="server" ValidationGroup="modelValidation" validationexpression="^[a-zA-Z0-9]{7}$" ControlToValidate="studentIDTB" ErrorMessage="7 characters!" Font-Size="Small" ForeColor="Red"></asp:RegularExpressionValidator>
                                <br />
                                <br />
                            </td>
                        </tr>                        
                        <tr>
                            <td class="auto-style10">
                                <asp:Label ID="Label2" runat="server" Text="Role ID"></asp:Label>
                                <br />
                                <br />
                            </td>
                            <td class="auto-style14">
                                <asp:TextBox ID="RoleIDTB" CssClass="form-control" runat="server" Width="222px"></asp:TextBox>
                                <br />
                            </td>
                            <td class="auto-style12">
                                &nbsp;

                                <asp:RequiredFieldValidator ID="roleRequiredValidator" runat="server" ValidationGroup="modelValidation" ControlToValidate="RoleIDTB" ErrorMessage="*" ForeColor="Red" Font-Size="Large"></asp:RequiredFieldValidator>
                                &nbsp;<asp:RegularExpressionValidator ID="roleRegexValidator" runat="server" ValidationGroup="modelValidation" validationexpression="^[0-9]{1,4}$" ControlToValidate="RoleIDTB" ErrorMessage="max 4 numbers!" Font-Size="Small" ForeColor="Red"></asp:RegularExpressionValidator>
                                <br />
                                <br />

                            </td>
                        </tr>

                        <tr>
                            <td class="auto-style6">
                                <br />
                                <br />
                            </td>
                            <td class="auto-style6">
                                <asp:Button ID="CreateBtn" ValidationGroup="modelValidation" CssClass="btn btn-success" runat="server" Text="Create" OnClick="CreateBtn_Click"  />
                                <br />
                            </td>
                            <td class="auto-style6">
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
                <asp:Label ID="InfoLabel" runat="server"></asp:Label>
                <br>
                <asp:Button ID="CreateMemberBtn" CssClass="btn btn-primary" runat="server" OnClientClick="myfunction(); return false;" UseSubmitBehavior="false" data-toggle="modal" data-target="#CreateMember" Text="Add Member" OnClick="CreateMemberBtn_Click" />
            </div>            
        </div>
        <br>
        <div class="row">
            <div class="col">
                <div style="overflow-x: auto;">
                    <asp:GridView ID="MemberGV" CssClass="table table-bordered" runat="server" AutoGenerateColumns="False" Width="1056px" OnRowCancelingEdit="MemberGV_RowCancelingEdit" OnRowEditing="MemberGV_RowEditing" OnRowUpdating="MemberGV_RowUpdating" OnPageIndexChanging="MemberGV_PageIndexChanging">
                        <HeaderStyle CssClass="thead-light" />
                        <Columns>
                            <asp:BoundField DataField="memberID" HeaderText="Member ID" ReadOnly="True" />
                            <asp:BoundField DataField="UserID" HeaderText="Student ID" ReadOnly="True" />
                            <asp:TemplateField HeaderText="Role ID">
                                <EditItemTemplate>
                                    <asp:TextBox ID="editRoleTB" CssClass="form-control" runat="server" Text='<%# Bind("roleID") %>'></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="editRoleRequiredValidator" runat="server" ValidationGroup="tableValidation" ControlToValidate="editRoleTB" ErrorMessage="*" ForeColor="Red" Font-Size="Large"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="editRoleRegexValidator" runat="server" ValidationGroup="tableValidation" validationexpression="^[0-9]{1,4}$" ControlToValidate="editRoleTB" ErrorMessage="max 4 numbers!" Font-Size="Small" ForeColor="Red"></asp:RegularExpressionValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="roleLabel" runat="server" Text='<%# Bind("roleID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ButtonType="Button" ShowEditButton="True" ValidationGroup="tableValidation"/>
                        </Columns>
                        <EmptyDataTemplate>
                           <div class="jumbotron jumbotron-fluid">
                                <div class="container">
                                    <h1 class="display-4">Seem Empty, add some members now!</h1>                                          
                                    <p>Members not showing?    <asp:HyperLink ID="emailLink" Text="click here!" NavigateUrl="mailto:projectflow.nyp.eadp@gmail.com" runat="server"></asp:HyperLink></p>
                                </div>
                           </div>
                       </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </div>
        </div>                                
    </div>
</asp:Content>
