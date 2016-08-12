using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using ZXing;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Quick_Response
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class create : Page
    {
        public static string all;
        public create()
        {
            this.InitializeComponent();
        }

        private void goback_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private async void exit_Click(object sender, RoutedEventArgs e)
        {
            var messagedialog = new MessageDialog("QR Creator & Scanner will be closed!", "Do you want to exit?");
            messagedialog.Commands.Add(new UICommand("Yes", (UICommandInvokedHandler) =>
            {
                Application.Current.Exit();
            }));
            messagedialog.Commands.Add(new UICommand("NO"));
            await messagedialog.ShowAsync();
        }



        private async void start_Click(object sender, RoutedEventArgs e)
        {
            string name1 = name.Text;
            string address1 = address.Text;
            string email1 = email.Text;
            string phone1 = phone.Text;
            string website1 = website.Text;
            string others1 = others.Text;

            all = name1 + "\n" + address1 + "\n" + phone1 + "\n" + email1 + "\n" + website1 + "\n" + others1;
            if (name1 == "" || address1 == "" || email1 == "" || phone1 == "" || website1 == "" || others1 == "")
            {
                var messagedialog = new MessageDialog("All Input fields must be filled!", "Error!");
                messagedialog.Commands.Add(new UICommand("OK"));
                await messagedialog.ShowAsync();
            }
            else
            {
                name.Visibility = Visibility.Collapsed;
                address.Visibility = Visibility.Collapsed;
                email.Visibility = Visibility.Collapsed;
                phone.Visibility = Visibility.Collapsed;
                website.Visibility = Visibility.Collapsed;
                others.Visibility = Visibility.Collapsed;
                start.Visibility = Visibility.Collapsed;
                QrCodeImg.Visibility = Visibility.Visible;


                try
                {
                    IBarcodeWriter writer = new BarcodeWriter
                    {
                        Format = BarcodeFormat.QR_CODE,//Mentioning type of bar code generation   
                        Options = new ZXing.Common.EncodingOptions
                        {
                            Height = 300,
                            Width = 300
                        },
                        Renderer = new ZXing.Rendering.PixelDataRenderer() { Foreground = Colors.Black }//Adding color QR code   
                    };


                    var result = writer.Write(all);
                    var wb = result.ToBitmap() as WriteableBitmap;
                    //Displaying QRCode Image   
                    QrCodeImg.Source = wb;
                    // Saving the QRCode








                }
                catch (Exception ex)
                {
                    this.Frame.Navigate(typeof(MainPage));
                }
            }


        }
    }
}
