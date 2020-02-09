<%@ Page Title="" Language="C#" MasterPageFile="~/ServicesWithContent.Master" AutoEventWireup="true" CodeBehind="StudentProject.aspx.cs" Inherits="ProjectFlow.DashBoard.studentProject" EnableEventValidation="false"%>
<%@ MasterType VirtualPath="/ServicesWithContent.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">   
    <div class="card card-body projectflow-card-shadow container">
        <div class="container">
            <div class="row p-3 mx-auto">
                <div class="row my-3">
                    <div class="d-flex align-content-end flex-wrap col-6">
                        <asp:TextBox ID="SearchTB" CssClass="form-control mr-3" placeholder="Project Name" runat="server"></asp:TextBox>
                    </div>
                    <div class="d-flex align-content-end flex-wrap col-6">
                        <asp:Button ID="searchBtn" CssClass="btn btn-primary mr-3" runat="server" Text="Search" OnClick="searchBtn_Click" />
                        <asp:Button ID="showAllBtn" runat="server" CssClass="btn btn-primary" OnClick="showAllBtn_Click" Text="Show All" />
                    </div>
                </div>
                <div class="row mb-3">
                    <div class="col">
                        <div style="overflow-x: auto;">
                            <asp:GridView ID="ProjectGV" CssClass="table table-bordered table-hover pointer projectflow-table" runat="server" OnRowDataBound="OnRowDataBound" AutoGenerateColumns="False" Width="1056px" AllowPaging="True" PageSize="4" OnSelectedIndexChanged="ProjectGV_SelectedIndexChanged" OnPageIndexChanging="ProjectGV_PageIndexChanging">
                                <HeaderStyle CssClass="thead-light" />
                                <Columns>
                                    <asp:BoundField DataField="projectID" HeaderText="Module ID" />
                                    <asp:TemplateField HeaderText="Module Name">
                                        <ItemTemplate>
                                            <asp:Label ID="nameLabel" runat="server" Text='<%# GetProjectName(Eval("projectID").ToString()) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="teamID" HeaderText="Team ID" />
                                    <asp:BoundField DataField="teamName" HeaderText="Team Name" />
                                    <asp:BoundField DataField="teamDescription" HeaderText="Description" />
                                </Columns>
                                <EmptyDataTemplate>
                                    <div class="jumbotron jumbotron-fluid">
                                        <div class="container">
                                            <h1 class="display-4">No Projects Assigned</h1>
                                            <p>Projects not showing?   
                                                <asp:HyperLink ID="emailLink" Text="click here!" NavigateUrl="mailto:projectflow.nyp.eadp@gmail.com" runat="server"></asp:HyperLink></p>
                                        </div>
                                    </div>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div class="row mb-3">
                    <div class="col">
                        <asp:Button ID="refreshBtn" CssClass="btn btn-primary" runat="server" Text="Refresh" OnClick="refreshBtn_Click" />
                    </div>
                </div>
                <br>                
                
            </div>
        </div>
    </div>
    <div class="card card-body projectflow-card-shadow container mt-3 mb-3">
        <div class="container">
            <div class="row mb-3 mt-3">
                <asp:Label ID="Label13" runat="server" text="Available Team to Join"></asp:Label>
            </div>
            <div class="row mb-3">
                <div class="row mb-3">
                    <div class="col">
                        <div style="overflow-x: auto;">
                            <asp:GridView ID="availableGV" CssClass="table table-bordered table-hover pointer projectflow-table" runat="server" AutoGenerateColumns="False" Width="1056px" AllowPaging="True" PageSize="4" OnSelectedIndexChanged="availableGV_SelectedIndexChanged" OnPageIndexChanging="availableGV_PageIndexChanging">
                                <HeaderStyle CssClass="thead-light" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Module ID">
                                        <ItemTemplate>
                                            <asp:Label ID="midLabel" runat="server" Text='<%# Bind("projectID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Module Name">
                                        <ItemTemplate>
                                            <asp:Label ID="nameLabel" runat="server" Text='<%# GetProjectName(Eval("projectID").ToString()) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="teamID" HeaderText="ID" />
                                    <asp:BoundField DataField="teamName" HeaderText="Name" />
                                    <asp:BoundField DataField="teamDescription" HeaderText="Description" />
                                    <asp:CommandField ButtonType="Button" SelectText="Join" ShowSelectButton="True">
                                        <ControlStyle CssClass="btn btn-success" />
                                    </asp:CommandField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <div class="jumbotron jumbotron-fluid">
                                        <div class="container">
                                            <h1 class="display-4">No Available team to join at this time</h1>
                                            <p>Projects not showing?   
                                                <asp:HyperLink ID="emailLink" Text="click here!" NavigateUrl="mailto:projectflow.nyp.eadp@gmail.com" runat="server"></asp:HyperLink></p>
                                        </div>
                                    </div>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </div>
                    </div>
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
