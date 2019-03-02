using System;
using System.Collections.Generic;
using System.Text;

namespace Project_0.Lib
{
    public class OrderGamesImp
    {
        public OrderGamesImp()
        {

        }

        public OrderGamesImp(int p_OrderId, int p_GameId, int p_GameQuantity)
        {
            OrderId = p_OrderId;
            GameId = p_GameId;
            GameQuantity = p_GameQuantity;
        }

        public int OrderId { get; set; }
        public int GameId { get; set; }
        public int GameQuantity { get; set; }
    }
}
