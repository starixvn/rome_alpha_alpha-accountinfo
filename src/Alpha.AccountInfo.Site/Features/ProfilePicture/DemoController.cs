namespace uploadAvarta.Controllers
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.ProfilePicture.Models;
    using Alpha.ProfilePicture.Services;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
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
        private ProfilePictureServices profilePictureServices;

        public DemoController(ProfilePictureServices profilePictureServices)
        {
            this.profilePictureServices = profilePictureServices;
        }

        public ActionResult ConvertImageToBinary() => View("~/Features/ProfilePicture/Views/ConvertImageToBinary.cshtml");

        [HttpPost]
        public ActionResult DoConvertImageToBinary(string url)
        {
            String searchFolder = @"";
            var filters = new String[] { "jpg", "jpeg", "png", "gif", "tiff", "bmp", "svg" };
            var files = GetFilesFrom(url, filters, false);

            var fileName = new DirectoryInfo(url).Name;

            string jsonData = JsonConvert.SerializeObject(files, Formatting.None);
            System.IO.File.WriteAllText(Server.MapPath($"~/JsonData/{fileName}.txt"), jsonData);

            return Json(files);
        }

        public ProfilePictureResult[] GetFilesFrom(String searchFolder, String[] filters, bool isRecursive)
        {
            List<ProfilePictureResult> results = new List<ProfilePictureResult>();
            var searchOption = isRecursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            foreach (var filter in filters)
            {
                var paths = Directory.GetFiles(searchFolder, String.Format("*.{0}", filter), searchOption);


                foreach (var path in paths)
                {
                    var image = Image.FromFile(path);
                    byte[] imageBinary = ImageToByteArray(image);
                    var dirInfo = new DirectoryInfo(path);
                    var name = $"{dirInfo.Parent.Name}-{dirInfo.Name}";

                    var limitedImageSize = Alpha.AccountInfo.Site.Features.Shared.Helpers.ImageConverter.ToLimitedImageSize(Convert.ToBase64String(imageBinary));
                    var result = profilePictureServices.UpdateManualProfilePictureCommand(limitedImageSize, name);

                    if (!result.IsSuccess)
                    {
                        results.Add(result);
                    }

                    //var binaryData = string.Join("", $"0x{BitConverter.ToString(imageBinary).Replace("-", string.Empty)}");

                    //filesFound.Add(new ImageBinary($"{name}", binaryData));
                }
            }


            return results.ToArray();
        }


        public byte[] ImageToByteArray(Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, ImageFormat.Jpeg);
            return ms.ToArray();
        }

        public Image ByteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
    }
}