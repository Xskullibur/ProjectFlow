<%@ Page Title="" Language="C#" MasterPageFile="~/Tasks/TaskNested.master" AutoEventWireup="true" CodeBehind="tDetailedView.aspx.cs" Inherits="ProjectFlow.Tasks.tDetailedView" %>

<%--TaskNested Master--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<%--Content--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container py-2">

        <div class="row pb-2">
            <div class="col">

                <div style="overflow-x: auto;">
                    <asp:GridView ID="taskGrid" runat="server" CssClass="table table-hover table-bordered" OnRowEditing="taskGrid_RowEditing" AutoGenerateColumns="False" OnRowCancelingEdit="taskGrid_RowCancelingEdit" OnRowUpdating="taskGrid_RowUpdating" OnRowDataBound="taskGrid_RowDataBound" OnRowDeleting="taskGrid_RowDeleting" OnSelectedIndexChanged="taskGrid_SelectedIndexChanged1" > 
                        <Columns>

                            <%--ID--%>
                            <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" />

                            <%--Task--%>
                            <asp:TemplateField HeaderText="Task">
                                <EditItemTemplate>
                                    <asp:TextBox ID="editTaskTxt" CssClass="form-control" runat="server" Text='<%# Bind("Task") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="gridTask" runat="server" Text='<%# Bind("Task") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%--Description--%>
                            <asp:TemplateField HeaderText="Description">
                                <EditItemTemplate>
                                    <asp:TextBox ID="editDescTxt" CssClass="form-control" runat="server" Text='<%# Bind("Description") %>' TextMode="MultiLine"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="gridDesc" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%--Milestone--%>
                            <asp:TemplateField HeaderText="Milestone">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="editMilestoneDDL" CssClass="form-control" runat="server"></asp:DropDownList>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="gridMilestone" runat="server" Text='<%# Bind("MileStone") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%--Start Date--%>
                            <asp:TemplateField HeaderText="Start">
                                <EditItemTemplate>
                                    <asp:TextBox ID="editStartDate" CssClass="form-control" runat="server" TextMode="Date" ></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="gridStart" runat="server" Text='<%# Bind("Start") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%--End Date--%>
                            <asp:TemplateField HeaderText="End">
                                <EditItemTemplate>
                                    <asp:TextBox ID="editEndDate" CssClass="form-control" runat="server" TextMode="Date"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="gridEnd" runat="server" Text='<%# Bind("End") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%--Allocation--%>
                            <asp:TemplateField HeaderText="Allocation">
                                <EditItemTemplate>
                                    <asp:ListBox ID="editAllocationList" CssClass="selectpicker form-control" data-live-search="true" data-actions-box="true" runat="server" SelectionMode="Multiple"></asp:ListBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="gridAllocation" runat="server" Text='<%# Bind("Allocation") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%--Status--%>
                            <asp:TemplateField HeaderText="Status">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="editStatusDDL" CssClass="form-control" runat="server"></asp:DropDownList>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="gridStatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:CommandField ShowSelectButton="True" ButtonType="Button" SelectText="Issues">
                                <ControlStyle CssClass="btn btn-success mb-2" />
                            </asp:CommandField>

                            <asp:CommandField ShowEditButton="True" ButtonType="Button" >
                                <ControlStyle CssClass="btn btn-primary mb-2" />
                            </asp:CommandField>

                            <asp:CommandField ShowDeleteButton="True" ButtonType="Button" >
                                <ControlStyle CssClass="btn btn-danger" />
                            </asp:CommandField>

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
