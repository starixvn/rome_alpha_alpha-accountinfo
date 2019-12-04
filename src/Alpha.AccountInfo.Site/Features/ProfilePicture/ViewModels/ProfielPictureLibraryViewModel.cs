namespace Alpha.AccountInfo.Site.Features.ProfilePicture.ViewModels
{
    using Alpha.AccountInfo.ProfilePicture.Models;
    using JW;
    using System.Collections.Generic;

    public class ProfielPictureLibraryViewModel
    {
        public ProfielPictureLibraryViewModel(IEnumerable<ProfilePictureModel> images, Pager pager)
        {
            Images = images;
            Pager = pager;
        }

        public IEnumerable<ProfilePictureModel> Images { get; private set; }

        public Pager Pager { get; set; }
    }
}