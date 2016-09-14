using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Murtain.Domain.Repositories;
using Murtain.GlobalSettings.Models;
using Murtain.GlobalSettings.Store;
using Murtain.Dependency;
using Murtain.Domain.Services;
using Murtain.Domain.UnitOfWork;
using Murtain.OAuth2.Domain.Repositories;
using Murtain.AutoMapper;

namespace Murtain.OAuth2.Core.Stores
{
    public class GlobalSettingStore : IGlobalSettingStore, IApplicationService
    {
        public IGlobalSettingRepository GlobalSettingRepository { get; set; }

        public GlobalSettingStore(IGlobalSettingRepository globalSettingRepository)
        {
            GlobalSettingRepository = globalSettingRepository;
        }

        public Task<GlobalSetting> GetSettingAsync(string name)
        {
            return Task.FromResult(GlobalSettingRepository.FirstOrDefault(x => x.Name == name).MapTo<GlobalSetting>());
        }
        public Task<GlobalSetting> AddOrUpdateSettingAsync(GlobalSetting setting)
        {
            var model = GlobalSettingRepository.FirstOrDefault(x => x.Name == setting.Name);
            if (model == null)
            {
                var result = GlobalSettingRepository.Add(setting.MapTo<Domain.Entities.GlobalSetting>()).MapTo<GlobalSetting>();
                return Task.FromResult(result);
            }

            model.Name = setting.Name;
            model.Group = setting.Group;
            model.Scope = setting.Scope;
            model.Description = setting.Description;
            model.Value = setting.Value;
            model.DisplayName = setting.DisplayName;

            return Task.FromResult(GlobalSettingRepository.Update(model).MapTo<GlobalSetting>());
        }
        public Task<GlobalSetting> DeleteSettingAsync(string name)
        {
            var result = GlobalSettingRepository.Remove(GlobalSettingRepository.FirstOrDefault(x => x.Name == name));
            return Task.FromResult(result.MapTo<GlobalSetting>());
        }
        public Task<List<GlobalSetting>> GetAllSettingsAsync()
        {
            var models = GlobalSettingRepository.Models.ToList().MapTo<List<GlobalSetting>>();

            return Task.FromResult(models);
        }
    }
}
