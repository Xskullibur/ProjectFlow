<%@ Page Title="" Language="C#" MasterPageFile="~/Tasks/TaskNested.master" AutoEventWireup="true" CodeBehind="CalendarView.aspx.cs" Inherits="ProjectFlow.Tasks.CalendarView" %>
<%@ MasterType VirtualPath="~/Tasks/TaskNested.master" %> 

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container mt-4" style="overflow-y: hidden;">

        <div id="calendar" class="m-3"></div>

    </div>

</asp:Content>
