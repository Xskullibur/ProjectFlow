<%@ Page Title="" Language="C#" MasterPageFile="~/Tasks/TaskNested.master" AutoEventWireup="true" CodeBehind="tDetailedView.aspx.cs" Inherits="ProjectFlow.Tasks.tDetailedView" %>

<%--TaskNested Master--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<%--Content--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container py-2">

        <div class="row pb-2">
            <div class="col">

                <div>
                    <asp:GridView ID="taskGrid" runat="server" CssClass="table table-hover table-bordered" OnRowEditing="taskGrid_RowEditing" AutoGenerateColumns="False" OnRowDataBound="taskGrid_RowDataBound" > 
                        <Columns>
                            <asp:CommandField ShowEditButton="True" />

                            <%--ID--%>
                            <asp:BoundField DataField="ID" HeaderText="ID" />

                            <%--Task--%>
                            <asp:BoundField DataField="Task" HeaderText="Task" />

                            <%--Description--%>
                            <asp:BoundField DataField="Description" HeaderText="Description" />

                            <%--Milestone--%>
                            <asp:TemplateField HeaderText="Milestone">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="editMilestoneDDL" CssClass="form-control" runat="server"></asp:DropDownList>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("MileStone") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%--Start Date--%>
                            <asp:BoundField DataField="Start" HeaderText="Start Date" />

                            <%--End Date--%>
                            <asp:BoundField DataField="End" HeaderText="End Date" />

                            <%--Allocation--%>
                            <asp:BoundField DataField="Allocation" HeaderText="Allocation" />

                            <%--Status--%>
                            <asp:TemplateField HeaderText="Status">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="editStatusDDL" CssClass="form-control" runat="server"></asp:DropDownList>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="gridStatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="thead-light" />
                    </asp:GridView>
                </div>

            </div>
        </div>

        <div class="row">
            <div class="col text-right">
                <asp:Button ID="refreshBtn" CssClass="btn btn-primary" runat="server" Text="Refresh" OnClick="refreshBtn_Click" />
            </div>
        </div>

    </div>

</asp:Content>
