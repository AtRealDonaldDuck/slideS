using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace FlickrSlideshow_1._0
{
    class FlikrAPI
    {
        private readonly string flikrApiKey = "4d8263ffe9c34dc4c5cb4465c4c600b3";
        private string flikrDomainName = $"https://www.flickr.com";
        private string flikrPath = $"/services/rest";
        private string flikrParameters;
        private Uri flikrApiEndpointUri;

        public async Task<List<Uri>> GetFlikrImageListAsync(string galleryId)
        {
            var backgroundImageUriList = new List<Uri>();

            flikrParameters = $@"/?method=flickr.galleries.getPhotos&api_key={flikrApiKey}&gallery_id={galleryId}&format=json&nojsoncallback=1flickr.auth.oauth.getAccessToken&get_user_info=1&extras=url_l,media,o_dims&per_page=500";

            flikrApiEndpointUri = new Uri($"{flikrDomainName}{flikrPath}{flikrParameters}");

            backgroundImageUriList = await Task.Run(() => GetImageUrlListFromFlikrSite());

            return backgroundImageUriList;
        }

        private async Task<List<Uri>> GetImageUrlListFromFlikrSite()
        {
            var flikrJObject = await Task.Run(() => JObject.Parse(new WebClient().DownloadString(flikrApiEndpointUri)));
            var flikrUriImageList = new List<Uri>();

            foreach (var item in flikrJObject["photos"]["photo"])
            {
                flikrUriImageList.Add((Uri)item["url_l"]);
            }
            return flikrUriImageList;
        }

        public bool ValidateGalleryId(string flikrGalleryId)
        {
            var request = HttpWebRequest.Create(new Uri($@"https://www.flickr.com/photos/flickr/galleries/{flikrGalleryId}/"));
            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (Exception e)
            {
                return false;
            }

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
