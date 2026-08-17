using System;
using STD.Core.Interface;
using UnityEngine;

namespace STD.Core.Entity
{
    public abstract class AbstractEntity : MonoBehaviour, IDamageable
    {
        public int MaxHP;
        public int CurrentHP { get; private set; }
        public int Armor { get; private set; }
        public float DamageMultiplier { get; private set; }
        public float EXP;

        protected virtual void Awake()
        {
            Armor = 0;
            DamageMultiplier = 0f;
        }
        protected virtual void OnEnable() { }
        protected virtual void OnDisable() { }

        public virtual void SetHP(int value) => CurrentHP = Math.Min(CurrentHP + value, MaxHP);

        public void SetArmor(int value = 2)
        {
            if(value == 0)
                return;
            this.Armor += value;
        }

        public void SetDamageMultiplier(float value = 0.1f)
        {
            if(value == 0)
                return;
            this.DamageMultiplier += value;
        }

        public virtual void TakeDamage(int damage) { }
    }
}