using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Infrastructure.ObjectModel.Animators.ConcreteAnimators
{
    public class ScaleAnimator : SpriteAnimator
    {
        private Vector2 m_DestinationScale;
        public ScaleAnimator(string i_Name, Vector2 i_DestinationScale, TimeSpan i_AnimationLength) : base(i_Name, i_AnimationLength)
        {
            m_DestinationScale = i_DestinationScale;
        }

        protected override void DoFrame(GameTime i_GameTime)
        {
            BoundSprite.Scales = new Vector2(
                BoundSprite.Scales.X - BoundSprite.Scales.X * ((float)i_GameTime.ElapsedGameTime.TotalSeconds / (float)this.AnimationLength.TotalSeconds),
                BoundSprite.Scales.Y - BoundSprite.Scales.Y * ((float)i_GameTime.ElapsedGameTime.TotalSeconds / (float)this.AnimationLength.TotalSeconds));
        }

        protected override void RevertToOriginal()
        {
            BoundSprite.Scales = m_OriginalSpriteInfo.Scales;
        }
    }
}
