﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ServicesWithContent.Master" AutoEventWireup="true" CodeBehind="ProjectMenu.aspx.cs" Inherits="ProjectFlow.DashBoard.ProjectMenu" %>
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
                                <asp:TextBox ID="NameTB" CssClass="form-control" runat="server"></asp:TextBox>
                                <br />
                            </td>
                            <td class="auto-style10">
                                &nbsp;
                                <asp:RequiredFieldValidator ID="nameRequiredValidator" runat="server" ControlToValidate="NameTB" ErrorMessage="Required!" ForeColor="Red" Font-Size="Small"></asp:RequiredFieldValidator>
                                &nbsp;<asp:RegularExpressionValidator ID="nameRegexValidator" runat="server" validationexpression="^.{1,255}$" ControlToValidate="NameTB" ErrorMessage="max 255 characters!" Font-Size="Small" ForeColor="Red"></asp:RegularExpressionValidator>
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
                                <asp:TextBox ID="DescTB" CssClass="form-control" runat="server"></asp:TextBox>
                                <br />
                            </td>
                            <td class="auto-style10">
                                &nbsp;

                                <asp:RequiredFieldValidator ID="descRequiredValidator" runat="server" ControlToValidate="DescTB" ErrorMessage="Required!" ForeColor="Red" Font-Size="Small"></asp:RequiredFieldValidator>
                                &nbsp;<asp:RegularExpressionValidator ID="descRegexValidator" runat="server" validationexpression="^.{1,255}$" ErrorMessage="max 255 characters!" Font-Size="Small" ForeColor="Red" ControlToValidate="DescTB"></asp:RegularExpressionValidator>
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
                                <asp:TextBox ID="ProjectIdTB" CssClass="form-control" runat="server"></asp:TextBox>
                                <br />
                            </td>
                            <td class="auto-style10">
                                &nbsp;

                                &nbsp;<asp:RequiredFieldValidator ID="IdRequirdValidator" runat="server" ControlToValidate="ProjectIdTB" ErrorMessage="Required!" ForeColor="Red" Font-Size="Small"></asp:RequiredFieldValidator>
                                &nbsp;<asp:RegularExpressionValidator ID="projectIdRegexValidator" runat="server" validationexpression="^[a-zA-Z0-9]{6}$" ControlToValidate="ProjectIdTB" ErrorMessage="6 characters!" Font-Size="Small" ForeColor="Red"></asp:RegularExpressionValidator>

                                <br />
                                <br />

                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style2">&nbsp;</td>
                            <td class="auto-style8">
                                <asp:Button ID="CreateBtn" CssClass="btn btn-success" runat="server" Text="Create" OnClick="CreateBtn_Click" />
                            &nbsp;&nbsp;&nbsp;
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
            <asp:Button ID="newProjectBtn" CssClass="btn btn-primary" runat="server" OnClientClick="myfunction(); return false;" UseSubmitBehavior="false" data-toggle="modal" data-target="#CreateProject" Text="New Project" OnClick="newProjectBtn_Click" />
            <asp:Label ID="Label6" runat="server"></asp:Label>
        </div>
        <br>
        <div>
            <asp:GridView ID="projectGV" CssClass="table table-dark table-hover table-bordered" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="projectGV_SelectedIndexChanged" Width="1325px" OnRowCancelingEdit="projectGV_RowCancelingEdit" OnRowEditing="projectGV_RowEditing" OnRowUpdating="projectGV_RowUpdating">
                <Columns>
                    <asp:BoundField DataField="projectID" HeaderText="Project ID" />
                    <asp:BoundField DataField="projectName" HeaderText="Name" />
                    <asp:BoundField DataField="projectDescription" HeaderText="Description" />
                    <asp:CommandField SelectText="Open" ShowSelectButton="True" ButtonType="Button">
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
