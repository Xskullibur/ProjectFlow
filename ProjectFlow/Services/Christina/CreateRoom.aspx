<%@ Page Title="" Language="C#" MasterPageFile="~/ServicesWithContent.Master" AutoEventWireup="true" CodeBehind="CreateRoom.aspx.cs" Inherits="ProjectFlow.Services.Christina.CreateRoom" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
            <%-- Create Room Content --%>
                <div class="card card-body projectflow-card-shadow container-fluid">
                    <div class="row py-3">
                        <div class="col-12">
                              <div class="form-group">
                                <label for="RoomNameTextBox">Room Name</label>
                                <asp:TextBox ID="RoomNameTextBox" CssClass="form-control" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="form-text text-danger" Font-Size="Small" runat="server" 
                                ErrorMessage="Room Name is Required!" ControlToValidate="RoomNameTextBox" Display="Dynamic" 
                                EnableClientScript="True"></asp:RequiredFieldValidator>
                              </div>
                              <div class="form-group">
                                  <label for="searchList">Attendees</label>
                                   <asp:ListBox ID="searchList" CssClass="selectpicker form-control" data-live-search="true" data-actions-box="true" runat="server" SelectionMode="Multiple"></asp:ListBox>
                                  <asp:Label ID="AttendeesErrorLbl" runat="server" Text="" ForeColor="Red"></asp:Label>
                              </div>
                              <div class="form-group">
                                 <label for="RoomDescriptionTextBox">Room Description</label>
                                 <asp:TextBox ID="RoomDescriptionTextBox" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                              </div>
                              <asp:Button ID="CreateRoomBtn" CssClass="btn btn-primary" runat="server" Text="New Room" OnClick="CreateRoomEvent" />
                        </div>
                    </div>
                </div>
</asp:Content>
