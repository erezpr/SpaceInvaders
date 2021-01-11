using Infrastructure.ObjectModel;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using System;

namespace SpaceInvaders.Sprites
{
    public class Bullet : Sprite, ICollidable2D
    {
        public event Action<Bullet> OutOfScreenBounds;
        public event Action<ICollidable, ICollidable> OtherGroupCollided;
        private const string k_AssetName = @"Sprites\Bullet";
        private readonly IPixelCollisionManager r_PixelCollisionManager;
        private readonly ISoundManager r_SoundManager;
        private readonly Game r_Game;
        private float m_LastYposition;
        private bool m_IsPixelCollided = false;
        private float m_Overlaping;

        public Bullet(Game i_Game) : base(k_AssetName, i_Game)
        {
            r_Game = i_Game;
            r_PixelCollisionManager = r_Game.Services.GetService(typeof(IPixelCollisionManager)) as IPixelCollisionManager;
            r_SoundManager = r_Game.Services.GetService(typeof(ISoundManager)) as ISoundManager;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime i_GameTime)
        {
            Rectangle rectangle = Rectangle.Intersect(this.Game.GraphicsDevice.Viewport.Bounds, this.Bounds);

            if (rectangle == Rectangle.Empty)
            {
                OutOfScreenBounds?.Invoke(this);
            }

            m_LastYposition = Position.Y;
            base.Update(i_GameTime);
        }

        public override void Collided(ICollidable i_Collidable)
        {
            if (i_Collidable is Sprite sprite && sprite.CollidableType != this.CollidableType)
            {
                if (r_PixelCollisionManager.PerPixelCollision(i_Collidable as ICollidable2D, this))
                {
                    OtherGroupCollided?.Invoke(this, i_Collidable);
                    if (sprite is Barrier)
                    {
                        handleBulletHitBarrier(i_Collidable);
                    }
                }
            }
        }

        private void handleBulletHitBarrier(ICollidable i_Collidable)
        {
            if (m_IsPixelCollided || r_PixelCollisionManager.PerPixelCollision(i_Collidable as ICollidable2D, this))
            {
                m_IsPixelCollided = true;
                m_Overlaping += Math.Abs(m_LastYposition - Position.Y);

                if (m_Overlaping >= this.Height * 0.7)
                {
                    r_SoundManager.PlaySound("BarrierHit");
                    r_PixelCollisionManager.PerPixelCollision(i_Collidable as ICollidable2D, this, i_PixelEatingTextureA: true, i_PixelEatingTextureB: false);
                    OutOfScreenBounds?.Invoke(this);
                    m_IsPixelCollided = false;
                    m_Overlaping = 0;
                }
            }
        }
    }
}
