using Infrastructure;
using Infrastructure.Managers;
using Infrastructure.ObjectModel;
using Infrastructure.ObjectModel.Animators.ConcreteAnimators;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using System;

namespace SpaceInvaders.Sprites
{
    public class MotherShip : Sprite, ICollidable2D
    {
        public int Value { get; private set; }
        public bool IsDying { get; set; } = false;
        private const string k_AssetName = @"Sprites\MotherShip_32x120";
        private ICollisionsManager m_CollisionsManager;
        private IRandomManager m_RandomManager;
        private IPixelCollisionManager m_PixelCollisionManager;
        private ISoundManager m_SoundManager;
        private const int k_ApearanceFrequency = 50;
        private bool isAlive = true;
        private readonly Game r_Game;

        public MotherShip(Game i_Game) : base(k_AssetName, i_Game)
        {
            r_Game = i_Game;
            Value = 800;
            TintColor = Color.Red;
            Velocity = new Vector2(100, 0);
            CollidableType = CollidableType.Hostile;
        }

        public override void Initialize()
        {
            base.Initialize();
            addAnimations();
            Position = new Vector2(-Texture.Width, 32);
            m_CollisionsManager = r_Game.Services.GetService(typeof(ICollisionsManager)) as ICollisionsManager;
            m_RandomManager = r_Game.Services.GetService(typeof(IRandomManager)) as IRandomManager;
            m_PixelCollisionManager = r_Game.Services.GetService(typeof(IPixelCollisionManager)) as IPixelCollisionManager;
            m_SoundManager = Game.Services.GetService(typeof(ISoundManager)) as ISoundManager;
        }

        public override void Update(GameTime i_GameTime)
        {
            if (isAlive == true)
            {
                if (Position.X > r_Game.GraphicsDevice.Viewport.Width)
                {
                    isAlive = false;
                }
            }

            if (isAlive == false)
            {
                int randomNumber = m_RandomManager.GetRandomNumber(0, k_ApearanceFrequency);
                if (randomNumber == 0)
                {
                    isAlive = true;
                    Visible = true;
                    m_CollisionsManager.AddObjectToMonitor(this);
                    Position = new Vector2(-Texture.Width, 32);
                    IsDying = false;
                }
            }

            base.Update(i_GameTime);
        }

        public override void Collided(ICollidable i_Collidable)
        {
            if (Animations.Enabled == false)
            {
                Sprite sprite = i_Collidable as Sprite;

                if (sprite.CollidableType != this.CollidableType)
                {
                    bool hit = m_PixelCollisionManager.PerPixelCollision(i_Collidable as ICollidable2D, this);

                    if (hit == true)
                    {
                        Animations.Enabled = true;
                        Animations.Restart();
                        Die();
                    }
                }
            }
        }

        public void Die()
        {
            m_SoundManager.PlaySound("MotherShipKill");
            Animations.Enabled = true;
            Animations["Scale"].Finished += (s, e) =>
            {
                Visible = false;
                isAlive = false;
                Animations.Enabled = false;
            };

            this.Dispose();
        }

        private void addAnimations()
        {
            Animations.Add(new BlinkAnimator("Blink", TimeSpan.FromSeconds(0.1), TimeSpan.FromSeconds(2.2)));
            Animations.Add(new FadeAnimator("Fade", false, TimeSpan.FromSeconds(2.2)));
            Animations.Add(new ScaleAnimator("Scale", Vector2.Zero, TimeSpan.FromSeconds(2.2)));
        }
    }
}
