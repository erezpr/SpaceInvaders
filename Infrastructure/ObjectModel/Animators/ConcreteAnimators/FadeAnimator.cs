using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Infrastructure.ObjectModel.Animators.ConcreteAnimators
{
    public class FadeAnimator : SpriteAnimator
    {
        private bool m_Loop;
        public FadeAnimator(string i_Name, bool i_Loop, TimeSpan i_AnimationLength) : base(i_Name, i_AnimationLength)
        {

            m_Loop = i_Loop;
        }

        protected override void DoFrame(GameTime i_GameTime)
        {
            float x = BoundSprite.Opacity - (float)i_GameTime.ElapsedGameTime.TotalSeconds / (float)this.AnimationLength.TotalSeconds;
            if (x < 0)
            {
                BoundSprite.Opacity = 0;
            }
            else
            {
                BoundSprite.Opacity -= (float)i_GameTime.ElapsedGameTime.TotalSeconds / (float)this.AnimationLength.TotalSeconds;
            }
        }

        protected override void RevertToOriginal()
        {
            BoundSprite.Opacity = m_OriginalSpriteInfo.Opacity;
        }
    }
}
