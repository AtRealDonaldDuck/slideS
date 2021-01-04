using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlickrSlideshow_1._0
{
    class Slideshow
    {
        List<Uri> imageUriList = new List<Uri>();

        public List<Uri> ImageUriList
        {
            get { return imageUriList; }

            set { imageUriList = value; }
        }

        public async Task FillImageUriList(string galleryId)
        {
            var flikrImages = new FlikrAPI();
            imageUriList = await Task.Run(() => flikrImages.GetFlikrImageListAsync(galleryId));
        }
    }

}
