using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace FlickrSlideshow_1._0
{
    public class CustomPresetButton : Button
    {
        double[] buttonMargin = { 10, 10 };
        double buttonWidth = 30;
        double buttonHeight = 30;

        string presetName;
        string flikrGalleryId;
        double transitionTimeInMilliseconds;
        double animationTimeInMilliseconds;

        //WHEN ADDING MENU ITEMS
        //add to the end of the list
        //not the start of the list, because reference to the contextmenuitems are hard coded list indexes
        List<MenuItem> menuItemList = new List<MenuItem>()
        {
            new MenuItem(){ Header = "Edit" },
            new MenuItem(){ Header = "Delete" }
        };

        public string PresetName
        {
            get { return presetName; }
            set { presetName = value; Content = value.Substring(0, 1); }
        }

        public string FlikrGalleryId
        {
            get { return flikrGalleryId; }
            set { flikrGalleryId = value; }
        }

        public double TransitionTimeInMilliSeconds //putting transition time at 0 breaks it
        {
            get { return transitionTimeInMilliseconds; }
            set
            {
                transitionTimeInMilliseconds = value > Properties.Settings.Default.minTransitionTimeInSeconds * 1000 ? value :
                  Properties.Settings.Default.minTransitionTimeInSeconds * 1000;
            }
        }

        public double TransitionTimeInSeconds
        {
            get { return transitionTimeInMilliseconds / 1000; }
            set { transitionTimeInMilliseconds = value * 1000; }
        }

        public double AnimationTimeInMilliseconds //putting transition time at 0 breaks it
        {
            get { return animationTimeInMilliseconds; }
            set
            {
                animationTimeInMilliseconds = value > Properties.Settings.Default.minAnimationTimeInSeconds * 1000 ? value :
                  Properties.Settings.Default.minAnimationTimeInSeconds * 1000;
            }
        }

        public double AnimationTimeInSeconds
        {
            get { return AnimationTimeInMilliseconds / 1000; }
            set { AnimationTimeInMilliseconds = value * 1000; }
        }

        private CustomPresetButton lastSelectedPresetButton
        {
            get { return Properties.Settings.Default.LastSelectedPresetButton; }
            set { Properties.Settings.Default.LastSelectedPresetButton = value; }
        }

        public CustomPresetButton()
        {
            //Intentionally Empty Constructor
        }

        public static CustomPresetButton GetDefaultPresetButton()
        {
            return new CustomPresetButton(Properties.Settings.Default.defaultGalleryName, Properties.Settings.Default.defaultGalleryId,
                Properties.Settings.Default.defaultTransitionTimeInSeconds, Properties.Settings.Default.defaultTransitionTimeInSeconds);
        }

        public CustomPresetButton(string presetName, string galleryId, double? transitionTimeInSeconds, double? animationTimeInSeconds)
        {
            Click += btnCustomPresetButton_Click;
            menuItemList[0].Click += btnCustomPresetButton_ContextMenu_Edit;
            menuItemList[1].Click += btnCustomPresetButton_ContextMenu_Delete;

            Margin = new Thickness(buttonMargin[0], buttonMargin[1], buttonMargin[0], buttonMargin[1]);
            Height = buttonHeight;
            Width = buttonWidth;

            ContextMenu = new ContextMenu();
            foreach (var item in menuItemList)
            {
                ContextMenu.Items.Add(item);
            }

            PresetName = presetName.Equals("") ? Properties.Settings.Default.defaultGalleryName : presetName;
            FlikrGalleryId = galleryId.Equals("") ? Properties.Settings.Default.defaultGalleryId : galleryId;
            TransitionTimeInSeconds = transitionTimeInSeconds == null ? Properties.Settings.Default.defaultTransitionTimeInSeconds :
                (double)transitionTimeInSeconds;
            AnimationTimeInSeconds = animationTimeInSeconds == null ? Properties.Settings.Default.defaultAnimationTimeInSeconds :
                (double)animationTimeInSeconds;
        }

        private void btnCustomPresetButton_Click(object sender, RoutedEventArgs e)
        {
            lastSelectedPresetButton = this;
        }

        private async void btnCustomPresetButton_ContextMenu_Edit(object sender, RoutedEventArgs e)
        {
            var presetButton = await new CreatePresetWindow(this).CreatePresetAsync();

            PresetName = presetButton.presetName;
            FlikrGalleryId = presetButton.flikrGalleryId;
            TransitionTimeInSeconds = presetButton.TransitionTimeInSeconds;
            AnimationTimeInSeconds = presetButton.AnimationTimeInSeconds;
        }

        private void btnCustomPresetButton_ContextMenu_Delete(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.CustomPresetButtonList.RemoveAt(
                Properties.Settings.Default.CustomPresetButtonList.IndexOf(this));

            this.Visibility = Visibility.Collapsed;
        }

        public SerializableCustomPresetButton GetAsSerializable()
        {
            return new SerializableCustomPresetButton(this);
        }
    }
}
