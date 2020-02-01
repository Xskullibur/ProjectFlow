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
                  </div>
                  <div class="form-group">
                       <asp:ListBox ID="searchList" CssClass="selectpicker form-control" data-live-search="true" data-actions-box="true" runat="server" SelectionMode="Multiple" AutoPostBack="true" OnSelectedIndexChanged="searchList_SelectedIndexChanged"></asp:ListBox>
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
