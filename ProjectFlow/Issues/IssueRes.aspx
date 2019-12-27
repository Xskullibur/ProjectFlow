<%@ Page Title="" Language="C#" MasterPageFile="~/ServicesWithContent.Master" AutoEventWireup="true" CodeBehind="IssueRes.aspx.cs" Inherits="ProjectFlow.Issues.IssueRes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container py-2">
               
            <div class="row">
                <div class="rounded-top p-1 bg-info text-white">
                    <asp:Label ID="lbMember" runat="server">Issue Raised by: </asp:Label><asp:Label ID="lbMember2" runat="server" Text=""></asp:Label>
                </div>
                <div class="rounded-bottom p-1 bg-light">
                    <asp:Label ID="lbIssue" runat="server" Text=""></asp:Label>
                </div>   
            </div>
            <div class="row">
                <asp:Button ID="btnYes" CssClass ="btn btn-success" runat="server" Text="Yes" OnClick="btnYes_Click" />&nbsp;
                <asp:Button ID="btnNo" CssClass ="btn btn-danger" runat="server" Text="No" OnClick="btnNo_Click" />&nbsp;
                <asp:Button ID="btnRandom" CssClass ="btn btn-secondary" runat="server" Text="Random" OnClick="btnRandom_Click" />
                <asp:Label ID="Label1" runat="server" Text="Your input: "></asp:Label>
            </div>
        
        <hr>
        <div class ="row">    
            
                <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine"></asp:TextBox>
                <asp:Button ID="Button1" CssClass ="btn btn-success" runat="server" Text="submit" OnClick="btnCommentSubmit_Click" />
            
        </div>        
                
        <div class ="row">
            <div class ="col">
            <asp:Repeater ID="Repeater1" runat="server">
                <HeaderTemplate>
                </HeaderTemplate>
                <ItemTemplate>
                    
                    <div class="row">
                        <asp:Label ID="lbCreatedBy" runat="server" Text='<%# Eval("CreatedBy") %>' ForeColor="#0066FF"></asp:Label>
                    </div>
                    <div class="row">
                        <asp:Label ID="lbComment" runat="server" Text='<%# Eval("Comment") %>'></asp:Label>
                    </div>                
                </ItemTemplate>     
            </asp:Repeater>
            </div>
        </div>   
    </div>
</asp:Content>