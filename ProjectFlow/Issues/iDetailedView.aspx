<%@ Page Title="" Language="C#" MasterPageFile="~/Issues/IssueNested.master" AutoEventWireup="true" CodeBehind="iDetailedView.aspx.cs" Inherits="ProjectFlow.Issues.iDetailedView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView ID="IssueView" runat="server" BorderStyle="None" BorderWidth="0px" GridLines="None" AutoGenerateColumns="False" CellPadding="5" OnSelectedIndexChanged="IssueView_SelectedIndexChanged" >
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="Task Id" />
                <asp:BoundField DataField="Task" HeaderText="Issue Name" />
                <asp:BoundField DataField="Description" HeaderText="Description" />
                <asp:BoundField DataField="CreatedBy" HeaderText="Created by" />
                <asp:CommandField ShowSelectButton="True" ButtonType="Button">
                    <ControlStyle CssClass="btn btn-success mb-2" />
                </asp:CommandField>
            </Columns>
        </asp:GridView>
</asp:Content>
