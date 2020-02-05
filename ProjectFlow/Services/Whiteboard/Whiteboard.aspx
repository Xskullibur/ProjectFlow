<%@ Page Title="" Language="C#" MasterPageFile="~/ServicesWithContent.Master" AutoEventWireup="true" CodeBehind="Whiteboard.aspx.cs" Inherits="ProjectFlow.Services.Whiteboard.Whiteboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        canvas {
            border: dashed;
            width: 800px;
            height: auto;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <%--Include Scripts--%>
    <script type="text/javascript" src="/Scripts/jquery.signalR-2.4.1.min.js"></script>
    <script type="text/javascript" src="/signalr/hubs"></script>
    <script type="text/javascript" src="/Scripts/ProjectFlow/whiteboard.js"></script>

    <div class="container">

        <div class="row">
            <div class="col">
                <div class="card projectflow-card-shadow w-100 m-0 my-3">
                    <%--Controls--%>
                    <div class="row">
                        <div class="col">
                            
                        </div>

                    </div>
                    <div class="row">
                        <div class="col">
                            <canvas id="board" class="center-block" width="1000" height="800">
                            </canvas>
                        </div>
                    </div>

                </div>
            </div>
        </div>

    </div>

</asp:Content>
