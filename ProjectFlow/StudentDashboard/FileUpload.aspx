<%@ Page Title="" Language="C#" MasterPageFile="~/ServicesWithContent.Master" AutoEventWireup="true" CodeBehind="FileUpload.aspx.cs" Inherits="ProjectFlow.DashBoard.FileUpload" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style6 {
            width: 186px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="modal fade" id="uploadModal" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <asp:Label ID="Label4" runat="server" Text="File Upload"></asp:Label>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                     <div class="container">        
                        <div class="row">            
                            <div class="col">
                                 <asp:FileUpload ID="FileUploadControl" runat="server" />
                            </div>
                            <div class="col">
                                 <asp:DropDownList ID="OptionDP" runat="server" CssClass="form-control border border-dark" AutoPostBack="True" OnSelectedIndexChanged="OptionDP_SelectedIndexChanged">
                                    <asp:ListItem>Encryption</asp:ListItem>
                                    <asp:ListItem>Custom Key</asp:ListItem>
                                    <asp:ListItem>No Encryption</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <br>
                        <div class="row">
                            <div class="col">
                                <asp:Button ID="GenKeyBtn" CssClass="btn btn-primary" runat="server" Text="Generate Key" OnClick="GenKeyBtn_Click" Visible="False"/>
                            </div>
                            <div class="col">
                                <asp:TextBox ID="KeyTB" CssClass="form-control" runat="server" Visible="False"></asp:TextBox>
                            </div>
                        </div>
                        <br>
                        <div class="row">
                            <div class="col">
                            </div>
                            <div class="col">
                                <asp:Button ID="Button1" CssClass="btn btn-success" runat="server" OnClick="UploadBtn_Click" Text="Upload" />
                            </div>                           
                        </div>
                    </div>                   
                </div>
                <div class="modal-footer">
                    <asp:Label ID="errorLabel" runat="server" ForeColor="Red"></asp:Label>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="decryptionModal" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <asp:Label ID="Label1" runat="server" Text="Key Decryption"></asp:Label>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                     <div class="container">        
                        <div class="row">            
                            <div class="col">
                                 <asp:TextBox ID="deKeyTB" CssClass="form-control" runat="server" Visible="True"></asp:TextBox>
                            </div>
                            <div class="col">
                                 <asp:Button ID="keyDownloadBtn" CssClass="btn btn-primary" runat="server" Text="Download" OnClick="keyDownloadBtn_Click" />
                            </div>
                        </div>
                        <br>                                                                      
                    </div>                   
                </div>           
            </div>
        </div>
    </div>
    <div class="card card-body projectflow-card-shadow container">
        <div class="container">           
            <div class="row mb-3 mt-3">
                <div class="col">
                    <asp:Button ID="openUploadModal" runat="server" Text="Upload File" CssClass="btn btn-primary" OnClientClick="myfunction(); return false;" UseSubmitBehavior="false" data-toggle="modal" data-target="#uploadModal" />
                </div>
            </div>
            <br>
            <div class="row mb-3">
                <div class="col-3">
                    <asp:TextBox ID="SearchTB" CssClass="form-control" placeholder="File Name" runat="server"></asp:TextBox>
                </div>
                <div class="col-1">
                    <asp:Button ID="searchBtn" CssClass="btn btn-primary" runat="server" Text="Search" OnClick="searchBtn_Click" />
                </div>
                <div class="col-1">
                    <asp:Button ID="showAllBtn" runat="server" CssClass="btn btn-primary" OnClick="showAllBtn_Click" Text="Show All" />
                </div>
            </div>
            <br>
            <div class="row mb-3">
                <div class="col">
                    <div style="overflow-x: auto;">
                        <asp:GridView ID="FileGV" CssClass="table table-bordered projectflow-table table-striped" runat="server" AutoGenerateColumns="False" Width="1056px" AllowPaging="True" PageSize="4" OnSelectedIndexChanged="FileGV_SelectedIndexChanged" OnRowDeleting="FileGV_RowDeleting" OnPageIndexChanging="FileGV_PageIndexChanging" OnRowCancelingEdit="FileGV_RowCancelingEdit" OnRowEditing="FileGV_RowEditing" OnRowUpdating="FileGV_RowUpdating">
                            <HeaderStyle CssClass="thead-light" />
                            <Columns>
                                
                                <asp:TemplateField HeaderText="File">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="editNameTB" CssClass="form-control" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="nameLabel" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="Size" HeaderText="Size" ReadOnly="true"/>
                                <asp:BoundField DataField="Status" HeaderText="Status" ReadOnly="true"/>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>                                      
                                        <%# ShowIcon((string)Eval("Status")) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Date" HeaderText="Date" ReadOnly="true"/>                                                              
                                <asp:CommandField ButtonType="Button" SelectText="Download" ShowSelectButton="True">
                                    <ControlStyle CssClass="btn btn-primary" />
                                </asp:CommandField>

                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button Text="Edit" CssClass="btn btn-primary" CommandName="Edit" runat="server" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Button ID="btnUpdate" CssClass="btn btn-sm btn-primary mb-2" runat="server" CommandName="Update" Text="Update" ValidationGroup="tableValidation" />
                                        <br>
                                        </br>
					                <asp:Button ID="btnCancel" CssClass="btn btn-sm btn-outline-danger" runat="server" CommandName="Cancel" Text="Cancel" />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="DeleteButton" Text="Delete" runat="server"
                                            CssClass="btn btn-danger"
                                            data-toggle="confirmation"
                                            data-btn-ok-icon-class="fa fa-check"
                                            data-btn-cancel-icon-class="fa fa-close"
                                            data-popout="true"
                                            CommandName="Delete" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <div class="jumbotron jumbotron-fluid">
                                    <div class="container">
                                        <h1 class="display-4">Storage is Empty</h1>
                                        <p>Files not showing?   
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
