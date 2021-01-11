using SpaceInvaders.Sprites;
using Infrastructure.ObjectModel;
using Microsoft.Xna.Framework;

namespace SpaceInvaders.Managers
{
    public class BarrierManager : CompositeDrawableComponent<Barrier>
    {
        private const int k_NumOfBarriers = 4;
        private const int k_BarriersWidth = 44;
        private const int k_BarriersHeight = 32;
        private const int k_Spacing = 10;
        private readonly float r_Speed = 45;
        private readonly Game r_Game;

        public BarrierManager(Game i_Game, int i_LevelNumber) : base(i_Game)
        {
            if (i_LevelNumber == 0)
            {
                r_Speed = 0;
            }
            else
            {
                r_Speed += r_Speed * (i_LevelNumber - 1) * 0.04f;
            }

            r_Game = i_Game;
        }

        public override void Initialize()
        {
            base.Initialize();
            float startingPosition = (r_Game.GraphicsDevice.Viewport.Width - (k_NumOfBarriers * k_BarriersWidth + (k_NumOfBarriers - 1) * k_BarriersWidth)) / 2;
            for (int i = 0; i < k_NumOfBarriers; i++)
            {
                Barrier barrier = new Barrier(r_Game);
                this.Add(barrier);
                barrier.Position = new Vector2(startingPosition + i * 2 * k_BarriersWidth, r_Game.GraphicsDevice.Viewport.Height - k_BarriersHeight * 3 - k_Spacing);
                barrier.Velocity = new Vector2(r_Speed, 0);
            }
        }
    }
}
