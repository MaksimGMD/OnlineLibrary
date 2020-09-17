using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using Microsoft.Office.Core;
using System.Reflection;
using Word = Microsoft.Office.Interop.Word;
using System.IO;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Windows;
using CheckBox = System.Web.UI.WebControls.CheckBox;
using System.Windows.Forms;
using SautinSoft.Document;

namespace LibraryOnline.Pages.ControlPanel
{
    public partial class EmployeeAccPage : System.Web.UI.Page
    {
        private string QR = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            QR = DBConnection.qrEmployeeList;
            if (!IsPostBack)
            {
                gvFill(QR);
                ddlPositionFill();
                ddlRoleFill();
            }
        }
        private void gvFill(string qr)
        {
            sdsEmployeeList.ConnectionString = DBConnection.connection.ConnectionString.ToString();
            sdsEmployeeList.SelectCommand = qr;
            sdsEmployeeList.DataSourceMode = SqlDataSourceMode.DataReader;
            gvEmployee.DataSource = sdsEmployeeList;
            gvEmployee.DataBind();
        }
        private void ddlRoleFill()
        {
            sdsRole.ConnectionString = DBConnection.connection.ConnectionString.ToString();
            sdsRole.SelectCommand = DBConnection.qrRoles;
            sdsRole.DataSourceMode = SqlDataSourceMode.DataReader;
            ddlRole.DataSource = sdsRole;
            ddlRole.DataTextField = "Название роли";
            ddlRole.DataValueField = "ID";
            ddlRole.DataBind();
        }
        private void ddlPositionFill()
        {
            sdsPosition.ConnectionString = DBConnection.connection.ConnectionString.ToString();
            sdsPosition.SelectCommand = DBConnection.qrPosition;
            sdsPosition.DataSourceMode = SqlDataSourceMode.DataReader;
            ddlPosition.DataSource = sdsPosition;
            ddlPosition.DataTextField = "Название должности";
            ddlPosition.DataValueField = "ID";
            ddlPosition.DataBind();
        }
        //Сортировка записей в таблице
        protected void gvEmployee_Sorting(object sender, GridViewSortEventArgs e)
        {
            SortDirection sortDirection = SortDirection.Ascending;
            string strField = string.Empty;
            switch (e.SortExpression)
            {
                case ("ID"):
                    e.SortExpression = "[ID]";
                    break;
                case ("Логин"):
                    e.SortExpression = "[Логин]";
                    break;
                case ("Роль"):
                    e.SortExpression = "[Роль]";
                    break;
                case ("Фамилия"):
                    e.SortExpression = "[Фамилия]";
                    break;
                case ("Имя"):
                    e.SortExpression = "[Имя]";
                    break;
                case ("Отчество"):
                    e.SortExpression = "[Отчество]";
                    break;
                case ("Номер паспорта"):
                    e.SortExpression = "[Номер паспорта]";
                    break;
                case ("Серия паспорта"):
                    e.SortExpression = "[Серия паспорта]";
                    break;
                case ("Отмечен для удаления"):
                    e.SortExpression = "[Отмечен для удаления]";
                    break;
                case ("Номер договора"):
                    e.SortExpression = "[Номер договора]";
                    break;
                case ("Дата заключения"):
                    e.SortExpression = "[Дата заключения]";
                    break;
                case ("Должность"):
                    e.SortExpression = "[Должность]";
                    break;
                case ("Зарплата"):
                    e.SortExpression = "[Зарплата]";
                    break;
            }
            sortGridView(gvEmployee, e, out sortDirection, out strField);
            string strDirection = sortDirection
                == SortDirection.Ascending ? "ASC" : "DESC";
            gvFill(QR + " order by " + e.SortExpression + " " + strDirection);
        }
        private void sortGridView(GridView gridView,
         GridViewSortEventArgs e,
         out SortDirection sortDirection,
         out string strSortField)
        {
            strSortField = e.SortExpression;
            sortDirection = e.SortDirection;

            if (gridView.Attributes["CurrentSortField"] != null &&
                gridView.Attributes["CurrentSortDirection"] != null)
            {
                if (strSortField ==
                    gridView.Attributes["CurrentSortField"])
                {
                    if (gridView.Attributes["CurrentSortDirection"]
                        == "ASC")
                    {
                        sortDirection = SortDirection.Descending;
                    }
                    else
                    {
                        sortDirection = SortDirection.Ascending;
                    }
                }
            }
            gridView.Attributes["CurrentSortField"] = strSortField;
            gridView.Attributes["CurrentSortDirection"] =
                (sortDirection == SortDirection.Ascending ? "ASC"
                : "DESC");
        }
        protected void gvEmployee_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[4].Visible = false;
            e.Row.Cells[12].Visible = false;
            e.Row.Cells[15].Visible = false;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvEmployee, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Нажмите, чтобы выбрать запись";
            }
        }
        //Выбор записи
        protected void gvEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvEmployee.Rows)
            {
                if (row.RowIndex == gvEmployee.SelectedIndex)
                {
                    row.ToolTip = string.Empty;
                }
                else
                {
                    row.ToolTip = "Нажмите, чтобы выбрать запись";
                }
            }
            int selectedRow = gvEmployee.SelectedRow.RowIndex;
            GridViewRow rows = gvEmployee.SelectedRow;
            DBConnection.idRecord = Convert.ToInt32(rows.Cells[1].Text.ToString());
            tbLogin.Text = rows.Cells[2].Text.ToString();
            ddlRole.SelectedIndex = Convert.ToInt32(rows.Cells[4].Text.ToString()) - 1;
            tbSurname.Text = rows.Cells[6].Text.ToString();
            tbName.Text = rows.Cells[7].Text.ToString();
            tbMiddleName.Text = rows.Cells[8].Text.ToString();
            tbPassportNumber.Text = rows.Cells[9].Text.ToString();
            tbPassportSeries.Text = rows.Cells[10].Text.ToString();
            CheckBox checkBox = (CheckBox)gvEmployee.Rows[selectedRow].Cells[11].Controls[0];
            cbDelete.Checked = checkBox.Checked;
            tbContractNumber.Text = rows.Cells[13].Text.ToString();
            tbContractDate.Text = Convert.ToDateTime(rows.Cells[14].Text.ToString()).ToString("yyyy-MM-dd");
            ddlPosition.SelectedIndex = Convert.ToInt32(rows.Cells[15].Text.ToString())-1;
            DBConnection.Pay = Convert.ToDecimal(rows.Cells[17].Text.ToString());
            SelectedMessage.Visible = true;
            cbPasswordChange.Visible = true;
            lblSelectedRow.Text = "Выбрана строка с ID " + DBConnection.idRecord;
            btUpdate.Visible = true;
            btDOCX.Visible = true;
            btPDF.Visible = true;
        }
        //Очитска полей
        protected void Cleaner()
        {
            tbLogin.Text = string.Empty;
            tbPassword.Text = string.Empty;
            tbContractDate.Text = string.Empty;
            tbContractNumber.Text = string.Empty;
            tbMiddleName.Text = string.Empty;
            tbName.Text = string.Empty;
            tbPassportNumber.Text = string.Empty;
            tbPassportSeries.Text = string.Empty;
            tbSurname.Text = string.Empty;
            ddlPosition.SelectedIndex = 0;
            ddlRole.SelectedIndex = 0;
            cbDelete.Checked = false;
            lblExportError.Visible = false;
        }
        //Отменить выбор
        protected void btCanselSelected_Click(object sender, EventArgs e)
        {
            Cleaner();
            btUpdate.Visible = false;
            DBConnection.idRecord = 0;
            SelectedMessage.Visible = false;
            lblExportError.Visible = false;
            btDOCX.Visible = false;
            btPDF.Visible = false;
            gvFill(QR);
        }
        //Добавление записи
        protected void btInsert_Click(object sender, EventArgs e)
        {
            DBConnection connection = new DBConnection();
            if (connection.LoginCheck(tbLogin.Text) > 0)
            {
                lblLoginCheck.Visible = true;
            }
            else
            {
                lblLoginCheck.Visible = false;
                bool banState;
                if (cbDelete.Checked)
                {
                    banState = true;
                }
                else
                {
                    banState = false;
                }
                string middleName;
                if (tbMiddleName.Text == "")
                {
                    middleName = Convert.ToString(DBNull.Value);
                }
                else
                {
                    middleName = tbMiddleName.Text.ToString();
                }
                DateTime theDate = DateTime.ParseExact(tbContractDate.Text, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                string dateToInsert = theDate.ToString("dd.MM.yyyy");
                DataProcedure procedure = new DataProcedure();
                procedure.EmployeeListInsert(tbLogin.Text.ToString(), tbPassword.Text.ToString(), Convert.ToInt32(ddlRole.SelectedValue.ToString()),
                    tbSurname.Text.ToString(), tbName.Text.ToString(), middleName, banState, dateToInsert,
                    tbPassportNumber.Text.ToString(), tbPassportSeries.Text.ToString(), Convert.ToInt32(ddlPosition.SelectedValue.ToString()));
                Cleaner();
                gvFill(QR);
                
            }
        }
        //Обновление записи
        protected void btUpdate_Click(object sender, EventArgs e)
        {
            GridViewRow rows = gvEmployee.SelectedRow;
            bool banCheck;
            if (cbDelete.Checked)
            {
                banCheck = true;
            }
            else
            {
                banCheck = false;
            }
            DateTime theDate = DateTime.ParseExact(tbContractDate.Text, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            string dateToInsert = theDate.ToString("dd.MM.yyyy");
            DataProcedure procedure = new DataProcedure();
            switch (cbPasswordChange.Checked)
            {
                case (true):
                    procedure.EmployeeListUpdate(DBConnection.idRecord, tbLogin.Text.ToString(), Convert.ToInt32(ddlRole.SelectedValue), tbSurname.Text.ToString(),
                        tbName.Text.ToString(), tbMiddleName.Text.ToString(), banCheck, dateToInsert, tbPassportNumber.Text.ToString(), tbPassportSeries.Text.ToString(),
                        Convert.ToInt32(ddlPosition.SelectedValue.ToString()));
                    procedure.EmployeeListPasswordUpdate(DBConnection.idRecord, tbPassword.Text.ToString());
                    break;
                case (false):
                    procedure.EmployeeListUpdate(DBConnection.idRecord, tbLogin.Text.ToString(), Convert.ToInt32(ddlRole.SelectedValue), tbSurname.Text.ToString(),
                        tbName.Text.ToString(), tbMiddleName.Text.ToString(), banCheck, dateToInsert, tbPassportNumber.Text.ToString(), tbPassportSeries.Text.ToString(),
                        Convert.ToInt32(ddlPosition.SelectedValue.ToString()));
                    break;
            }
            Cleaner();
            gvFill(QR);
            btUpdate.Visible = false;
            SelectedMessage.Visible = false;
            cbPasswordChange.Visible = false;
            DBConnection.idRecord = 0;
        }
        //Удаление записи
        protected void gvEmployee_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int Index = Convert.ToInt32(e.RowIndex);
            DataProcedure procedure = new DataProcedure();
            GridViewRow rows = gvEmployee.SelectedRow;
            DBConnection.idRecord = Convert.ToInt32(gvEmployee.Rows[Index].Cells[1].Text.ToString());
            procedure.EmployeeListDelete(DBConnection.idRecord);
            gvFill(QR);
        }
        //Поиск
        protected void btSearch_Click(object sender, EventArgs e)
        {
            if (tbSearch.Text != "")
            {
                foreach (GridViewRow row in gvEmployee.Rows)
                {
                    if (row.Cells[1].Text.Equals(tbSearch.Text) ||
                        row.Cells[2].Text.Equals(tbSearch.Text) ||
                        row.Cells[3].Text.Equals(tbSearch.Text) ||
                        row.Cells[5].Text.Equals(tbSearch.Text) ||
                        row.Cells[6].Text.Equals(tbSearch.Text) ||
                        row.Cells[7].Text.Equals(tbSearch.Text) ||
                        row.Cells[8].Text.Equals(tbSearch.Text) ||
                        row.Cells[9].Text.Equals(tbSearch.Text) ||
                        row.Cells[10].Text.Equals(tbSearch.Text) ||
                        row.Cells[13].Text.Equals(tbSearch.Text) ||
                        row.Cells[14].Text.Equals(tbSearch.Text) ||
                        row.Cells[16].Text.Equals(tbSearch.Text) ||
                        row.Cells[17].Text.Equals(tbSearch.Text))
                        row.BackColor = ColorTranslator.FromHtml("#a1f2be");
                    else
                        row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                }
                btCancel.Visible = true;
            }
        }
        //Фильтрация
        protected void btFilter_Click(object sender, EventArgs e)
        {
            if (tbSearch.Text != "")
            {
                string newQR = QR + "where [ID] like '%" + tbSearch.Text + "%' or [Логин] like '%" + tbSearch.Text + "%' or [Роль] like '%" + tbSearch.Text + "%' or" +
                    "[Фамилия] like '%" + tbSearch.Text + "%' or [Имя] like '%" + tbSearch.Text + "%' or [Отчество] like '%" + tbSearch.Text + "%' or" +
                    "[Номер паспорта] like '%" + tbSearch.Text + "%' or [Серия паспорта] like '%" + tbSearch.Text + "%' or [Номер договора] like '%" + tbSearch.Text + "%' or" +
                    "[Дата заключения] like '%" + tbSearch.Text + "%' or [Должность] like '%" + tbSearch.Text + "%' or [Зарплата] like '%" + tbSearch.Text + "%'";
                gvFill(newQR);
                btCancel.Visible = true;
            }
        }
        //Отмена поиска и фильтрации
        protected void btCancel_Click(object sender, EventArgs e)
        {
            tbSearch.Text = "";
            btCancel.Visible = false;
            cbPasswordChange.Visible = false;
            gvFill(QR);
        }
        /// <summary>
        /// Создание нового документа
        /// </summary>
        /// <param name="Format">Формат файла</param>
        protected void CreateDocx(string Format)
        {
            DBConnection connection = new DBConnection();
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"Contract № " + tbContractNumber.Text + Format;
            DocumentCore dc = new DocumentCore();
            Section section = new Section(dc);
            dc.Sections.Add(section);
            section.PageSetup.PaperType = PaperType.A4;
            //Добавление строк
            dc.Content.End.Insert("\nТРУДОВОЙ ДОГОВОР № " + tbContractNumber.Text + "", new CharacterFormat() { FontName = "Times New Roman", Size = 18, FontColor = SautinSoft.Document.Color.Black, Bold = true });
            SpecialCharacter lBr = new SpecialCharacter(dc, SpecialCharacterType.LineBreak);
            dc.Content.End.Insert(lBr.Content);
            dc.Content.End.Insert("" + tbContractDate.Text + "г.",
                new CharacterFormat() { FontName = "Times New Roman", Size = 14, FontColor = SautinSoft.Document.Color.Black, });
            SpecialCharacter lBr2 = new SpecialCharacter(dc, SpecialCharacterType.LineBreak);
            dc.Content.End.Insert(lBr2.Content);
            dc.Content.End.Insert("Организация библиотека, именуемое в дальнейшем «Работодатель», в лице генерального директора Иванова Ивана Ивановича, " +
            "и гражданин(ка) " + tbSurname.Text.ToString() + " " + tbName.Text + " " + tbMiddleName.Text + ", заключают трудовой договор. Гражданин вступает в должность  " + ddlPosition.SelectedItem.Text + " " +
            "с заработной платой в размере " + Convert.ToString(DBConnection.Pay) + " рублей.",
                new CharacterFormat() { FontName = "Times New Roman", Size = 14, FontColor = SautinSoft.Document.Color.Black, });
            SpecialCharacter lBr3 = new SpecialCharacter(dc, SpecialCharacterType.LineBreak);
            dc.Content.End.Insert(lBr2.Content);
            dc.Content.End.Insert("Паспорт: серия " + tbPassportNumber.Text + " № " + tbPassportSeries.Text + "",
                new CharacterFormat() { FontName = "Times New Roman", Size = 14, FontColor = SautinSoft.Document.Color.Black });
            //Документ в формате .docx
            if (Format == ".docx")
            {
                // Сохраняем документ в формате .docx
                dc.Save(docPath, new DocxSaveOptions());

                // Открываем документ
                Process.Start(new ProcessStartInfo(docPath) { UseShellExecute = true });
            }
            //Документ в другом формате (тут .pdf)
            else
            {
                dc.Save(docPath, new PdfSaveOptions()
                {
                    Compliance = PdfCompliance.PDF_A,
                    PreserveFormFields = true
                });

                // Open the result for demonstration purposes.
                Process.Start(new ProcessStartInfo(docPath) { UseShellExecute = true });
            }
        }
        //Экспорт в word
        protected void btDOCX_Click(object sender, EventArgs e)
        {
            try
            {
                CreateDocx(".docx");
                Cleaner();
                lblExportError.Visible = false;
                btDOCX.Visible = false;
                btPDF.Visible = false;
            }
            catch
            {
                lblExportError.Visible = true;
            }
        }
        //Экспорт в Excel
        protected void btXLS_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            Response.AppendHeader("content-disposition", "attachment; filename=Employees.xls");
            Response.ContentType = "appliction/excel";
            StringWriter stringWriter = new StringWriter();
            HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);
            gvEmployee.RenderControl(htmlTextWriter);
            Response.Write(stringWriter.ToString());
            Response.End();
        }
 
        //Экспорт в PDF
        protected void btPDF_Click(object sender, EventArgs e)
        {
            try
            {
                CreateDocx(".pdf");
                Cleaner();
                btPDF.Visible = false;
                btDOCX.Visible = false;
                lblExportError.Visible = false;
            }
            catch
            {
                lblExportError.Visible = true;
            }
        }
       
    }
}