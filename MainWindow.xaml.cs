using Newtonsoft.Json.Linq;
using PexelsDotNetSDK.Api;
using PexelsDotNetSDK.Models;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using WeathForecast.DTO;
using WeathForecast.ViewModels;
using System.Configuration;
using System.IO;

namespace WeathForecast
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        /// <summary>
        /// Stores the result of the photo search from the Pexels API.
        /// </summary>
        private PhotoPage? result;

        /// <summary>
        /// The key used to get the images.
        /// </summary>
        private readonly string? PexelsApiKey;

        /// <summary>
        /// Current time in the city.
        /// </summary>
        private string? _cityTime;

        /// <summary>
        /// A flag that protects against multiple simultaneous clicks.
        /// </summary>
        private bool ClickProtection;

        private DispatcherTimer? timer;
        private CancellationTokenSource? cancellationTokenSource;

        public MainWindow()
        {
            InitializeComponent();
            PexelsApiKey = ConfigurationManager.AppSettings["PexelsApiKey"];
            ClickProtection = true;
        }

        private void StartTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1000);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object? sender, object e)
        {
            UpdateCityTime();

            TimeXAML.Text = (_cityTime?.Substring(0, 5));
        }

        private void UpdateCityTime()
        {
            if (_cityTime != null)
            {
                DateTime currentTime = DateTime.Parse(_cityTime);
                currentTime = currentTime.AddSeconds(1);
                _cityTime = currentTime.ToString("HH:mm:ss");
            }
            else
            {
                _cityTime = DateTime.Now.ToString("HH:mm:ss");
            }

        }

        private async void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (ClickProtection && e.Key == Key.Enter)
            {
                await GetWeathForecast();
            }
        }

        private async void SearchCityForecast(object sender, MouseButtonEventArgs e)
        {
            if (ClickProtection)
            {
                await GetWeathForecast();
            }
        }

        private void StopAndClearTimer()
        {
            if (timer != null && timer.IsEnabled)
            {
                timer.Stop();
            }

            timer = null;
        }

        private async Task GetWeathForecast()
        {
            ClickProtection = false;

            cancellationTokenSource?.Cancel();

            cancellationTokenSource = new CancellationTokenSource();

            string searchText = SearchTextBox.Text;

            if (string.IsNullOrEmpty(searchText))
            {
                searchText = CityXAML.Text;
            }

            try
            {
                WeatherInfoDto weatherInfoDto = await GetMainWeatherForecast(searchText);
                ChangeWeatherIcon(weatherInfoDto);
                await GetCityPhotos(cancellationTokenSource.Token);
            }

            catch (HttpRequestException)
            {
                TakeBorderAndErrorText(ErrorConnectionTextXAML);
                NoConnectionXAML.Margin = new Thickness(0, 192, 124, 0);
            }

            catch (TimeoutException)
            {
                TakeBorderAndErrorText(ErrorServerTextXAML);
                NoConnectionXAML.Margin = new Thickness(0, 195, 114, 0);
            }

            catch (Exception)
            {
                TakeBorderAndErrorText(ErrorAnotherTextXAML);

                NoConnectionXAML.Margin = new Thickness(0, 193, 131, 0);
            }
            ClickProtection = true;
            return;
        }

        private async Task GetCityPhotos(CancellationToken cancellationToken)
        {
            var pexelsClient = new PexelsClient(PexelsApiKey);

            try
            {
                result = await pexelsClient.SearchPhotosAsync(CityXAML.Text);

                if (result.photos.Count != 0)
                {
                    await LoopCityPhotos(result, cancellationToken);
                }

                else
                {
                    ChangeBorderImageVisibility(BorderCityImageThird);
                }
            }
            catch (Exception)
            {
                ChangeBorderImageVisibility(BorderCityImageThird);
            }
        }

        private async Task LoopCityPhotos(PhotoPage result, CancellationToken cancellationToken)
        {
            bool photoChange = true;
            HttpClient httpClient = new HttpClient();


            while (!cancellationToken.IsCancellationRequested)
            {
                ClickProtection = true;

                foreach (var photo in result.photos)
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        break;
                    }

                    BitmapImage bitmap = null;
                    var loadImageTask = LoadBitmapImageAsync(photo.source.original, httpClient);
                    var timeoutTask = Task.Delay(3000);

                    var completedTask = await Task.WhenAny(loadImageTask, timeoutTask);

                    if (completedTask == loadImageTask)
                    {
                        bitmap = await loadImageTask; 
                    }


                    if (bitmap == null)
                    {
                        BorderCityImageThird.Visibility = Visibility.Visible;
                        continue;
                    }

                    if (photoChange)
                    {
                        FillImageInBorder(PhotoCityXAML, bitmap);

                        if (cancellationToken.IsCancellationRequested) return;

                        ChangeBorderVisibility(BorderCityImageFirst, photoChange);
                        await Task.Delay(2000);
                    }

                    else
                    {
                        FillImageInBorder(PhotoCityXAMLSecond, bitmap);

                        if (cancellationToken.IsCancellationRequested) return;

                        ChangeBorderVisibility(BorderCityImageSecond, photoChange);
                        await Task.Delay(2000);
                    }
                    photoChange = !photoChange;

                }
            }
        }

        private async Task<BitmapImage> LoadBitmapImageAsync(string imageUrl, HttpClient httpClient)
        {
            try
            {
                using (HttpResponseMessage response = await httpClient.GetAsync(imageUrl))
                {
                    response.EnsureSuccessStatusCode();

                    using (Stream stream = await response.Content.ReadAsStreamAsync())
                    {
                        var bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.CacheOption = BitmapCacheOption.OnLoad;
                        bitmap.StreamSource = stream;
                        bitmap.EndInit();
                        bitmap.Freeze();
                        return bitmap;
                    }
                }
            }
            catch
            {
                return null;
            }
        }


        private void ChangeBorderVisibility(Border border, bool photoChange)
        {
            BorderCityImageThird.Visibility = Visibility.Hidden;
            BorderCityImageSecond.Visibility = Visibility.Hidden;
            BorderCityImageFirst.Visibility = Visibility.Hidden;
            border.Visibility = Visibility.Visible;
        }

        private void FillImageInBorder(System.Windows.Controls.Image image, BitmapImage bitmap)
        {
            image.Source = bitmap;
            image.Stretch = Stretch.Fill;
        }

        private void ChangeWeatherIcon(WeatherInfoDto weatherInfoDto)
        {
            switch (weatherInfoDto.Main)
            {
                case "Clouds":
                    SetWeatherIcon(Clouds);
                    break;
                case "Rain":
                    SetWeatherIcon(Rain);
                    break;
                case "Thunderstorm":
                    SetWeatherIcon(Thunderstorm);
                    break;
                case "Clear":
                    SetWeatherIcon(Clear);
                    break;
                case "Snow":
                    SetWeatherIcon(Snow);
                    break;

                default:
                    break;
            }
        }

        private async Task<WeatherInfoDto> GetMainWeatherForecast(string searchText)
        {
            var weatherInfoDto = await WeathForecastVM.CityInformationHandler(searchText, CityXAML.Text);

            SearchTextBox.Text = "";

            TimeXAML.Text = weatherInfoDto.CityCurrentTime.ToString()?.Substring(0, 5);
            _cityTime = TimeXAML.Text == null ? "12:51" : TimeXAML.Text;

            StopAndClearTimer();

            StartTimer();

            CityXAML.Text = weatherInfoDto.City;
            TempXAML.Text = weatherInfoDto.CelsiusTemperature.ToString() + "°C";
            WindXAML.Text = weatherInfoDto.WindSpeed.ToString() + " m/s";
            HumidXAML.Text = weatherInfoDto.AirHumidity.ToString() + "%";
            return weatherInfoDto;
        }

        private void ChangeBorderImageVisibility(Border border)
        {
            BorderCityImageFirst.Visibility = Visibility.Hidden;
            BorderCityImageSecond.Visibility = Visibility.Hidden;
            BorderCityImageThird.Visibility = Visibility.Hidden;
            border.Visibility = Visibility.Visible;
        }

        private void TakeBorderAndErrorText(TextBlock textBlock)
        {
            BorderXAML.Visibility = Visibility.Visible;
            ErrorConnectionTextXAML.Visibility = Visibility.Hidden;
            ErrorServerTextXAML.Visibility = Visibility.Hidden;
            ErrorAnotherTextXAML.Visibility = Visibility.Hidden;
            textBlock.Visibility = Visibility.Visible;
        }

        private void SetWeatherIcon(System.Windows.Controls.Image image)
        {
            Clouds.Visibility = Visibility.Hidden;
            Rain.Visibility = Visibility.Hidden;
            Thunderstorm.Visibility = Visibility.Hidden;
            Clear.Visibility = Visibility.Hidden;
            Snow.Visibility = Visibility.Hidden;
            image.Visibility = Visibility.Visible;
        }

        private void CloseApp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void CloseButtonErrorConnection(object sender, MouseButtonEventArgs e)
        {
            BorderXAML.Visibility = Visibility.Hidden;
        }
    }
}
