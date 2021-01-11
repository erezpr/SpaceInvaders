using Microsoft.Xna.Framework.Input;

namespace SpaceInvaders
{
    public class MovementKeys
    {
        public Keys Left { get; private set; }
        public Keys Right { get; private set; }
        public Keys Shoot { get; private set; }

        public MovementKeys(Keys i_LeftKey, Keys i_RightKey, Keys i_ShootingKey)
        {
            Left = i_LeftKey;
            Right = i_RightKey;
            Shoot = i_ShootingKey;
        }
    }
}
