using SpaceInvaders.Sprites;
using Infrastructure;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Infrastructure.ObjectModel;

namespace SpaceInvaders.Managers
{
    public class ShootingManager : CompositeDrawableComponent<Bullet>
    {
        public List<Bullet> Bullets { get; } = new List<Bullet>();
        public List<Bullet> BulletPool { get; } = new List<Bullet>();
        private readonly ICollisionsManager r_CollisionsManager;
        private readonly Game r_Game;
        private readonly Color r_BulletsColor;
        private readonly Vector2 r_BulletsSpeed;
        private readonly CollidableType r_CollidableType;

        public ShootingManager(Game i_Game, Color i_BulletColor, Vector2 i_BullesSpeed, CollidableType i_CollidableType) : 
            base(i_Game)
        {
            r_Game = i_Game;
            r_CollisionsManager = r_Game.Services.GetService(typeof(ICollisionsManager)) as ICollisionsManager;
            r_BulletsColor = i_BulletColor;
            r_BulletsSpeed = i_BullesSpeed;
            r_CollidableType = i_CollidableType;
        }

        public Bullet Shoot(Vector2 i_ShootingPoint)
        {
            Bullet bullet;

            if (BulletPool.Count == 0)
            {
                bullet = new Bullet(r_Game)
                {
                    Velocity = r_BulletsSpeed,
                    TintColor = r_BulletsColor
                };

                this.Add(bullet);
                bullet.Initialize();
                bullet.CollidableType = r_CollidableType;
            }
            else
            {
                bullet = BulletPool[0];
                bullet.Enabled = true;
                bullet.Visible = true;
                r_CollisionsManager.AddObjectToMonitor(bullet);
                BulletPool.RemoveAt(0);
            }

            bullet.Position = new Vector2(i_ShootingPoint.X - bullet.Texture.Width / 2, i_ShootingPoint.Y - bullet.Texture.Height);
            if (bullet != null)
            {
                Bullets.Add(bullet);
            }

            return bullet;
        }

        public void ClearBullets()
        {
            while(Bullets.Count>0)
            {
                DisposeBullet(Bullets[0]);
            }
        }

        public void DisposeBullet(Bullet i_Bullet)
        {
            if (BulletPool.Contains(i_Bullet) == false && Bullets.Contains(i_Bullet) == true)
            {
                i_Bullet.Dispose();
                i_Bullet.OutOfScreenBounds -= DisposeBullet;
                i_Bullet.Enabled = false;
                i_Bullet.Visible = false;
                Bullets.Remove(i_Bullet);
                BulletPool.Add(i_Bullet);
            }
        }
    }
}