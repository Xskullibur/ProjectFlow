<%@ Page Title="" Language="C#" MasterPageFile="~/ServicesWithContent.Master" AutoEventWireup="true" CodeBehind="IssueRes.aspx.cs" Inherits="ProjectFlow.Issues.IssueRes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <%-- <asp:ScriptManager ID="ScriptManager1" runat="server" /> --%>
    
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
    <div class="container py-2">
    
        <div class="row">
                <div class="col-sm-12 col-lg-8 p-3">
                    <div class="card h-100">
                    
                                <div class="card-body">
                                    <asp:Label ID="lbMember" runat="server"></asp:Label>  
                                    <asp:Label ID="lbIssue" runat="server"></asp:Label>
                                </div>

                    </div>
                </div>
                <div class="col-lg-4 p-3">
                    <div class="card">
                        <div class="card-body">
                            <h6 class="card-subtitle mb-2 text-muted">Issue Info:</h6>
                            <div class=""><i class="fa fa-check-square-o" aria-hidden="true">&nbsp;</i><label>Active:&nbsp;</label><asp:Label ID="IssueActive" runat="server" Text=""></asp:Label></div>
                            <div class=""><i class="fa fa-exclamation-circle" aria-hidden="true">&nbsp;</i><label>Status:&nbsp;</label><asp:Label ID="IssueStatus" runat="server" Text=""></asp:Label></div>
                            <div class=""><i class="fa fa-smile-o" aria-hidden="true">&nbsp;</i><label>Raised by:&nbsp;</label><asp:Label ID="IssueRaisedBy" runat="server" Text=""></asp:Label></div>
                        </div>
                    </div>
                </div>
        </div>

        <hr/>
        <div class="card card-body projectflow-card-shadow row py-3 px-0">
            <div class="col-12">
                <nav>
                    <div class="nav nav-tabs" id="nav-tab" role="tablist">
                    <a class="nav-item nav-link active" id="nav-meeting-logger-table-tab" data-toggle="tab" href="#nav-comments" role="tab" aria-controls="nav-meeting-logger-table" aria-selected="true">Comments</a>
                    <a class="nav-item nav-link" id="nav-christina-tab" data-toggle="tab" href="#nav-conclusion" role="tab" aria-controls="nav-christina" aria-selected="false">
                        Solution
                    </a>
                    </div>
                </nav>
                <div class="tab-content" id="nav-tabContent">
                    <div class="tab-pane fade show active" id="nav-comments" role="tabpanel" aria-labelledby="nav-comments">
                        <div class="row pd-2">
                            <div class="col-12">

                                        <br>
                                        <div class="">
                                            <asp:TextBox ID="tbComments" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                
                                            <div class="text-right">
                                            <asp:Button ID="btnComment" CssClass="btn btn-primary px-4 mt-3" runat="server" Text="Post" OnClick="btnCommentSubmit_Click"/>
                                            </div>
                                        </div>
                                        <div class ="col">
                                            <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater_ItemDataBound">
                                                <HeaderTemplate>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <br>
                                                    </br>
                                                    <div class="mdc-card">
                                                        <div class="card-body">
                                                            <asp:Label ID="lbCreatedBy" CssClass="card-title" runat="server" Text='<%# Eval("CreatedBy") %>' ForeColor="#0066FF"></asp:Label>
                                                            <asp:Label ID="lbComment" CssClass="card-text" runat="server" Text='<%# Eval("Comment") %>'></asp:Label>
                                                        </div>
                                                    </div>
                                  
                                                </ItemTemplate> 
                                                <FooterTemplate>
                                                    <%-- Label used for showing Error Message --%>
                                                    <asp:Label ID="lblErrorMsg" runat="server" CssClass="align-middle font-weight-light" Text="There are no comments to show" Visible="false">
                                                    </asp:Label>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </div>
                            </div>
                        </div>
                    </div>
                    <div class="tab-pane fade" id="nav-conclusion" role="tabpanel" aria-labelledby="nav-conclusion">
                        <br />
                        <div class="mb-2">
                            <asp:Button Text="Add solution" CssClass="btn btn-outline-primary dropdown-toggle" runat="server" ID="btnAddSolution" OnClick="showTaskModal_Click"/>
                        </div>
                       
                        <div>
                            <asp:GridView ID="solutionView" runat="server" CssClass="table table-bordered table-hover table-striped projectflow-table" AutoGenerateColumns="False" OnSelectedIndexChanged="showSolution_click" OnRowDataBound="OnRowDataBound">
                                <HeaderStyle CssClass="thead-light" />   
                                <Columns>
                                    <asp:BoundField DataField="solutionId" HeaderText="Solution Id" />
                                    <asp:BoundField DataField="Title" HeaderText="Solution Name" />
                                    <asp:BoundField DataField="CreatedBy" HeaderText="Created by" />
                                    <asp:BoundField DataField="privacy" HeaderText="Privacy" />
                                    <asp:CommandField ShowSelectButton="True" ButtonType="Button">
                                        <ControlStyle CssClass="btn btn-success mb-2" />
                                    </asp:CommandField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <div class="jumbotron jumbotron-fluid">
                                        <div class="container">
                                            <h1 class="display-4">Freedoommm!</h1>
                                            <p class="load">No Solutions Found!</p>
                                            <hr class="my-4" />
                                            <p>If Solutions are expected but not shown please contact us <asp:HyperLink ID="emailLink" Text="here" NavigateUrl="mailto:projectflow.nyp.eadp@gmail.com" runat="server"></asp:HyperLink>.</p>
                                        </div>
                                    </div>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </div>

                    </div>
                </div>
            </div>
        </div>

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

                                <asp:RegularExpressionValidator ID="tNameRegexValidator" CssClass="form-text text-danger" Font-Size="Small" runat="server" ErrorMessage="Maximum Length of 50 Characters!" ValidationExpression="^[\s\S]{1,50}$" Display="Dynamic" ControlToValidate="tNameTxt" ValidationGroup="AddTask"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="tNameRequiredValidator" CssClass="form-text text-danger" Font-Size="Small" runat="server" ErrorMessage="Issue Name Field is Required!" ControlToValidate="tNameTxt" Display="Dynamic" ValidationGroup="AddTask" EnableClientScript="True"></asp:RequiredFieldValidator>
                            </div>

                            <%--Description--%>
                            <div class="form-group">
                                <asp:Label CssClass="control-label" Text="Description:" AssociatedControlID="tDescTxt" runat="server" />
                                <asp:TextBox ID="tDescTxt" CssClass="form-control" runat="server" TextMode="MultiLine" ValidationGroup="AddTask" />

                                <asp:RegularExpressionValidator ID="tDescRegexValidator" CssClass="form-text text-danger" Font-Size="Small" runat="server" ErrorMessage="Maximum Length of 500 Characters!" ValidationExpression="^[\s\S]{1,500}$" Display="Dynamic" ControlToValidate="tDescTxt" ValidationGroup="AddTask"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="tDescRequiredValidator" CssClass="form-text text-danger" Font-Size="Small" runat="server" ErrorMessage="Description Field is Required!" ControlToValidate="tDescTxt" Display="Dynamic" ValidationGroup="AddTask" EnableClientScript="True"></asp:RequiredFieldValidator>
                            </div>
                            
                            <%--Public--%>
                            <div>
                                <asp:Label CssClass="control-label" Text="Public:" AssociatedControlID="cbPublic" runat="server" />
                                <asp:CheckBox ID="cbPublic" CssClass="form-control" runat="server" />

                                <asp:Label ID="checkBoxErrorLbl" CssClass="form-text text-danger" Font-Size="Small" runat="server" Text="" Visible="False"></asp:Label>
                                
                            </div>

                        </div>

                        <%--Error Summary--%>
                        <asp:ValidationSummary ID="addTaskSummaryValidator" CssClass="form-text text-danger" Font-Size="Small" runat="server" ValidationGroup="AddTask" ShowMessageBox="True" ShowSummary="False" />

                    </div>

                    <%--Footer--%>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                        <asp:Button id="tSaveBtn" CssClass="btn btn-primary" Text="Save" runat="server" ValidationGroup="AddTask" OnClientClick="$('#taskModal1').modal('hide'); return true;"  OnClick="addTask_Click" AutoPostBack="true"/>
                    </div>
                </div>

            </div>
        </div>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>