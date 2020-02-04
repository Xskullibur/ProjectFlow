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
                        <asp:GridView ID="gradeGV" CssClass="table table-bordered projectflow-table table-striped" runat="server" AutoGenerateColumns="False" Width="1056px" OnRowEditing="gradeGV_RowEditing" OnRowUpdating="gradeGV_RowUpdating" AllowPaging="True" PageSize="4" OnSelectedIndexChanging="gradeGV_SelectedIndexChanging" OnRowCancelingEdit="gradeGV_RowCancelingEdit">
                            <HeaderStyle CssClass="thead-light" />
                            <Columns>
                                <asp:TemplateField HeaderText="ID">
                                    <ItemTemplate>
                                        <asp:Label ID="ProposalLabel" runat="server" Text='<%# Bind("scoreID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate>
                                        <asp:Label ID="nameLabel" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Student.firstName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Proposal(10%)">
                                    <ItemTemplate>
                                        <asp:Label ID="ProposalLabel" runat="server" Text='<%# Bind("proposal") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="editProposalTB" CssClass="form-control" runat="server" Text='<%# Bind("proposal") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Report(10%)">
                                    <ItemTemplate>
                                        <asp:Label ID="reportLabel" runat="server" Text='<%# Bind("report") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="editReportTB" CssClass="form-control" runat="server" Text='<%# Bind("report") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="1st Review(5%)">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="editReviewOneTB" CssClass="form-control" runat="server" Text='<%# Bind("reviewOne") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="reviewoneLabel" runat="server" Text='<%# Bind("reviewOne") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="2nd Review(15%)">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="editReviewTwoTB" CssClass="form-control" runat="server" Text='<%# Bind("reviewTwo") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                     <ItemTemplate>
                                        <asp:Label ID="reviewtwoLabel" runat="server" Text='<%# Bind("reviewTwo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Presentation(20%)">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="editPreTB" CssClass="form-control" runat="server" Text='<%# Bind("presentation") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="presentationLabel" runat="server" Text='<%# Bind("presentation") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Test(20%)">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="editTestTB" CssClass="form-control" runat="server" Text='<%# Bind("test") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="testLabel" runat="server" Text='<%# Bind("test") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="SDL(10%)">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="editsdlTB" CssClass="form-control" runat="server" Text='<%# Bind("sdl") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="sdlLabel" runat="server" Text='<%# Bind("sdl") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Participation(10%)">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="editPartTB" CssClass="form-control" runat="server" Text='<%# Bind("participation") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="particationLabel" runat="server" Text='<%# Bind("participation") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Total">

                                </asp:TemplateField>

                                <asp:CommandField ButtonType="Button" ShowEditButton="True">
                                    <ControlStyle CssClass="btn btn-warning" />
                                </asp:CommandField>
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
                    <asp:Button ID="refreshBtn" CssClass="btn btn-primary" runat="server" Text="Refresh" OnClick="refreshBtn_Click"/>
                </div>
            </div>
        </div>
    </div>   
</asp:Content>
