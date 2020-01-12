﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ServicesWithContent.Master" AutoEventWireup="true" CodeBehind="StudentProject.aspx.cs" Inherits="ProjectFlow.DashBoard.studentProject" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="container">
        <div class="row">
            <div class="col">
                <div style="overflow-x: auto;">
                    <asp:GridView ID="ProjectGV" CssClass="table table-bordered" runat="server" AutoGenerateColumns="False" Width="1056px" AllowPaging="True" PageSize="4" OnSelectedIndexChanged="ProjectGV_SelectedIndexChanged">
                        <HeaderStyle CssClass="thead-light"/>
                        <Columns>
                            <asp:BoundField DataField="teamID" HeaderText="ID" />
                            <asp:BoundField DataField="teamName" HeaderText="Name" />
                            <asp:BoundField DataField="teamDescription" HeaderText="Description" />
                            <asp:CommandField ButtonType="Button" SelectText="Open" ShowSelectButton="True">
                                <ControlStyle CssClass="btn btn-success" />
                            </asp:CommandField>
                        </Columns>
                        <EmptyDataTemplate>
                           <div class="jumbotron jumbotron-fluid">
                                <div class="container">
                                    <h1 class="display-4">No Projects Assigned</h1>                                          
                                    <p>Projects not showing?    <asp:HyperLink ID="emailLink" Text="click here!" NavigateUrl="mailto:projectflow.nyp.eadp@gmail.com" runat="server"></asp:HyperLink></p>
                                </div>
                           </div>
                       </EmptyDataTemplate>
                    </asp:GridView>

                </div>
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
