using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BurgerShop.Messages.Entities;

namespace BurgerShop.Messages.Events
{
    public class NewTop10
    {
        public IEnumerable<SongSummary> SongSummaries {get; set;}

        public DateTime LastUpdated { get; set; }

        public string RoutedBy { get; set; }

        public string ComputedBy { get; set; }

        public string StoredBy { get; set; }

        public string PublishedBy { get; set; }

        public string TransportedBy { get; set; }
    }
}
