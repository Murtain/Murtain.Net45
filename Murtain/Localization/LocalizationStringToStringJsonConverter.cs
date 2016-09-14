using System;
using Newtonsoft.Json;
using Murtain.Localization.Settings;

namespace Murtain.Localization
{
    /// <summary>
    /// This class can be used to serialize <see cref="ILocalizationString"/> to <see cref="string"/> during serialization.
    /// It does not work for deserialization.
    /// </summary>
    public class LocalizationStringToStringJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            var localizableString = (ILocalizationString) value;
            writer.WriteValue(localizableString.Localize(new LocalizationContext(LocalizationHelper.LocalizationManager)));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof (ILocalizationString).IsAssignableFrom(objectType);
        }
    }
}