<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Users/Site.Master" AutoEventWireup="true" CodeBehind="ProfilePage.aspx.cs" Inherits="LibraryOnline.Pages.Users.ProfilePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="server">
    <asp:SqlDataSource ID="sdsPosition" runat="server"></asp:SqlDataSource>
    <script>
        $(document).ready(function () {
            $('#<%=tbPhone.ClientID%>').mask("+7 999 999-9999");

        });
    </script>
    <div class="wrapper">
        <div class="container mt-4 mb-5">
            <div class="sign-form">
                <h1 class="text-uppercase">Оставить анкету</h1>
                <p id="InsertMess" runat="server" visible="false" style="font-size: 14px; color: #068C1C;">Анкета успешно отправлена!</p>
                <div class="row">
                    <div class="col mt-2">
                        <asp:TextBox ID="tbName" runat="server" CssClass="user-input" placeholder="Имя" MaxLength="30" autocomplete="off" Style="width: 90%"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="Error" ErrorMessage="Введите имя" ControlToValidate="tbName" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col mt-2">
                        <asp:TextBox ID="tbSurname" runat="server" CssClass="user-input" placeholder="Фамилия" MaxLength="30" Style="width: 90%"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="Error" ErrorMessage="Введите фамилию" ControlToValidate="tbSurname" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col mt-2">
                        <asp:TextBox ID="tbAge" runat="server" CssClass="user-input" placeholder="Возраст" MaxLength="3" Style="width: 90%"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="Error" ErrorMessage="Введите возраст" ControlToValidate="tbAge" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:Label ID="lblAgeCheck" runat="server" Visible="false" CssClass="Error" Text="Возраст должен быть больше или равен 18" Display="Dynamic"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col mt-2">
                        <asp:TextBox ID="tbPhone" runat="server" CssClass="user-input" placeholder="Номер телефона" type="tel" Style="width: 90%"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="Error" ErrorMessage="Введите номер телефона"
                            Display="Dynamic" ControlToValidate="tbPhone"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col mt-2">
                        <asp:DropDownList ID="ddlPosition" runat="server" CssClass="filter-list" Style="width: 90%"></asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col mt-2">
                        <asp:CheckBox ID="cbEducation" runat="server" />
                        <label style="font-size: 16px; font-weight: 400">Высшее образование</label>
                    </div>
                </div>
                <div class="row align-items-center">
                    <div class="col">
                        <asp:Button ID="btInsert" runat="server" CssClass="sign-button" Text="Оставить анкету" OnClick="btInsert_Click"/>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
