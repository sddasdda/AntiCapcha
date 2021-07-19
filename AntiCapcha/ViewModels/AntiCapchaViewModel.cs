using AntiCapcha.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AntiCapcha.ViewModels
{
    public class AntiCapchaViewModel : INotifyPropertyChanged
    {
        private AntiCapchaModel AntiCapcha;

        private string result;
        public string Result
        {
            get { return result; }
            set
            {
                result = value;
                OnPropertyChanged("Result");
            }
        }

        private string uploadedImageSource;
        public string UploadedImageSource
        {
            get { return uploadedImageSource; }
            set
            {
                uploadedImageSource = value;
                OnPropertyChanged("UploadedImageSource");
            }
        }

        private RelayAsyncCommand openImage;
        public RelayAsyncCommand OpenImage
        {
            get
            {
                return openImage ??
                  (openImage = new RelayAsyncCommand((obj) =>
                  {
                      var dlg = new DefaultDialogService();
                      dlg.OpenFileDialog();
                      AntiCapcha.UploadedImage = new Bitmap(dlg.FilePath);
                      AntiCapcha.ImageSource = dlg.FilePath;
                      UploadedImageSource = AntiCapcha.ImageSource;
                  }));
            }
        }

        private RelayAsyncCommand solveCapcha;
        public RelayAsyncCommand SolveCapcha
        {
            get 
            {
                return solveCapcha ??
                  (solveCapcha = new RelayAsyncCommand((obj) =>
                  {
                      Solve(); 
                  }));
            }
        }

        private async void Solve()
        {
            await AntiCapcha.SolveCaptcha();
            Result = AntiCapcha.Result;
        }

        private void uploadImage()
        {
            var dlg = new DefaultDialogService();
            dlg.OpenFileDialog();
            AntiCapcha.UploadedImage = new Bitmap(dlg.FilePath);
        }

        public AntiCapchaViewModel()
        {
            AntiCapcha = new AntiCapchaModel();
            
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
