<%@ Page Title="" Language="C#" MasterPageFile="~/Issues/IssueNested.master" AutoEventWireup="true" CodeBehind="iDetailedView.aspx.cs" Inherits="ProjectFlow.Issues.iDetailedView" %>
<%@ MasterType VirtualPath="~/Issues/IssueNested.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid py-2 w-100 h-100">
        <div class="row pb-2">
            <div class="col">
                <div style="overflow-x: auto;">
                    <asp:GridView ID="IssueView" runat="server" CssClass="table table-bordered table-hover table-striped projectflow-table" AutoGenerateColumns="False" OnRowEditing="IssueView_RowEditing" OnSelectedIndexChanged="IssueView_SelectedIndexChanged" OnRowDataBound="IssueView_RowDataBound" OnRowCancelingEdit="IssueView_RowCancelingEdit" OnRowUpdating="IssueView_RowUpdating" OnRowDeleting="IssueView_RowDeleting" AllowPaging="True" OnPageIndexChanging="IssueView_PageIndexChanging" PageSize="4">
                        <HeaderStyle CssClass="thead-light" />   
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="Issue Id" ReadOnly="True" />
                            <%--Issue Name--%>
                            <asp:TemplateField HeaderText="Issue Name">
                                <EditItemTemplate>
                                    <asp:TextBox ID="editNameTxt" CssClass="form-control" runat="server" Text='<%# Bind("Task") %>' TextMode="MultiLine"></asp:TextBox>
                                
                             
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="gridName" runat="server" Text='<%# Bind("Task") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%--Description--%>
                            <asp:TemplateField HeaderText="Description">
                                <EditItemTemplate>
                                    <asp:TextBox ID="editDescTxt" CssClass="form-control" runat="server" Text='<%# Bind("Description") %>' TextMode="MultiLine"></asp:TextBox>
                                
                                   
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="gridDesc" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField DataField="CreatedBy" HeaderText="Created by" ReadOnly="True" />

                            <%--Status--%>
                            <asp:TemplateField HeaderText="Status">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="editStatusDDL" CssClass="form-control" runat="server"></asp:DropDownList>
                                
                                    <asp:Label ID="statusErrorLbl" CssClass="form-text text-danger" Font-Size="Small" runat="server" Text="" Visible="False"></asp:Label>
                                    <asp:RequiredFieldValidator ID="statusRequiredValidator" CssClass="form-text text-danger" Font-Size="Small" runat="server" ErrorMessage="This Field is Required!" ControlToValidate="editStatusDDL" Display="Dynamic" ValidationGroup="EditTask" EnableClientScript="True"></asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="gridStatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%--Action Settings--%>
                            <asp:TemplateField ShowHeader="false">
                                <ItemTemplate>  
                                    <div class="mb-2">
                                        <asp:Button Text="Edit" CssClass="btn btn-sm btn-primary dropdown-toggle" CommandName="Edit" runat="server" />
                                    </div>
                                    
                                    <asp:Button ID="DeleteButton" Text="Drop" runat="server" 
                                        CssClass="btn btn-sm btn-danger"
                                        data-toggle="confirmation"
                                        data-btn-ok-icon-class="fa fa-check"
                                        data-btn-cancel-icon-class="fa fa-close"
                                        data-popout="true"
                                        CommandName="Delete" /> 
                                </ItemTemplate>

                                <edititemtemplate>
                                    <asp:Button id="btnUpdate" CssClass="btn btn-sm btn-primary mb-2" runat="server" commandname="Update" text="Update" />
					                <asp:Button id="btnCancel" CssClass="btn btn-sm btn-outline-danger" runat="server" commandname="Cancel" text="Cancel" />
                                </edititemtemplate>
                            </asp:TemplateField>

                        </Columns>
                        <EmptyDataTemplate>
                            <div class="jumbotron jumbotron-fluid">
                                <div class="container">
                                    <h1 class="display-4">Freedoommm!</h1>
                                    <p class="load">No Ongoing Issues Found!</p>
                                    <hr class="my-4" />
                                    <p>If ongoing Issues are expected but not shown please contact us <asp:HyperLink ID="emailLink" Text="here" NavigateUrl="mailto:projectflow.nyp.eadp@gmail.com" runat="server"></asp:HyperLink>.</p>
                                </div>
                            </div>
                        </EmptyDataTemplate>
                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="3" />
                        <PagerStyle CssClass="pagination-ys table-borderless" />
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
