namespace Alpha.AccountInfo.Site.Features.SearchAccount.ViewModels
{
    using Alpha.AccountInfo.Features.UnsuspendDownlines.AppResources;
    using Alpha.AccountInfo.Features.UnsuspendDownlines.Models;
    using Alpha.Products.Core.Services;
    using System;
    using System.Collections.Generic;
    using System.Resources;

    public class UnsuspendMultipleViewModel
    {
        public UnsuspendMultipleViewModel(
            string language,
            IEnumerable<UnsuspendMultipleDataViewModel> data,
            Func<string, ResourceManager, string> convertResourcesJson = null)
        {
            Data = data;

            var ConvertResourcesJson = convertResourcesJson ?? SimpleServiceLocator.GetService<Func<string, ResourceManager, string>>();
            Resources = ConvertResourcesJson(language, UnsuspendDownlinesResources.ResourceManager);
        }

        public IEnumerable<UnsuspendMultipleDataViewModel> Data { get; private set; }

        public string Resources { get; private set; }
    }

    public class UnsuspendMultipleDataViewModel
    {
        public UnsuspendMultipleDataViewModel(
            int custId,
            byte uplineRoleId,
            string username,
            bool suspended,
            byte roleId,
            IEnumerable<UnsuspendMultipleByUserModel> unsuspendByLevel)
        {
            CustId = custId;
            UplineRoleId = uplineRoleId;
            Username = username;
            Suspended = suspended;
            RoleId = roleId;
            UnsuspendByLevel = unsuspendByLevel;
        }

        public int CustId { get; private set; }

        public byte UplineRoleId { get; private set; }

        public string Username { get; private set; }

        public bool Suspended { get; private set; }

        public byte RoleId { get; private set; }

        public IEnumerable<UnsuspendMultipleByUserModel> UnsuspendByLevel { get; private set; }
    }
}
