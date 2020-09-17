<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistrationPage.aspx.cs" Inherits="LibraryOnline.Pages.RegistrationPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="icon" href="../Content/img/LibraryIcon.ico" type="image/x-icon" />
    <link rel="stylesheet" href="../Content/bootstrap.min.css" />
    <link rel="stylesheet" href="../Content/bootstrap-grid.css" />
    <link rel="stylesheet" href="../Content/css/StyleSheet.css" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700&amp;subset=cyrillic" rel="stylesheet" />
    <title>Окно регистрации</title>
</head>
<body>
    <form id="RegistrationForm" runat="server">
        <div class="sign-form">
            <h1 class="text-uppercase">регистрация</h1>
            <asp:TextBox ID="tbSurname" runat="server" CssClass="sign-input" placeholder="ФАМИЛИЯ" MaxLength="30" autocomplete="off"></asp:TextBox>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="Error" ErrorMessage="Введите фамилию" Display="Dynamic" ControlToValidate="tbSurname"></asp:RequiredFieldValidator>
            <asp:TextBox ID="tbName" runat="server" CssClass="sign-input" placeholder="ИМЯ" MaxLength="30" autocomplete="off"></asp:TextBox>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="Error" ErrorMessage="Введите имя" Display="Dynamic" ControlToValidate="tbName"></asp:RequiredFieldValidator>
            <br />
            <asp:TextBox ID="tbPhone" runat="server" CssClass="sign-input" placeholder="НОМЕР ТЕЛЕФОНА" type="tel" autocomplete="off"></asp:TextBox>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="Error" ErrorMessage="Введите номер телефона" Display="Dynamic" ControlToValidate="tbPhone"></asp:RequiredFieldValidator>
            <br />
            <asp:TextBox ID="tbLogin" runat="server" CssClass="sign-input" placeholder="ЛОГИН" autocomplete="off" MaxLength="20"></asp:TextBox>
            <br />
            <asp:Label ID="lblLoginCheck" runat="server" CssClass="Error" Visible="false" Text="Пользователь с таким логином уже есть в базе" Display="Dynamic"></asp:Label>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" CssClass="Error" ErrorMessage="Введите логин" ControlToValidate="tbLogin" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" CssClass="Error" ErrorMessage="Слишком короткий логин"
                ControlToValidate="tbLogin" Display="Dynamic" ValidationExpression="\S{5,20}"></asp:RegularExpressionValidator>
            <br />
            <asp:TextBox ID="tbPassword" runat="server" CssClass="sign-input" placeholder="ПАРОЛЬ" type="password" autocomplete="off" MaxLength="20"></asp:TextBox>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" CssClass="Error" ErrorMessage="Введите пароль" ControlToValidate="tbPassword" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" CssClass="Error"
                ErrorMessage="Длина пароля должна быть больше 6 символов, содеражть минимум одну ланитскую букву, одну цифру и один из символов !@#$%^&*_" ControlToValidate="tbPassword" Display="Dynamic"
                ValidationExpression="(?=.*[0-9])(?=.*[!@#$%^&*_])((?=.*[a-z])|(?=.*[A-Z]))[0-9a-zA-Z!@#$%^&*_]{6,}"></asp:RegularExpressionValidator>
            <br />
            <asp:TextBox ID="tbPasswordConfirm" runat="server" CssClass="sign-input" placeholder="ВВЕДИТЕ ПАРОЛЬ ПОВТОРНО" type="password" autocomplete="off" MaxLength="20"></asp:TextBox>
            <br />
            <asp:Label ID="lbPasswordConfirm" runat="server" CssClass="Error" Visible="false" Text="Не правильный пароль" Display="Dynamic"></asp:Label>
            <asp:TextBox ID="tbPassportNumber" runat="server" CssClass="sign-input" placeholder="ВВЕДИТЕ НОМЕР ПАСПОРТА" autocomplete="off" MaxLength="4"></asp:TextBox>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="Error" ErrorMessage="Введите номер паспорта" Display="Dynamic" ControlToValidate="tbPassportNumber"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" CssClass="Error" ErrorMessage="Номер паспорта должен содержать 4 цифры"
                Display="Dynamic" ControlToValidate="tbPassportNumber" ValidationExpression="\d{4}"></asp:RegularExpressionValidator>
            <br />
            <asp:TextBox ID="tbPassportSeries" runat="server" CssClass="sign-input" placeholder="ВВЕДИТЕ СЕРИЮ ПАСПОРТА" autocomplete="off" MaxLength="6"></asp:TextBox>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" CssClass="Error" ErrorMessage="Введите серию паспорта" ControlToValidate="tbPassportSeries" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" CssClass="Error" ErrorMessage="Серия паспорта должна содержать 6 цифр" Display="Dynamic"
                ControlToValidate="tbPassportSeries" ValidationExpression="\d{6}"></asp:RegularExpressionValidator>
            <br />
            <asp:Button ID="btRegistration" runat="server" CssClass="sign-button" Text="ЗАРЕГИСТРИРОВАТЬСЯ" OnClick="btRegistration_Click" />
        </div>
    </form>
    <script src="../Scripts/jquery-3.4.1.min.js"></script>
    <script src="../Scripts/jquery.maskedinput.min.js"></script>
    <%--Маска для ввода номера телефона--%>
    <script>
        $(document).ready(function () {
            $("#tbPhone").mask("+7 999 999-9999");
        });
    </script>
</body>
</html>
