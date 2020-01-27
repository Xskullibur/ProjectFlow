<%@ Page Title="" Language="C#" MasterPageFile="~/ServicesWithContent.Master" AutoEventWireup="true" CodeBehind="RoomDetail.aspx.cs" Inherits="ProjectFlow.Services.Christina.RoomDetail" %>
<%@ Register Assembly="ProjectFlow"  Namespace="ProjectFlow.Utils.MaterialIO"  TagPrefix="mio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Content/ProjectFlow/CSS/christina.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">



    <%-- Current selected room id --%>
    <asp:HiddenField ID="RoomID" runat="server" />
    <div class="container">
        <div class="row">
            <div class="card w-100 m-3">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div class="card-body">
                            <h5 class="card-title">Meeting Minutes</h5>
                            <h6 class="card-subtitle mb-2 text-muted">Room Info:</h6>
                            <div class="col-12 col-md-6"><i class="fa fa-calendar" aria-hidden="true">&nbsp;</i><label>Meeting Date:&nbsp;</label><asp:Label ID="MeetingDate" runat="server" Text=""></asp:Label></div>
                            <div class="col-12 col-md-6"><i class="fa fa-clock-o" aria-hidden="true">&nbsp;</i><label>Meeting Time:&nbsp;</label><asp:Label ID="MeetingTime" runat="server" Text=""></asp:Label></div>
                            <div class="col-12 col-md-6"><i class="fa fa-user" aria-hidden="true">&nbsp;</i><label>Attendees:&nbsp;</label><asp:Label ID="AttendeesLbl" runat="server" Text=""></asp:Label></div>
                            <div class="col-12 col-md-6"><i class="fa fa-smile-o" aria-hidden="true">&nbsp;</i><label>Meeting made by:&nbsp;</label><asp:Label ID="MadeByLbl" runat="server" Text=""></asp:Label></div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="row">
            <div class="col-12">
                <nav>
                    <div class="nav nav-tabs" id="nav-tab" role="tablist">
                    <a class="nav-item nav-link active" id="nav-meeting-logger-table-tab" data-toggle="tab" href="#nav-meeting-logger-table" role="tab" aria-controls="nav-meeting-logger-table" aria-selected="true">Meeting Logger</a>
                    <a class="nav-item nav-link" id="nav-christina-tab" data-toggle="tab" href="#nav-christina" role="tab" aria-controls="nav-christina" aria-selected="false">
                        Christina
                        <span class="badge badge-pill badge-info">Experimental</span>
                    </a>
                    </div>
                </nav>
                <div class="tab-content" id="nav-tabContent">
                    <div class="tab-pane fade show active" id="nav-meeting-logger-table" role="tabpanel" aria-labelledby="nav-meeting-logger-table-tab">
                        <div class="row pd-2">
                            <div class="col-12">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <mio:MaterialIOTableControl CssClass="w-100" runat="server" ID="materialTable">
                                            <Headers>
                                                <mio:MaterialIOTableRowHeaderData HeaderName="Type"/>
                                                <mio:MaterialIOTableRowHeaderData HeaderName="Suggester"/>
                                                <mio:MaterialIOTableRowHeaderData HeaderName="Message"/>
                                            </Headers>
                                        </mio:MaterialIOTableControl>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                    <div class="tab-pane fade" id="nav-christina" role="tabpanel" aria-labelledby="nav-christina-tab">
                        <div class="row">
                            <div class="col-12">
                                <h5>Transcript</h5>
                                <asp:TextBox ID="transcriptTxtBox" CssClass="w-100 h-100 my-2" runat="server" ReadOnly="True" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            
        </div>
    </div>
</asp:Content>
