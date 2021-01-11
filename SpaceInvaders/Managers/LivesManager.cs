using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceInvaders.Sprites;

namespace SpaceInvaders.Managers
{
    public class LivesManager : DrawableGameComponent
    {
        private readonly int[] r_PlayersLives;
        private readonly Texture2D[] r_PlayerTexture;
        private const int k_StartingLifes = 3;

        public LivesManager(Game i_Game, int i_NumberOfPlayers) : base(i_Game)
        {
            r_PlayersLives = new int[i_NumberOfPlayers];
            r_PlayerTexture = new Texture2D[i_NumberOfPlayers];
        }

        public override void Initialize()
        {
            base.Initialize();
            for (int i = 1; i <= r_PlayersLives.Length; i++)
            {
                string assetName = string.Format("Sprites\\Ship0{0}_32x32", i);
                r_PlayerTexture[i - 1] = Game.Content.Load<Texture2D>(assetName);
                r_PlayersLives[i - 1] = k_StartingLifes;
            }
        }

        public void LifeChanged(SpaceShip i_Player)
        {
            r_PlayersLives[i_Player.PlayerNumber] = i_Player.Lives;
        }

        public override void Draw(GameTime i_GameTime)
        {
            GameBase gameBase = Game as GameBase;
            gameBase.SpriteBatch.Begin();
            for (int i = 0; i < r_PlayersLives.Length; i++)
            {
                for (int j = 0; j < r_PlayersLives[i]; j++)
                {
                    gameBase.SpriteBatch.Draw(
                                       r_PlayerTexture[i],
                                       new Vector2(Game.GraphicsDevice.Viewport.Width - (j + 1) * 24, 10 + i * 24),
                                       null,
                                       Color.White * 0.5f,
                                       0,
                                       Vector2.Zero,
                                       0.5f,
                                       SpriteEffects.None,
                                       0);
                }
            }

            gameBase.SpriteBatch.End();
            base.Draw(i_GameTime);
        }
    }
}
