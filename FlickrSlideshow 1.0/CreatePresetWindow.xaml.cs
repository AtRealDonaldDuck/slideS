using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FlickrSlideshow_1._0
{
    /// <summary>
    /// Interaction logic for CreatePresetWindow.xaml
    /// </summary>
    public partial class CreatePresetWindow : Window
    {
        static FlickrSlideshow_1._0.Properties.Settings appSettings = FlickrSlideshow_1._0.Properties.Settings.Default;

        bool customProfileSaved = false;
        CustomPresetButton presetButton = CustomPresetButton.GetDefaultPresetButton();

        public CreatePresetWindow()
        {
            InitializeComponent();
        }

        public CreatePresetWindow(CustomPresetButton presetButton)//intended for editing
        {
            InitializeComponent();

            txtBoxPresetName.Text = presetButton.PresetName;
            txtBoxGalleryId.Text = presetButton.FlikrGalleryId;
            txtBoxTransitionTime.Text = presetButton.TransitionTimeInSeconds.ToString();
            txtBoxAnimationTime.Text = presetButton.AnimationTimeInSeconds.ToString();
        }

        /* replaced with field presetButton
        private void SavePresetButtonToSettings(CustomPresetButton presetButton)
        {
            FlickrSlideshow_1._0.Properties.Settings.Default.CustomPresetButtonList.Add(presetButton);

            customProfileSaved = true;
        }*/

        private MessageBoxResult AskUserToSave()
        {
            return MessageBox.Show(
                "You are about to Exit without saving,\nWould you like to Save your Settings?", "", MessageBoxButton.YesNoCancel);
        }

        //Ensures that only numeric data is entered into the text box
        private void txtBoxTransitionTime_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var regex = new System.Text.RegularExpressions.Regex("[^0.0-9.0]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        //Ensures that only numeric data is entered into the text box
        private void txtBoxFadeOutTime_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var regex = new System.Text.RegularExpressions.Regex("[^0.0-9.0]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveDetails();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (customProfileSaved == false)
            {
                MessageBoxResult messageBoxResults = AskUserToSave();

                switch (messageBoxResults)
                {
                    case MessageBoxResult.Cancel:
                        e.Cancel = true;
                        break;

                    case MessageBoxResult.No:
                        //Do Nothing
                        break;

                    case MessageBoxResult.Yes:
                        SaveDetails();

                        break;
                }
            }
        }

        public async Task<CustomPresetButton> CreatePresetAsync()
        {
            this.Show();

            while (customProfileSaved == false) await Task.Run(() => System.Threading.Thread.Sleep(100));

            return presetButton;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SaveDetails();
            }
        }

        private void SaveDetails()
        {
            if (new FlikrAPI().ValidateGalleryId(txtBoxGalleryId.Text.Equals("") ? appSettings.defaultGalleryId : txtBoxGalleryId.Text))
            {
                presetButton = new CustomPresetButton
                                (txtBoxPresetName.Text, txtBoxGalleryId.Text,
                                txtBoxTransitionTime.Text.Equals("") ? 3 : Double.Parse(txtBoxTransitionTime.Text),
                                txtBoxAnimationTime.Text.Equals("") ? 1 : Double.Parse(txtBoxAnimationTime.Text));

                customProfileSaved = true;

                Close();
            }
            else
            {
                MessageBox.Show("The Gallery ID you have entered is Invalid.\nGallery IDs are found in the url of a Flicr.com Gallery," +
                    "You can browse Flikr galleries by clicking the [...] button near the Gallery Id field.", "Invalid GalleryId",
                    MessageBoxButton.OK);
            }
        }

        private void btnBrowseGalleries_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(appSettings.flikrGalleryBrowserUrl);
        }
    }
}
