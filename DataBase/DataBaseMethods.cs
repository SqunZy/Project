using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBase.Models;
using Microsoft.Data.Sqlite;
using Windows.Storage;

namespace DataBase
{
    public static class DataBaseMethods
    {
        private static string dbPath = ApplicationData.Current.LocalFolder.Path; //הגדרת משתנה שאומר איפה וויזואל סטודיו מקים דאטה בייס
        private static string connectionString = "Filename=" + dbPath + "\\DataBase.db"; //מחרוזת שמהווה שילוב של שם הקובץ, המיקום שלו ושם הדאטה בייס 



        /// <summary>
        /// הפעולה מוסיפה משתמש חדש למסד הנתונים, בתנאי שהוא באמת חדש.
        /// </summary>
        /// <param name="name"> שם המשתמש הנרשם</param>
        /// <param name="password">סיסמאת המשתמש החדש</param>
        /// <param name="mail">מייל של המשתמש החדש</param>
        /// <returns>מחזירה משתמש שנרשם או נול אם הוא כבר קיים</returns>
        public static User AddUser(string name, string password, string mail)
        {
            User user = GetUser(name, password);//נסיון שליפת המשתמש מהמאגר
            if (user != null) //זאת אומרת שהמשתמש קיים
            {
                return null; //הגעה למקום הלא נכון, המשתמש קיים, עבור להתחברות
            }
            //אם אנו ממשיכים זאת אומרת שהמשתמש לא קיים
            string query = $"INSERT INTO [Users] (UserName,Password,Money,Score,CurrentCharacter,Email)" + $"VALUES ('{name}','{password}',{100},{0},{1},'{mail}')";
            Execute(query); //המשתמש החדש מתווסף למאגר
            user = GetUser(name, password); //קבלת המשתמש שהתווסף כרגע
            return user;
        }
        /// <summary>
        /// הפעולה שולפת את המשתמש הקיים ממסד הנתונים בתנאי שהוא קיים
        /// </summary>
        /// <param name="name">שם המשתמש</param>
        /// <param name="password">סיסמאת המשתמש</param>
        /// <returns>מחזירה את המשתמש עצמו או נול אם הוא לא קיים</returns>
        public static User GetUser(string name, string password)
        {
            string query = $"SELECT * FROM [Users] WHERE UserName='{name}' AND Password='{password}'";
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open(); //פתיחת החיבור
                SqliteCommand command = new SqliteCommand(query, connection); //הפקודה
                SqliteDataReader reader = command.ExecuteReader(); //ביצוע הפקודה
                if (reader.HasRows) // האם יש נתונים
                {
                    reader.Read(); // קריאת שורה אחת
                    User user = new User
                    {
                        Id = reader.GetInt32(0),
                        UserName = reader.GetString(1),
                        Password = reader.GetString(2),
                        Email = reader.GetString(3),
                        CurrentCharacter = reader.GetInt32(4),
                        Money = reader.GetInt32(5),
                        Score = reader.GetInt32(6),
                    };
                    return user;
                }
            }
            return null; //המשתמש לא קיים
        }
        /// <summary>
        /// הפעולה מבצעת שאילתה
        /// </summary>
        /// <param name="query">שאילתה שאותה יש לבצע</param>
        private static void Execute(string query)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(query, connection);
                command.ExecuteNonQuery();
            }
        }
    }
}
