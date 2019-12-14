<%@ Page Title="" Language="C#" MasterPageFile="~/Tasks/TaskNested.master" AutoEventWireup="true" CodeBehind="tDetailedView.aspx.cs" Inherits="ProjectFlow.Tasks.tDetailedView" %>

<%--TaskNested Master--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<%--Content--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">

        <div class="row">
            <div class="col">
                Detailed View

                <div class="mdc-data-table">  
                    <asp:GridView ID="taskGrid" runat="server" >
                    </asp:GridView>
                </div>
            </div>
        </div>

    </div>

</asp:Content>
