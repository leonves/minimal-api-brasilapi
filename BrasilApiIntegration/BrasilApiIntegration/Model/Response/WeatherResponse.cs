using BrasilApiIntegration.Model.Core;
using System.Text.Json.Serialization;

namespace BrasilApiIntegration.Model.Response
{
    public class WeatherResponse : EntityResponse
    {
        [JsonPropertyName("codigo_icao")]
        public string CodigoIcao { get; set; }

        [JsonPropertyName("atualizado_em")]
        public DateTime AtualizadoEm { get; set; }

        [JsonPropertyName("pressao_atmosferica")]
        public int PressaoAtmosferica { get; set; }

        [JsonPropertyName("visibilidade")]
        public string Visibilidade { get; set; }

        [JsonPropertyName("vento")]
        public int Vento { get; set; }

        [JsonPropertyName("direcao_vento")]
        public int DirecaoVento { get; set; }

        [JsonPropertyName("umidade")]
        public int Umidade { get; set; }

        [JsonPropertyName("condicao")]
        public string Condicao { get; set; }

        [JsonPropertyName("condicao_Desc")]
        public string CondicaoDescricao { get; set; }

        [JsonPropertyName("temp")]
        public int Temp { get; set; }
    }
}
