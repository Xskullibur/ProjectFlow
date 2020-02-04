<%@ Page Title="" Language="C#" MasterPageFile="~/ServicesWithContent.Master" AutoEventWireup="true" CodeBehind="Grading.aspx.cs" Inherits="ProjectFlow.TutorDashboard.Grading" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="card card-body projectflow-card-shadow container">
        <div class="container-fluid">            
            <div class="row mb-3">
                <div class="col">
                    <h3>
                        <asp:Label ID="InfoLabel" runat="server" Font-Size="Medium"></asp:Label>
                    </h3>
                </div>
            </div>
            <br>
            <div class="row mb-3">                                           
                <div class="col-1">
                    <asp:Button ID="exportBtn" CssClass="btn btn-primary" runat="server" Text="Export" />
                </div>               
            </div>
            <br>
            <div class="row mb-3">
                <div class="col">
                    <div style="overflow-x: auto;">
                        <asp:GridView ID="MemberGV" CssClass="table table-bordered projectflow-table table-striped" runat="server" AutoGenerateColumns="False" Width="1056px" OnRowCancelingEdit="MemberGV_RowCancelingEdit" OnRowEditing="MemberGV_RowEditing" OnRowUpdating="MemberGV_RowUpdating" OnPageIndexChanging="MemberGV_PageIndexChanging" AllowPaging="True" PageSize="4">
                            <HeaderStyle CssClass="thead-light" />
                            <Columns>
                                <asp:BoundField HeaderText="ScoreID" />
                                <asp:BoundField HeaderText="Name" />
                                <asp:TemplateField HeaderText="Proposal"></asp:TemplateField>
                                <asp:TemplateField HeaderText="Report"></asp:TemplateField>
                                <asp:TemplateField HeaderText="1st Review"></asp:TemplateField>
                                <asp:TemplateField HeaderText="2nd Review"></asp:TemplateField>
                                <asp:TemplateField HeaderText="Presentation"></asp:TemplateField>
                                <asp:TemplateField HeaderText="test"></asp:TemplateField>
                                <asp:TemplateField HeaderText="sdl"></asp:TemplateField>
                                <asp:TemplateField HeaderText="partication"></asp:TemplateField>
                                <asp:TemplateField HeaderText="Total"></asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <div class="jumbotron jumbotron-fluid">
                                    <div class="container">
                                        <h1 class="display-4">Seem Empty, add some members now!</h1>
                                        <p>Members not showing?   
                                            <asp:HyperLink ID="emailLink" Text="click here!" NavigateUrl="mailto:projectflow.nyp.eadp@gmail.com" runat="server"></asp:HyperLink></p>
                                    </div>
                                </div>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <br>
            <div class="row mb-3">
                <div class="col">
                    <asp:Button ID="refreshBtn" CssClass="btn btn-primary" runat="server" Text="Refresh" OnClick="refreshBtn_Click" />
                </div>
            </div>
        </div>
    </div>   
</asp:Content>
