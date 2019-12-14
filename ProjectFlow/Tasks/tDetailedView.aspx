<%@ Page Title="" Language="C#" MasterPageFile="~/Tasks/TaskNested.master" AutoEventWireup="true" CodeBehind="tDetailedView.aspx.cs" Inherits="ProjectFlow.Tasks.tDetailedView" %>

<%--TaskNested Master--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<%--Content--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container py-2">

        <div class="row">
            <div class="col">

                <div>
                    <asp:GridView ID="taskGrid" runat="server" CssClass="table table-hover table-striped table-bordered" > 
                        <HeaderStyle CssClass="thead-dark" />
                    </asp:GridView>
                </div>

            </div>
        </div>

    </div>

</asp:Content>
