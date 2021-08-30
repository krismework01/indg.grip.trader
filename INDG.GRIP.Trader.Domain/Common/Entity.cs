using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace INDG.GRIP.Trader.Domain.Common
{
    public abstract class Entity
    {
        [NotMapped]
        [JsonIgnore]
        public bool IsNew { get; protected set; }

        public DateTime Created { get; set; } 
    }
}