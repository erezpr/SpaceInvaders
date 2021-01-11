using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Infrastructure.ObjectModel.Animators.ConcreteAnimators
{
    public class SpinAnimator : SpriteAnimator
    {
        private float m_AngularVelocity;
        private Vector2 m_RotationOrigin;
        public SpinAnimator(string i_Name, float i_AngularVelocity, Vector2 i_RotationOrigin, TimeSpan i_AnimationLength) : base(i_Name, i_AnimationLength)
        {
            m_AngularVelocity = i_AngularVelocity;
            m_RotationOrigin = i_RotationOrigin;
        }

        protected override void DoFrame(GameTime i_GameTime)
        {
            BoundSprite.AngularVelocity = m_AngularVelocity;
            BoundSprite.RotationOrigin = m_RotationOrigin;
        }

        protected override void RevertToOriginal()
        {
            BoundSprite.AngularVelocity = m_OriginalSpriteInfo.AngularVelocity;
            //BoundSprite.RotationOrigin = m_OriginalSpriteInfo.RotationOrigin;
        }
    }
}
