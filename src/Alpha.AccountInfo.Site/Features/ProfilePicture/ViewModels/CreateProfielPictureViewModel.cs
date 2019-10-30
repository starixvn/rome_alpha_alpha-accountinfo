namespace Alpha.AccountInfo.Site.Features.ProfilePicture.ViewModels
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.Web;

    public class CreateProfielPictureViewModel
    {
        public CreateProfielPictureViewModel(string base64Image)
        {
            Base64Image = base64Image;
            //using (Image image = Image.FromStream(file.InputStream))
            //{
            //    using (MemoryStream m = new MemoryStream())
            //    {
            //        image.Save(m, image.RawFormat);
            //        Base64Image = Convert.ToBase64String(m.ToArray());
            //    }
            //}
        }

        public string Base64Image { get; private set; }
    }
}