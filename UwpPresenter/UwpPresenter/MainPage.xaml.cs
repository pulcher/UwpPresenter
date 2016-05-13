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

            GetFiles();
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
            flipView.SelectedIndex = 5;
        }

        private void flipView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
