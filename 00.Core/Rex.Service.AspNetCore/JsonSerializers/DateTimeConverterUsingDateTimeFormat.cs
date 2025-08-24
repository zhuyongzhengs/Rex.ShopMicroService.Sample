using System.Text.Json;

namespace Rex.Service.AspNetCore.JsonSerializers
{
    /// <summary>
    /// 日期格式装换器
    /// </summary>
    public class DateTimeConverterUsingDateTimeFormat : System.Text.Json.Serialization.JsonConverter<DateTime>
    {
        private readonly string _dateTimeFormat;

        public DateTimeConverterUsingDateTimeFormat(string dateTimeFormat)
        {
            _dateTimeFormat = dateTimeFormat;
        }

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.ParseExact(reader.GetString(), _dateTimeFormat, null);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(_dateTimeFormat));
        }
    }
}