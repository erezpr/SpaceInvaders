using System;
using SpaceInvaders.Managers;
using Microsoft.Xna.Framework;
using Infrastructure;
using Infrastructure.ObjectModel;
using Infrastructure.ServiceInterfaces;
using Infrastructure.ObjectModel.Animators.ConcreteAnimators;
using Infrastructure.Managers;

namespace SpaceInvaders.Sprites
{
    public class Enemy : Sprite, ICollidable2D
    {
        public event Action<Enemy> IDied;
        public event Action HitBottomScreen;
        public bool IsDying { get; set; } = false;
        public ShootingManager Gun { get; }
        public int MaxBullets { get; set; } = 1;
        public float ShootingFrequency { get; set; } = 1500;
        public int Value { get; set; }
        public Vector2 positionInAsset;
        private IPixelCollisionManager m_PixelCollisionManager;
        private IRandomManager m_RandomManager;
        private ISoundManager m_SoundManager;
        private readonly Game r_Game;
        private const string k_AssetName = @"Sprites\Enemies";
        private const int k_NumOfEnemies = 3;
        private const int k_NumOfFramesPerEnemy = 2;
        private const int k_EnemyHeight = 32;
        private const int k_Spacing = 10;

        public Enemy(Game i_Game) : base(k_AssetName, i_Game)
        {
            r_Game = i_Game;
            Gun = new ShootingManager(i_Game, Color.Blue, new Vector2(0, 160), CollidableType.Hostile);
            CollidableType = CollidableType.Hostile;
        }

        public override void Initialize()
        {
            base.Initialize();
            m_PixelCollisionManager = r_Game.Services.GetService(typeof(IPixelCollisionManager)) as IPixelCollisionManager;
            m_RandomManager = r_Game.Services.GetService(typeof(IRandomManager)) as IRandomManager;
            m_SoundManager = Game.Services.GetService(typeof(ISoundManager)) as ISoundManager;
            addAnimations();
        }

        public override void Update(GameTime i_GameTime)
        {
            shoot();

            if (Position.Y + Height >= r_Game.GraphicsDevice.Viewport.Height - k_EnemyHeight - k_Spacing)
            {
                HitBottomScreen?.Invoke();
            }

            base.Update(i_GameTime);
        }

        protected override void InitSourceRectangle()
        {
            base.InitSourceRectangle();
            m_WidthBeforeScale /= k_NumOfEnemies;
            m_HeightBeforeScale /= k_NumOfFramesPerEnemy;
        }

        public void Jump(int i_Direction)
        {
            float newPositionInAsset = positionInAsset.Y == 0 ? 1 : 0;
            Position = new Vector2(Position.X + Width / 2 * i_Direction, Position.Y);
            positionInAsset = new Vector2(positionInAsset.X, newPositionInAsset);
            SourceRectangle = new Rectangle(
                (int)Width * (int)positionInAsset.X,
                (int)Height * (int)positionInAsset.Y,
                (int)Width,
                (int)Height);
        }

        public void JumpDown()
        {
            Position = new Vector2(Position.X, Position.Y + 16f);
        }

        private void shoot()
        {
            if (Gun.Bullets.Count < MaxBullets)
            {
                int randomNumber = m_RandomManager.GetRandomNumber(0, (int)ShootingFrequency);

                if (randomNumber == 0)
                {
                    m_SoundManager.PlaySound("EnemyGunShot");
                    Bullet bullet = Gun.Shoot(new Vector2(Position.X + Bounds.Width / 2, Position.Y + Height));
                    bullet.OtherGroupCollided += bulletCollided;
                    bullet.OutOfScreenBounds += disposeBullet; ;
                }
            }
        }

        private void disposeBullet(Bullet i_Bullet)
        {
            Gun.DisposeBullet(i_Bullet);
        }

        private void bulletCollided(ICollidable i_Sender, ICollidable i_Collidable)
        {
            int randomNumber;
            Bullet bullet = i_Sender as Bullet;

            if (i_Collidable is Bullet)
            {
                randomNumber = m_RandomManager.GetRandomNumber(0, 4);

                if (randomNumber == 0)
                {
                    Gun.DisposeBullet(bullet);
                }
            }

            else if (i_Collidable is Barrier == false)
            {
                Gun.DisposeBullet(bullet);
            }
        }

        public override void Collided(ICollidable i_Collidable)
        {
            Sprite sprite = i_Collidable as Sprite;

            if (Animations.Enabled == false)
            {
                if (sprite.CollidableType != this.CollidableType && sprite is Bullet == true)
                {
                    bool hit = m_PixelCollisionManager.PerPixelCollision(i_Collidable as ICollidable2D, this);
                    if (hit == true)
                    {
                        Die();
                    }
                }
            }
        }

        public void Die()
        {
            m_SoundManager.PlaySound("EnemyKill");
            Animations.Enabled = true;
            Animations["Scale"].Finished += (s, e) =>
            {
                IDied?.Invoke(this);
            };

            Dispose();
        }

        private void addAnimations()
        {
            Animations.Add(new SpinAnimator("Spin", MathHelper.TwoPi * 6, new Vector2(Width / 2, Height / 2), TimeSpan.FromSeconds(1.2)));
            Animations.Add(new ScaleAnimator("Scale", Vector2.Zero, TimeSpan.FromSeconds(1.2)));
        }
    }
}
