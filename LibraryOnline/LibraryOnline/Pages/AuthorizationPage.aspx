<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AuthorizationPage.aspx.cs" Inherits="LibraryOnline.Pages.AuthorizationPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="icon" href="../Content/img/LibraryIcon.ico" type="images/x-icon" />
    <link rel="stylesheet" href="../Content/bootstrap.min.css" />
    <link rel="stylesheet" href="../Content/bootstrap-grid.css" />
    <link rel="stylesheet" href="../Content/css/StyleSheet.css" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700&amp;subset=cyrillic" rel="stylesheet" />
    <title>Окно авторизации</title>
</head>
<body>
    <form id="AuthorizationForm" runat="server">
        <div class="sign-form">
            <h1 class="text-uppercase">авторизация</h1>
            <asp:Label ID="lbAuthError" runat="server" Text="Введен неверный логин или пароль" style="color:#FF0000" Visible="false"></asp:Label>
            <br />
            <asp:TextBox runat="server" ID="tbLogin" CssClass="sign-input" placeholder="ЛОГИН" MaxLength="20"></asp:TextBox>
            <br />
            <asp:TextBox runat="server" ID="tbPassword" CssClass="sign-input" placeholder="ПАРОЛЬ" type="password" MaxLength="20"></asp:TextBox>
            <br />
            <asp:Button runat="server" ID="btEnter" CssClass="sign-button" Text="ВОЙТИ" OnClick="btEnter_Click" />
            <br />
            <asp:Button runat="server" ID="btRegistration" CssClass="sign-button" Text="РЕГИСТРАЦИЯ" OnClick="btRegistration_Click" />
        </div>
    </form>
</body>
</html>
