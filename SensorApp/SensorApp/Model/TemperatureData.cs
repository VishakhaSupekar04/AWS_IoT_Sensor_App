using System;
namespace SensorApp.Model
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class TemperatureData
    {
        [JsonProperty("rowId")]
        public Guid RowId { get; set; }

        [JsonProperty("temperature")]
        public double Temperature { get; set; }
    }
}
