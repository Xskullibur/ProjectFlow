<%@ Page Title="" Language="C#" MasterPageFile="~/ServicesWithContent.Master" AutoEventWireup="true" CodeBehind="ManageMembers.aspx.cs" Inherits="ProjectFlow.StudentDashboard.ManageMembers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="card card-body projectflow-card-shadow container">
        <div class="container">            
            <div class="row mb-3 mt-3">
                <div class="col-2">
                    <asp:Label ID="yourStatus" runat="server"></asp:Label>
                </div>
                <div class="col-2">
                     <asp:Label ID="lockStatus" runat="server"></asp:Label>
                </div>
                <div class="col-2">
                    <asp:Button ID="leaderBtn" runat="server" CssClass="btn btn-primary" Text="Become Leader" OnClick="leaderBtn_Click" data-toggle="confirmation"
                        data-btn-ok-icon-class="fa fa-check"
                        data-btn-cancel-icon-class="fa fa-close"
                        data-popout="true" />
                </div>
                <div class="col-2">
                    <asp:Button ID="STbtn" runat="server" CssClass="btn btn-primary" OnClick="STbtn_Click" Text="Step Down" data-toggle="confirmation"
                        data-btn-ok-icon-class="fa fa-check"
                        data-btn-cancel-icon-class="fa fa-close"
                        data-popout="true" />
                </div>
                <div class="col-1">
                    <asp:LinkButton ID="lockBtn" runat="server" CssClass="btn btn-primary" OnClick="lockBtn_Click">
                        <i style="color: white;" class="fas fa-lg fa-lock"></i>
                    </asp:LinkButton>
                </div>
                <div class="col-1">
                    <asp:LinkButton ID="unlockBtn" runat="server" CssClass="btn btn-primary" OnClick="unlockBtn_Click">
                        <i style="color: white;" class="fas fa-lg fa-unlock"></i>
                    </asp:LinkButton>
                </div>
                <div class="col-2">
                    <asp:Button ID="leaveBtn" runat="server" CssClass="btn btn-danger" Text="Leave" data-toggle="confirmation"
                        data-btn-ok-icon-class="fa fa-check"
                        data-btn-cancel-icon-class="fa fa-close"
                        data-popout="true" OnClick="leaveBtn_Click" />
                </div>
            </div>
            <br>
            <div class="row mb-3">
                <div class="col">
                    <div style="overflow-x: auto;">
                        <asp:GridView ID="MemberGV" CssClass="table table-bordered projectflow-table table-striped" runat="server" AutoGenerateColumns="False" Width="1056px" AllowPaging="True" PageSize="4" OnSelectedIndexChanged="MemberGV_SelectedIndexChanged" OnPageIndexChanging="MemberGV_PageIndexChanging">
                            <HeaderStyle CssClass="thead-light" />
                            <Columns>
                                <asp:BoundField DataField="memberID" HeaderText="Member ID" ReadOnly="True" />
                                <asp:TemplateField HeaderText="Student ID">
                                    <ItemTemplate>
                                        <asp:Label ID="idLabel" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Student.studentID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate>
                                        <asp:Label ID="nameLabel" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Student.firstName") + " " + DataBinder.Eval(Container.DataItem,"Student.lastName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Role">
                                    <ItemTemplate>
                                        <asp:Label ID="roleLabel" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Role.role1") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="deleteBtn" Text="Kick" runat="server"
                                        CssClass="btn btn-danger"
                                        data-toggle="confirmation"
                                        data-btn-ok-icon-class="fa fa-check"
                                        data-btn-cancel-icon-class="fa fa-close"
                                        data-popout="true"
                                        CommandName="Select" />
                                </ItemTemplate>
                            </asp:TemplateField>
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
        </div>
    </div>
    <div class="card card-body projectflow-card-shadow container mt-3">
        <div class="container">
            <div class="row mb-3 mt-3">
                <div class="col-1">                    
                     <asp:Label ID="justLabel" runat="server">Team Name:</asp:Label>                   
                </div>
                <div class="col-3">
                    <asp:TextBox ID="NameTB" CssClass="form-control" placeholder="Required, max 255" runat="server"></asp:TextBox>
                </div>
                <div class="col-1">
                    <asp:Button ID="changeButton" CssClass="btn btn-primary" runat="server" Text="Change" OnClick="changeButton_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
