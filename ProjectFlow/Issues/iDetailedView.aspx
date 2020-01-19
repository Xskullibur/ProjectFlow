<%@ Page Title="" Language="C#" MasterPageFile="~/Issues/IssueNested.master" AutoEventWireup="true" CodeBehind="iDetailedView.aspx.cs" Inherits="ProjectFlow.Issues.iDetailedView" %>
<%@ MasterType VirtualPath="~/Issues/IssueNested.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container py-2 w-100 h-100">
        <div class="row pb-2">
            <div class="col">
                <div style="overflow-x: auto;">
                    <asp:GridView ID="IssueView" runat="server" CssClass="table table-hover table-bordered" AutoGenerateColumns="False" OnSelectedIndexChanged="IssueView_SelectedIndexChanged" OnRowDeleting="IssueView_RowDeleting">
                        <HeaderStyle CssClass="thead-light" />   
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="Issue Id" />
                            <asp:BoundField DataField="TaskID" HeaderText="Task Id" />
                            <asp:BoundField DataField="Task" HeaderText="Issue Name" />
                            <asp:BoundField DataField="Description" HeaderText="Description" />
                            <asp:BoundField DataField="CreatedBy" HeaderText="Created by" />
                            <asp:CommandField ShowSelectButton="True" ButtonType="Button">
                                <ControlStyle CssClass="btn btn-success mb-2" />
                            </asp:CommandField>
                            <asp:CommandField ShowDeleteButton="True" ButtonType="Button">
                                <ControlStyle CssClass="btn btn-danger" />
                            </asp:CommandField>
                        </Columns>
                        <EmptyDataTemplate>
                            <div class="jumbotron jumbotron-fluid">
                                <div class="container">
                                    <h1 class="display-4">Freedoommm!</h1>
                                    <p class="load">No Ongoing Issues Found!</p>
                                    <hr class="my-4" />
                                    <p>If ongoing Issues are expected but not shown please contact us <asp:HyperLink ID="emailLink" Text="here" NavigateUrl="mailto:projectflow.nyp.eadp@gmail.com" runat="server"></asp:HyperLink>.</p>
                                </div>
                            </div>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
