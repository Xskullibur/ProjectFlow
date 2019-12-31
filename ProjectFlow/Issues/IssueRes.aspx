<%@ Page Title="" Language="C#" MasterPageFile="~/ServicesWithContent.Master" AutoEventWireup="true" CodeBehind="IssueRes.aspx.cs" Inherits="ProjectFlow.Issues.IssueRes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container py-2">
        <div class="jumbotron jumbotron-fluid">
            <div class="container">
                <asp:Label ID="lbMember" runat="server"></asp:Label>  
                <br />
                <div class="row"> 
                    <div class ="col">
                        <div>
                            <asp:Label ID="lbIssue" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                    <div class="col-xl-3 col-12 text-right">

                        <div>
                            <asp:Button ID="btnYes" CssClass ="" runat="server" Text="&#128077;" OnClick="btnYes_Click" BorderStyle="None" BackColor="White" />&nbsp;
                        </div>
                        <div>
                            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                        </div>
                        <div>
                            <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                        </div>
                        <div>
                            <asp:Button ID="btnNo" CssClass ="" runat="server" Text="&#128078;" OnClick="btnNo_Click" BorderStyle="None" BackColor="White" />&nbsp;
                        </div>
                        <div>
                            <asp:Button ID="btnRandom" CssClass ="" runat="server" Text="&#127922;" OnClick="btnRandom_Click" BorderStyle="None" BackColor="White" />
                        </div>
                    </div>
                </div>
            
            <hr/>
        <div>
            <asp:TextBox ID="tbComments" runat="server" TextMode="MultiLine"></asp:TextBox>&nbsp;
            <asp:Button ID="Button1" CssClass ="btn btn-success" runat="server" Text="submit" OnClick="btnCommentSubmit_Click" />
           
        </div>
        
            <div class ="col">
            <asp:Repeater ID="Repeater1" runat="server">
                <HeaderTemplate>
                </HeaderTemplate>
                <ItemTemplate>
                    <br>
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
    </div>
</asp:Content>