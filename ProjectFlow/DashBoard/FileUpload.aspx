<%@ Page Title="" Language="C#" MasterPageFile="~/ServicesWithContent.Master" AutoEventWireup="true" CodeBehind="FileUpload.aspx.cs" Inherits="ProjectFlow.DashBoard.FileUpload" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style6 {
            width: 186px;
        }
        .auto-style7 {
            width: 122px;
        }
        .auto-style8 {
            margin-right: 13px;
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
                    <table class="auto-style1">
                        <tr>
                            <td class="auto-style6">
                                <asp:FileUpload ID="FileUploadControl" runat="server" />
                                <br />
                            </td>
                            <td class="auto-style7">&nbsp;

                                &nbsp;&nbsp;<br />
                                <br />

                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style6">
                                
                                <br />
                            </td>
                            <td class="auto-style7">&nbsp;

                                &nbsp;&nbsp;<br />
                                <asp:Button ID="UploadBtn" runat="server" OnClick="UploadBtn_Click" Text="Upload" />
                                <br />

                            </td>
                        </tr>
                    </table>
                </div>
                <div class="modal-footer">
                    <asp:Label ID="errorLabel" runat="server" ForeColor="Red"></asp:Label>
                </div>
            </div>
        </div>
    </div>
    <div class="container">        
        <div class="row">            
            <div class="col">                                            
                <asp:Button ID="openUploadModal" runat="server" Text="Upload File" CssClass="btn btn-primary" OnClientClick="myfunction(); return false;" UseSubmitBehavior="false" data-toggle="modal" data-target="#uploadModal"/>                
            </div>            
            <div class="col">
                <asp:Label ID="InfoLabel" runat="server"></asp:Label>
            </div>
        </div>
        <br>
        <div class="row">
            <div class="col">
                <div style="overflow-x: auto;">                   
                    <asp:GridView ID="FileGV" CssClass="table table-bordered" runat="server" AutoGenerateColumns="False" width ="1056px" AllowPaging="True" PageSize="4" OnSelectedIndexChanged="FileGV_SelectedIndexChanged">
                        <HeaderStyle CssClass="thead-light" />
                        <Columns>
                            <asp:BoundField DataField="Name" HeaderText="File" />
                            <asp:CommandField ButtonType="Button" SelectText="Download" ShowSelectButton="True">
                                <ControlStyle CssClass="btn btn-primary" />
                            </asp:CommandField>
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
     </div>
</asp:Content>
