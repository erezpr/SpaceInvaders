using Infrastructure.ObjectModel;
using Microsoft.Xna.Framework;

namespace SpaceInvaders.Sprites
{
    public class BackGround : Sprite
    {
        public BackGround(string i_AssetName, Game i_Game) : base(i_AssetName, i_Game)
        { }

        protected override void InitBounds()
        {
            base.InitBounds();
            this.DrawOrder = int.MinValue;
        }
    }
}
