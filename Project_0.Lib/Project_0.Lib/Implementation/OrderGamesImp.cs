﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Project_0.Lib
{
    public class OrderGamesImp
    {
        public OrderGamesImp()
        {

        }

        public OrderGamesImp(int p_OrderId, int p_GameId, int p_GameQuantity, int p_edition)
        {
            OrderId = p_OrderId;
            GameId = p_GameId;
            GameQuantity = p_GameQuantity;
            Edition = p_edition;
            //Price = p_price;
        }

        public OrderGamesImp(int p_OrderId, int p_GameId, int p_GameQuantity, int p_edition, decimal p_price)
        {
            OrderId = p_OrderId;
            GameId = p_GameId;
            GameQuantity = p_GameQuantity;
            Edition = p_edition;
            Price = p_price;
        }

        public OrderGamesImp(int p_OrderId, int p_GameId, int p_GameQuantity, int p_edition, GamesImp p_game)
        {
            OrderId = p_OrderId;
            GameId = p_GameId;
            GameQuantity = p_GameQuantity;
            Edition = p_edition;
            Game = p_game;
        }

        public int OrderId { get; set; }
        public int GameId { get; set; }
        public int GameQuantity { get; set; }
        public int Edition { get; set; } //value must be 1, 2, or 3 (1 = standard, 2 = advanced, 3 = deluxe)
        public decimal Price { get; set; }

        public GamesImp Game { get; set; }

        public decimal GetCostOfPurchase()
        {
            switch(Edition)
            {
                case 1:
                    Price = GameQuantity * Game.Cost;
                    break;
                case 2:
                    Price = GameQuantity * Game.AdvancedCost;
                    break;
                case 3:
                    Price = GameQuantity * (Game.AdvancedCost + 10.00m);
                    break;
                default:
                    throw new ArgumentException("Edition must be 1 - 3");
            }
            return Price;
        }
    }
}
