<%@ Page Title="" Language="C#" MasterPageFile="~/ServicesWithContent.Master" AutoEventWireup="true" CodeBehind="IssueView.aspx.cs" Inherits="ProjectFlow.Issues.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mdc-data-table">  
        <asp:GridView ID="IssueView" runat="server" >
        </asp:GridView>
    </div>
</asp:Content>
