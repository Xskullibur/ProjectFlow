<%@ Page Title="" Language="C#" MasterPageFile="~/ServicesWithContent.Master" AutoEventWireup="true" CodeBehind="ProjectTeamDashboard.aspx.cs" Inherits="ProjectFlow.ProjectTeamDashboard.ProjectTeamDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://kodhus.com/kodhus-ui/kodhus-0.1.0.min.css" rel="stylesheet" type="text/css"/>
    <script src="https://kodhus.com/kodhus-ui/kodhus-0.1.0.min.js"></script>
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

        function loadProgressBar(activeIndex) {
            var progressBar = document.getElementById("progressbar");
            progressBar = new Kodhus.StepProgressBar();
            progressBar.init({ activeIndex })
        }
    </script>

    <style>
        .cdt-step-progressbar.horizontal li .indicator{
            z-index: 2;
        }

        .cdt-step-progressbar.horizontal li.active .indicator {
            border-color: #28a745;
            background-color: #28a745;
            color: white;
        }

        .cdt-step-progressbar.horizontal li.active .title{
            color: black;
        }

        .cdt-step-progressbar.horizontal li.active:before{
            background-color: #28a745;
        }

        .cdt-step-progressbar.horizontal li:before{
            height: 5px;
            z-index: 1;
        }

        .cdt-step-progressbar li .indicator{
            width: 40px;
            height: 40px;
            border-width: 2.5px;
        }

        body{
            font-size: 1rem;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <div class="container">

        <div class="row mb-4">
            <div class="col">

                <div class="card card-body projectflow-card-shadow">

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
                                <HeaderStyle CssClass="thead-light" />
                            </asp:GridView>
                        </div>
                    </div>

                </div>
            <//div>
        </div>
    </div>

</asp:Content>
