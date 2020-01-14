<%@ Page Title="" Language="C#" MasterPageFile="~/ServicesWithContent.Master" AutoEventWireup="true" CodeBehind="AddMilestone.aspx.cs" Inherits="ProjectFlow.DashBoard.AddMilestone" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style6 {
            width: 16px;
        }
        .auto-style7 {
            width: 156px;
        }
        .auto-style8 {
            width: 96px;
        }
        .auto-style9 {
            width: 96px;
            height: 41px;
        }
        .auto-style10 {
            height: 41px;
        }
        .auto-style11 {
            width: 156px;
            height: 41px;
        }
        .auto-style12 {
            width: 96px;
            height: 42px;
        }
        .auto-style13 {
            height: 42px;
        }
        .auto-style14 {
            width: 156px;
            height: 42px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="modal fade" id="addMilestone" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <asp:Label ID="Label4" runat="server" Text="Add Milestone"></asp:Label>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                      <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">                    
                    <table class="auto-style1">
                        <tr>
                            <td class="auto-style9">
                                <asp:Label ID="Label1" runat="server" CssClass="control-label" Text="Name"></asp:Label>
                                &nbsp;<br />
                                <br />
                            </td>
                            <td class="auto-style10">
                                <asp:TextBox ID="NameTB" CssClass="form-control" runat="server" Width="223px"></asp:TextBox>
                                <br />
                            </td>
                            <td class="auto-style11">
                                &nbsp;<asp:RequiredFieldValidator ID="nameRequiredValidator" runat="server" ValidationGroup="modelValidation" ControlToValidate="NameTB" ErrorMessage="*" ForeColor="Red" Font-Size="Large"></asp:RequiredFieldValidator>
                                &nbsp;&nbsp;<asp:RegularExpressionValidator ID="nameRegexValidator" runat="server" ValidationGroup="modelValidation" validationexpression="^.{1,255}$" ControlToValidate="NameTB" ErrorMessage="max 255 characters!" Font-Size="Small" ForeColor="Red"></asp:RegularExpressionValidator>
                                <br />
                                <br />
                            </td>
                        </tr>                        
                        <tr>
                            <td class="auto-style12">
                                <asp:Label ID="Label2" runat="server" CssClass="control-label" Text="Start Date"></asp:Label>
                                <br />
                                <br />
                            </td>
                            <td class="auto-style13">
                                <asp:TextBox ID="startTB" CssClass="form-control" runat="server" TextMode="Date" Width="222px"></asp:TextBox>
                                <br />
                            </td>
                            <td class="auto-style14">
                                &nbsp;
                                <asp:RequiredFieldValidator ID="startDateRequiredValidator" runat="server" ValidationGroup="modelValidation" ControlToValidate="startTB" ErrorMessage="*" ForeColor="Red" Font-Size="Large"></asp:RequiredFieldValidator>
                                &nbsp;

                                &nbsp;<br />
                                <br />

                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style8">
                                <asp:Label ID="Label3" runat="server" CssClass="control-label" Text="End Date"></asp:Label>
                                <br />
                                <br />
                            </td>
                            <td class="auto-style14">
                                <asp:TextBox ID="endTB" CssClass="form-control" runat="server" TextMode="Date" Width="222px"></asp:TextBox>
                                <br />
                            </td>
                            <td class="auto-style7">
                                &nbsp;
                                <asp:RequiredFieldValidator ID="endDateRequiredValidator" runat="server" ValidationGroup="modelValidation" ControlToValidate="endTB" ErrorMessage="*" ForeColor="Red" Font-Size="Large"></asp:RequiredFieldValidator>
                                &nbsp;

                                &nbsp;<br />
                                <br />

                            </td>
                        </tr>

                        <tr>
                            <td class="auto-style8">
                                <br />
                                <br />
                            </td>
                            <td class="auto-style6">
                                <br />
                                <asp:Button ID="addBtn" CssClass="btn btn-success" runat="server" Text="Add" OnClick="addBtn_Click" ValidationGroup="modelValidation" />
                            </td>
                            <td class="auto-style7">
                                &nbsp;

                                &nbsp;&nbsp;<br />
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
                <asp:DropDownList ID="PageSelectDP" CssClass="form-control border border-dark" runat="server" AutoPostBack="True" OnSelectedIndexChanged="PageSelectDP_SelectedIndexChanged">
                    <asp:ListItem Value="0">Add Members</asp:ListItem>
                    <asp:ListItem Value="1">Add MileStone</asp:ListItem>
                </asp:DropDownList>                
            </div>
            <div class="col">                                            
                <asp:Button ID="openMilestone" runat="server" Text="Add Milestone" CssClass="btn btn-primary" OnClientClick="myfunction(); return false;" UseSubmitBehavior="false" data-toggle="modal" data-target="#addMilestone"/>                
            </div>            
            <div class="col">
                <asp:Label ID="InfoLabel" runat="server"></asp:Label>
            </div>
        </div>
        <br>
        <div class="row">
            <div class="col">
                <div style="overflow-x: auto;">
                    <asp:GridView ID="MilestoneGV" CssClass="table table-bordered" runat="server" AutoGenerateColumns="False" Width="1056px" OnRowCancelingEdit="MilestoneGV_RowCancelingEdit" OnRowEditing="MilestoneGV_RowEditing" OnRowUpdating="MilestoneGV_RowUpdating" AllowPaging="True" PageSize="4" OnPageIndexChanging="MilestoneGV_PageIndexChanging">
                        <HeaderStyle CssClass="thead-light" />
                        <Columns>
                            <asp:BoundField DataField="milestoneID" HeaderText="ID" ReadOnly="true"/>
                            <asp:TemplateField HeaderText="Name">
                                <EditItemTemplate>
                                    <asp:TextBox ID="editNameTB" CssClass="form-control" runat="server" Text='<%# Bind("milestoneName") %>'></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="editNameRequiredValidator" runat="server" ValidationGroup="tableValidation" ControlToValidate="editNameTB" ErrorMessage="*" ForeColor="Red" Font-Size="Large"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="editNameRegexValidator" runat="server" ValidationGroup="tableValidation" validationexpression="^.{1,255}$" ControlToValidate="editNameTB" ErrorMessage="max 255 characters!" Font-Size="Small" ForeColor="Red"></asp:RegularExpressionValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="nameLabel" runat="server" Text='<%# Bind("milestoneName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Start">
                                <EditItemTemplate>
                                    <asp:TextBox ID="editStartTB" CssClass="form-control" runat="server" Text='<%# Bind("startDate", "{0:yyyy-MM-dd}") %>' TextMode="Date"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="editStartRequiredValidator" runat="server" ValidationGroup="tableValidation" ControlToValidate="editStartTB" ErrorMessage="*" ForeColor="Red" Font-Size="Large"></asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="startLabel" runat="server" Text='<%# Bind("startDate", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="End">
                                <EditItemTemplate>
                                    <asp:TextBox ID="editEndTB" CssClass="form-control" runat="server" Text='<%# Bind("endDate", "{0:yyyy-MM-dd}") %>' TextMode="Date"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="editEndRequiredValidator" runat="server" ValidationGroup="tableValidation" ControlToValidate="editEndTB" ErrorMessage="*" ForeColor="Red" Font-Size="Large"></asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="endLabel" runat="server" Text='<%# Bind("endDate", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ButtonType="Button" ShowEditButton="True" ValidationGroup="tableValidation">
                                <ControlStyle CssClass="btn btn-warning" />
                            </asp:CommandField>
                        </Columns>
                        <EmptyDataTemplate>
                           <div class="jumbotron jumbotron-fluid">
                                <div class="container">
                                    <h1 class="display-4">Seem Empty, add a milestone now!</h1>                                          
                                    <p>Milestone not showing?    <asp:HyperLink ID="emailLink" Text="click here!" NavigateUrl="mailto:projectflow.nyp.eadp@gmail.com" runat="server"></asp:HyperLink></p>
                                </div>
                           </div>
                       </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </div>
        </div> 
     </div>
</asp:Content>
