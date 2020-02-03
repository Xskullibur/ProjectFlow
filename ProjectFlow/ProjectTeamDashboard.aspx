<%@ Page Title="" Language="C#" MasterPageFile="~/ServicesWithContent.Master" AutoEventWireup="true" CodeBehind="ProjectTeamDashboard.aspx.cs" Inherits="ProjectFlow.ProjectTeamDashboard.ProjectTeamDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type='text/javascript'>
        function loadDoughnutChart(canvasID, data1, data2) {

            var ctx = document.getElementById(canvasID).getContext('2d')
            var chart = new Chart(ctx, {
                type: 'doughnut',
                data: {
                    datasets: [{
                        data: [data1, data2],
                        backgroundColor: ['Green'],
                    }]
                },
                options: {
                    responsive: true,
                    maintainAspectRatio: true,
                    tooltips: {
                        enabled: false
                    },
                    legend: {
                        onClick: (e) => e.stopPropagation()
                    }
                }
            })

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <div class="container">

        <%--Topbar--%>
        <div class="row mb-2">
            <div class="col">
                <div class="card card-body projectflow-card-shadow">

                    <asp:Panel ID="TaskPanel" CssClass="row" runat="server">
                    </asp:Panel>

                </div>
            </div>
        </div>

        <%--Middle Bar--%>
        <div class="row mb-2">

            <div class="col-6 p-3">
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
                                <HeaderStyle CssClass="thead-light" />
                            </asp:GridView>
                        </div>
                    </div>

                </div>
            </div>

            <div class="col-6 p-3">
                <div class="card card-body projectflow-card-shadow">

                    <div class="row">
                        <div class="col">
                            <h5 class="text-center">Top 5 Overdue Tasks</h5>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col">
                            <asp:GridView ID="overdueTaskGrid" runat="server" CssClass="table table-sm table-bordered" AutoGenerateColumns="False" OnRowDataBound="overdueTaskGrid_RowDataBound" > 
                                <Columns>
                                    <asp:BoundField HeaderText="Due" ReadOnly="True" >
                                        <ItemStyle Font-Bold="True" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="taskID" HeaderText="ID" />
                                    <asp:BoundField DataField="taskName" HeaderText="Task Name" />
                                    <asp:BoundField DataField="endDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="End" />
                                </Columns>
                                <HeaderStyle CssClass="thead-light" />
                            </asp:GridView>
                        </div>
                    </div>

                </div>
            <//div>
        </div>
    </div>

</asp:Content>
