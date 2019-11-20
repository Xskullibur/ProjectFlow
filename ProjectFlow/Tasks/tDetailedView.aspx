<%@ Page Title="" Language="C#" MasterPageFile="~/Tasks/TaskNested.master" AutoEventWireup="true" CodeBehind="tDetailedView.aspx.cs" Inherits="ProjectFlow.Tasks.tDetailedView" %>

<%-- Register Material Grid View --%>
<%@ Register Src="~/MaterialGridView.ascx" TagName="MaterialGridView" TagPrefix="ProjectFlow" %>

<%--TaskNested Master--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<%--Content--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">

        <div class="row">
            <div class="col">
                Detailed View

                <ProjectFlow:MaterialGridView runat="server">

                </ProjectFlow:MaterialGridView>

                <div class="mdc-data-table">  
                    <asp:GridView ID="taskView" runat="server" >
                    </asp:GridView>
                </div>
            </div>
        </div>

    </div>

</asp:Content>
