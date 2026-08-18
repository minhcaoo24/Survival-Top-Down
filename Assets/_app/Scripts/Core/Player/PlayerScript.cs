using System;
using STD.Core.Entity;
using STD.Utils;
using UnityEngine;

namespace STD.Core.Player
{
    using static STD.Utils.Constants.Player;
    public class PlayerScript : AbstractPlayer
    {
        private void OnEnable()
        {
            Observer.Subscribe("OnSiegeDie", (Action<float>)IncreaseLevel);
        }

        private void OnDisable()
        {
            Observer.Unsubscribe("OnSiegeDie", (Action<float>)IncreaseLevel);
        }

        private void IncreaseLevel(float exp)
        {
            EXP += exp;
            if(this.EXP >= 100)
                this.EXP -= 100;
        }
    }
}
