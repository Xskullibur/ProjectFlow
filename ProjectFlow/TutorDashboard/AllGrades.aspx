<%@ Page Title="" Language="C#" MasterPageFile="~/ServicesWithContent.Master" AutoEventWireup="true" CodeBehind="AllGrades.aspx.cs" Inherits="ProjectFlow.TutorDashboard.AllGrades" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="card card-body projectflow-card-shadow container">
        <div class="container-fluid">            
            <div class="row mb-3">
                <div class="col">
                    <h3>
                        <asp:Label ID="InfoLabel" runat="server" Font-Size="Medium"></asp:Label>
                    </h3>
                </div>
            </div>
            <br>
            <div class="row mb-3">                     
                <div class="col-1">
                    <asp:Button ID="exportBtn" CssClass="btn btn-primary" runat="server" Text="Export" OnClick="exportBtn_Click" />
                </div>               
            </div>
            <br>
            <div class="row mb-3">
                <div class="col">
                    <div style="overflow-x: auto;">
                        <asp:GridView ID="gradeGV" CssClass="table table-bordered projectflow-table table-striped" runat="server" AutoGenerateColumns="False" Width="1056px" OnRowEditing="gradeGV_RowEditing" OnRowUpdating="gradeGV_RowUpdating" AllowPaging="True" PageSize="4" OnSelectedIndexChanging="gradeGV_SelectedIndexChanging" OnRowCancelingEdit="gradeGV_RowCancelingEdit">
                            <HeaderStyle CssClass="thead-light" />
                            <Columns>
                                <asp:TemplateField HeaderText="ID">
                                    <ItemTemplate>
                                        <asp:Label ID="scoreLabel" runat="server" Text='<%# Bind("scoreID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate>
                                        <asp:Label ID="sidLabel" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Student.studentID")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate>
                                        <asp:Label ID="nameLabel" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Student.firstName") + " " + DataBinder.Eval(Container.DataItem,"Student.lastName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Proposal (5%)">
                                    <ItemTemplate>
                                        <asp:Label ID="ProposalLabel" runat="server" Text='<%# Bind("proposal") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="editProposalTB" CssClass="form-control" runat="server" Text='<%# Bind("proposal") %>'></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="editProposalRegexValidator" runat="server" ValidationGroup="studentScoreValidation" validationexpression="\b(0?[0-5])\b" ControlToValidate="editProposalTB" ErrorMessage="0-5 only!" Font-Size="Small" ForeColor="Red"></asp:RegularExpressionValidator>
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Report (5%)">
                                    <ItemTemplate>
                                        <asp:Label ID="reportLabel" runat="server" Text='<%# Bind("report") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="editReportTB" CssClass="form-control" runat="server" Text='<%# Bind("report") %>'></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="editReportRegexValidator" runat="server" ValidationGroup="studentScoreValidation" validationexpression="\b(0?[0-5])\b" ControlToValidate="editReportTB" ErrorMessage="0-5 only!" Font-Size="Small" ForeColor="Red"></asp:RegularExpressionValidator>
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="1st Review (5%)">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="editReviewOneTB" CssClass="form-control" runat="server" Text='<%# Bind("reviewOne") %>'></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="editReviewOneRegexValidator" runat="server" ValidationGroup="studentScoreValidation" validationexpression="\b(0?[0-5])\b" ControlToValidate="editReviewOneTB" ErrorMessage="0-5 only!" Font-Size="Small" ForeColor="Red"></asp:RegularExpressionValidator>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="reviewoneLabel" runat="server" Text='<%# Bind("reviewOne") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="2nd Review (15%)">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="editReviewTwoTB" CssClass="form-control" runat="server" Text='<%# Bind("reviewTwo") %>'></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="editReviewTwoRegexValidator" runat="server" ValidationGroup="studentScoreValidation" validationexpression="\b(0?[0-9]|1[0-5])\b" ControlToValidate="editReviewTwoTB" ErrorMessage="0-15 only!" Font-Size="Small" ForeColor="Red"></asp:RegularExpressionValidator>
                                    </EditItemTemplate>
                                     <ItemTemplate>
                                        <asp:Label ID="reviewtwoLabel" runat="server" Text='<%# Bind("reviewTwo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Presentation (15%)">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="editPreTB" CssClass="form-control" runat="server" Text='<%# Bind("presentation") %>'></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="editPreTBTwoRegexValidator" runat="server" ValidationGroup="studentScoreValidation" validationexpression="\b(0?[0-9]|1[0-5])\b" ControlToValidate="editPreTB" ErrorMessage="0-15 only!" Font-Size="Small" ForeColor="Red"></asp:RegularExpressionValidator>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="presentationLabel" runat="server" Text='<%# Bind("presentation") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Test (20%)">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="editTestTB" CssClass="form-control" runat="server" Text='<%# Bind("test") %>'></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="editTestTBTwoRegexValidator" runat="server" ValidationGroup="studentScoreValidation" validationexpression="\b(0?[0-9]|1[0-9]|2[0-0])\b" ControlToValidate="editTestTB" ErrorMessage="0-20 only!" Font-Size="Small" ForeColor="Red"></asp:RegularExpressionValidator>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="testLabel" runat="server" Text='<%# Bind("test") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="SDL (10%)">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="editsdlTB" CssClass="form-control" runat="server" Text='<%# Bind("sdl") %>'></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="editsdlTBTwoRegexValidator" runat="server" ValidationGroup="studentScoreValidation" validationexpression="\b(0?[0-9]|1[0-0])\b" ControlToValidate="editsdlTB" ErrorMessage="0-10 only!" Font-Size="Small" ForeColor="Red"></asp:RegularExpressionValidator>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="sdlLabel" runat="server" Text='<%# Bind("sdl") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Participation (10%)">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="editPartTB" CssClass="form-control" runat="server" Text='<%# Bind("participation") %>'></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="editPartRegexValidator" runat="server" ValidationGroup="studentScoreValidation" validationexpression="\b(0?[0-9]|1[0-0])\b" ControlToValidate="editPartTB" ErrorMessage="0-10 only!" Font-Size="Small" ForeColor="Red"></asp:RegularExpressionValidator>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="particationLabel" runat="server" Text='<%# Bind("participation") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField HeaderText="Proposal Group (5%)" DataField="proposalG" ReadOnly="true"/>

                                <asp:BoundField DataField="reportG" HeaderText="Report Group (5%)" ReadOnly="true"/>
                                <asp:BoundField DataField="presentationG" HeaderText="Presentation Group (5%)" ReadOnly="true"/>

                                <asp:TemplateField HeaderText="Total (100%)">
                                    <ItemTemplate>
                                        <asp:Label ID="totalLabel" runat="server" Text='<%#

                                            (

                                            (double)Eval("participation") +
                                            (double)Eval("sdl") + 
                                            (double)Eval("test") +
                                            (double)Eval("presentation") +
                                            (double)Eval("reviewTwo") +
                                            (double)Eval("reviewOne") +
                                            (double)Eval("report") +
                                            (double)Eval("proposal") +
                                            (double)Eval("reportG") +
                                            (double)Eval("presentationG") +
                                            (double)Eval("proposalG")
                                            ).ToString()
                                            %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <%# CheckFailure(
                                         (double)Eval("participation") +
                                         (double)Eval("sdl") + 
                                         (double)Eval("test") +
                                         (double)Eval("presentation") +
                                         (double)Eval("reviewTwo") +
                                         (double)Eval("reviewOne") +
                                         (double)Eval("report") +
                                         (double)Eval("proposal") +
                                         (double)Eval("reportG") +
                                         (double)Eval("presentationG") +
                                         (double)Eval("proposalG")
                                        ) %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button Text="Edit Role" CssClass="btn btn-primary" CommandName="Edit" runat="server" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Button ID="btnUpdate" CssClass="btn btn-sm btn-primary mb-2" runat="server" CommandName="Update" Text="Update" ValidationGroup="tableValidation" />
                                        </br>
                                        <asp:Button ID="btnCancel" CssClass="btn btn-sm btn-outline-danger" runat="server" CommandName="Cancel" Text="Cancel" />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                               
                            </Columns>
                            <EmptyDataTemplate>
                                <div class="jumbotron jumbotron-fluid">
                                    <div class="container">
                                        <h1 class="display-4">Seem Empty, add some members now!</h1>
                                        <p>Members not showing?   
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
                    <asp:Button ID="refreshBtn" CssClass="btn btn-primary" runat="server" Text="Refresh" OnClick="refreshBtn_Click"/>
                </div>
            </div>
        </div>
    </div>       
</asp:Content>
