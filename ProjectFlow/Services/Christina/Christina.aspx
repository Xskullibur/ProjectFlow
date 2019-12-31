<%@ Page Title="" Language="C#" MasterPageFile="~/ServicesWithContent.Master" AutoEventWireup="true" CodeBehind="Christina.aspx.cs" Inherits="ProjectFlow.Services.Christina.Christina" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <script type="text/javascript" src="/Scripts/jquery.signalR-2.4.1.min.js"></script>
    <script type="text/javascript" src="/signalr/hubs"></script>
    <script type="text/javascript" src="/Scripts/ProjectFlow/christina.js"></script>

    <script src="https://www.WebRTC-Experiment.com/RecordRTC.js"></script>

    <div class="row">
        <div id="canvas_window" class="col-12">
            <canvas id="speaker_display">

            </canvas>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            canvas = $('#speaker_display');
            canvas_window = $('#canvas_window');
            init_display(canvas_window,canvas);
        });
    </script>
</asp:Content>
