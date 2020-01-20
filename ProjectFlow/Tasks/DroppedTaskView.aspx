<%@ Page Title="" Language="C#" MasterPageFile="~/Tasks/TaskNested.master" AutoEventWireup="true" CodeBehind="DroppedTaskView.aspx.cs" Inherits="ProjectFlow.Tasks.DroppedTaskView" Async="true" %>
<%@ MasterType VirtualPath="~/Tasks/TaskNested.master" %> 

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container py-2">
        <div class="row">

            <div class="col">
                <div style="overflow-x: auto;">
                    <asp:UpdatePanel ID="TaskGridUpdatePanel" runat="server">
                        <ContentTemplate>

                            <asp:GridView ID="taskGrid" runat="server" CssClass="table table-bordered" OnRowDeleting="taskGrid_RowDeleting" AllowPaging="True" OnPageIndexChanging="taskGrid_PageIndexChanging" PageSize="4" AutoGenerateColumns="False"> 
                                <HeaderStyle CssClass="thead-light" />
                                <Columns>

                                    <asp:BoundField DataField="ID" HeaderText="ID" />
                                    <asp:BoundField DataField="Task" HeaderText="Task" />
                                    <asp:BoundField DataField="Description" HeaderText="Description" />
                                    <asp:BoundField DataField="MileStone" HeaderText="Milestone" />
                                    <asp:BoundField DataField="Start" HeaderText="Start" />
                                    <asp:BoundField DataField="End" HeaderText="End" />
                                    <asp:BoundField DataField="Allocation" HeaderText="Allocation" />
                                    <asp:BoundField DataField="Status" HeaderText="Status" />

                                    <asp:TemplateField ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:Button ID="DeleteButton" Text="Restore" runat="server" 
                                                CssClass="btn btn-primary"
                                                data-toggle="confirmation"
                                                data-btn-ok-icon-class="fa fa-check"
                                                data-btn-cancel-icon-class="fa fa-close"
                                                data-popout="true"
                                                CommandName="Delete" />               
                                        </ItemTemplate>
                                    </asp:TemplateField> 

                                </Columns>

                                <EmptyDataTemplate>
                                    <div class="jumbotron jumbotron-fluid">
                                        <div class="container">
                                            <h1 class="display-4">Good Job, Very Consistent!</h1>
                                            <p class="load">No Dropped Task Found!</p>
                                            <hr class="my-4" />
                                            <p>If dropped tasks are expected but not shown please contact us <asp:HyperLink ID="emailLink" Text="here" NavigateUrl="mailto:projectflow.nyp.eadp@gmail.com" runat="server"></asp:HyperLink>.</p>
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
    </div>

</asp:Content>
