namespace SpaceInvaders.Menus.Abstract
{
    public abstract class MenuItem
    {
        protected string m_Text;
        public eMenuItemState State { get; set; }

        public MenuItem(string i_Text)
        {
            m_Text = i_Text;
            State = eMenuItemState.Inactive;
        }

        public override string ToString()
        {
            return m_Text;
        }
    }

    public enum eMenuItemState
    {
        Active,
        Inactive
    }

    public interface IToggleableMenuItem
    {
        void ToggleUp();
        void ToggleDown();
    }

    public interface IClickableMenuItem
    {
        void Click();
    }

    public interface ICommand
    {
        void Execute();
    }
}
