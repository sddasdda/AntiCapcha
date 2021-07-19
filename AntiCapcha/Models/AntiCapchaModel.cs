using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace AntiCapcha.Models
{
    public class AntiCapchaModel : INotifyPropertyChanged
    {
        private Bitmap uploadedImage;
        public Bitmap UploadedImage
        {
            get { return uploadedImage; }
            set
            {
                uploadedImage = value;
                OnPropertyChanged("uploadedImage");
            }
        }

        private string imageSource;
        public string ImageSource
        {
            get { return imageSource; }
            set
            {
                imageSource = value;
                OnPropertyChanged("ImageSource");
            }
        }

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

        public async Task<string> SolveCaptcha()
        {
            using (var client = new HttpClient())
            {
                var task = new CaptchaTask()
                {
                    type = "ImageToTextTask",
                    body = Convert.ToBase64String(ImageToByteArray(UploadedImage)),
                    phrase = false,
                    @case = false,
                    numeric = true
                };

                var solveRequest = new CaptchaSolveRequest() { clientKey = "2e302049a792aaeba157b3aff1aee8d4", task = task };
                var json = JsonConvert.SerializeObject(solveRequest);
                var response = await client.PostAsync("http://api.anti-captcha.com/createTask", new StringContent(json, Encoding.UTF8, "application/json"));

                response.EnsureSuccessStatusCode();
                json = response.Content.ReadAsStringAsync().Result;

                var requestResult = JsonConvert.DeserializeObject<CaptchaSolveResponse>(json);

                if (requestResult.errorId != 0)
                    throw new AnticaptchaException(requestResult.errorDescription);

                var timeout = Environment.TickCount + 60 * 1000;
                while (true)
                {
                    if (Environment.TickCount > timeout)
                        throw new AnticaptchaException("Timeout");

                    var solutionRequest = new CaptchaSolutionRequest() { clientKey = "2e302049a792aaeba157b3aff1aee8d4", taskId = requestResult.taskId };

                    json = JsonConvert.SerializeObject(solutionRequest);
                    response = client.PostAsync("http://api.anti-captcha.com/getTaskResult", new StringContent(json, Encoding.UTF8, "application/json")).Result;
                    response.EnsureSuccessStatusCode();
                    json = response.Content.ReadAsStringAsync().Result;

                    var solutionResponse = JsonConvert.DeserializeObject<CaptchaSolutionResponse>(json);
                    if (solutionResponse.errorId != 0)
                        throw new AnticaptchaException(solutionResponse.errorDescription);

                    if (solutionResponse.status.ToLower() == "ready")
                    {
                        Result = solutionResponse.solution.text;
                        return solutionResponse.solution.text;
                    }

                    //Task.Delay(2000).Wait();
                }
            }
        }

        public byte[] ImageToByteArray(Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
