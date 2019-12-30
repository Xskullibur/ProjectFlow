<%@ Page Title="" Language="C#" MasterPageFile="~/Tasks/TaskNested.master" AutoEventWireup="true" CodeBehind="DroppedTaskView.aspx.cs" Inherits="ProjectFlow.Tasks.DroppedTaskView" %>
<%@ MasterType VirtualPath="~/Tasks/TaskNested.master" %> 

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container py-2">
        <div class="row">

            <div class="col">
                <div style="overflow-x: auto;">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>

                            <asp:GridView ID="taskGrid" runat="server" CssClass="table table-hover table-bordered" OnRowDeleting="taskGrid_RowDeleting" > 
                                <HeaderStyle CssClass="thead-light" />
                                <Columns>
                                    <asp:CommandField DeleteText="Restore" ShowDeleteButton="True" ButtonType="Button" >
                                    <ControlStyle CssClass="btn btn-primary" />
                                    </asp:CommandField>
                                </Columns>

                                <EmptyDataTemplate>
                                    <div class="jumbotron jumbotron-fluid">
                                        <div class="container">
                                            <h1 class="display-4">Good Job, Very Consistent!</h1>
                                            <p class="load">No Dropped Task Found!</p>
                                            <hr class="my-4" />
                                            <p>If dropped tasks are expected but not shown please contact us <asp:HyperLink ID="emailLink" Text="here" NavigateUrl="mailto:projectflow.nyp.eadp@gmail.com" runat="server"></asp:HyperLink>.</p>
                                        </div>
                                    </div>
                                </EmptyDataTemplate>

                            </asp:GridView>


                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>

        </div>
    </div>

</asp:Content>
