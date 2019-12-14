<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="identity.aspx.cs" Inherits="ProjectFlow.identity" %>

<% 
    Response.Write(System.Security.Principal.WindowsIdentity.GetCurrent().Name);
%>