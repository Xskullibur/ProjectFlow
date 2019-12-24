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
                                <Columns>
                                    <asp:CommandField DeleteText="Restore" ShowDeleteButton="True" ButtonType="Button" >
                                    <ControlStyle CssClass="btn btn-primary" />
                                    </asp:CommandField>
                                </Columns>
                                <HeaderStyle CssClass="thead-light" />
                            </asp:GridView>


                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>

        </div>
    </div>

</asp:Content>
