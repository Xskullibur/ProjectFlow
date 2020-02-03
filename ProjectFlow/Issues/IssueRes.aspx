<%@ Page Title="" Language="C#" MasterPageFile="~/ServicesWithContent.Master" AutoEventWireup="true" CodeBehind="IssueRes.aspx.cs" Inherits="ProjectFlow.Issues.IssueRes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="card card-body projectflow-card-shadow container py-2">
        <div class="row">
            <div class="col-lg-8 p-3">
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <div class="card-body">
                                <asp:Label ID="lbMember" runat="server"></asp:Label>  
                                <asp:Label ID="lbIssue" runat="server" Text=""></asp:Label>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
            </div>
            <div class="col-lg-4 p-3">
                <div class="card">
                    <div class="card-body">
                        <h6 class="card-subtitle mb-2 text-muted">Issue Info:</h6>
                        <div class=""><i class="fa fa-check-square-o" aria-hidden="true">&nbsp;</i><label>Active:&nbsp;</label><asp:Label ID="IssueActive" runat="server" Text=""></asp:Label></div>
                        <div class=""><i class="fa fa-exclamation-circle" aria-hidden="true">&nbsp;</i><label>Status:&nbsp;</label><asp:Label ID="IssueStatus" runat="server" Text=""></asp:Label></div>
                        <div class=""><i class="fa fa-eye" aria-hidden="true">&nbsp;</i><label>Public:&nbsp;</label><asp:Label ID="IssuePublic" runat="server" Text=""></asp:Label></div>
                        <div class=""><i class="fa fa-smile-o" aria-hidden="true">&nbsp;</i><label>Raised by:&nbsp;</label><asp:Label ID="IssueRaisedBy" runat="server" Text=""></asp:Label></div>
                    </div>
                </div>
            </div>
        </div>


        <div class ="row justify-content-between">    
            <div class="col-xl-5 col-12 align-self-end">
                <div class="btn-group ">
                    <asp:Button ID="btnYes" CssClass ="btn btn-outline-secondary" runat="server" Text="&#128077;" OnClick="btnYes_Click" />
                    <asp:Button ID="btnYesCount" CssClass ="btn btn-outline-secondary" runat="server" Text="" enabled="false"/>
                </div>&nbsp;
                <div class="btn-group ">
                    <asp:Button ID="btnNo" CssClass ="btn btn-outline-secondary" runat="server" Text="&#128078;" OnClick="btnNo_Click" />
                    <asp:Button ID="btnNoCount" CssClass ="btn btn-outline-secondary" runat="server" Text="" enabled="false"/>
                </div>&nbsp;
                <asp:Button ID="btnRandom" CssClass ="btn btn-outline-secondary" runat="server" Text="&#127922;" OnClick="btnRandom_Click" />
            </div>
            <div class="col-xl-3 col-12 text-right">
                <asp:Button Text="Edit Issue" CssClass="btn btn-primary" runat="server" ID="editIssueBtn" CausesValidation="False" OnClick="edit_click"/>
            </div>
        </div>
        <hr/>
        <div class="">
            <asp:TextBox ID="tbComments" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                
            <div class="text-right">
            <asp:Button ID="btnComment" CssClass="btn btn-primary px-4 mt-3" runat="server" Text="Post" OnClick="btnCommentSubmit_Click"/>
            </div>
        </div>
        <div class ="col">
            <asp:Repeater ID="Repeater1" runat="server">
                <HeaderTemplate>
                </HeaderTemplate>
                <ItemTemplate>
                    </br>
                    <div class="mdc-card">
                        <div class="card-body">
                            <asp:Label ID="lbCreatedBy" CssClass="card-title" runat="server" Text='<%# Eval("CreatedBy") %>' ForeColor="#0066FF"></asp:Label>
                            <asp:Label ID="lbComment" CssClass="card-text" runat="server" Text='<%# Eval("Comment") %>'></asp:Label>
                        </div>
                    </div>
                                  
                </ItemTemplate>     
            </asp:Repeater>
        </div>   
    </div>
</asp:Content>