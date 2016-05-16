using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Sensors;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UwpPresenter
{
    public sealed partial class MainPage : Page
    {
        public bool IsPhone;
        public string PhoneApi = "Windows.Phone.UI.Input.HardwareButtons";

        public bool IsIot;

        public bool IsDeskTop;

        public bool IsXbox;

        public MainPage()
        {
            this.InitializeComponent();

            GetDevice();

            ConfigurePage();

            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent(PhoneApi))
            {
                Windows.Phone.UI.Input.HardwareButtons.CameraPressed += HardwareButtons_CameraPressed;
            }

            SetupAccelerometer();

            GetFiles();
        }

        private void ConfigurePage()
        {
            if (IsIot)
            {
                Device.Text = "Iot";
                DisableAccelerometer();
            }

            if (IsPhone)
            {
                Device.Text = "Phone";
            }

            if (IsXbox)
            {
                Device.Text = "Xbox";
                DisableAccelerometer();
            }

            if (IsDeskTop)
            {
                Device.Text = "Desktop";
                DisableAccelerometer();
            }
        }

        private void DisableAccelerometer()
        {
            AccelX.Visibility = Visibility.Collapsed;
            AccelY.Visibility = Visibility.Collapsed;
            AccelZ.Visibility = Visibility.Collapsed;
        }

        private void GetDevice()
        {
            var device = Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily;

            switch (device)
            {
                case "Windows.Phone":
                    IsPhone = true;
                    break;
                case "Windows.IoT":
                    IsIot = true;
                    break;
                case "Windows.XBox":
                    IsXbox = true;
                    break;
                default:
                    IsDeskTop = true;
                    break;
            }
        }

        private void HardwareButtons_CameraPressed(object sender, Windows.Phone.UI.Input.CameraEventArgs e)
        {
            var stff = "this";
        }

        private void SetupAccelerometer()
        {
            var accelerometer = Accelerometer.GetDefault();     

            if (accelerometer != null)
            {
                accelerometer.ReportInterval = 1000;
                accelerometer.ReadingChanged += Accelerometer_ReadingChanged;
            }
        }

        private void Accelerometer_ReadingChanged(Accelerometer sender, AccelerometerReadingChangedEventArgs args)
        {
            var task = Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                AccelX.Text = args.Reading.AccelerationX.ToString();
                AccelY.Text = args.Reading.AccelerationY.ToString();
                AccelZ.Text = args.Reading.AccelerationZ.ToString();
            });
        }

        private async System.Threading.Tasks.Task GetFiles()
        {
            var appInstalledFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            var assets = await appInstalledFolder.GetFolderAsync("Assets");
            var folder = await assets.GetFolderAsync("IotHubTalk");
            var files = await folder.GetFilesAsync();

            var imageList = new List<BitmapImage>();
            foreach (var item in files)
            {
                using(var fileStream = await item.OpenReadAsync()){
                    var bitmapImage = new BitmapImage();
                    bitmapImage.SetSource(fileStream);
                    imageList.Add(bitmapImage);
                }
            }

            flipView.ItemsSource = imageList;
        }

        private void flipView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
