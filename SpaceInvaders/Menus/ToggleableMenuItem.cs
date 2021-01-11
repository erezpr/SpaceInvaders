using Microsoft.Xna.Framework;
using SpaceInvaders.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.Menus
{
    public abstract class ToggleableMenuItem : MenuItem, IToggleableMenuItem
    {
        private int m_CurrentToggleTextIdx;
        private List<string> m_ToggleTextList;
        public ToggleableMenuItem(Game i_Game, string i_Text, List<string> i_ToggleTextList) : base(i_Game, i_Text)
        {
            m_ToggleTextList = i_ToggleTextList;
            m_CurrentToggleTextIdx = 0;
        }

        public List<string> ToggleTextList { get => m_ToggleTextList; }

        public virtual void ToggleUp()
        {
            m_CurrentToggleTextIdx = (m_CurrentToggleTextIdx + 1) % m_ToggleTextList.Count;
        }
        
        public virtual void ToggleDown()
        {
            m_CurrentToggleTextIdx = (m_CurrentToggleTextIdx - 1) % m_ToggleTextList.Count;
        }

        public override string ToString()
        {
            return string.Format("{0}: {1}", Text, m_ToggleTextList[m_CurrentToggleTextIdx]);
        }
    }
}
