using Infrastructure.ObjectModel;
using Microsoft.Xna.Framework;
using System;

namespace Infrastructure.Managers
{
    public class RandomManager : GameService, IRandomManager
    {
        private readonly Random r_Random;

        public RandomManager(Game i_Game) : base(i_Game)
        {
            r_Random = new Random();
        }

        protected override void RegisterAsService()
        {
            this.Game.Services.AddService(typeof(IRandomManager), this);
        }

        public int GetRandomNumber(int i_min, int i_Max)
        {
            int num = r_Random.Next(i_min, i_Max);
            //Debug.WriteLine(num);
            return num;
        }
    }

    public interface IRandomManager
    {
        int GetRandomNumber(int i_min, int i_Max);
    }
}
