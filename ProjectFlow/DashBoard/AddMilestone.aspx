<%@ Page Title="" Language="C#" MasterPageFile="~/ServicesWithContent.Master" AutoEventWireup="true" CodeBehind="AddMilestone.aspx.cs" Inherits="ProjectFlow.DashBoard.AddMilestone" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="container">        
        <div class="row">
            <div class="col">                
                <asp:DropDownList ID="PageSelectDP" CssClass="form-control border border-dark" runat="server" AutoPostBack="True" OnSelectedIndexChanged="PageSelectDP_SelectedIndexChanged">
                    <asp:ListItem Value="0">Add Members</asp:ListItem>
                    <asp:ListItem Value="1">Add MileStone</asp:ListItem>
                </asp:DropDownList>                
            </div>
            <div class="col">                                            
                <asp:Button ID="createBtn" runat="server" Text="Add Milestone" CssClass="btn btn-primary" OnClientClick="myfunction(); return false;" UseSubmitBehavior="false"/>                
            </div>            
            <div class="col">
                <asp:Label ID="InfoLabel" runat="server"></asp:Label>
            </div>
        </div>
        <br>
        <div class="row">
            <div class="col">
                <div style="overflow-x: auto;">
                    <asp:GridView ID="MilestoneGV" CssClass="table table-bordered" runat="server" AutoGenerateColumns="False" Width="1056px">
                        <HeaderStyle CssClass="thead-light" />
                        <Columns>
                            <asp:BoundField DataField="milestoneID" HeaderText="ID" />
                            <asp:TemplateField HeaderText="Name">
                                <ItemTemplate>
                                    <asp:Label ID="nameLabel" runat="server" Text='<%# Bind("milestoneName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Start">
                                <ItemTemplate>
                                    <asp:Label ID="startLabel" runat="server" Text='<%# Bind("startDate", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="End">
                                <ItemTemplate>
                                    <asp:Label ID="endLabel" runat="server" Text='<%# Bind("endDate", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                           <div class="jumbotron jumbotron-fluid">
                                <div class="container">
                                    <h1 class="display-4">Seem Empty, add a milestone now!</h1>                                          
                                    <p>Milestone not showing?    <asp:HyperLink ID="emailLink" Text="click here!" NavigateUrl="mailto:projectflow.nyp.eadp@gmail.com" runat="server"></asp:HyperLink></p>
                                </div>
                           </div>
                       </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </div>
        </div>    
</asp:Content>
