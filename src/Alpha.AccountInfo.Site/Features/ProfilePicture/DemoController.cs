namespace uploadAvarta.Controllers
{
    using Alpha.ProfilePicture.Services;
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Web;
    using System.Web.Mvc;

    public class ImageBinary
    {
        public ImageBinary(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; }
        public string Value { get; }
    }

    public class DemoController : Controller
    {
        private readonly ProfilePictureServices profilePictureServices;

        public DemoController(ProfilePictureServices profilePictureServices)
        {
            this.profilePictureServices = profilePictureServices;
        }

        public ActionResult Index() => View("~/Features/ProfilePicture/Views/ConvertImageToBinary.cshtml");

        [HttpPost]
        public ActionResult DoConvertImageToBinary()
        {
            try
            {
                HttpPostedFileBase fileBase = Request.Files[0];
                var image = Image.FromStream(fileBase.InputStream, true, true);
                byte[] imageBinary = ImageToByteArray(image);
                var limitedImageSize = Alpha.AccountInfo.Site.Features.Shared.Helpers.ImageConverter.ToLimitedImageSize(Convert.ToBase64String(imageBinary));
                var result = profilePictureServices.UpdateManualProfilePictureCommand(limitedImageSize, string.Empty);
                return Json(new { success = result.IsSuccess, error = result.Message });
            }
            catch (Exception ex)
            {
                return Json(new { success = "false", error = ex.Message });
            }
        }

        public byte[] ImageToByteArray(Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, ImageFormat.Jpeg);
            return ms.ToArray();
        }
    }
}