using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Microsoft.SqlServer.Server;
using System.Text;
using System.Security.Cryptography;
using Encryption_Library;

namespace LibraryOnline
{
    public class DataProcedure
    {
        private SqlCommand command = new SqlCommand("", DBConnection.connection);
        //Процедуры для манипуляции данными из БД
        private void commandConfig(string config)
        {
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "[dbo].[" + config + "]";
            command.Parameters.Clear();
        }

        //Регистрация читателя
        public void UsersInsert(string Login, string Password, int Role_ID, string Surname, string Name, string Phone_Reader,
           string Reader_Passport_Number, string Reader_Passport_Series, bool Ban)
        {
            Password =  Encryption.EncryptedDate(Password);
            commandConfig("Users_Insert");
            command.Parameters.AddWithValue("@Login", Login);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@Role_ID", Role_ID);
            command.Parameters.AddWithValue("@Surname", Surname);
            command.Parameters.AddWithValue("@Name", Name);
            command.Parameters.AddWithValue("@Phone_Reader", Phone_Reader);
            command.Parameters.AddWithValue("@Reader_Passport_Number", Reader_Passport_Number);
            command.Parameters.AddWithValue("@Reader_Passport_Series", Reader_Passport_Series);
            command.Parameters.AddWithValue("@Ban", Ban);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Добавление пользователя
        public void UsersRegistrtion(string Login, string Password, int Role_ID, string Surname, string Name, string Phone_Reader)
        {
            Password = Encryption.EncryptedDate(Password);
            commandConfig("Users_Registration");
            command.Parameters.AddWithValue("@Login", Login);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@Role_ID", Role_ID);
            command.Parameters.AddWithValue("@Surname", Surname);
            command.Parameters.AddWithValue("@Name", Name);
            command.Parameters.AddWithValue("@Phone_Reader", Phone_Reader);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Удаление пользователя
        public void UsersDelete(int ID_Authorization)
        {
            commandConfig("Authorization_Delete");
            command.Parameters.AddWithValue("@ID_Authorization", ID_Authorization);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Обновление читателей
        public void UsersUpdate(int ID_Authorization, string Login, int Role_ID, string Surname, string Name, string Phone_Reader,
        string Reader_Passport_Number, string Reader_Passport_Series, bool Ban)
        {
            commandConfig("Users_Update");
            command.Parameters.AddWithValue("@ID_Authorization", ID_Authorization);
            command.Parameters.AddWithValue("@Login", Login);
            command.Parameters.AddWithValue("@Role_ID", Role_ID);
            command.Parameters.AddWithValue("@Surname", Surname);
            command.Parameters.AddWithValue("@Name", Name);
            command.Parameters.AddWithValue("@Phone_Reader", Phone_Reader);
            command.Parameters.AddWithValue("@Reader_Passport_Number", Reader_Passport_Number);
            command.Parameters.AddWithValue("@Reader_Passport_Series", Reader_Passport_Series);
            command.Parameters.AddWithValue("@Ban", Ban);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        public void UsersUpdatePassword(int ID_Authorization, string Password)
        {
            commandConfig("Users_Update_Password");
            Password = Encryption.EncryptedDate(Password);
            command.Parameters.AddWithValue("@ID_Authorization", ID_Authorization);
            command.Parameters.AddWithValue("@Password", Password);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Добавление анкеты
        public void ProfileInsert(string Surname_Profile, string Name_Profile, string Age, string Phone,
            bool Higher_Education, int Profile_Position_ID)
        {
            commandConfig("Profile_Insert");
            command.Parameters.AddWithValue("@Surname_Profile", Surname_Profile);
            command.Parameters.AddWithValue("@Name_Profile", Name_Profile);
            command.Parameters.AddWithValue("@Age", Age);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@Higher_Education", Higher_Education);
            command.Parameters.AddWithValue("@Profile_Position_ID", Profile_Position_ID);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Обновление анкеты
        public void ProfileUpdate(int ID_Profile, string Surname_Profile, string Name_Profile, string Age, string Phone,
            bool Higher_Education, int Profile_Position_ID)
        {
            commandConfig("Profile_Update");
            command.Parameters.AddWithValue("@ID_Profile", ID_Profile);
            command.Parameters.AddWithValue("@Surname_Profile", Surname_Profile);
            command.Parameters.AddWithValue("@Name_Profile", Name_Profile);
            command.Parameters.AddWithValue("@Age", Age);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@Higher_Education", Higher_Education);
            command.Parameters.AddWithValue("@Profile_Position_ID", Profile_Position_ID);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Удаление анкеты
        public void ProfileDelete(int ID_Profile)
        {
            commandConfig("Profile_Delete");
            command.Parameters.AddWithValue("@ID_Profile", ID_Profile);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Добавление роли
        public void RoleInsert(string Role_Name)
        {
            commandConfig("Role_Insert");
            command.Parameters.AddWithValue("@Role_Name", Role_Name);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Обновление роли
        public void RoleUpdate(int ID_Role,string Role_Name)
        {
            commandConfig("Role_Update");
            command.Parameters.AddWithValue("@ID_Role", ID_Role);
            command.Parameters.AddWithValue("@Role_Name", Role_Name);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Удаление роли роли
        public void RoleDelete(int ID_Role)
        {
            commandConfig("Role_Delete");
            command.Parameters.AddWithValue("@ID_Role", ID_Role);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Добавление автора
        public void AuthorInsert(string Surname, string Name, string Middle_Name)
        {
            commandConfig("Author_Insert");
            command.Parameters.AddWithValue("@Surname", Surname);
            command.Parameters.AddWithValue("@Name", Name);
            command.Parameters.AddWithValue("@Middle_Name", Middle_Name);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Обновление автора
        public void AuthorUpdate(int ID_Author, string Surname, string Name, string Middle_Name)
        {
            commandConfig("Author_Update");
            command.Parameters.AddWithValue("@ID_Author", ID_Author);
            command.Parameters.AddWithValue("@Surname", Surname);
            command.Parameters.AddWithValue("@Name", Name);
            command.Parameters.AddWithValue("@Middle_Name", Middle_Name);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Удаление автора
        public void AuthorDelete(int ID_Author)
        {
            commandConfig("Author_Delete");
            command.Parameters.AddWithValue("@ID_Author", ID_Author);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Добавление жанра
        public void GenreInsert(string Genre_Name)
        {
            commandConfig("Genre_Insert");
            command.Parameters.AddWithValue("@Genre_Name", Genre_Name);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Обновление жанра
        public void GenreUpdate(int ID_Genre, string Genre_Name)
        {
            commandConfig("Genre_Update");
            command.Parameters.AddWithValue("@ID_Genre", ID_Genre);
            command.Parameters.AddWithValue("@Genre_Name", Genre_Name);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Удаление жанров
        public void GenreDelete(int ID_Genre)
        {
            commandConfig("Genre_Delete");
            command.Parameters.AddWithValue("@ID_Genre", ID_Genre);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Добавление должности
        public void PositionInsert(string Position_Name, decimal Pay)
        {
            commandConfig("Position_Insert");
            command.Parameters.AddWithValue("@Position_Name", Position_Name);
            command.Parameters.AddWithValue("@Pay", Pay);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Обновление должности
        public void PositionUpdate(int ID_Position, string Position_Name, decimal Pay)
        {
            commandConfig("Position_Update");
            command.Parameters.AddWithValue("@ID_Position", ID_Position);
            command.Parameters.AddWithValue("@Position_Name", Position_Name);
            command.Parameters.AddWithValue("@Pay", Pay);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Удаление должности
        public void PositionDelete(int ID_Position)
        {
            commandConfig("Position_Delete");
            command.Parameters.AddWithValue("@ID_Position", ID_Position);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Добавление трудового договора и сотрудника
        public void EmployeeListInsert(string Login, string Password, int Role_ID,
            string Surname_Employee, string Name_Employee, string Middle_Name_Employee, bool Employee_Logical_Delete,
            string Contract_Date, string Passport_Number, string Passport_Series,
            int Position_ID)
        {
            Password = Encryption.EncryptedDate(Password);
            commandConfig("Employee_List_Insert");
            command.Parameters.AddWithValue("@Login", Login);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@Role_ID", Role_ID);
            command.Parameters.AddWithValue("@Surname_Employee", Surname_Employee);
            command.Parameters.AddWithValue("@Name_Employee", Name_Employee);
            command.Parameters.AddWithValue("@Middle_Name_Employee", Middle_Name_Employee);
            command.Parameters.AddWithValue("@Employee_Logical_Delete", Employee_Logical_Delete);
            command.Parameters.AddWithValue("@Contract_Date", Contract_Date);
            command.Parameters.AddWithValue("@Passport_Number", Passport_Number);
            command.Parameters.AddWithValue("@Passport_Series", Passport_Series);
            command.Parameters.AddWithValue("@Position_ID", Position_ID);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Удаление сотруудника
        public void EmployeeListDelete(int ID_Authorization)
        {
            commandConfig("Employee_List_Delete");
            command.Parameters.AddWithValue("@ID_Authorization", ID_Authorization);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Обновление трудового договора и сотрудника
        public void EmployeeListUpdate(int ID_Authorization, string Login, int Role_ID,
            string Surname_Employee, string Name_Employee, string Middle_Name_Employee, bool Employee_Logical_Delete,
            string Contract_Date, string Passport_Number, string Passport_Series,
            int Position_ID)
        {
            commandConfig("Employee_List_Update");
            command.Parameters.AddWithValue("@ID_Authorization", ID_Authorization);
            command.Parameters.AddWithValue("@Login", Login);
            command.Parameters.AddWithValue("@Role_ID", Role_ID);
            command.Parameters.AddWithValue("@Surname_Employee", Surname_Employee);
            command.Parameters.AddWithValue("@Name_Employee", Name_Employee);
            command.Parameters.AddWithValue("@Middle_Name_Employee", Middle_Name_Employee);
            command.Parameters.AddWithValue("@Employee_Logical_Delete", Employee_Logical_Delete);
            command.Parameters.AddWithValue("@Contract_Date", Contract_Date);
            command.Parameters.AddWithValue("@Passport_Number", Passport_Number);
            command.Parameters.AddWithValue("@Passport_Series", Passport_Series);
            command.Parameters.AddWithValue("@Position_ID", Position_ID);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        public void EmployeeListPasswordUpdate(int ID_Authorization, string Password)
        {
            Password = Encryption.EncryptedDate(Password);
            commandConfig("Employee_List_Password_Update");
            command.Parameters.AddWithValue("@ID_Authorization", ID_Authorization);
            command.Parameters.AddWithValue("@Password", Password);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Добавление приказа об увольнении
        public void TerminationContractInsert(string Termination_Date, string Reason, int Contract_ID)
        {
            commandConfig("Termination_Contract_Insert");
            command.Parameters.AddWithValue("@Termination_Date", Termination_Date);
            command.Parameters.AddWithValue("@Reason", Reason);
            command.Parameters.AddWithValue("@Contract_ID", Contract_ID);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Обновление приказа об увольнении
        public void TerminationContractUpdate(int ID_Termination_Contract, string Termination_Date, string Reason, int Contract_ID)
        {
            commandConfig("Termination_Contract_Update");
            command.Parameters.AddWithValue("@ID_Termination_Contract", ID_Termination_Contract);
            command.Parameters.AddWithValue("@Termination_Date", Termination_Date);
            command.Parameters.AddWithValue("@Reason", Reason);
            command.Parameters.AddWithValue("@Contract_ID", Contract_ID);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Удаление прииказа об увольнении
        public void TerminationContracDelete(int ID_Termination_Contract)
        {
            commandConfig("Termination_Contract_Delete");
            command.Parameters.AddWithValue("@ID_Termination_Contract", ID_Termination_Contract);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Добавление заказа
        public void OrderInsert(int Book_ID, string Issuance_Date, string Return_Date, int Status_ID, 
            int Authorization_Reader_ID, int Authorization_Employee_ID)
        {
            commandConfig("Order_Insert");
            command.Parameters.AddWithValue("@Book_ID", Book_ID);
            command.Parameters.AddWithValue("@Issuance_Date", Issuance_Date);
            command.Parameters.AddWithValue("@Return_Date", Return_Date);
            command.Parameters.AddWithValue("@Status_ID", Status_ID);
            command.Parameters.AddWithValue("@Authorization_Reader_ID", Authorization_Reader_ID);
            command.Parameters.AddWithValue("@Authorization_Employee_ID", Authorization_Employee_ID);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Добавление заказа
        public void UserOrderInsert(int Book_ID, int Status_ID,
            int Authorization_Reader_ID)
        {
            commandConfig("UserOrderInsert");
            command.Parameters.AddWithValue("@Book_ID", Book_ID);
            command.Parameters.AddWithValue("@Status_ID", Status_ID);
            command.Parameters.AddWithValue("@Authorization_Reader_ID", Authorization_Reader_ID);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Удаление заказа
        public void OrderDelete(int ID_Order)
        {
            commandConfig("Order_Delete");
            command.Parameters.AddWithValue("@ID_Order", ID_Order);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Обновление заказа
        public void OrderUpdate(int ID_Order, int Book_ID, string Issuance_Date, string Return_Date, int Status_ID,
            int Authorization_Reader_ID)
        {
            commandConfig("Order_Update");
            command.Parameters.AddWithValue("@ID_Order", ID_Order);
            command.Parameters.AddWithValue("@Book_ID", Book_ID);
            command.Parameters.AddWithValue("@Issuance_Date", Issuance_Date);
            command.Parameters.AddWithValue("@Return_Date", Return_Date);
            command.Parameters.AddWithValue("@Status_ID", Status_ID);
            command.Parameters.AddWithValue("@Authorization_Reader_ID", Authorization_Reader_ID);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Добавление книги
        public void BookInsert(string Book_Name, string Year, int Amount, string Image, int Genre, int Author)
        {
            commandConfig("Book_Insert");
            command.Parameters.AddWithValue("@Book_Name", Book_Name);
            command.Parameters.AddWithValue("@Year", Year);
            command.Parameters.AddWithValue("@Amount", Amount);
            command.Parameters.AddWithValue("@Image", Image);
            command.Parameters.AddWithValue("@Genre", Genre);
            command.Parameters.AddWithValue("@Author", Author);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Обновление книги
        public void Book_Update(int ID_Book, string Book_Name, string Year, int Amount, string Image, int Genre, int Author)
        {
            commandConfig("Book_Update");
            command.Parameters.AddWithValue("@ID_Book", ID_Book);
            command.Parameters.AddWithValue("@Book_Name", Book_Name);
            command.Parameters.AddWithValue("@Year", Year);
            command.Parameters.AddWithValue("@Amount", Amount);
            command.Parameters.AddWithValue("@Image", Image);
            command.Parameters.AddWithValue("@Genre", Genre);
            command.Parameters.AddWithValue("@Author", Author);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Удаление книг
        public void BookDelete(int ID_Book)
        {
            commandConfig("Book_Delete");
            command.Parameters.AddWithValue("@ID_Book", ID_Book);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
    }
}