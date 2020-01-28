<%@ Page Title="" Language="C#" MasterPageFile="~/ServicesWithContent.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="ProjectFlow.Profile.Profile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <div class="container">
        <div class="row">
            <div class="col-12 col-md-10 mx-auto">
                <div class="card w-100">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-2 border-right mx-auto text-center">
                                <asp:FileUpload id="ImageFileUploadControl" runat="server" />
                                <asp:ImageButton ID="ProfileImg" runat="server" CssClass="card-img-top" OnClick="ChangeProfileEvent"/>
                                <%--<img src="https://upload.wikimedia.org/wikipedia/commons/a/ac/No_image_available.svg" class="card-img-top" alt="Profile-Image">--%>
                                <asp:Label ID="UsernameLbl" CssClass="card-title" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="col-10">
                                
                                <p class="card-text">Here is your profile informations :)</p>
                                <hr>
                                <h5>Admin No:</h5>
                                <asp:Label ID="AdminNoLbl" runat="server" Text=""></asp:Label><br><br>
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

</asp:Content>
