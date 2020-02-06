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
                <div class="card projectflow-card-shadow w-100 m-0 my-3 p-3">

                    <%--Controls--%>
                    <div class="row">

                        <div class="col">
                            <div class="btn-toolbar">
                                <div class="btn-group mr-2">
                                    <button type="button" class="btn" onclick="saveCanvas()">
                                        <i class="fas fa-tint"></i>
                                    </button>
                                    <button type="button" class="btn" onclick="saveCanvas()">
                                        <i class="fas fa-tint"></i>
                                    </button>
                                    <button type="button" class="btn" onclick="saveCanvas()">
                                        <i class="fas fa-tint"></i>
                                    </button>
                                </div>   

                                <div class="btn-group mr-2">
                                    <button type="button" class="btn btn-primary" onclick="saveCanvas()">
                                        <i class="fas fa-save"></i>
                                    </button>
                                </div>
                            </div>
                        </div>

                        <div class="col text-right">
                            <button type="button" class="btn btn-primary" onclick="requestFullScreen()">
                                <i class="fas fa-compress"></i>
                            </button>
                        </div>
                    </div>

                </div>
            </div>
        </div>

        <div class="row">
            <div class="col">
                <div class="card projectflow-card-shadow w-100 m-0 my-3">

                    <div class="row">
                        <div class="col p-4">
                            <canvas id="board" class="d-block mx-auto" width="1000" height="800">
                            </canvas>
                        </div>
                    </div>

                </div>
            </div>
        </div>

    </div>

</asp:Content>
