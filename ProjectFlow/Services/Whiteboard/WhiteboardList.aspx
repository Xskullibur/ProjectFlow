<%@ Page Title="" Language="C#" MasterPageFile="~/ServicesWithContent.Master" AutoEventWireup="true" CodeBehind="WhiteboardList.aspx.cs" Inherits="ProjectFlow.Services.Whiteboard.WhiteboardList" EnableEventValidation="false"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <div class="card card-body projectflow-card-shadow pt-3 container">

        <div class="row mb-2">
            <div class="col text-right">
                <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#addBoardModal">New Board</button>
            </div>
        </div>

        <div class="row">
            <div class="col">

                <asp:UpdatePanel ID="WhiteboardUpdatePanel" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="sessionGrid" runat="server" CssClass="table table-bordered table-hover projectflow-table" 
                            AllowPaging="True" AutoGenerateColumns="False" OnPageIndexChanging="sessionGrid_PageIndexChanging" OnRowDataBound="sessionGrid_RowDataBound" OnSelectedIndexChanged="sessionGrid_SelectedIndexChanged">

                            <Columns>
                                <asp:TemplateField HeaderText="Session Name">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("groupName") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("groupName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Creation Date">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("creationDateTime") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("creationDateTime") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                    <div class="container text-center">
                                      <h2>No Whiteboard Session Found.</h2><br>
                                      <i class="fas fa-chalkboard-teacher fa-10x"></i>
                                    </div>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>
        </div>
    
    </div>

    <%--New Board Modal--%>
    <div class="modal fade" id="addBoardModal">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">New Whiteboard</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:Label CssClass="control-label" Text="Session Name:" AssociatedControlID="sessionNameTxt" runat="server" />
                    <asp:TextBox ID="sessionNameTxt" CssClass="form-control form-control-sm" runat="server" placeholder="Plan For Whiteboard Wireframe"></asp:TextBox>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    <asp:Button CssClass="btn btn-primary" Text="Create" runat="server" ID="createBtn" OnClick="createBtn_Click" />                    
                </div>
            </div>
        </div>
    </div>

</asp:Content>
