<%@ Page Title="" Language="C#" MasterPageFile="~/Issues/IssueNested.master" AutoEventWireup="true" CodeBehind="iAllView.aspx.cs" Inherits="ProjectFlow.Issues.iAllView" %>
<%@ MasterType VirtualPath="~/Issues/IssueNested.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container py-2 w-100 h-100">
        <div class="row pb-2">
            <div class="col">
                <div style="overflow-x: auto;">
                    <asp:GridView ID="IssueView" runat="server" CssClass="table table-bordered table-hover table-striped projectflow-table" AutoGenerateColumns="False" OnRowDataBound="IssueView_RowDataBound"  OnSelectedIndexChanged="IssueView_SelectedIndexChanged" OnRowDeleting="IssueView_RowDeleting" AllowPaging="True" OnPageIndexChanging="IssueView_PageIndexChanging" PageSize="4">
                        <HeaderStyle CssClass="thead-light" />   
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="Issue Id" />
                            <asp:BoundField DataField="TaskID" HeaderText="Task Id" />
                            <asp:BoundField DataField="Task" HeaderText="Issue Name" />
                            <asp:BoundField DataField="Description" HeaderText="Description" />
                            <asp:BoundField DataField="CreatedBy" HeaderText="Created by" />
                            <asp:BoundField DataField="Active" HeaderText="Active" />
                            <asp:BoundField DataField="Status" HeaderText="Status" />
                            <asp:BoundField DataField="IsPublic" HeaderText="Public vote" />
                            <asp:CommandField ShowSelectButton="True" ButtonType="Button">
                                <ControlStyle CssClass="btn btn-success mb-2" />
                            </asp:CommandField>
                        </Columns>
                        <EmptyDataTemplate>
                            <div class="jumbotron jumbotron-fluid">
                                <div class="container">
                                    <h1 class="display-4">Freedoommm!</h1>
                                    <p class="load">No Issues Found!</p>
                                    <hr class="my-4" />
                                    <p>If Issues are expected but not shown please contact us <asp:HyperLink ID="emailLink" Text="here" NavigateUrl="mailto:projectflow.nyp.eadp@gmail.com" runat="server"></asp:HyperLink>.</p>
                                </div>
                            </div>
                        </EmptyDataTemplate>
                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="3" />
                        <PagerStyle CssClass="pagination-ys table-borderless" />
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
