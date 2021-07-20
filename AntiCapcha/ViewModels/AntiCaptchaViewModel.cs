using AntiCaptcha.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AntiCaptcha.ViewModels
{
    public class AntiCaptchaViewModel : INotifyPropertyChanged
    {
        private AntiCaptchaModel AntiCaptcha;

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
                      AntiCaptcha.UploadedImage = new Bitmap(dlg.FilePath);
                      AntiCaptcha.ImageSource = dlg.FilePath;
                      UploadedImageSource = AntiCaptcha.ImageSource;
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
            await AntiCaptcha.SolveCaptcha();
            Result = AntiCaptcha.Result;
        }

        private void uploadImage()
        {
            var dlg = new DefaultDialogService();
            dlg.OpenFileDialog();
            AntiCaptcha.UploadedImage = new Bitmap(dlg.FilePath);
        }

        public AntiCaptchaViewModel()
        {
            AntiCaptcha = new AntiCaptchaModel();
            
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
