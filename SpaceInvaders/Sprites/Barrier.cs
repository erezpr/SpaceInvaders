using Infrastructure.ObjectModel;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;

namespace SpaceInvaders.Sprites
{
    public class Barrier : Sprite, ICollidable2D
    {
        private const string k_AssetName = @"Sprites\Barrier_44x32";
        private float m_DistanceLeftToChangeDirection;
        private IPixelCollisionManager m_PixelCollisionManager;
        private float m_Direction = 1;
        private Color[] m_Pixels;
        private readonly Game r_Game;

        public Barrier(Game i_Game) : base(k_AssetName, i_Game)
        {
            r_Game = i_Game;
            CollidableType = Infrastructure.CollidableType.Objective;
            Velocity = new Vector2(0, 0);
        }

        public override void Initialize()
        {
            base.Initialize();
            Texture = this.Clone();
            m_DistanceLeftToChangeDirection = Width / 2;
            m_PixelCollisionManager = r_Game.Services.GetService(typeof(IPixelCollisionManager)) as IPixelCollisionManager;
        }

        public override void Update(GameTime i_GameTime)
        {
            base.Update(i_GameTime);
            m_DistanceLeftToChangeDirection -= Velocity.X * m_Direction * (float)i_GameTime.ElapsedGameTime.TotalSeconds;

            if (m_DistanceLeftToChangeDirection <= 0)
            {
                m_DistanceLeftToChangeDirection = Width;
                m_Direction *= -1;
                Velocity *= -1;
            }
        }


        private bool isTranspert()
        {
            bool result = true;

            if (m_Pixels == null)
            {
                m_Pixels = new Color[Texture.Width * Texture.Height];
                Texture.GetData(m_Pixels);
            }
            else
            {
                foreach (Color pixel in m_Pixels)
                {
                    if (pixel.A != 0)
                    {
                        result = false;
                        break;
                    }
                }
            }

            return result;
        }

        public override void Collided(ICollidable i_Collidable)
        {
            Sprite sprite = i_Collidable as Sprite;
            if (Animations.Enabled == false)
            {
                if (sprite.CollidableType != this.CollidableType)
                {
                    if (sprite is Enemy)
                    {
                        if (isTranspert() == false)
                        {
                            m_PixelCollisionManager.PerPixelCollision(i_Collidable as ICollidable2D, this, false, true);
                        }
                    }
                }
            }
        }
    }
}
