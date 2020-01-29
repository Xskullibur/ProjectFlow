﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ServicesWithContent.Master" AutoEventWireup="true" CodeBehind="ManageMembers.aspx.cs" Inherits="ProjectFlow.StudentDashboard.ManageMembers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="container">
        <div class="row">
            <div class="col">
                <h1>
                    <asp:Label ID="Label3" runat="server" Font-Size="XX-Large" Text="Bros in my team"></asp:Label>
                </h1>
            </div>            
        </div>        
        <br>
        <div class="row">
            <div class="col-3">
                
                <asp:Label ID="yourStatus" runat="server"></asp:Label>
                
            </div>
            <div class="col-1">
                
            </div> 
            <div class="col-1">
                
            </div>
        </div>
        <br>
        <div class="row">
            <div class="col">
                <div style="overflow-x: auto;">
                    <asp:GridView ID="MemberGV" CssClass="table table-bordered" runat="server" AutoGenerateColumns="False" Width="1056px" AllowPaging="True" PageSize="4" OnSelectedIndexChanged="MemberGV_SelectedIndexChanged">
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
                                    <asp:Label ID="nameLabel" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Student.firstName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="Role">              
                                <ItemTemplate>
                                    <asp:Label ID="roleLabel" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Role.role1") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>                     
                                    <asp:Button ID="deleteBtn" CssClass="btn btn-danger" runat="server" CausesValidation="False" CommandName="Delete" Text="Kick" OnClientClick="return confirm('Are you sure to delete member?');"></asp:Button>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                           <div class="jumbotron jumbotron-fluid">
                                <div class="container">
                                    <h1 class="display-4">Seem Empty, add some members now!</h1>                                          
                                    <p>Members not showing?    <asp:HyperLink ID="emailLink" Text="click here!" NavigateUrl="mailto:projectflow.nyp.eadp@gmail.com" runat="server"></asp:HyperLink></p>
                                </div>
                           </div>
                       </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </div>
        </div>
        <br>    
</asp:Content>
