using Infrastructure;
using Infrastructure.ObjectModel;
using Infrastructure.ObjectModel.Animators.ConcreteAnimators;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using SpaceInvaders.Managers;

using System;

namespace SpaceInvaders.Sprites
{
    public class SpaceShip : Sprite, ICollidable2D
    {
        public int Score
        {
            get
            {
                return m_Score;
            }
            private set
            {
                m_Score = value;
                if (m_Score < 0)
                {
                    m_Score = 0;
                }
            }
        }

        public int PlayerNumber { get; private set; }
        public int Lives { get; private set; }
        public ShootingManager Gun { get; }
        public event Action outOfLives;
        public event Action<SpaceShip> LivesChanged;
        public event Action<SpaceShip> ScoreChanged;
        public Vector2 InitialPosition { get; set; }
        private IInputManager m_InputManager;
        private ISoundManager m_SoundManager;
        private BlinkAnimator m_BlinkAnimation;
        private readonly MovementKeys r_MovementKeys;
        private const int k_MaxBullets = 2;
        private const int k_LifeLostValue = 1200;
        private int m_Score = 0;

        public SpaceShip(Game i_Game, string i_AssetName, int i_PlayerNumber, MovementKeys i_MovementKeys) : base(i_AssetName, i_Game)
        {
            Lives = 3;
            PlayerNumber = i_PlayerNumber;
            r_MovementKeys = i_MovementKeys;
            CollidableType = CollidableType.Friendly;
            Gun = new ShootingManager(i_Game, Color.Red, new Vector2(0, -160), CollidableType.Friendly);
        }

        public override void Initialize()
        {
            base.Initialize();
            m_BlinkAnimation = new BlinkAnimator("Blink", TimeSpan.FromSeconds(0.083), TimeSpan.FromSeconds(2.5));
            m_BlinkAnimation.Finished += (s, e) => { Animations.Enabled = false; };
            Animations.Add(m_BlinkAnimation);
            m_InputManager = Game.Services.GetService(typeof(IInputManager)) as IInputManager;
            m_SoundManager = Game.Services.GetService(typeof(ISoundManager)) as ISoundManager;
            initPosition();
        }
        protected override void LoadContent()
        {
            base.LoadContent();
        }
        private void initPosition()
        {
            Position = InitialPosition;
        }

        public override void Update(GameTime i_GameTime)
        {
            if (Lives > 0)
            {
                float leftScreen = 0;
                float rigthScreen = Game.GraphicsDevice.Viewport.Width - Texture.Width;

                keyboardMovments();

                float PositionRange = MathHelper.Clamp(Position.X, 0, rigthScreen);
                if (PositionRange == rigthScreen)
                {
                    Position = new Vector2(rigthScreen, Position.Y);
                }

                if (PositionRange == leftScreen)
                {
                    Position = new Vector2(leftScreen, Position.Y);
                }

                if (m_InputManager.KeyPressed(r_MovementKeys.Shoot))
                {
                    shoot();
                }
            }

            base.Update(i_GameTime);
        }

        private void shoot()
        {
            if (Gun.Bullets.Count < k_MaxBullets)
            {
                m_SoundManager.PlaySound("SSGunShot");

                Bullet bullet = Gun.Shoot(new Vector2(Position.X + Bounds.Width / 2, Position.Y));
                bullet.OtherGroupCollided += bulletCollided;
                bullet.OutOfScreenBounds += unsignOwnBullet;

            }
        }

        private void unsignOwnBullet(Bullet i_Bullet)
        {
            i_Bullet.OtherGroupCollided -= bulletCollided;
            i_Bullet.OutOfScreenBounds -= unsignOwnBullet;
            Gun.DisposeBullet(i_Bullet);
        }

        private void bulletCollided(object i_Sender, ICollidable i_OtherGroup)
        {

            Bullet bullet = i_Sender as Bullet;

            if (i_OtherGroup is Barrier == false)
            {
                unsignOwnBullet(bullet);

                if (i_OtherGroup is Enemy enemy && enemy.IsDying == false)
                {
                    enemy.IsDying = true;
                    Score += enemy.Value;
                    ScoreChanged?.Invoke(this);
                }

                if (i_OtherGroup is MotherShip motherShip && motherShip.IsDying == false)
                {
                    motherShip.IsDying = true;
                    Score += motherShip.Value;
                    ScoreChanged?.Invoke(this);
                }
            }
        }

        private void keyboardMovments()
        {
            if (m_InputManager.KeyboardState.IsKeyDown(r_MovementKeys.Right))
            {
                Velocity = new Vector2(145, 0);
            }
            else if (m_InputManager.KeyboardState.IsKeyDown(r_MovementKeys.Left))
            {
                Velocity = new Vector2(-145, 0);
            }
            else
            {
                Velocity = Vector2.Zero;
            }
        }

        public override void Collided(ICollidable i_Collidable)
        {
            if (Lives > 0)
            {
                if (Animations.Enabled == false)
                {
                    if (i_Collidable is Sprite sprite && sprite.CollidableType != this.CollidableType)
                    {
                        Lives--;
                        LivesChanged?.Invoke(this);
                        m_SoundManager.PlaySound("LifeDie");
                        if (Lives > 0 && !(sprite is Enemy))
                        {
                            m_BlinkAnimation.Restart();
                            Animations.Enabled = true;
                            initPosition();
                        }
                        else
                        {
                            Lives = 0;
                            Die();
                        }

                        Score -= k_LifeLostValue;
                        ScoreChanged?.Invoke(this);
                    }
                }
            }
        }

        public void Die()
        {
            FadeAnimator fadeAnimator = new FadeAnimator("Fade", false, TimeSpan.FromSeconds(2.5));
            Animations.Add(new SpinAnimator("Spin", MathHelper.TwoPi * 4, new Vector2(Width / 2, Height / 2), TimeSpan.FromSeconds(2.5)));
            Animations.Add(fadeAnimator);
            Animations.Enabled = true;
            fadeAnimator.Finished += (s, e) =>
            {
                Enabled = false;
                Visible = false;
                outOfLives?.Invoke();
            };

            this.Dispose();
        }
    }
}
