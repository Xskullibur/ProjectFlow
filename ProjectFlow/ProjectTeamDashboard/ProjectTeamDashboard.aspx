<%@ Page Title="" Language="C#" MasterPageFile="~/ServicesWithContent.Master" AutoEventWireup="true" CodeBehind="ProjectTeamDashboard.aspx.cs" Inherits="ProjectFlow.ProjectTeamDashboard.ProjectTeamDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    
    <div class="container">

        <%--Title--%>
        <div class="row mb-3">
            <div class="col">
                <h1>
                    Overal Project Dashboard
                </h1>
            </div>
        </div>

        <script>
            $(function () {
                var ctx2 = document.getElementById('" + priorityName + @"_chart').getContext('2d');

                var fetch_url = './ProjectTeamDashboard.aspx/LoadDoughnutChart';
                return $.ajax({
                    url: fetch_url,
                    type: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    success: function (res, textStatus) {

                        var parsedJson = JSON.parse(res.d);
                        var chartData = parsedJson;
                        var chartOptions = {
                            responsive: false,
                            maintainAspectRatio: true,
                            tooltips: {
                                enabled: false
                            },
                            legend: {
                                onClick: (e) => e.stopPropagation()
                            }
                        };

                        var myChart = new Chart(ctx2, { type: 'doughnut', data: chartData, options: chartOptions }); 
                    },
                    error: function (res, textStatus) {

                    }
                });

            });
        </script>

        <%--Topbar--%>
        <div class="row mb-3">
            <div class="col">

                <asp:Panel ID="TaskPanel" CssClass="row" runat="server">
                </asp:Panel>

            </div>
        </div>

        <%--Middle Bar--%>
        <div class="row mb-3">
            <div class="col-6 p-3 border">

                <div class="row">
                    <div class="col">
                        <h5 class="text-center">Top 5 Upcoming Tasks</h5>
                    </div>
                </div>

                <div class="row">
                    <div class="col">
                        <asp:GridView ID="upcomingTaskGrid" runat="server" CssClass="table table-bordered" AutoGenerateColumns="False" > 
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

            <div class="col-6 border p-3">

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
                                <asp:BoundField DataField="taskID" HeaderText="ID" />
                                <asp:BoundField DataField="taskName" HeaderText="Task Name" />
                                <asp:BoundField DataField="endDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="End" />
                            </Columns>
                            <HeaderStyle CssClass="thead-light" />
                        </asp:GridView>
                    </div>
                </div>

            </div>
        </div>
    </div>

</asp:Content>
