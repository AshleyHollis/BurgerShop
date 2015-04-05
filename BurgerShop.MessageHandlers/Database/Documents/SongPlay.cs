using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerShop.MessageHandlers.Database.Documents
{
    public class SongPlay
    {
        public ObjectId Id { get; set; }

        public string Artist { get; set; }

        public DateTime RecordedAtUtc { get; set; }
    }
}
