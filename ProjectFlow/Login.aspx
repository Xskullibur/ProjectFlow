<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ProjectFlow.LoginPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row-fluid">
        <div class="col-12 col-md-5 mx-auto">
              <div class="form-group">
                <label for="exampleInputEmail1">Email address</label>
                  <asp:TextBox CssClass="form-control" ID="emailTextBox" runat="server" placeholder="Email"></asp:TextBox>
              </div>
              <div class="form-group">
                <label for="exampleInputPassword1">Password</label>
                 <asp:TextBox  CssClass="form-control" ID="passwordTextBox" runat="server" placeholder="Password" TextMode="Password"></asp:TextBox>
              </div>
              <div class="row">
                  <div class="form-group form-check col align-self-start">
                    <asp:CheckBox CssClas="form-check-input" ID="rememberMeCheckBox" runat="server" />
                    <label class="form-check-label" for="rememberMeCheckBox">Remember me</label>
                  </div>
                  <div class="form-group col align-self-end text-right">
                      <asp:HyperLink runat="server">Create Account</asp:HyperLink>
                  </div>
              </div>
              <asp:Button CssClass="btn btn-primary" ID="submitBtn" runat="server" Text="Submit" OnClick="LoginValidateAction" />
        </div>
    </div>

</asp:Content>
