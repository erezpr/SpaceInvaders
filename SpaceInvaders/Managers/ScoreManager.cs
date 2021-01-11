using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceInvaders.Sprites;

namespace SpaceInvaders.Managers
{
    public class ScoreManager : DrawableGameComponent
    {
        public int[] PlayersScores { get; }
        private readonly Game r_Game;
        private SpriteFont m_ComicSansMsFont;

        public ScoreManager(Game i_Game, int i_NumberOfPlayers) : base(i_Game)
        {
            r_Game = i_Game;
            PlayersScores = new int[i_NumberOfPlayers];
        }

        public override void Initialize()
        {
            m_ComicSansMsFont = r_Game.Content.Load<SpriteFont>(@"Fonts\ComicSansMS");
        }

        public override void Draw(GameTime i_GameTime)
        {
            GameBase gameBase = r_Game as GameBase;
            gameBase.SpriteBatch.Begin();

            for (int i = 0; i < PlayersScores.Length; i++)
            {
                gameBase.SpriteBatch.DrawString(m_ComicSansMsFont, string.Format(@"P{0} Score: {1}", i + 1, PlayersScores[i]), new Vector2(5, 5 + i * 20), Color.White);
            }

            gameBase.SpriteBatch.End();
        }

        public void ScoreChanged(SpaceShip i_Player)
        {
            PlayersScores[i_Player.PlayerNumber] = i_Player.Score;
        }
    }
}
