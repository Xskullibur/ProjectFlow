<%@ Page Title="" Language="C#" MasterPageFile="~/ServicesWithContent.Master" AutoEventWireup="true" CodeBehind="IssueSolutions.aspx.cs" Inherits="ProjectFlow.Issues.IssueSolutions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
   
    <div class="container py-2">
    
        <div class="row">
            <div class="col-sm-12 col-lg-8 p-3">
                <div class="card h-100">
                    
                    <div class="card-body">
                        <asp:Label ID="lbMember" runat="server" CssClass="card-title">
                            What is Lorem Ipsum?
                        </asp:Label>  
                        <asp:Label ID="lbIssue" runat="server" CssClass="card-text">
                            Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.
                            Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.
                        </asp:Label>
                    </div>

                </div>
            </div>
            <div class="col-lg-4 p-3">
                <div class="card">
                    <div class="card-body">
                        <h6 class="card-subtitle mb-2 text-muted">Issue Info:</h6>
                        <div class=""><i class="fa fa-check-square-o" aria-hidden="true">&nbsp;</i><label>Active:&nbsp;</label><asp:Label ID="IssueActive" runat="server" Text=""></asp:Label></div>
                        <div class=""><i class="fa fa-exclamation-circle" aria-hidden="true">&nbsp;</i><label>Status:&nbsp;</label><asp:Label ID="IssueStatus" runat="server" Text=""></asp:Label></div>
                        <div class=""><i class="fa fa-eye" aria-hidden="true">&nbsp;</i><label>Public:&nbsp;</label><asp:Label ID="IssuePublic" runat="server" Text=""></asp:Label></div>
                        <div class=""><i class="fa fa-smile-o" aria-hidden="true">&nbsp;</i><label>Raised by:&nbsp;</label><asp:Label ID="IssueRaisedBy" runat="server" Text=""></asp:Label></div>
                    </div>
                </div>
            </div>
        </div>


        <div class ="row justify-content-between">    
            <div class="col-xl-5 col-12 align-self-end">
                <div class="btn-group ">
                    <asp:Button ID="btnYes" CssClass ="btn btn-outline-secondary" runat="server" Text="&#128077;"/>
                    <asp:Button ID="btnYesCount" CssClass ="btn btn-outline-secondary" runat="server" Text="" enabled="false"/>
                </div>&nbsp;
                <div class="btn-group ">
                    <asp:Button ID="btnNo" CssClass ="btn btn-outline-secondary" runat="server" Text="&#128078;"/>
                    <asp:Button ID="btnNoCount" CssClass ="btn btn-outline-secondary" runat="server" Text="" enabled="false"/>
                </div>&nbsp;
                <asp:Button ID="btnRandom" CssClass ="btn btn-outline-secondary" runat="server" Text="&#127922;"/>
            </div>
            <div class="col-xl-3 col-12 text-right">
                <div class="dropdown">
                    <button type="button" class="btn btn-outline-primary dropdown-toggle" data-toggle="dropdown">
                        Edit
                    </button>

                    <div class="dropdown-menu">
                        <asp:Button Text="Edit Issue" CssClass="dropdown-item" runat="server" ID="updateIssueBtn"/>
                        <asp:Button Text="Drop Issue" CssClass="dropdown-item" runat="server" ID="dropIssueBtn"/>
                    </div>
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
                                <asp:Label CssClass="control-label" Text="Issue Name:" AssociatedControlID="tNameTxt" runat="server" />
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
                            
                            <%--Status--%>
                            <div class="form-group">
                                <asp:Label CssClass="control-label" Text="Status:" AssociatedControlID="IssueStatusDLL" runat="server" />
                                <asp:DropDownList ID="IssueStatusDLL" CssClass="form-control" runat="server"></asp:DropDownList>

                                <asp:Label ID="statusErrorLbl" CssClass="form-text text-danger" Font-Size="Small" runat="server" Text="" Visible="False"></asp:Label>
                                <asp:RequiredFieldValidator ID="statusRequiredValidator" CssClass="form-text text-danger" Font-Size="Small" runat="server" ErrorMessage="This Field is Required!" ControlToValidate="IssueStatusDLL" Display="Dynamic" ValidationGroup="AddTask" EnableClientScript="True"></asp:RequiredFieldValidator>
                            </div>

                            <%--Public--%>
                            <div class="form-group">
                                <asp:Label CssClass="control-label" Text="Public:" AssociatedControlID="cbPublic" runat="server" />
                                <asp:CheckBox ID="cbPublic" CssClass="form-control" runat="server" />

                                <asp:Label ID="checkBoxErrorLbl" CssClass="form-text text-danger" Font-Size="Small" runat="server" Text="" Visible="False"></asp:Label>
                                
                            </div>

                            <%--Conclusion--%>
                            <div class="form-group">
                                <asp:Label CssClass="control-label" Text="Conclusion:" AssociatedControlID="tConcText" runat="server" />
                                <asp:TextBox ID="tConcText" CssClass="form-control" runat="server" TextMode="MultiLine" ValidationGroup="AddTask" />

                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" CssClass="form-text text-danger" Font-Size="Small" runat="server" ErrorMessage="Maximum Length of 255 Characters!" ValidationExpression="^[\s\S]{1,255}$" Display="Dynamic" ControlToValidate="tConcText" ValidationGroup="AddTask"></asp:RegularExpressionValidator>
                                
                            </div>
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
</asp:Content>
