using System.Windows;
using System.Windows.Media;
using System.Globalization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Windows.Media.Animation;

namespace FlickrSlideshow_1._0
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<CustomPresetButton> PresetButtonList
        {
            get { return FlickrSlideshow_1._0.Properties.Settings.Default.CustomPresetButtonList; }
            set { FlickrSlideshow_1._0.Properties.Settings.Default.CustomPresetButtonList = value; }
        }

        private CustomPresetButton LastSelectedCustomPresetButton
        {
            get { return FlickrSlideshow_1._0.Properties.Settings.Default.LastSelectedPresetButton; }

            set { FlickrSlideshow_1._0.Properties.Settings.Default.LastSelectedPresetButton = value; }
        }

        private bool exitSlideshow = false;

        public MainWindow()
        {
            Debug.CreateDebugFiles();
            InitializeContextMenu();
            InitializeComponent();
            foreach (var item in PresetButtonList)
            {
                stkPnlPresetButtonList.Children.Add(item);
            }
        }

        private void InitializeContextMenu()
        {
            var mnuItmOpenDebugLog = new MenuItem() { Header = "Open Debug Log"};
            mnuItmOpenDebugLog.Click += OpenLogs;

            var contextMenu = new ContextMenu();
            contextMenu.Items.Add(mnuItmOpenDebugLog);

            ContextMenu = contextMenu;
        }

        private void OpenLogs(object sender, RoutedEventArgs e)
        {
            Debug.OpenLogFile();
        }

        private void btnStartSlideshow_Click(object sender, RoutedEventArgs e)
        {
            btnStartSlideshow.Visibility = Visibility.Hidden;
            btnExitSlideshow.Visibility = Visibility.Visible;

            StartSlideshow(LastSelectedCustomPresetButton);
        }

        private async void StartSlideshow(CustomPresetButton lastSelectedPresetButton)
        {
            var slideshow = new Slideshow();
            await Task.Run(() => slideshow.FillImageUriList(lastSelectedPresetButton.FlikrGalleryId));

            while (!exitSlideshow)
            {
                foreach (var item in slideshow.ImageUriList)
                {
                    if (!exitSlideshow)
                    {
                        ChangeBackgroundImage(item);

                        await Task.Run(() => Thread.Sleep((int)lastSelectedPresetButton.TransitionTimeInMilliSeconds));
                    }
                    else
                    {
                        break;
                    }
                }
            }
            exitSlideshow = false;
        }

        public void ChangeBackgroundImage(Uri imageUri)
        {
            var fadeInAnimation = new DoubleAnimation(1, TimeSpan.FromSeconds(LastSelectedCustomPresetButton.AnimationTimeInSeconds));

            var backgrounImage = new Image()
            {
                Source = new BitmapImage(imageUri),
                Opacity = grdBackgroundImage.Children.Count == 0 ? 1 : 0
            };

            var borderFiller = ZoomAndBlurImage(new Image()
            {
                Source = new BitmapImage(imageUri),
                Opacity = grdBackgroundImage.Children.Count == 0 ? 1 : 0
            });
                        
            grdBackgroundImage.Children.Add(borderFiller);
            grdBackgroundImage.Children.Add(backgrounImage);

            if (grdBackgroundImage.Children.Count > 1)
            {
                grdBackgroundImage.Children[grdBackgroundImage.Children.Count - 1].BeginAnimation(OpacityProperty, fadeInAnimation);
                grdBackgroundImage.Children[grdBackgroundImage.Children.Count - 2].BeginAnimation(OpacityProperty, fadeInAnimation);
            }
        }

        private Image ZoomAndBlurImage(Image image)
        {
            image.Effect = new System.Windows.Media.Effects.BlurEffect() { Radius = 40 };
            image.RenderTransform = new ScaleTransform() { ScaleX = 1.2, ScaleY = 1.2, CenterX = 500, CenterY = 500 };
            image.Stretch = Stretch.Fill;
            return image;
        }

        private void btnExitSlideshow_Click(object sender, RoutedEventArgs e)
        {
            grdBackgroundImage.Children.RemoveRange(0, grdBackgroundImage.Children.Count);

            btnStartSlideshow.Visibility = Visibility.Visible;
            btnExitSlideshow.Visibility = Visibility.Hidden;

            exitSlideshow = true;
        }

        private async void btnAddPresetButton_Click(object sender, RoutedEventArgs e)
        {
            var presetButton = await new CreatePresetWindow().CreatePresetAsync();

            PresetButtonList.Add(presetButton);
            stkPnlPresetButtonList.Children.Add(presetButton);
        }

    }
}
