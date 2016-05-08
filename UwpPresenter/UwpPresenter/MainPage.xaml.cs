using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            var imageList = new List<string>();

            GetFiles();

            imageList.Add(@"Assets/IotHubTalk/iotHubTalk-png.001.png");
            imageList.Add(@"Assets/IotHubTalk/iotHubTalk-png.002.png");

            //flipView.Items.Add("Assets/IotHubTalk/iotHubTalk-png.001.png");
            //flipView.Items.Add("Assets/IotHubTalk/iotHubTalk-png.002.png");

            //foreach (var item in imageList)
            //{
            //    var uri = new Uri(string.Format(@"ms-appx:/{0}", item));
            //    var img = new BitmapImage(uri);
            //    flipView.Items.Add(img);
            //}
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
                var file = item.Name;
                var path = item.Path;

                //var uri = new Uri(string.Format(@"ms-appx:/Assests/IotHubTalk/{0}", file));
                //var img = new BitmapImage(uri);

                using(var fileStream = await item.OpenReadAsync()){
                    var bitmapImage = new BitmapImage();
                    bitmapImage.SetSource(fileStream);
                    imageList.Add(bitmapImage);
                    //flipView.Items.Add(bitmapImage);
                }
            }

            flipView.ItemsSource = imageList;
        }

        private void flipView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
