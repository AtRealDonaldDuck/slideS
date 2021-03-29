using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Xml.Serialization;

namespace FlickrSlideshow_1._0
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Exit(object sender, ExitEventArgs e)
        {
            var serializablePresetButtonList = new List<SerializableCustomPresetButton>();

            foreach (var item in FlickrSlideshow_1._0.Properties.Settings.Default.CustomPresetButtonList)
            {
                serializablePresetButtonList.Add(item.GetAsSerializable());
            }

            using (TextWriter textWriter = new StreamWriter("CustomPresetButtonList.xml"))
            {
                new XmlSerializer(typeof(List<SerializableCustomPresetButton>)).Serialize(textWriter, serializablePresetButtonList);
            }
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            {
                var serializablePresetButtonList = new List<SerializableCustomPresetButton>();

                if (File.Exists("CustomPresetButtonList.xml") && !File.ReadAllLines("CustomPresetButtonList.xml").Equals(""))
                    using (var streamReader = File.OpenRead("CustomPresetButtonList.xml"))
                    {
                        serializablePresetButtonList = (List<SerializableCustomPresetButton>)
                            new XmlSerializer(typeof(List<SerializableCustomPresetButton>)).Deserialize(streamReader);
                    }

                foreach (var item in serializablePresetButtonList)
                {
                    FlickrSlideshow_1._0.Properties.Settings.Default.CustomPresetButtonList.Add(new CustomPresetButton(
                        item.PresetName, item.FlikrGalleryId, item.TransitionTimeInSeconds, item.AnimationTimeInSeconds));
                }
            }
        }
    }
}
