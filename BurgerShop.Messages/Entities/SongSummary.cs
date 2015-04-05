using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerShop.Messages.Entities
{
    public class SongSummary
    {
        public int Order { get; set; }

        public string Title { get; set; }

        public long PlayCount { get; set; }
    }
}
