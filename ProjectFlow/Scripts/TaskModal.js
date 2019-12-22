function showModal() {

    var modalTemplate = `
            <div class="modal fade" id="taskModal" tabindex="-1" role="dialog" aria-labelledby="taskModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-md" role="document">
                    <div class="modal-content">

                        <%--Header--%>
                    <div class="modal-header">
                            <h5 class="modal-title">Add Task</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>

                        <%--Body--%>
                    <div class="modal-body">

                            <div class="container-fluid" runat="server">

                                <%--Name--%>
                            <div class="form-group">
                                    <asp: Label CssClass="control-label" Text="Task Name:" AssociatedControlID="tNameTxt" runat="server" />
                                <asp: TextBox runat="server" CssClass="form-control" ID="tNameTxt" TextMode="SingleLine" />
                            </div>

                                <%--Description--%>
                            <div class="form-group">
                                    <asp: Label CssClass="control-label" Text="Description:" AssociatedControlID="tDescTxt" runat="server" />
                                <asp: TextBox ID="tDescTxt" CssClass="form-control" runat="server" TextMode="MultiLine" />
                            </div>

                                <%--Start Date--%>
                            <div class="form-group">
                                    <asp: Label CssClass="control-label" Text="Start Date:" AssociatedControlID="tStartTxt" runat="server" />
                                <asp: TextBox ID="tStartTxt" CssClass="form-control" runat="server" TextMode="Date" />
                            </div>

                                <%--End Date--%>
                            <div class="form-group">
                                    <asp: Label CssClass="control-label" Text="End Date:" AssociatedControlID="tEndTxt" runat="server" />
                                <asp: TextBox ID="tEndTxt" CssClass="form-control" runat="server" TextMode="Date" />
                            </div>

                                <%--Allocation--%>
                            <div class="form-group">
                                    <asp: Label CssClass="control-label" Text="Allocation:" AssociatedControlID="allocationDLL" runat="server" />
                                <asp: DropDownList CssClass="form-control selectpicker" data-live-search="true" multiple data-actions-box="true" ID="allocationDLL" runat="server"/>
                            </div>

                            </div>

                        </div>

                        <%--Footer--%>
                    <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                            <button type="button" class="btn btn-primary">Save changes</button>
                        </div>
                    </div>
                </div>
            </div>`

    document.body.append(modalTemplate);
    $("#taskModal").modal("toggle")

}