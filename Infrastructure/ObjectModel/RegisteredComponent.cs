﻿using Microsoft.Xna.Framework;

namespace Infrastructure.ObjectModel
{
    public class RegisteredComponent : GameComponent
    {
        public RegisteredComponent(Game i_Game, int i_UpdateOrder)
            : base(i_Game)
        {
            this.UpdateOrder = i_UpdateOrder;
            Game.Components.Add(this); // self-register as a component
        }

        public RegisteredComponent(Game i_Game)
            : this(i_Game, int.MaxValue)
        { }
    }
}
