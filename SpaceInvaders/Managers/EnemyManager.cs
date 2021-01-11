using SpaceInvaders.Sprites;
using Microsoft.Xna.Framework;
using System;
using Infrastructure.ObjectModel;

namespace SpaceInvaders.Managers
{
    public class EnemyManager : CompositeDrawableComponent<IGameComponent>
    {
        public event Action GameOver;
        public event Action LevelOver;
        private readonly int r_LevelNumber;
        private readonly Game r_Game;
        private readonly int r_Columns = 9;
        private const int k_Rows = 5;
        private readonly int r_YellowScore = 100;
        private readonly int r_BlueScore = 200;
        private readonly int r_PinkScore = 250;
        private const int k_EnemySpeedGainTrigger = 5;
        private const int k_EnemySize = 32;
        private const float k_Spacing = 1.6f;
        private const double k_SpeedUp = 0.95;
        private const int k_PointsToAddPerLevel = 140;
        private int m_Direction = 1;
        private int m_NumOfKilledEnemies = 0;
        private TimeSpan m_JumpTime = TimeSpan.FromSeconds(0.5);
        private TimeSpan m_TimeLeftForJump = TimeSpan.FromSeconds(0.5);

        public EnemyManager(Game i_Game, int i_LevelNumber) : base(i_Game)
        {
            r_Game = i_Game;
            r_LevelNumber = i_LevelNumber;
            r_Columns += r_LevelNumber;
            r_YellowScore += k_PointsToAddPerLevel * r_LevelNumber;
            r_BlueScore += k_PointsToAddPerLevel * r_LevelNumber;
            r_PinkScore += k_PointsToAddPerLevel * r_LevelNumber;
        }

        private void enemyHitBottomScreen()
        {
            GameOver?.Invoke();
        }

        private void enemyDisposed(object sender, EventArgs e)
        {
            m_NumOfKilledEnemies++;
            if (m_NumOfKilledEnemies % k_EnemySpeedGainTrigger == 0)
            {
                speedUp();
            }
        }

        private void enemyDies(Enemy i_Enemy)
        {
            this.Remove(i_Enemy.Gun);
            this.Remove(i_Enemy);
            everyoneDied();
        }

        private void everyoneDied()
        {
            foreach (IGameComponent component in this)
            {
                if (component is Enemy)
                {
                    return;
                }
            }
            LevelOver?.Invoke();
        }

        public override void Initialize()
        {
            base.Initialize();
            for (int i = 0; i < k_Rows; i++)
            {
                for (int j = 0; j < r_Columns; j++)
                {
                    switch (i)
                    {
                        case 0:
                            {
                                initEnemy(i, j, new Vector2(0, 0), Color.Pink, r_PinkScore);
                                break;
                            }
                        case 1:
                            {
                                initEnemy(i, j, new Vector2(1, 0), Color.LightBlue, r_BlueScore);
                                break;
                            }
                        case 2:
                            {
                                initEnemy(i, j, new Vector2(1, 1), Color.LightBlue, r_BlueScore);
                                break;
                            }
                        case 3:
                            {
                                initEnemy(i, j, new Vector2(2, 0), Color.Yellow, r_YellowScore);
                                break;
                            }
                        case 4:
                            {
                                initEnemy(i, j, new Vector2(2, 1), Color.Yellow, r_YellowScore);
                                break;
                            }
                    }
                }
            }
        }

        private void initEnemy(int i, int j, Vector2 i_SourceRectangle, Color i_Color, int i_Value)
        {
            Enemy enemy = new Enemy(Game);
            this.Add(enemy);
            this.Add(enemy.Gun);
            enemy.Disposed += enemyDisposed;
            enemy.IDied += enemyDies;
            enemy.HitBottomScreen += enemyHitBottomScreen;
            enemy.positionInAsset = i_SourceRectangle;
            enemy.TintColor = i_Color;
            enemy.Value = i_Value;
            enemy.Position = new Vector2(j * k_EnemySize * k_Spacing, i * k_EnemySize * k_Spacing + 96);
            enemy.SourceRectangle = new Rectangle(
                k_EnemySize * (int)i_SourceRectangle.X,
                k_EnemySize * (int)i_SourceRectangle.Y,
                (int)enemy.Width,
                (int)enemy.Height);
            enemy.MaxBullets += r_LevelNumber;
        }

        public override void Update(GameTime i_GameTime)
        {
            m_TimeLeftForJump -= i_GameTime.ElapsedGameTime;

            if (m_TimeLeftForJump.TotalSeconds <= 0)
            {
                m_TimeLeftForJump += m_JumpTime;
                bool isNeedToChangeDirection = checkIfNeedToChangeDirection();

                if (isNeedToChangeDirection == true)
                {
                    changeDirection();
                    speedUp();
                    jumpEnemiesDown();
                }
                else
                {
                    jumpEnemies();
                }
            }

            base.Update(i_GameTime);
        }

        private void jumpEnemies()
        {
            foreach (IGameComponent component in this)
            {
                if (component is Enemy enemy)
                {
                    enemy.Jump(m_Direction);
                }
            }
        }

        private void jumpEnemiesDown()
        {
            foreach (IGameComponent component in this)
            {
                if (component is Enemy enemy)
                {
                    enemy.JumpDown();
                }
            }
        }

        private bool checkIfNeedToChangeDirection()
        {
            bool result = false;

            foreach (IGameComponent component in this)
            {
                if (component is Enemy enemy)
                {
                    float range = MathHelper.Clamp(enemy.Position.X + m_Direction * k_EnemySize / 2, -0.1f, r_Game.GraphicsDevice.Viewport.Width - k_EnemySize);
                    if (range == r_Game.GraphicsDevice.Viewport.Width - k_EnemySize || range == -0.1f)
                    {
                        result = true;
                        break;
                    }
                }
            }

            return result;
        }

        private void changeDirection()
        {
            m_Direction *= -1;
        }

        private void speedUp()
        {
            m_JumpTime = TimeSpan.FromSeconds(m_JumpTime.TotalSeconds * k_SpeedUp);
        }
    }
}
