﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ServicesWithContent.Master" AutoEventWireup="true" CodeBehind="ProjectMainPage.aspx.cs" Inherits="ProjectFlow.ProjectMainPage" %>
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
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                      <span aria-hidden="true">&times;</span>
                    </button>
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
                                <asp:DropDownList ID="RoleDP" CssClass="form-control border border-dark" runat="server">
                                    <asp:ListItem>Member</asp:ListItem>
                                    <asp:ListItem>Leader</asp:ListItem>
                                </asp:DropDownList>
                                <br />
                            </td>
                            <td class="auto-style12">
                                &nbsp;

                                &nbsp;<br />
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
                <h1>
                    <asp:Label ID="Label3" class="label label-primary" runat="server" Text="Team Members"></asp:Label>
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
            <div class="col">                
                <asp:DropDownList ID="PageSelectDP" CssClass="form-control border border-dark" runat="server" AutoPostBack="True" OnSelectedIndexChanged="PageSelectDP_SelectedIndexChanged">
                    <asp:ListItem Value="0">Add Members</asp:ListItem>
                    <asp:ListItem Value="1">Add MileStone</asp:ListItem>
                </asp:DropDownList>                
            </div>
            <div class="col">                             
                <asp:Button ID="CreateMemberBtn" CssClass="btn btn-primary" runat="server" OnClientClick="myfunction(); return false;" UseSubmitBehavior="false" data-toggle="modal" data-target="#CreateMember" Text="Add Member" OnClick="CreateMemberBtn_Click" AllowPaging="True" PageSize="4"/>
            </div>                        
        </div>
        <br>
        <div class="row">
            <div class="col">
                <div style="overflow-x: auto;">
                    <asp:GridView ID="MemberGV" CssClass="table table-bordered" runat="server" AutoGenerateColumns="False" Width="1056px" OnRowCancelingEdit="MemberGV_RowCancelingEdit" OnRowEditing="MemberGV_RowEditing" OnRowUpdating="MemberGV_RowUpdating" OnPageIndexChanging="MemberGV_PageIndexChanging" AllowPaging="True" PageSize="4" OnRowDeleting="MemberGV_RowDeleting">
                        <HeaderStyle CssClass="thead-light" />
                        <Columns>
                            <asp:BoundField DataField="memberID" HeaderText="Member ID" ReadOnly="True" />
                            <asp:TemplateField HeaderText="Student ID">
                                <ItemTemplate>
                                    <asp:Label ID="idLabel" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Student.studentID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name">
                                <ItemTemplate>
                                    <asp:Label ID="nameLabel" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Student.firstName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="Role ID">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="editRoleDP" CssClass="form-control border border-dark" Text='<%# DataBinder.Eval(Container.DataItem,"Role.role1") %>' runat="server">
                                        <asp:ListItem>Member</asp:ListItem>
                                        <asp:ListItem>Leader</asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="roleLabel" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Role.role1") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ButtonType="Button" ShowEditButton="True" ValidationGroup="tableValidation">
                                <ControlStyle CssClass="btn btn-warning" />
                            </asp:CommandField>
                            <asp:TemplateField>
                                <ItemTemplate>                     
                                    <asp:Button ID="deleteBtn" CssClass="btn btn-danger" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete" OnClientClick="return confirm('Are you sure to delete member?');"></asp:Button>
                                </ItemTemplate>
                            </asp:TemplateField>
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
