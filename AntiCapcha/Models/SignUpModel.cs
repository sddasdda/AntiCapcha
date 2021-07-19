using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Data.Entity;

namespace AntiCapcha.Models
{
    public class SignUpModel : INotifyPropertyChanged
    {
        UserContext database;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public void AddInTable(string login, string password)
        {
            database = new UserContext();

            var user = new User();
            user.Login = login;
            user.HashPassword(password);
            

            database.Accounts.Add(user);
        }
    }
}
