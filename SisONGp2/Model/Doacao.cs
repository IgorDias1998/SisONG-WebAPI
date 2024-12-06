using System.Text.Json.Serialization;
using System.Text.Json;

namespace SisONGp2.Model
{
    public class Doacao
    {
        public int DoacaoId { get; set; }
        public decimal ValorDoacao { get; set; }

        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime DataDoacao { get; set; }

        //Relacionamento com o doador
        public int DoadorId { get; set; }
    }

    public class DateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.ParseExact(reader.GetString(), "dd/MM/yyyy", null);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("dd/MM/yyyy"));
        }
    }
}
