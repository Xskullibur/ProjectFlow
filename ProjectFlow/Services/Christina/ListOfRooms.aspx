<%@ Page Title="" Language="C#" MasterPageFile="~/ServicesWithContent.Master" AutoEventWireup="true" CodeBehind="ListOfRooms.aspx.cs" Inherits="ProjectFlow.Services.Christina.ListOfRooms" EnableEventValidation="false"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-4 mx-auto text-center">
                <h1>
                    <asp:Label ID="Label" runat="server" Font-Size="XX-Large" Text="Past created rooms"></asp:Label>
                </h1>
            </div>                   
        </div>
        <%-- Controls --%>
        <div class="row">
            <div class="col-12">

            </div>
        </div>
        <div class="row">
            <div class="col-12">
                <asp:UpdatePanel ID="TaskGridUpdatePanel" runat="server">
                        <ContentTemplate>

                            <asp:GridView ID="roomsGridView" runat="server" CssClass="table table-bordered table-hover" 
                                AllowPaging="True" PageSize="4" OnPageIndexChanging="roomsGridView_PageIndexChanging" 
                                AutoGenerateColumns="False" OnSelectedIndexChanged="OnSelectedIndexChanged" OnRowDataBound="OnRowDataBound"> 
                                <HeaderStyle CssClass="thead-light" />
                                <Columns>

                                    <%--Creator--%>
                                    <asp:TemplateField HeaderText="Creator">
                                        <ItemTemplate>
                                            <asp:Label ID="userNameLbl" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Student.aspnet_Users.UserName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <%--Creation Date--%>
                                    <asp:TemplateField HeaderText="Creation Date">
                                        <ItemTemplate>
                                            <asp:Label ID="creationDateLbl" runat="server" Text='<%# Bind("creationDate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>

                                <EmptyDataTemplate>
                                    <div class="container">
                                        <h2>No Tasks Found.</h2><br>
                                        <i class="fas fa-tasks"></i>
                                    </div>
                                </EmptyDataTemplate>
                                <PagerSettings Mode="NumericFirstLast" PageButtonCount="3" />
                                <PagerStyle CssClass="pagination-ys table-borderless" />
                            </asp:GridView>
                            <asp:LinkButton ID="lnkDummy" runat="server"></asp:LinkButton>
                        </ContentTemplate>
                    </asp:UpdatePanel>
            </div>
        </div>
    </div>
                        
</asp:Content>
