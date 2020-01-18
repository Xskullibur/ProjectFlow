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
    <div class="container"> 
        <div class="row">
            <div class="col"></div>
            <div class="col">
                <h1>
                    <asp:Label ID="Label5" class="label label-primary" runat="server" Text="File Sharing"></asp:Label>
                </h1>
            </div>
            <div class="col"></div>
        </div>
        <br>
        <div class="row">            
            <div class="col">                                            
                <asp:Button ID="openUploadModal" runat="server" Text="Upload File" CssClass="btn btn-primary" OnClientClick="myfunction(); return false;" UseSubmitBehavior="false" data-toggle="modal" data-target="#uploadModal"/>                
            </div>                        
        </div>
        <br>
        <div class="row">
            <div class="col">
                <div style="overflow-x: auto;">                   
                    <asp:GridView ID="FileGV" CssClass="table table-bordered" runat="server" AutoGenerateColumns="False" width ="1056px" AllowPaging="True" PageSize="4" OnSelectedIndexChanged="FileGV_SelectedIndexChanged" OnRowDeleting="FileGV_RowDeleting">
                        <HeaderStyle CssClass="thead-light" />
                        <Columns>
                            <asp:BoundField DataField="Name" HeaderText="File" />
                            <asp:BoundField DataField="Size" HeaderText="Size" />
                            <asp:BoundField DataField="Status" HeaderText="Status" />
                            <asp:BoundField DataField="Date" HeaderText="Date" />
                            <asp:TemplateField HeaderText="Key">
                                <ItemTemplate>
                                    <asp:TextBox ID="tableKeyTB" CssClass="form-control" runat="server" Visible="True"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ButtonType="Button" SelectText="Download" ShowSelectButton="True">
                                <ControlStyle CssClass="btn btn-primary" />
                            </asp:CommandField>       
                            <asp:TemplateField>
                                <ItemTemplate>                     
                                    <asp:Button ID="deleteBtn" CssClass="btn btn-danger" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete" OnClientClick="return confirm('Are you sure to delete file?');"></asp:Button>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                           <div class="jumbotron jumbotron-fluid">
                                <div class="container">
                                    <h1 class="display-4">Storage is Empty</h1>                                          
                                    <p>Files not showing?    <asp:HyperLink ID="emailLink" Text="click here!" NavigateUrl="mailto:projectflow.nyp.eadp@gmail.com" runat="server"></asp:HyperLink></p>
                                </div>
                           </div>
                       </EmptyDataTemplate>
                    </asp:GridView>                   
                </div>
            </div>
        </div>
        <br>
        <div class="row">
            <div class="col">
                <asp:Button ID="refreshBtn" CssClass="btn btn-primary" runat="server" Text="Refresh" OnClick="refreshBtn_Click" />
            </div>
        </div>
     </div>
</asp:Content>
