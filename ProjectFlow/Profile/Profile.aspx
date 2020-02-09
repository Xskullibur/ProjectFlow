<%@ Page Title="" Language="C#" MasterPageFile="~/ServicesWithContentBase.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="ProjectFlow.Profile.Profile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headBase" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBase" runat="server">

    <div class="container">
        <div class="row">
            <div class="col-12 mx-auto">
                <div class="card projectflow-card-shadow w-100">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-2 border-right mx-auto text-center">
                                <%-- Profile image --%>
                                <asp:FileUpload ID="ImageFileUploadControl" CssClass="d-none" runat="server"/>
                                <asp:Image ID="ProfileImg" runat="server" CssClass="card-img-top content-profile-image pointer" />
                                <asp:Button ID="ImageChangeBtn" runat="server" CssClass="d-none" OnClick="ChangeProfileImageEvent"></asp:Button><br>
                                <asp:Label ID="UsernameLbl" CssClass="card-title" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="col-10">
                                
                                <p class="card-text">Here is your profile informations :)</p>
                                <hr>
                                <h5>Admin No:</h5>
                                <asp:Label ID="AdminNoLbl" runat="server" Text=""></asp:Label><br><br>
                                <h5>Name:</h5>
                                <asp:Label ID="NameLbl" runat="server" Text=""></asp:Label><br><br>
                                <h5>Email:</h5>
                                <asp:Label ID="EmailLbl" runat="server" Text=""></asp:Label><br><br>

                                <h5>Password:</h5>
                                <asp:UpdatePanel ID="UpdateSettingsPanel" runat="server">
                                    <ContentTemplate>
                                        <asp:Panel runat="server" ID="PasswordPanelChange" Visible="False">
                                            <div class="form-group">
                                            <label for="PasswordTextBox">New Password:</label>
                                            <asp:TextBox ID="PasswordTextBox" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>
                                            <%-- Validator --%>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" CssClass="form-text text-danger" Font-Size="Small" runat="server" 
                                                ErrorMessage="Password must be at least 7 characters!" ValidationExpression="^(.){7,}$" Display="Dynamic" 
                                                ControlToValidate="PasswordTextBox"></asp:RegularExpressionValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="form-text text-danger" Font-Size="Small" runat="server" 
                                                ErrorMessage="Password is Required!" ControlToValidate="PasswordTextBox" Display="Dynamic" 
                                                EnableClientScript="True"></asp:RequiredFieldValidator>
                                               <br>
                                           
                                            <label for="PasswordConfirmTextBox">Confirm New Password:</label>
                                            <asp:TextBox ID="PasswordConfirmTextBox" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>
                                            <%-- Validator --%>
                                            <asp:RegularExpressionValidator ID="passwordConfirmRegexValidator" CssClass="form-text text-danger" Font-Size="Small" runat="server" 
                                                ErrorMessage="Password must be at least 7 characters!" ValidationExpression="^(.){7,}$" 
                                                Display="Dynamic" ControlToValidate="PasswordConfirmTextBox" ></asp:RegularExpressionValidator>
                                            <asp:RequiredFieldValidator ID="passwordConfirmRequiredValidator" CssClass="form-text text-danger" Font-Size="Small" runat="server" 
                                                ErrorMessage="Confirm password is Required!" ControlToValidate="PasswordConfirmTextBox" Display="Dynamic" 
                                                EnableClientScript="True"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator id="PasswordConfirmCompareValidator"  CssClass="form-text text-danger" Font-Size="Small"  runat="server"
                                              ControlToValidate="PasswordConfirmTextBox" ForeColor="red"
                                              Display="Dynamic" ControlToCompare="PasswordTextBox"
                                              ErrorMessage="Confirm password must match password." />
                                            </div>
                                            <asp:Button CssClass="btn btn-primary" ID="Button1" runat="server" Text="Change Password" OnClick="UpdatePasswordEvent" />
                                            <asp:Button CssClass="btn btn-warning" ID="Button2" runat="server" Text="Cancel" OnClick="DisplayPasswordPanelEvent" CausesValidation="false"/>
                                        </asp:Panel>
                                        <asp:Panel runat="server" ID="PasswordPanel">
                                            <asp:Label ID="Label1" runat="server" Text="*****"></asp:Label><br>
                                            <asp:Button CssClass="btn btn-primary" ID="ChangePasswordBtn" runat="server" Text="Change Password"  OnClick="DisplayPasswordPanelChangeEvent" CausesValidation="false" />
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                       </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%-- File upload script for profile image --%>
    <script type="text/javascript">
        var fileUploadID = '<%=ImageFileUploadControl.ClientID%>';
        var profileImageID = '<%=ProfileImg.ClientID%>';
        var imageChangeBtnID = '<%=ImageChangeBtn.ClientID%>';
        $(document).ready(function () {

            //Bind to the fileUpload to detect for changes
            $('#' + fileUploadID).change(function () {
                $('#' + imageChangeBtnID).click();
            });

            $('#' + profileImageID).click(function () {
                showFileUploadDialogForProfileChange();
            });

        });

        function showFileUploadDialogForProfileChange() {
            document.getElementById(fileUploadID).style.display = '';
            var result = document.getElementById(fileUploadID).click();
            document.getElementById(fileUploadID).style.display = 'none';

            return result;
        }
    </script>
</asp:Content>
