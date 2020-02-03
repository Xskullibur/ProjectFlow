<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmailTest.aspx.cs" Inherits="ProjectFlow.Tasks.WebForm1" Async="True" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="https://kodhus.com/kodhus-ui/kodhus-0.1.0.min.css" rel="stylesheet" type="text/css"/>
    <script src="https://kodhus.com/kodhus-ui/kodhus-0.1.0.min.js"></script>
</head>
<body>

    <asp:Panel ID="Panel1" runat="server">
        <ul id="progressbar" class="cdt-step-progressbar horizontal">
          <li>
            <span class="indicator">1</span>
            <span class="title">Initiate</span>
          </li>
          <li>
            <span class="indicator">2</span>
            <span class="title">Buy item 1</span>
          </li>
          <li>
            <span class="indicator">3</span>
            <span class="title">Finally make it</span>
          </li>
          <li>
            <span class="indicator">4</span>
            <span class="title">In production</span>
          </li>
          <li>
            <span class="indicator">5</span>
            <span class="title">Delivery</span>
          </li>
        </ul>
    </asp:Panel>

</body>

    <script type="text/javascript">
        var progressBar = document.getElementById("progressbar");
        progressBar = new Kodhus.StepProgressBar();
        progressBar.init({activeIndex: 0});
    </script>

</html>
