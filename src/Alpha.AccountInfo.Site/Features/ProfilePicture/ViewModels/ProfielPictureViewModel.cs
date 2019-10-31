namespace Alpha.AccountInfo.Site.Features.ProfilePicture.ViewModels
{
    using System.Collections.Generic;

    public class ProfielPictureViewModel
    {
        public ProfielPictureViewModel(
            IEnumerable<DefaultProfielPictureViewModel> images,
            DefaultProfielPictureViewModel actorAvatar)
        {
            ActorAvatar = actorAvatar;
            Images = images;
        }

        public DefaultProfielPictureViewModel ActorAvatar { get; private set; }

        public IEnumerable<DefaultProfielPictureViewModel> Images { get; private set; }
    }
}