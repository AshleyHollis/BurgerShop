using MongoDB.Bson;
using BurgerShop.Core.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerShop.Core.Database.Documents
{
    public class LogEntry
    {
        public ObjectId Id { get; set; }

        public DateTime RecordedAtUtc { get; set; }

        public string Host { get; set; }

        public Level Level { get; set; }

        public string Message { get; set; }

        public override string ToString()
        {
            return string.Format("{0}-{1}-{2}. {3}", Host, RecordedAtUtc, Level,  Message);
        }
    }
}
