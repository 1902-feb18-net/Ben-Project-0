using System;

namespace Project_0.Lib
{
    public abstract class Games
    {

        public virtual string Name { get; set; } //Name of game
        public virtual double Cost { get; set; } //Base cost of game
        public virtual double AdvancedCost { get; set; } //Advanced cost of game
        public virtual double DeluxeCost { get; set; } //Delux cost of game

    }
}
