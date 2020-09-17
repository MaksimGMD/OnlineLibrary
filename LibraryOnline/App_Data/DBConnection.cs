using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Security.Cryptography;
using Encryption_Library;


namespace LibraryOnline
{
    public class DBConnection
    {
        //Подключение к базе данных 
        public static SqlConnection connection = new SqlConnection(
            "Data Source=DESKTOP-2OC8HFJ\\MYGRIT; Initial Catalog=Library;" +
            "Integrated Security=True; Connect Timeout=30; Encrypt=False;" +
            "TrustServerCertificate=False; ApplicationIntent=ReadWrite; MultiSubnetFailover=False");
        //Переменные
        public static int idUser, idRecord;
        public static decimal Pay;

        public DataTable dtBook_List = new DataTable("Book_List");
        public DataTable dtReadersINF = new DataTable("Reader");
        public static string qrBook_List = "select * from [Book_List]",
            qrAuthor = "select [ID_Author], [Name]+' '+[Surname] as Автор from [Author] order by Автор ASC",
            qrAuthors = "select [ID_Author] as 'ID', [Name] as 'Имя', [Surname] as 'Фамилия', [Middle_Name] as 'Отчество' from [Author]",
            qrGenre = "select [ID_Genre], [Genre_Name] from [Genre] order by [Genre_Name] ASC",
            qrGenres = "select [ID_Genre] as 'ID', [Genre_Name] as 'Название жанра' from [Genre]",
            qrYear = "select [Year] from [Book] order by [Year] ASC",
            qrHistory = "select [Book_Name] as 'Название книги', [Issuance_Date] as 'Дата выдачи', [Return_Date] as 'Дата возврата', [Status_Name] as 'Статус' from [Order] " +
            "inner join[Book] on[ID_Book] = [Book_ID] " +
            "inner join[Status] on[ID_Status] = [Status_ID] " +
            "where [Status_ID] = '2'",
             qrBooksInProgress = "select [Book_Name] as 'Название книги', [Issuance_Date] as 'Дата выдачи', [Return_Date] as 'Дата возврата', [Status_Name] as 'Статус' from [Order] " +
             "inner join[Book] on [Book_ID] = [ID_Book]" +
             "inner join[Status] on [Status_ID] = [ID_Status] where [Status_ID] = '1' ",
             qrOrder = "select [ID_Order], [Book_Name] as 'Название книги', [Status_Name] as 'Статус' from [Order] " +
             "inner join[Book] on [Book_ID] = [ID_Book] " +
             "inner join[Status] on [Status_ID] = [ID_Status] where  [Status_ID] = '3' ",
             qrUsers = "select [ID_Authorization] as 'ID', [Login] as 'Логин', [Surname] as 'Фамилия', [Name] as 'Имя', " +
            "[Phone_Reader] as 'Номер телефона', [Ticket_Number] as 'Номер билета'," +
            "[Reader_Passport_Number] as 'Номер паспорта', [Reader_Passport_Series] as 'Серия паспорта', [ID_Role], [Role_Name] as 'Роль', [Ban] as 'Заблокирован' from [Authorization]" +
            "inner join[Reader] on[Authorization_ID] = [ID_Authorization]" +
            "inner join[Role] on[Role_ID] = [ID_Role]",
            qrRoles = "select [ID_Role] as 'ID', [Role_Name] as 'Название роли' from [Role]",
            qrProfile = "select [ID_Profile] as 'ID', [Surname_Profile] as 'Фамилия', [Name_Profile] as 'Имя', [Age] as 'Возраст', [Phone] as 'Номер телефона', [Higher_Education] as 'Высшее образование'," +
            "[Profile_Position_ID], [Position_Name] as 'Должность' from [Profile] " +
            "inner join [Position] on [Profile_Position_ID] = [ID_Position]",
            qrPosition = "select [ID_Position] 'ID', [Position_Name] as 'Название должности', [Pay] as 'Зарплата' from [Position]",
            qrEmployeeList = "select * from [Employee_List]",
            qrTermination_Contract_List = "select * from [Termination_Contract_List]",
            qrContractList = "select [Employee_Contract_ID], [Surname_Employee] + ' ' + [Name_Employee] + ' ' + [Middle_Name_Employee] + ' ' + [Contract_Number] as 'Контракт' from [Contract] inner join[Employee] on[Employee_Contract_ID] = [ID_Contract]",
            qrOrders_List = "select * from [Orders_List]",
            qrOrderBooks = "select [ID_Book], [Book_Name] + '  ' + [Year] + ' г.' as 'Книга'from [Book]",
            qrOrderTicketNumber = "select [Authorization_ID], [Ticket_Number] from [Reader]",
            qrBooks = "select [ID_Book] as 'ID', [Book_Name] as 'Название книги', [Year] as 'Год', [Image], [ID_Author], [Name] + ' ' + [Surname] as 'Автор', [ID_Genre], [Genre_Name] as 'Жанр', [Amount] as 'Количество' from[Book] " +
                "inner join book_author on book_id = ID_Book inner join Author on Author_ID = ID_Author " +
                "inner join book_genre on[dbo].[Book_Genre].[Book_ID] = ID_Book inner join Genre on ID_Genre = Genre_ID ";
        private SqlCommand command = new SqlCommand("", connection);

        //Возвращает id авторизовавшегося пользователя
        public Int32 Authorization(string login, string password)
        {
            password = Encryption.EncryptedDate(password);
            try
            {
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "select [ID] from [dbo].[Users] " +
               "where [Login] = '" + login + "' and [Password] = '" + password + "'";
                DBConnection.connection.Open();
                idUser = Convert.ToInt32(command.ExecuteScalar().ToString());
                return (idUser);
            }
            catch
            {
                idUser = 0;
                return (idUser);
            }
            finally
            {
                connection.Close();
            }
        }
        //Возвращает роль авторизовавшегося пользователя
        public string userRole(Int32 idUser)
        {
            string RoleID;
            try
            {
                command.CommandText = "select Role from [Users] where ID like '%" + idUser + "%'";
                connection.Open();
                RoleID = command.ExecuteScalar().ToString();
                return RoleID;
            }
            catch
            {
                RoleID = "1";
                return RoleID;
            }
            finally
            {
                connection.Close();
            }
        }
        /// <summary>
        /// Проверка уникальностии логина
        /// </summary>
        /// <param name="login">Логин</param>
        /// <returns>Количество найденных пользователей</returns>
        public Int32 LoginCheck(string login)
        {
            int loginCheck;
            try
            {
                command.CommandText = "select count (*) from [Authorization] where Login like '%" + login + "%'";
                connection.Open();
                loginCheck = Convert.ToInt32(command.ExecuteScalar().ToString());
                return loginCheck;
            }
            catch
            {
                loginCheck = 0;
                return loginCheck;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}