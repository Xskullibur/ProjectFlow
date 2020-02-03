<%@ Page Title="" Language="C#" MasterPageFile="~/Tasks/TaskNested.master" AutoEventWireup="true" CodeBehind="OngoingTaskView.aspx.cs" Inherits="ProjectFlow.Tasks.tDetailedView" Async="true" %>
<%@ MasterType VirtualPath="~/Tasks/TaskNested.master" %> 

<%--TaskNested Master--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<%--Content--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container container-fluid py-2">

        <div class="row pb-2">
            <div class="col">

                <div style="overflow-x: auto;">
                    <asp:UpdatePanel ID="TaskGridUpdatePanel" runat="server">
                        <ContentTemplate>

                            <asp:GridView ID="taskGrid" runat="server" CssClass="table table-bordered table-striped projectflow-table" OnRowEditing="taskGrid_RowEditing" AutoGenerateColumns="False" OnRowCancelingEdit="taskGrid_RowCancelingEdit" OnRowUpdating="taskGrid_RowUpdating" OnRowDataBound="taskGrid_RowDataBound" OnRowDeleting="taskGrid_RowDeleting" AllowPaging="True" AllowSorting="True" OnPageIndexChanging="taskGrid_PageIndexChanging" PageSize="4" OnRowCommand="taskGrid_RowCommand" > 
                                <HeaderStyle CssClass="thead-light" />
                                <Columns>

                                    <%--Due Date--%>
                                    <asp:BoundField HeaderText="Due" ReadOnly="True" >
                                        <ItemStyle Font-Bold="True" />
                                    </asp:BoundField>

                                    <%--ID--%>
                                    <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" />
                                                                        
                                    <%--Priority--%>
                                    <asp:TemplateField HeaderText="Priority">
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="editPriorityDDL" CssClass="form-control" runat="server"></asp:DropDownList>
                                
                                            <asp:Label ID="priorityErrorLbl" CssClass="form-text text-danger" Font-Size="Small" runat="server" Text="" Visible="False"></asp:Label>
                                            <asp:RequiredFieldValidator ID="priorityRequiredValidator" CssClass="form-text text-danger" Font-Size="Small" runat="server" ErrorMessage="This Field is Required!" ControlToValidate="editStatusDDL" Display="Dynamic" ValidationGroup="EditTask" EnableClientScript="True"></asp:RequiredFieldValidator>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="gridPriority" runat="server" Text='<%# Bind("Priority") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

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
                                            <asp:Label ID="gridStart" runat="server" Text='<%# Bind("Start", "{0: dd/MM/yyyy}") %>'></asp:Label>
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
                                            <asp:Label ID="gridEnd" runat="server" Text='<%# Bind("End", "{0: dd/MM/yyyy}") %>'></asp:Label>
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

                                    <%--Action Settings--%>
                                    <asp:TemplateField ShowHeader="false">
                                        <ItemTemplate>
                                            <div class="dropdown mb-2">
                                                <button type="button" class="btn btn-sm btn-primary dropdown-toggle" data-toggle="dropdown">
                                                    Edit
                                                </button>

                                                <div class="dropdown-menu">
                                                    <asp:Button Text="Edit Details" CssClass="dropdown-item" CommandName="Edit" runat="server" />
                                                    <asp:Button ID="updateStatusBtn" Text="Update Status" CssClass="dropdown-item" CommandName="UpdateStatus" runat="server" />
                                                </div>
                                            </div>
                                            <asp:Button ID="DeleteButton" Text="Delete" runat="server" 
                                                CssClass="btn btn-sm btn-danger"
                                                data-toggle="confirmation"
                                                data-btn-ok-icon-class="fa fa-check"
                                                data-btn-cancel-icon-class="fa fa-close"
                                                data-popout="true"
                                                CommandName="Delete" /> 
                                        </ItemTemplate>

                                        <edititemtemplate>
					                        <asp:Button id="btnUpdate" CssClass="btn btn-sm btn-primary mb-2" runat="server" commandname="Update" text="Update" />
					                        <asp:Button id="btnCancel" CssClass="btn btn-sm btn-outline-danger" runat="server" commandname="Cancel" text="Cancel" />
				                        </edititemtemplate>

                                    </asp:TemplateField>

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
