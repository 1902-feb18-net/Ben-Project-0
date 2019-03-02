using System;
using System.Collections.Generic;

namespace Project0.Context
{
    public partial class Inventory
    {
        public int StoreId { get; set; }
        public int GameId { get; set; }
        public int GameRemaining { get; set; }

        public virtual Games Game { get; set; }
        public virtual Stores Store { get; set; }
    }
}
