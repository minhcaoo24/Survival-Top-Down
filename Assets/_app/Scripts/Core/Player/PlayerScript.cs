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
            Observer.Subscribe("OnMinionDie", (Action<float>)Levelup);
        }

        private void OnDisable()
        {
            Observer.Unsubscribe("OnMinionDie", (Action<float>)Levelup);
        }

        private void Levelup(float exp)
        {
            EXP += exp;
            if (this.EXP >= 100)
            {
                this.EXP -= 100;
                ++Level;
                Armor += 2;
                DamageMultiplier += 0.1f;
                CurrentHp += 40;
                MaxHp += 40;
                Observer.Publish("OnMaxHpChanged", MaxHp);
                Observer.Publish("OnHealthChanged", CurrentHp);
                Observer.Publish("OnLevelUp", Level);
            }
        }
    }
}
