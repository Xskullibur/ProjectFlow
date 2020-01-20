<%@ Page Title="" Language="C#" MasterPageFile="~/Tasks/TaskNested.master" AutoEventWireup="true" CodeBehind="OngoingTaskView.aspx.cs" Inherits="ProjectFlow.Tasks.tDetailedView" %>
<%@ MasterType VirtualPath="~/Tasks/TaskNested.master" %> 

<%--TaskNested Master--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<%--Content--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container py-2 w-100 h-100">

        <div class="row pb-2">
            <div class="col">

                <div style="overflow-x: auto;">

                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>

                            <asp:GridView ID="taskGrid" runat="server" CssClass="table table-bordered" OnRowEditing="taskGrid_RowEditing" AutoGenerateColumns="False" OnRowCancelingEdit="taskGrid_RowCancelingEdit" OnRowUpdating="taskGrid_RowUpdating" OnRowDataBound="taskGrid_RowDataBound" OnRowDeleting="taskGrid_RowDeleting" AllowPaging="True" AllowSorting="True" OnPageIndexChanging="taskGrid_PageIndexChanging" PageSize="4" > 
                                <HeaderStyle CssClass="thead-light" />
                                <Columns>

                                    <%--ID--%>
                                    <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" />

                                    <%--Task--%>
                                    <asp:TemplateField HeaderText="Task">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="editTaskTxt" CssClass="form-control" runat="server" Text='<%# Bind("Task") %>'></asp:TextBox>

                                            <asp:Label ID="tNameErrorLbl" CssClass="form-text text-danger" Font-Size="Small" runat="server" Text="" Visible="False"></asp:Label>
                                            <asp:RegularExpressionValidator ID="tNameRegexValidator" CssClass="form-text text-danger" Font-Size="Small" runat="server" ErrorMessage="Maximum Length of 255 Characters!" ValidationExpression="^[\s\S]{1,255}$" Display="Dynamic" ControlToValidate="editTaskTxt" ValidationGroup="EditTask"></asp:RegularExpressionValidator>
                                            <asp:RequiredFieldValidator ID="tNameRequiredValidator" CssClass="form-text text-danger" Font-Size="Small" runat="server" ErrorMessage="Task Name Field is Required!" ControlToValidate="editTaskTxt" Display="Dynamic" ValidationGroup="EditTask" EnableClientScript="True"></asp:RequiredFieldValidator>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="gridTask" runat="server" Text='<%# Bind("Task") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <%--Description--%>
                                    <asp:TemplateField HeaderText="Description">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="editDescTxt" CssClass="form-control" runat="server" Text='<%# Bind("Description") %>' TextMode="MultiLine"></asp:TextBox>
                                
                                            <asp:Label ID="tDescErrorLbl" CssClass="form-text text-danger" Font-Size="Small" runat="server" Text="" Visible="False"></asp:Label>
                                            <asp:RegularExpressionValidator ID="tDescRegexValidator" CssClass="form-text text-danger" Font-Size="Small" runat="server" ErrorMessage="Maximum Length of 255 Characters!" ValidationExpression="^[\s\S]{1,255}$" Display="Dynamic" ControlToValidate="editDescTxt" ValidationGroup="EditTask"></asp:RegularExpressionValidator>
                                            <asp:RequiredFieldValidator ID="tDescRequiredValidator" CssClass="form-text text-danger" Font-Size="Small" runat="server" ErrorMessage="Description Field is Required!" ControlToValidate="editDescTxt" Display="Dynamic" ValidationGroup="EditTask" EnableClientScript="True"></asp:RequiredFieldValidator>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="gridDesc" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <%--Milestone--%>
                                    <asp:TemplateField HeaderText="Milestone">
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="editMilestoneDDL" CssClass="form-control" runat="server"></asp:DropDownList>
                                
                                            <asp:Label ID="tMilestoneErrorLbl" CssClass="form-text text-danger" Font-Size="Small" runat="server" Text="" Visible="False"></asp:Label>
                                            <asp:RequiredFieldValidator ID="milestoneRequiredValidator" CssClass="form-text text-danger" Font-Size="Small" runat="server" ErrorMessage="Milestone Field is Required!" ControlToValidate="editMilestoneDDL" Display="Dynamic" ValidationGroup="EditTask" EnableClientScript="True"></asp:RequiredFieldValidator>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="gridMilestone" runat="server" Text='<%# Bind("MileStone") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <%--Start Date--%>
                                    <asp:TemplateField HeaderText="Start">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="editStartDate" CssClass="form-control" runat="server" TextMode="Date" ></asp:TextBox>

                                            <asp:Label ID="tStartDateErrorLbl" CssClass="form-text text-danger" Font-Size="Small" runat="server" Text="" Visible="False"></asp:Label>
                                            <asp:RequiredFieldValidator ID="startDateRequiredValidator" CssClass="form-text text-danger" Font-Size="Small" runat="server" ErrorMessage="Start-Date Field is Required!" ControlToValidate="editStartDate" Display="Dynamic" ValidationGroup="EditTask" EnableClientScript="True"></asp:RequiredFieldValidator>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="gridStart" runat="server" Text='<%# Bind("Start") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <%--End Date--%>
                                    <asp:TemplateField HeaderText="End">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="editEndDate" CssClass="form-control" runat="server" TextMode="Date"></asp:TextBox>
                                
                                            <asp:Label ID="startEndDateErrorLbl" CssClass="form-text text-danger" Font-Size="Small" runat="server" Text="" Visible="False"></asp:Label>
                                            <asp:CompareValidator ID="datesCompareValidator" CssClass="form-text text-danger" Font-Size="Small" runat="server" ErrorMessage="Start Date cannot be later than End Date!" ValidationGroup="EditTask" ControlToValidate="editStartDate" Display="Dynamic" ControlToCompare="editEndDate" Operator="LessThanEqual" Type="Date"></asp:CompareValidator>

                                            <asp:Label ID="tEndDateErrorLbl" CssClass="form-text text-danger" Font-Size="Small" runat="server" Text="" Visible="False"></asp:Label>
                                            <asp:RequiredFieldValidator ID="endDateRequiredValidator" CssClass="form-text text-danger" Font-Size="Small" runat="server" ErrorMessage="End-Date Field is Required!" ControlToValidate="editEndDate" Display="Dynamic" ValidationGroup="EditTask" EnableClientScript="True"></asp:RequiredFieldValidator>
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
                                
                                            <asp:Label ID="statusErrorLbl" CssClass="form-text text-danger" Font-Size="Small" runat="server" Text="" Visible="False"></asp:Label>
                                            <asp:RequiredFieldValidator ID="statusRequiredValidator" CssClass="form-text text-danger" Font-Size="Small" runat="server" ErrorMessage="This Field is Required!" ControlToValidate="editStatusDDL" Display="Dynamic" ValidationGroup="EditTask" EnableClientScript="True"></asp:RequiredFieldValidator>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="gridStatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:CommandField ShowEditButton="True" ButtonType="Button" ValidationGroup="EditTask">
                                        <ControlStyle CssClass="btn btn-primary mb-2" />
                                    </asp:CommandField>

                                    <asp:CommandField ShowDeleteButton="True" ButtonType="Button" >
                                        <ControlStyle CssClass="btn btn-danger" />
                                    </asp:CommandField>

                                </Columns>

                                <EmptyDataTemplate>
                                    <div class="jumbotron jumbotron-fluid">
                                        <div class="container">
                                            <h1 class="display-4">Freedoommm!</h1>
                                            <p class="load">No Ongoing Tasks Found!</p>
                                            <hr class="my-4" />
                                            <p>If ongoing tasks are expected but not shown please contact us <asp:HyperLink ID="emailLink" Text="here" NavigateUrl="mailto:projectflow.nyp.eadp@gmail.com" runat="server"></asp:HyperLink>.</p>
                                        </div>
                                    </div>
                                </EmptyDataTemplate>
                                <PagerSettings Mode="NumericFirstLast" PageButtonCount="3" />
                                <PagerStyle CssClass="pagination-ys table-borderless" />
                            </asp:GridView>

                        </ContentTemplate>
                    </asp:UpdatePanel>

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
