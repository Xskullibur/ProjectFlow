<%@ Page Title="" Language="C#" MasterPageFile="~/ServicesWithContent.Master" AutoEventWireup="true" CodeBehind="ProjectTeamDashboard.aspx.cs" Inherits="ProjectFlow.ProjectTeamDashboard.ProjectTeamDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <%--ChartJS--%>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.9.3/Chart.bundle.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.9.3/Chart.bundle.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.9.3/Chart.css" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.9.3/Chart.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.9.3/Chart.min.css" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.9.3/Chart.min.js"></script>

    <link href="https://kodhus.com/kodhus-ui/kodhus-0.1.0.min.css" rel="stylesheet" type="text/css"/>
    <script src="https://kodhus.com/kodhus-ui/kodhus-0.1.0.min.js"></script>
    <script type="text/javascript" src="/Scripts/ProjectFlow/TeamDashboard.js"></script>
    <link href="\Content\ProjectFlow\CSS\progressBar.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <div class="container">

        <div class="row mb-4">
            <div class="col">

                <div class="card card-body projectflow-card-shadow py-3">
                    <asp:Literal ID="milestoneLiteral" runat="server"></asp:Literal>
                </div>

            </div>
        </div>

        <%--Topbar--%>
        <div class="row mb-2">
            <div class="col">
                <div class="card card-body projectflow-card-shadow">

                    <asp:Panel ID="TaskPanel" CssClass="row p-3" runat="server">
                    </asp:Panel>

                </div>
            </div>
        </div>

        <%--Upcoming Task / Issues--%>
        <div class="row mb-2">

            <%--Upcoming Task--%>
            <div class="col p-3">
                <div class="card card-body projectflow-card-shadow">

                    <div class="row">
                        <div class="col">
                            <h5 class="text-center">Top 5 Upcoming Tasks</h5>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col">
                            <asp:GridView ID="upcomingTaskGrid" runat="server" CssClass="table table-sm table-bordered" AutoGenerateColumns="False" > 
                                <Columns>
                                    <asp:BoundField DataField="ID" HeaderText="ID" />
                                    <asp:BoundField DataField="Task" HeaderText="Task Name" />
                                    <asp:BoundField DataField="Allocation" HeaderText="Allocation" />
                                    <asp:BoundField DataField="End" DataFormatString="{0:dd/MM/yyyy}" HeaderText="End" />
                                    <asp:BoundField DataField="Status" HeaderText="Status" />
                                </Columns>
                                <EmptyDataTemplate>
                                    <div class="jumbotron jumbotron-fluid m-0 text-center">
                                        <h2>No Upcoming Tasks</h2><br>
                                        <i class="fas fa-check-circle fa-5x text-success"></i>
                                    </div>
                                </EmptyDataTemplate>
                                <HeaderStyle CssClass="thead-light" />
                            </asp:GridView>
                        </div>
                    </div>

                </div>
            </div>

            <%--Issues--%>
            <div class="col p-3">
                <div class="card card-body projectflow-card-shadow">

                    <div class="row">
                        <div class="col">
                            <h5 class="text-center">Issues</h5>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col">
                            <asp:GridView ID="IssueGrid" runat="server" CssClass="table table-sm table-bordered" AutoGenerateColumns="False" > 
                                <Columns>
                                    <asp:BoundField DataField="ID" HeaderText="Issue ID" />
                                    <asp:BoundField DataField="Task" HeaderText="Issue Name" />
                                    <asp:BoundField DataField="TaskID" HeaderText="Task ID" />
                                    <asp:BoundField DataField="Description" HeaderText="Description" />
                                </Columns>
                                <EmptyDataTemplate>
                                    <div class="jumbotron jumbotron-fluid m-0 text-center">
                                        <h2>No Issues</h2><br>
                                        <i class="fas fa-check-circle fa-5x text-success"></i>
                                    </div>
                                </EmptyDataTemplate>
                                <HeaderStyle CssClass="thead-light" />
                            </asp:GridView>
                        </div>
                    </div>

                </div>
            </div>

        </div>

        <%--Overdue Task--%>
        <div class="row mb-2">
            <div class="col p-3">
                <div class="card card-body projectflow-card-shadow">

                    <div class="row">
                        <div class="col">
                            <h5 class="text-center">Top 5 Overdue Tasks</h5>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col">
                            <asp:GridView ID="overdueTaskGrid" runat="server" CssClass="table table-bordered" AutoGenerateColumns="False" OnRowDataBound="overdueTaskGrid_RowDataBound" > 
                                <Columns>
                                    <asp:BoundField HeaderText="Due" ReadOnly="True" >
                                        <ItemStyle Font-Bold="True" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ID" HeaderText="ID" />
                                    <asp:BoundField DataField="Task" HeaderText="Task Name" />
                                    <asp:BoundField DataField="End" DataFormatString="{0:dd/MM/yyyy}" HeaderText="End" />
                                    <asp:BoundField DataField="Status" HeaderText="Status" />
                                </Columns>
                                <EmptyDataTemplate>
                                    <div class="jumbotron jumbotron-fluid m-0 text-center">
                                        <h2>No Overdue Tasks</h2><br>
                                        <i class="fas fa-check-circle fa-5x text-success"></i>
                                    </div>
                                </EmptyDataTemplate>
                                <HeaderStyle CssClass="thead-light" />
                            </asp:GridView>
                        </div>
                    </div>

                </div>
            <//div>
        </div>
    </div>

</asp:Content>
