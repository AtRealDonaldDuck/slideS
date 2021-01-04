using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FlickrSlideshow_1._0
{
    [Serializable()]
    public class SerializableCustomPresetButton : ISerializable
    {
        string presetName;
        string flikrGalleryId;
        double transitionTimeInMilliseconds;
        double animationTimeInMilliseconds;

        public string PresetName
        {
            get { return presetName; }
            set { presetName = value;}
        }

        public string FlikrGalleryId
        {
            get { return flikrGalleryId; }
            set { flikrGalleryId = value; }
        }

        public double TransitionTimeInSeconds
        {
            get { return transitionTimeInMilliseconds / 1000; }
            set { transitionTimeInMilliseconds = value * 1000; }
        }

        public double AnimationTimeInSeconds
        {
            get { return animationTimeInMilliseconds / 1000; }
            set { animationTimeInMilliseconds = value * 1000; }
        }

        public static CustomPresetButton GetDefaultPresetButton()
        {
            return new CustomPresetButton("Default Gallery", "72157716671842277", 3, 1);
        }

        public SerializableCustomPresetButton()
        {

        }

        public SerializableCustomPresetButton(CustomPresetButton customPresetButton)
        {
            presetName = customPresetButton.PresetName;
            flikrGalleryId = customPresetButton.FlikrGalleryId;
            transitionTimeInMilliseconds = customPresetButton.TransitionTimeInMilliSeconds;
            animationTimeInMilliseconds = customPresetButton.AnimationTimeInMilliseconds;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("presetName", presetName);
            info.AddValue("flikrGalleryId", flikrGalleryId);
            info.AddValue("transitionTimeInMilliseconds", transitionTimeInMilliseconds);
            info.AddValue("animationTimeInMilliseconds", animationTimeInMilliseconds);
        }
    }
}
