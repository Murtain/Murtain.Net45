using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using IdentityServer3.Core;
using IdentityServer3.Core.Services;
using Murtain.Dependency;
using Murtain.Localization;
using Murtain.Domain.Services;

using Constants = Murtain.OAuth2.Core.Constants;

namespace Murtain.OAuth2.Web.Configuration.Services
{

    public class IdentityServerLocallizationManager : IIdentityServerLocallizationManager
    {
        public ILocalizationManager LocalizationManager { get; set; }

        public IdentityServerLocallizationManager()
        {
            LocalizationManager = NullLocalizationManager.Instance;
        }

        public virtual string GetString(string category, string id)
        {
            switch (category)
            {
                case Constants.Localization.LocalizationCategories.Messages:
                    return LocalizationManager.GetSource(Constants.Localization.SourceName.Messages).GetString(id);
                case Constants.Localization.LocalizationCategories.Events:
                    return LocalizationManager.GetSource(Constants.Localization.SourceName.Events).GetString(id);
                case Constants.Localization.LocalizationCategories.Scopes:
                    return LocalizationManager.GetSource(Constants.Localization.SourceName.Scopes).GetString(id);
            }
            return null;
        }
    }


    public interface IIdentityServerLocallizationManager : ILocalizationService, IDependency
    {
        string GetString(string category, string id);
    }
}