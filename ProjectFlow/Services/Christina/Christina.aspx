<%@ Page Title="" Language="C#" MasterPageFile="~/ServicesWithContent.Master" AutoEventWireup="true" CodeBehind="Christina.aspx.cs" Inherits="ProjectFlow.Services.Christina.Christina" %>
<%@ Register Assembly="ProjectFlow"  Namespace="ProjectFlow.Utils.MaterialIO"  TagPrefix="mio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Content/ProjectFlow/CSS/christina.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <script type="text/javascript" src="/Scripts/jquery.signalR-2.4.1.min.js"></script>
    <script type="text/javascript" src="/signalr/hubs"></script>
    <script type="text/javascript" src="/Scripts/ProjectFlow/christina.js"></script>

    <script type="text/javascript" src="https://www.WebRTC-Experiment.com/RecordRTC.js"></script>

    <div class="container">
        <div class="row">
            <div class="col-12">
                <nav>
                    <div class="nav nav-tabs" id="nav-tab" role="tablist">
                    <a class="nav-item nav-link active" id="nav-meeting-logger-table-tab" data-toggle="tab" href="#nav-meeting-logger-table" role="tab" aria-controls="nav-meeting-logger-table" aria-selected="true">Meeting Logger</a>
                    <a class="nav-item nav-link" id="nav-christina-tab" data-toggle="tab" href="#nav-christina" role="tab" aria-controls="nav-christina" aria-selected="false">Christina</a>
                    </div>
                </nav>
                <div class="tab-content" id="nav-tabContent">
                    <div class="tab-pane fade show active" id="nav-meeting-logger-table" role="tabpanel" aria-labelledby="nav-meeting-logger-table-tab">
                        <div class="row">
                            <div class="col-12">
                                <mio:MaterialIOTableControl CssClass="w-100" runat="server" ID="materialTable">
                                    <Headers>
                                        <mio:MaterialIOTableRowHeaderData HeaderName="Type"/>
                                        <mio:MaterialIOTableRowHeaderData HeaderName="Suggester"/>
                                    </Headers>
                                    <Rows>
                                        <mio:MaterialIOTableRow>
                                            <Datas>
                                                <mio:MaterialIOTableRowData Value="Hello1"/>
                                                <mio:MaterialIOTableRowData Value="Hello2"/>
                                            </Datas>
                                        </mio:MaterialIOTableRow>
                                        <mio:MaterialIOTableRow>
                                            <Datas>
                                                <mio:MaterialIOTableRowData Value="Hello1"/>
                                                <mio:MaterialIOTableRowData Value="Hello2"/>
                                            </Datas>
                                        </mio:MaterialIOTableRow>
                                        <mio:MaterialIOTableRow>
                                            <Datas>
                                                <mio:MaterialIOTableRowData Value="sdsdsd"/>
                                                <mio:MaterialIOTableRowData Value="Hello2"/>
                                            </Datas>
                                        </mio:MaterialIOTableRow>
                                    </Rows>
                                </mio:MaterialIOTableControl>
                                <%-- Inputs --%>
                                <div class="container">
                                   <div class="row py-3 mx-auto">
                                       <div class="col-10 px-0">
                                           <asp:TextBox ID="SuggestionTextBox" CssClass="form-control" runat="server"></asp:TextBox>
                                       </div>
                                       <div class="col-2">
                                           <asp:Button ID="SuggestBtn" CssClass="btn btn-primary" runat="server" Text="Suggest" OnClick="SuggestEvent" />
                                       </div>
                                       <div>
                                           <asp:Label ID="ErrMsg" runat="server" Text="" ForeColor="Red"></asp:Label><br>
                                           <asp:Label ID="ErrLine" runat="server" Text="" ForeColor="Black"></asp:Label>
                                       </div>
                                   </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="tab-pane fade" id="nav-christina" role="tabpanel" aria-labelledby="nav-christina-tab">
                        <div id="canvas_window" class="col-12">
                            <canvas id="speaker_display">

                            </canvas>
                        </div>
                    </div>
                </div>
            </div>

            
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
