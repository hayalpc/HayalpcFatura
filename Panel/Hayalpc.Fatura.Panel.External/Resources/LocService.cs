using JsonFlatFileDataStore;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Hayalpc.Fatura.Panel.External.Resources
{
    public class LocService
    {
        private readonly IStringLocalizer _localizer;
        private DataStore store = new DataStore("language.json");

        public LocService(IStringLocalizerFactory factory)
        {
            var type = typeof(SharedResource);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            _localizer = factory.Create("SharedResource", assemblyName.Name);
        }

        public LocalizedString Get(string key)
        {
            var str = _localizer[key];
            if (str.ResourceNotFound == true)
            {
                var collection = store.GetCollection<LanguageData>();
                if (!collection.AsQueryable().Any(t => t.Key == key))
                {
                    var isSuccess = collection.InsertOne(new LanguageData { Key = key, Text = key });
                }
            }
            return str;
        }
    }

    public class LanguageData
    {
        public string Key { get; set; }
        public string Text { get; set; }
    }
}
