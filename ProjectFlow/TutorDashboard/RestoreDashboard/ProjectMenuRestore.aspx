﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ServicesWithContentBase.Master" AutoEventWireup="true" CodeBehind="ProjectMenuRestore.aspx.cs" Inherits="ProjectFlow.TutorDashboard.RestoreDashboard.ProjectMenuRestore" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headBase" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBase" runat="server">
    <div class="card card-body projectflow-card-shadow container">       
            <div class="row mb-3 mt-3">
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
            <div class="row mb-3">
                <div class="col">
                    <div style="overflow-x: auto;">
                        <asp:GridView ID="projectRestoreGV" CssClass="table table-bordered projectflow-table table-striped" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="projectRestoreGV_SelectedIndexChanged" Width="1056px" AllowPaging="True" PageSize="4" OnPageIndexChanging="projectRestoreGV_PageIndexChanging">
                            <HeaderStyle CssClass="thead-light" />
                            <Columns>
                                <asp:CommandField ButtonType="Button" SelectText="Restore" ShowSelectButton="True">
                                    <ControlStyle CssClass="btn btn-success" />
                                </asp:CommandField>
                                <asp:BoundField DataField="projectID" HeaderText="Project ID" ReadOnly="true" />

                                <asp:TemplateField HeaderText="Project Name">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="editNameTB" CssClass="form-control" runat="server" Text='<%# Bind("projectName") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="editNameRequiredValidator" runat="server" ControlToValidate="editNameTB" ErrorMessage="*" ForeColor="Red" Font-Size="Large" ValidationGroup="tableValidation"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="editNameRegexValidator" runat="server" ValidationGroup="tableValidation" ValidationExpression="^.{1,255}$" ControlToValidate="editNameTB" ErrorMessage="max 255 characters!" Font-Size="Small" ForeColor="Red"></asp:RegularExpressionValidator>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="nameLabel" runat="server" Text='<%# Bind("projectName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Description">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="editDescTB" CssClass="form-control" runat="server" Text='<%# Bind("projectDescription") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="descLabel" runat="server" Text='<%# Bind("projectDescription") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="createDate" DataFormatString="{0:dd/MM/yyyy}" ReadOnly="true" HeaderText="Date Created" />

                            </Columns>
                            <EmptyDataTemplate>
                                <div class="jumbotron jumbotron-fluid">
                                    <div class="container">
                                        <h1 class="display-4">No Project to restore!</h1>
                                        <p>Deleted Projects not showing?   
                                            <asp:HyperLink ID="emailLink" Text="click here!" NavigateUrl="mailto:projectflow.nyp.eadp@gmail.com" runat="server"></asp:HyperLink></p>
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
</asp:Content>
