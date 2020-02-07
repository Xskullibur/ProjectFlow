<%@ Page Title="" Language="C#" MasterPageFile="~/ServicesWithContent.Master" AutoEventWireup="true" CodeBehind="Whiteboard.aspx.cs" Inherits="ProjectFlow.Services.Whiteboard.Whiteboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        canvas {
            border: dashed;
            width: 800px;
            height: auto;
        }
        .btn-outline-secondary:not(:disabled):not(.disabled).active{
            background-color: rgba(108, 117, 125, 0.3)!important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <%--Include Scripts--%>
    <script type="text/javascript" src="/Scripts/jquery.signalR-2.4.1.min.js"></script>
    <script type="text/javascript" src="/signalr/hubs"></script>
    <script type="text/javascript" src="/Scripts/ProjectFlow/whiteboard.js"></script>

    <link href="/Content/bootstrap-toast/toast.min.css" rel="stylesheet" />
    <script type="text/javascript" src="/Scripts/bootstrap-toast/toast.min.js"></script>

    <div class="container">

        <div class="row">
            <div class="col">
                <div class="card projectflow-card-shadow w-100 m-0 my-3 p-3">

                    <%--Controls--%>
                    <div class="row">

                        <div class="col">
                            <div class="btn-toolbar">
                                <div class="btn-group btn-group-toggle mr-2" data-toggle="buttons">
                                                                        
                                    <label class="btn btn-outline-secondary active" onclick="changeColor('#000')">
                                        <input type="radio"/><i class="fas fa-tint text-dark"></i>
                                    </label>                                                                        
                                    <label class="btn btn-outline-secondary" onclick="changeColor('#28a745')">
                                        <input type="radio"/><i class="fas fa-tint text-success"></i>
                                    </label>                                                                        
                                    <label class="btn btn-outline-secondary" onclick="changeColor('#ffc107')">
                                        <input type="radio" /><i class="fas fa-tint text-warning"></i>
                                    </label>                                                                        
                                    <label class="btn btn-outline-secondary" onclick="changeColor('#dc3545')">
                                        <input type="radio" /><i class="fas fa-tint text-danger"></i>
                                    </label>                                                                        
                                    <label class="btn btn-outline-secondary" onclick="changeColor('#17a2b8')">
                                        <input type="radio" /><i class="fas fa-tint text-info"></i>
                                    </label>                                                                        

                                </div>   

                                <div class="btn-group btn-group-toggle mr-2" data-toggle="buttons">
                                                                        
                                    <label class="btn btn-outline-secondary active" onclick="changeSize(1)">
                                        <input type="radio"/><i class="fas fa-pen fa-xs text-dark"></i>
                                    </label>                                                                           
                                    <label class="btn btn-outline-secondary" onclick="changeSize(3)">
                                        <input type="radio"/><i class="fas fa-pen fa-sm text-dark"></i>
                                    </label>                                                                           
                                    <label class="btn btn-outline-secondary" onclick="changeSize(7)">
                                        <input type="radio"/><i class="fas fa-pen fa-lg text-dark"></i>
                                    </label>                                                                                                                                           

                                </div>   

                                <div class="btn-group mr-2">
                                    <button type="button" class="btn btn-primary" onclick="saveCanvas()">
                                        <i class="fas fa-save"></i>
                                    </button>
                                    <button type="button" class="btn btn-primary" onclick="clearCanvas()">
                                        <i class="fas fa-eraser"></i>
                                    </button>
                                </div>
                                <div id="connected_users" class="btn-group mr-2">
                                    <b>Also editing this whiteboard:</b> &nbsp;
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
