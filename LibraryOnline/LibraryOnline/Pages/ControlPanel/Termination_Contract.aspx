<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/ControlPanel/ControlPanel.Master" AutoEventWireup="true" CodeBehind="Termination_Contract.aspx.cs" Inherits="LibraryOnline.Pages.ControlPanel.Termination_Contract" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="server">
    <script type="text/javascript">
        function isDelete() {
            return confirm("Вы уверенны, что хотите удалить выбранную строку?");
        }
    </script>
    <asp:SqlDataSource ID="sdsContract" runat="server"></asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsContractList" runat="server"></asp:SqlDataSource>
    <div class="row mb-4">
        <nav class="nav">
            <a class="nav-link" href="EmployeeAccPage.aspx">Сотрудники/Трудовые договоры</a>
            <a class="nav-link" href="Termination_Contract.aspx">Приказы об увольнении</a>
        </nav>
    </div>
        <div class="container">
            <center>
            <h2>Приказы об увольнении</h2>
        </center>
            <div class="row">
                <div class="col mt-2 ml-auto">
                    <asp:Button ID="btDOCX" runat="server" class="btn btn-info float-right" Text=".DOCX" ToolTip="Экспортировать в DOCX" CausesValidation="False" />
                    <asp:Button ID="btPDF" runat="server" class="btn btn-info float-right" Text=".PDF" ToolTip="Экспортировать в PDF" CausesValidation="False" />
                    <asp:Button ID="btXLS" runat="server" class="btn btn-info float-right" Text=".XLS" ToolTip="Экспортировать в XLS" CausesValidation="False" OnClick="btXLS_Click"/>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-8 mt-2">
                    <asp:DropDownList ID="ddlContract" CssClass="DropDownList" runat="server"></asp:DropDownList>
                </div>
                <div class="col-lg-4 mt-2">
                    <asp:TextBox ID="tbData" runat="server" CssClass="tbForm" TextMode="Date"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Заполните датуу" ControlToValidate="tbData" CssClass="Error" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-8 mt-2">
                    <asp:TextBox ID="tbReason" runat="server" CssClass="tbForm" placeholder="Причина увольнения"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Заполните поля с причиной увольнения" ControlToValidate="tbReason" CssClass="Error" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
                <div class="col-lg-4 mt-2">
                    <asp:TextBox ID="tbNumber" runat="server" CssClass="tbForm" Enabled="false" placeholder="Номер приказа"></asp:TextBox>
                </div>
            </div>
            <asp:Button ID="btInsert" runat="server" Text="Добавить" CssClass="btn-dark" ToolTip="Добавить новую запись" OnClick="btInsert_Click" />
            <asp:Button ID="btUpdate" runat="server" Text="Обновить" CssClass="btn-dark" ToolTip="Обновить запись" Style="margin-left: 1%" Visible="false" OnClick="btUpdate_Click" />
        </div>
        <div class="grid-system mt-5">
            <div class="row mb-2">
                <div class="col">
                    <div class="form-inline">
                        <asp:TextBox ID="tbSearch" CssClass="input-search" runat="server" placeholder="Поиск" type="text" TextMode="Search"></asp:TextBox>
                        <button runat="server" id="btSearch" class="btn-search" title="Поиск" causesvalidation="False" onserverclick="btSearch_Click">
                            <i class="fa fa-search"></i>
                        </button>
                        <button runat="server" id="btFilter" class="btn-dark" onserverclick="btFilter_Click"
                            title="Фильтр" causesvalidation="False" style="width: 40px!important; height: 36px!important; margin: 0 0 0 1%; color: #e1f1ff;">
                            <i class="fas fa-filter"></i>
                        </button>
                        <asp:Button ID="btCancel" runat="server" Text="Отмена" CssClass="btn-dark" Visible="false"
                            ToolTip="Отменить поиск и фильтрацию" CausesValidation="False" Style="margin: 0 0 0 1%" OnClick="btCancel_Click" />
                    </div>
                </div>
            </div>
            <div id="SelectedMessage" class="form-inline" runat="server" visible="false" display="Dynamic">
                <button id="btCanselSelected" runat="server" onserverclick="btCanselSelected_Click" causesvalidation="False"
                    style="background: none; padding: 0; margin-right: 5px; border: none">
                    <i class="fas fa-times" title="Отменить выбор строки"></i>
                </button>
                <asp:Label ID="lblSelectedRow" runat="server" Style="font-weight: 500"></asp:Label>
            </div>
            <div class="table" style="overflow-x: auto; width: 100%">
                <asp:GridView ID="gvTContract" runat="server" AllowSorting="true"
                    CssClass="table table-condensed table-hover" UseAccessibleHeader="true" CurrentSortDirection="ASC" Font-Size="14px" OnSorting="gvTContract_Sorting" AlternatingRowStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" OnRowDataBound="gvTContract_RowDataBound" OnSelectedIndexChanged="gvTContract_SelectedIndexChanged" OnRowDeleting="gvTContract_RowDeleting">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="btDelete" runat="server" ImageUrl="~/Content/img/delete-image.png" ControlStyle-Width="24px" CommandName="Delete" OnClientClick="return isDelete()" ToolTip="Удалить" ViewStateMode="Inherit" CausesValidation="False" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
</asp:Content>
