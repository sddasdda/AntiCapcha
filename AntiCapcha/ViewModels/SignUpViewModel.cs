using AntiCaptcha.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using AntiCaptcha.Models;

namespace AntiCaptcha.ViewModels
{
    

    class SignUpViewModel : INotifyPropertyChanged
    {
        private SignUpModel signUp;

        private string login;
        public string Login
        {
            get { return login; }
            set
            {
                login = value;
                OnPropertyChanged("Login");
            }
        }
        
        public string Password;
       

        public SignUpViewModel()
        {
            signUp = new SignUpModel();
        }
        

        public void Add()
        {
            MessageBox.Show("lol");
            signUp.AddInTable(Login, Password);
        }

        public void test()
        {
            MessageBox.Show(Login + " " + Password);
        }

        private RelayCommand addInTable;
        public RelayCommand AddInTable
        {
            get
            {
                return addInTable ??
                (addInTable = new RelayAsyncCommand((obj) =>
                {
                    PasswordBox boxPass = (PasswordBox)obj;
                    Password = boxPass.Password;
                    Add();
                }));
            }
        }

        private string ConvertToUnsecureString(SecureString securePassword)
        {
            if (securePassword == null)
            {
                return string.Empty;
            }

            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(securePassword);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
