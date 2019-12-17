<%@ Page Title="" Language="C#" MasterPageFile="~/Issues/IssueNested.master" AutoEventWireup="true" CodeBehind="tConflictRes1.aspx.cs" Inherits="ProjectFlow.Issues.tConflictRes1" %>

<%--TaskNested Master--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<%--Content--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Button ID="btnYes" CssClass ="btn btn-success" runat="server" Text="Yes" />
    <asp:Button ID="btnNo" CssClass ="btn btn-danger" runat="server" Text="No" />
    
</asp:Content>