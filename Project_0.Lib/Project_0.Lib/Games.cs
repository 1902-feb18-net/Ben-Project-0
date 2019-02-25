using System;

namespace Project_0.Lib
{
    public abstract class Games
    {

        protected virtual string Name { get; set; } //Name of game
        protected virtual double Cost { get; set; } //Base cost of game
        protected virtual double AdvancedCost { get; set; } //Advanced cost of game
        protected virtual double DeluxeCost { get; set; } //Delux cost of game

    }
}
