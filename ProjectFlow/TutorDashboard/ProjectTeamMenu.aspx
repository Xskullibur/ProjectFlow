﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ServicesWithContent.Master" AutoEventWireup="true" CodeBehind="ProjectTeamMenu.aspx.cs" Inherits="ProjectFlow.DashBoard.ProjectTeamMenu" %>
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
        .auto-style15 {
            width: 150px;
            height: 42px;
        }
        .auto-style16 {
            width: 150px;
        }
        .auto-style17 {
            width: 53px;
            height: 42px;
        }
        .auto-style18 {
            width: 53px;
        }
        .auto-style19 {
            width: 153px;
        }
        .auto-style20 {
            width: 153px;
            height: 42px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="modal fade" id="CreateTeam" role="dialog">
        <div class="modal-dialog">
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
                                <br />
                                <asp:Label ID="Label1" runat="server" Text="Team Name"></asp:Label>
                                &nbsp;<br />
                                <br />
                            </td>
                            <td class="auto-style13">
                                <asp:TextBox ID="NameTB" CssClass="form-control" placeholder="Required, max 255" runat="server" Width="223px"></asp:TextBox>
                                <br />
                            </td>
                            <td class="auto-style19">
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
                            <td class="auto-style20">
                                &nbsp;

                                &nbsp;<asp:RegularExpressionValidator ID="descRegexValidator" runat="server" ValidationGroup="modelValidation" validationexpression="^.{1,255}$" ControlToValidate="DescTB" ErrorMessage="max 255 characters!" Font-Size="Small" ForeColor="Red"></asp:RegularExpressionValidator>
                                <br />
                                <br />

                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style10">
                                <asp:Label ID="Label5" runat="server" Text="Group"></asp:Label>
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
                            <td class="auto-style20">
                              
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
                            <td class="auto-style19">
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
    <div class="modal fade" id="BulkCreateTeam" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <asp:Label ID="Label6" runat="server" Text="Bulk Create"></asp:Label>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                      <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">                    
                    <table class="auto-style1">
                             
                        <tr>
                            <td class="auto-style10">
                                <asp:Label ID="Label8" runat="server" Text="Number of teams"></asp:Label>
                                <br />
                                <br />
                            </td>
                            <td class="auto-style14">
                                <asp:DropDownList ID="amountDP" CssClass="form-control border border-dark" runat="server" AutoPostBack="False">
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
                                &nbsp;

                                &nbsp;<br />
                                <br />

                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style10">
                                <asp:Label ID="Label9" runat="server" Text="Group"></asp:Label>
                                <br />
                                <br />
                            </td>
                            <td class="auto-style14">
                                <asp:DropDownList ID="bulkGroupDP" CssClass="form-control border border-dark" runat="server" AutoPostBack="False">
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
                                <asp:Button ID="bulkAddBtn" CssClass="btn btn-success" runat="server" Text="Create" OnClick="bulkAddBtn_Click" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
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
                                <asp:Label ID="Label10" runat="server" ForeColor="Red"></asp:Label>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="controlModel" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <asp:Label ID="Label7" runat="server" Text="Access Control"></asp:Label>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                      <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">                    
                    <table class="auto-style1">
                             
                        <tr>
                            <td class="auto-style15">
                                Group<br />
                                <br />
                            </td>
                            <td class="auto-style14">
                                <asp:DropDownList ID="groupAccessDP" CssClass="form-control border border-dark" runat="server" AutoPostBack="False">
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
                            <td class="auto-style17">
                                &nbsp;

                                &nbsp;<br />
                                <br />

                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style15">
                                <br />
                                <br />
                            </td>
                            <td class="auto-style14">
                                <br />
                                <asp:LinkButton ID="lockBtn" runat="server" CssClass="btn btn-primary" OnClick="lockBtn_Click">
                                    <i style="color: white;" class="fas fa-lg fa-lock"></i>
                                </asp:LinkButton>
&nbsp;&nbsp;
                                <asp:LinkButton ID="unlockDP" runat="server" CssClass="btn btn-primary" OnClick="unlockDP_Click">
                                    <i style="color: white;" class="fas fa-lg fa-unlock"></i>
                                </asp:LinkButton>
                            </td>
                            <td class="auto-style17">
                              
                            </td>
                        </tr>

                        <tr>
                            <td class="auto-style16">
                                <br />
                                <br />
                            </td>
                            <td class="auto-style13">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <br />
                            </td>
                            <td class="auto-style18">
                                &nbsp;

                                &nbsp;&nbsp;<br />
                                <br />

                            </td>
                        </tr>                       
                    </table>                   
                </div>
                <div class="modal-footer">
                                <asp:Label ID="Label13" runat="server" ForeColor="Red"></asp:Label>
                </div>
            </div>
        </div>
    </div>
    <div class="card card-body projectflow-card-shadow container">
        <div class="container"> 
            <div class="row mb-3">
                <div class="col">
                    <h3>
                        <asp:Label ID="InfoLabel" runat="server" Font-Size="Medium"></asp:Label>
                    </h3>
                </div>            
            </div>
            <br>
            <div class="row mb-3">
                <div class="col-2">
                    <asp:Button ID="bulkCreateBtn" CssClass="btn btn-primary mr-3" runat="server" OnClick="bulkCreateBtn_Click" Text="Bulk Create" OnClientClick="myfunction(); return false;" UseSubmitBehavior="false" data-toggle="modal" data-target="#BulkCreateTeam"/>
                </div> 
                <div class="col-2">
                    <asp:Button ID="CreateTeamBtn" CssClass="btn btn-primary mr-3" runat="server" OnClientClick="myfunction(); return false;" UseSubmitBehavior="false" data-toggle="modal" data-target="#CreateTeam" Text="Create Team" />
                </div>
                <div class="col-2">
                    <asp:Button ID="control" runat="server" Text="Release Team" CssClass="btn btn-primary mr-3" OnClientClick="myfunction(); return false;" UseSubmitBehavior="false" data-toggle="modal" data-target="#controlModel"/>
                </div> 
                <div class="col-3">
                     <asp:DropDownList ID="PageSelectDP" CssClass="form-control border border-dark mr-3" runat="server" AutoPostBack="True" OnSelectedIndexChanged="PageSelectDP_SelectedIndexChanged">
                        <asp:ListItem Value="0">Avaliable</asp:ListItem>
                        <asp:ListItem Value="1">Deleted</asp:ListItem>
                    </asp:DropDownList>
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
            <div class="row mb-3">
                <div class="col">
                    <div style="overflow-x: auto;">
                        <asp:GridView ID="TeamGV" CssClass="table table-bordered projectflow-table table-striped" runat="server" AutoGenerateColumns="False" Width="1056px" OnRowCancelingEdit="TeamGV_RowCancelingEdit" OnRowEditing="TeamGV_RowEditing" OnRowUpdating="TeamGV_RowUpdating" OnSelectedIndexChanged="TeamGV_SelectedIndexChanged" OnPageIndexChanging="TeamGV_PageIndexChanging" AllowPaging="True" PageSize="4" OnRowDeleting="TeamGV_RowDeleting">
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
                                        <asp:Label ID="groupLabel" runat="server" Text='<%# Bind("group") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                                          
                                 <asp:TemplateField HeaderText="Joinable">                                   
                                    <ItemTemplate>
                                        <%# (bool)Eval("open")? "<i style=\"color: green;\" class=\"fas fa-lg fa-lock-open\"></i>" : "<i style=\"color: red;\" class=\"fas fa-lg fa-lock\"></i>"%>                                        
                                    </ItemTemplate>
                                </asp:TemplateField>
                            
                                <asp:CommandField ButtonType="Button" SelectText="Dashboard" ShowSelectButton="True">
                                    <ControlStyle CssClass="btn btn-primary" />
                                </asp:CommandField>
                                
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button Text="Edit Team" CssClass="btn btn-primary" CommandName="Edit" runat="server" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Button ID="btnUpdate" CssClass="btn btn-sm btn-primary mb-2" runat="server" CommandName="Update" Text="Update" ValidationGroup="tableValidation" />
                                        <br>
                                        </br>
                                        <asp:Button ID="btnCancel" CssClass="btn btn-sm btn-outline-danger" runat="server" CommandName="Cancel" Text="Cancel" />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                            
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="DeleteButton" Text="Delete" runat="server"
                                            CssClass="btn btn-danger"
                                            data-toggle="confirmation"
                                            data-btn-ok-icon-class="fa fa-check"
                                            data-btn-cancel-icon-class="fa fa-close"
                                            data-popout="true"
                                            CommandName="Delete" />
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
