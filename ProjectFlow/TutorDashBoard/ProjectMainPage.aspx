<%@ Page Title="" Language="C#" MasterPageFile="~/ServicesWithContent.Master" AutoEventWireup="true" CodeBehind="ProjectMainPage.aspx.cs" Inherits="ProjectFlow.ProjectMainPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div = "container">
        <div>
            <asp:Label ID="InfoLabel" runat="server"></asp:Label>
        </div>
        <br>
        <div class="row">
            <div class="col-lg-12">
                <div class="table-responsive">
                    <asp:GridView ID="MemberGV" CssClass="table table-hover table-bordered" runat="server" AutoGenerateColumns="False" Width="1056px">
                        <HeaderStyle CssClass="thead-light" />
                        <Columns>
                            <asp:BoundField DataField="memberID" HeaderText="Member ID" ReadOnly="True" />
                            <asp:BoundField DataField="studentID" HeaderText="Student ID" ReadOnly="True" />
                            <asp:TemplateField HeaderText="Role ID">
                                <ItemTemplate>
                                    <asp:Label ID="roleLabel" runat="server" Text='<%# Bind("roleID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>                                
    </div>
</asp:Content>
