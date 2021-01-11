using Microsoft.Xna.Framework;
using SpaceInvaders.Sprites;
using System;
using System.Collections.Generic;

namespace SpaceInvaders.Menus
{
    public class ScreenMode : ToggleableMenuItem
    {
        private ScreenToggle m_ScreenToggle;
        public ScreenMode(Game i_Game, string i_Text) : base(i_Game, i_Text, new List<string> { "On", "Off" })
        {
            m_ScreenToggle = new ScreenToggle(new GraphicsDeviceManager(i_Game));
        }

        public override void ToggleUp()
        {
            base.ToggleUp();
            m_ScreenToggle.Execute();
        }
        
        public override void ToggleDown()
        {
            base.ToggleDown();
            m_ScreenToggle.Execute();
        }
    }

    public class ScreenToggle : ICommand
    {
        private readonly GraphicsDeviceManager m_Graphics;
        public ScreenToggle(GraphicsDeviceManager i_Graphics)
        {
            m_Graphics = i_Graphics;
        }
        public void Execute()
        {
            m_Graphics.ToggleFullScreen();
        }
    }
    
}
