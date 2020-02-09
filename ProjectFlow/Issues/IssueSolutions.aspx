<%@ Page Title="" Language="C#" MasterPageFile="~/ServicesWithContent.Master" AutoEventWireup="true" CodeBehind="IssueSolutions.aspx.cs" Inherits="ProjectFlow.Issues.IssueSolutions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="container py-2">
    
        <div class="row">
            <div class="col-sm-12 col-lg-12 p-3">
                <div class="card h-100">
                    
                    <div class="card-body">
                        <asp:Button ID="BackBtn" CssClass="float-right btn-sm btn-primary" runat="server" Text="Back" OnClick="GoBackEvent" />
                        <asp:Label ID="lbSolutionInfo" runat="server" CssClass="card-title"></asp:Label> 
                        <hr/>
                        <asp:Label ID="lbSolutionTitle" runat="server" CssClass="card-title"></asp:Label>  
                        <asp:Label ID="lbSolutionDesc" runat="server" CssClass="card-text"></asp:Label>

                        <%--Repuposed Filter Panel--%>

                        <asp:Panel ID="currentFiltersPanel" CssClass="d-flex flex-wrap" runat="server">
                            <asp:Button ID="Button1" runat="server" CssClass="btn btn-danger my-4 mr-1" OnClick="download_Click" Enabled="False" Visible="False" />
                        </asp:Panel>
                            
                    </div>

                </div>
            </div>

        </div>


        <div class ="row justify-content-between">    
            <div class="col-xl-5 col-12 align-self-end">
                <div class="btn-group ">
                    <asp:Button ID="btnYes" CssClass ="btn btn-outline-secondary" runat="server" Text="&#128077;" OnClick="btnYes_Click"/>
                    <asp:Button ID="btnYesCount" CssClass ="btn btn-outline-secondary" runat="server" Text="" enabled="false" />
                </div>&nbsp;
                <div class="btn-group ">
                    <asp:Button ID="btnNo" CssClass ="btn btn-outline-secondary" runat="server" Text="&#128078;" OnClick="btnNo_Click" />
                    <asp:Button ID="btnNoCount" CssClass ="btn btn-outline-secondary" runat="server" Text="" enabled="false"/>
                </div>&nbsp;
                <asp:Button ID="btnRandom" CssClass ="btn btn-outline-secondary" runat="server" Text="&#127922;" OnClick="btnRandom_Click" />
            </div>
            <div class="col-xl-3 col-12 text-right">
                <div class="dropdown">
                        <asp:Button Text="Delete" CssClass="btn btn-outline-danger" runat="server" ID="deleteSolutionBtn" OnClick="solutionDelete" Enabled="False" OnClientClick="return confirm('Are you sure to delete?');" Visible="False"/>
                </div>
            </div>
        </div>

        <hr/>

    </div>
    
    <div class="container">
        <!-- Add Modal -->
        <div class="modal fade" id="taskModal1" tabindex="-1" role="dialog" aria-labelledby="taskModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-md" role="document">

                <div class="modal-content">

                    <%--Main Content--%>
                    <div class="modal-header">
                        <asp:Label ID="tTitleLbl" CssClass="modal-title" Text="Add Issue" runat="server" />
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>

                    <%--Body--%>
                    <div class="modal-body">
                            
                        <div class="container-fluid" runat="server">

                            <%--Name--%>
                            <div class="form-group">
                                <asp:Label CssClass="control-label" Text="Title:" AssociatedControlID="tNameTxt" runat="server" />
                                <asp:TextBox runat="server" CssClass="form-control" ID="tNameTxt" TextMode="SingleLine" ValidationGroup="AddTask" />

                                <asp:RegularExpressionValidator ID="tNameRegexValidator" CssClass="form-text text-danger" Font-Size="Small" runat="server" ErrorMessage="Maximum Length of 255 Characters!" ValidationExpression="^[\s\S]{1,255}$" Display="Dynamic" ControlToValidate="tNameTxt" ValidationGroup="AddTask"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="tNameRequiredValidator" CssClass="form-text text-danger" Font-Size="Small" runat="server" ErrorMessage="Issue Name Field is Required!" ControlToValidate="tNameTxt" Display="Dynamic" ValidationGroup="AddTask" EnableClientScript="True"></asp:RequiredFieldValidator>
                            </div>

                            <%--Description--%>
                            <div class="form-group">
                                <asp:Label CssClass="control-label" Text="Description:" AssociatedControlID="tDescTxt" runat="server" />
                                <asp:TextBox ID="tDescTxt" CssClass="form-control" runat="server" TextMode="MultiLine" ValidationGroup="AddTask" />

                                <asp:RegularExpressionValidator ID="tDescRegexValidator" CssClass="form-text text-danger" Font-Size="Small" runat="server" ErrorMessage="Maximum Length of 255 Characters!" ValidationExpression="^[\s\S]{1,255}$" Display="Dynamic" ControlToValidate="tDescTxt" ValidationGroup="AddTask"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="tDescRequiredValidator" CssClass="form-text text-danger" Font-Size="Small" runat="server" ErrorMessage="Description Field is Required!" ControlToValidate="tDescTxt" Display="Dynamic" ValidationGroup="AddTask" EnableClientScript="True"></asp:RequiredFieldValidator>
                            </div>
                            


                            <%--Error Summary--%>
                            <asp:ValidationSummary ID="addTaskSummaryValidator" CssClass="form-text text-danger" Font-Size="Small" runat="server" ValidationGroup="AddTask" ShowMessageBox="True" ShowSummary="False" />

                        </div>

                        <%--Footer--%>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                            <asp:Button id="tSaveBtn" CssClass="btn btn-primary" Text="Save" runat="server" ValidationGroup="AddTask" OnClientClick="$('#taskModal1').modal('hide'); return true;" AutoPostBack="true"/>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>
