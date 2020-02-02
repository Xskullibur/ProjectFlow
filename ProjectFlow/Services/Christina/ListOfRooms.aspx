<%@ Page Title="" Language="C#" MasterPageFile="~/ServicesWithContent.Master" AutoEventWireup="true" CodeBehind="ListOfRooms.aspx.cs" Inherits="ProjectFlow.Services.Christina.ListOfRooms" EnableEventValidation="false"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="card card-body projectflow-card-shadow pt-3 container">
        <%-- Controls --%>
        <div class="row py-3">
            <div class="col-12 py-2">
                <asp:LinkButton CssClass="py-1 ml-1" ID="LinkButton1" runat="server" OnClick="SearchSelfEvent">Created By Me</asp:LinkButton>
                <asp:ListBox ID="searchList" CssClass="selectpicker form-control mt-2" data-live-search="true" data-actions-box="true" runat="server" SelectionMode="Multiple"></asp:ListBox>
            </div>
            <div class="col-12 py-2">
                <asp:Button ID="SearchBtn" CssClass="btn btn-primary" runat="server" Text="Search" OnClick="SearchEvent" />
                <asp:Button ID="ClearBtn" CssClass="btn btn-primary" runat="server" Text="Clear" OnClick="ClearEvent" />
            </div>
        </div>
        <div class="row">
            <div class="col-12">
                <asp:UpdatePanel ID="TaskGridUpdatePanel" runat="server">
                        <ContentTemplate>

                            <asp:GridView ID="roomsGridView" runat="server" CssClass="table table-bordered table-hover projectflow-table" 
                                AllowPaging="True" PageSize="10" OnPageIndexChanging="roomsGridView_PageIndexChanging" 
                                AutoGenerateColumns="False" OnSelectedIndexChanged="OnSelectedIndexChanged" OnRowDataBound="OnRowDataBound"> 
                                <HeaderStyle CssClass="thead-light" />
                                <Columns>

                                    
                                    <%--Room name--%>
                                    <asp:TemplateField HeaderText="Room Name">
                                        <ItemTemplate>
                                            <asp:Label ID="roomNameLbl" runat="server" Text='<%# Bind("roomName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
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
                                    <div class="container text-center">
                                      <h2>No Tasks Found.</h2><br>
                                      <i class="fas fa-tasks fa-10x"></i>
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
