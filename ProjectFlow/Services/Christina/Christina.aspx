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


    <%-- Current selected room id --%>
    <asp:HiddenField ID="RoomID" runat="server" />
    <div class="container">
        <div class="row">
            <div class="card w-100 m-3">
                <%-- Fire event to update meeting details --%>
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <asp:LinkButton ID="RoomUpdateEventLinkBtn" CssClass="d-none" runat="server" OnClick="UpdateRoomDetailEvent">
                        </asp:LinkButton>
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
                        <div class="row py-2 px-3">
                            <asp:Button ID="CreateNewBtn" CssClass="btn btn-primary" runat="server" Text="Create New" OnClick="ShowCreateActionItemModalEvent" />
                            <asp:Button ID="Button1" CssClass="btn btn-primary" runat="server" Text="Select Mode" OnClick="ToggleSelectEvent" />
                            <asp:Button ID="Button2" CssClass="btn btn-danger" runat="server" Text="Delete" OnClick="DeleteEvent" OnClientClick="delete" />
                        </div>
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
                                <%-- Inputs --%>
                                <div class="container">
                                   <div class="row py-3 mx-auto">
                                       <div class="col-10 px-0">
                                           <asp:TextBox  ID="ExecuteTextBox" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                                       </div>
                                       <div class="col-2">
                                           <asp:Button ID="ExecuteBtn" CssClass="btn btn-primary" runat="server" Text="Execute" OnClick="ExecuteEvent" />
                                           
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
                        <div class="row">
                            <div id="canvas_window" class="col-9">
                            <canvas id="speaker_display">

                            </canvas>
                            
                        </div>
                        <div class="col-3">
                            <h5>Transcript</h5>
                            <textarea rows="2" cols="20" readonly="readonly" id="transcriptTxtBox" class="w-100 h-100 my-2"></textarea>
                        </div>
                        </div>
                    </div>
                </div>
            </div>

            
        </div>
    </div>

    <%-- Speaker Modal --%>
    <div id="actionItemCreateModal" class="modal fade" tabindex="-1" role="dialog">
        <asp:UpdatePanel ID="modalUpdatePanel" runat="server">
          <ContentTemplate>
              <div class="modal-dialog" role="document">
            <div class="modal-content">
              <div class="modal-header">
                <h5 class="modal-title">Create Action Item</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span>
                </button>
              </div>
              <div class="modal-body">
                <div class="form-group">
                    <label for="typeTxtBox">Type:</label>
                    <asp:TextBox CssClass="form-control" ID="typeTxtBox" runat="server" placeholder="type"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="personNameTxtBox">Person Name:</label>
                    <asp:TextBox CssClass="form-control" ID="personNameTxtBox" runat="server" placeholder="person name"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="messageTxtBox">Topic:</label>
                    <asp:TextBox CssClass="form-control" ID="topicTxtBox" runat="server" placeholder="topic"></asp:TextBox>
                </div>
              </div>
              <div class="modal-footer">
                <div class="highlight w-100">
                    <code id="generated_code"></code>
                    <asp:HiddenField ID="GeneratedCodeLbl" runat="server"></asp:HiddenField>
                </div>
                <asp:Button CssClass="btn btn-primary" ID="createActionItemBtn" runat="server" Text="Create Action Item" OnClick="CreateActionItemEvent" />
                <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
              </div>
            </div>
          </div>
          </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {

            canvas = $('#speaker_display');
            canvas_window = $('#canvas_window');
            init_display(canvas_window, canvas);

            $('div.form-group input').change(function () {
                
                updateCode();
            });
            
        });
        function updateCode() {
            let code = "person: " + $('#<%=personNameTxtBox.ClientID %>').val() + ' topic: "' + $('#<%=topicTxtBox.ClientID %>').val() + '"' + " type: " + $('#<%=typeTxtBox.ClientID %>').val() + ';';

            $('<%=GeneratedCodeLbl.ClientID %>').val(code);
            $('#generated_code').text(code);
        }

    </script>
</asp:Content>
