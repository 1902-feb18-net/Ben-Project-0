using System;
using System.Collections.Generic;

namespace Project0.Context
{
    public partial class OrderGames
    {
        public int OrderId { get; set; }
        public int GameId { get; set; }
        public int GameQuantity { get; set; }
        public int Edition { get; set; }

        public virtual Games Game { get; set; }
        public virtual Orders Order { get; set; }
    }
}
