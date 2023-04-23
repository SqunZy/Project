using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Models
{
    public class User
    {
        public int Id { get; set; } //המזהה היחודי של המשתמש
        public string UserName { get; set; } //שם המשתמש
        public string Password { get; set; } //סיסמה
        public string Email { get; set; } //סיסמה
        public int CurrentCharacter { get; set; } //מספר הדמות, איתה משחק השחקן
        public int Money { get; set; } //הכסף המצטבר
        public int Score { get; set; } //הישג
    }
}
