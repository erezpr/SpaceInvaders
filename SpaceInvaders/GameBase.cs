using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceInvaders
{
    public abstract class GameBase : Game
    {
        public SpriteBatch SpriteBatch { get; set; }
        public GraphicsDeviceManager Graphics { get; }

        public GameBase()
        {
            Graphics = new GraphicsDeviceManager(this);
        }
        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
        }
    }
}
