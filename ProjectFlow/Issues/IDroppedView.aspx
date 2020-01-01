<%@ Page Title="" Language="C#" MasterPageFile="~/Issues/IssueNested.master" AutoEventWireup="true" CodeBehind="iDroppedView.aspx.cs" Inherits="ProjectFlow.Issues.IDroppedView" %>
<%@ MasterType VirtualPath="~/Issues/IssueNested.master" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container py-2 w-100 h-100">
        <div class="row pb-2">
            <div class="col">
                <div style="overflow-x: auto;">
                    <asp:GridView ID="IssueView" runat="server" CssClass="table table-hover table-bordered" AutoGenerateColumns="False">
                        <HeaderStyle CssClass="thead-light" />   
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="Issue Id" />
                            <asp:BoundField DataField="TaskID" HeaderText="Task Id" />
                            <asp:BoundField DataField="Task" HeaderText="Issue Name" />
                            <asp:BoundField DataField="Description" HeaderText="Description" />
                            <asp:BoundField DataField="CreatedBy" HeaderText="Created by" />
                        </Columns>
                        <EmptyDataTemplate>
                            <div class="jumbotron jumbotron-fluid">
                                <div class="container">
                                    <h1 class="display-4">Freedoommm!</h1>
                                    <p class="load">No inactive issues Found!</p>
                                    <hr class="my-4" />
                                    <p>If inactive Issues are expected but not shown please contact us <asp:HyperLink ID="emailLink" Text="here" NavigateUrl="mailto:projectflow.nyp.eadp@gmail.com" runat="server"></asp:HyperLink>.</p>
                                </div>
                            </div>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
