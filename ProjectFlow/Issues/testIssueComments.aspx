<%@ Page Title="" Language="C#" MasterPageFile="~/ServicesWithContent.Master" AutoEventWireup="true" CodeBehind="testIssueComments.aspx.cs" Inherits="ProjectFlow.Issues.testIssueComments" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class ="row">
        <div class ="col-md-2">
            <asp:Label ID="Label1" runat="server" Text="Comment"></asp:Label>
        </div>
        <div class ="col-md-10">
            <div class =" row">     
                <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine"></asp:TextBox>
            </div>
            <div class =" row">
                <asp:Button ID="Button1" CssClass ="btn btn-success" runat="server" Text="submit" />
            </div>
        </div>
    </div>    
    <div class ="row">
        <div class ="col">
        <asp:Repeater ID="Repeater1" runat="server">
            <HeaderTemplate>
            </HeaderTemplate>
            <ItemTemplate>
                <div class="row">
                    <asp:Label ID="Label2" runat="server" Text=""></asp:Label><asp:Label ID="Label3" runat="server" Text="<%# Container.DataItem %>"></asp:Label>
                </div>
            </ItemTemplate>     
        </asp:Repeater>
        </div>
    </div>
</asp:Content>
