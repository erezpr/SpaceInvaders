using System;
using System.Collections.Generic;

namespace SpaceInvaders.Menus.Abstract
{
    public abstract class ToggleableMenuItem : MenuItem, IToggleableMenuItem
    {
        public List<string> ToggleTextList { get; }
        private readonly CircleToggle r_CircleToggle;

        public ToggleableMenuItem(string i_Text, List<string> i_ToggleTextList) : base(i_Text)
        {
            r_CircleToggle = new CircleToggle(i_ToggleTextList.Count);
            ToggleTextList = i_ToggleTextList;
        }

        public virtual void ToggleUp()
        {
            r_CircleToggle.ToggleUp();
        }

        public virtual void ToggleDown()
        {
            r_CircleToggle.ToggleDown();
        }

        public override string ToString()
        {
            return string.Format("{0}: {1}", m_Text, ToggleTextList[r_CircleToggle.CurrentIndex]);
        }
    }

    public class CircleToggle
    {
        private readonly int r_Count;
        public int CurrentIndex { get; private set; }

        public CircleToggle(int i_count)
        {
            r_Count = i_count;
        }

        public void ToggleUp()
        {
            CurrentIndex = Math.Abs(CurrentIndex + 1) % r_Count;
        }

        public void ToggleDown()
        {
            CurrentIndex = Math.Abs(CurrentIndex - 1 + r_Count) % r_Count;
        }
    }
}
