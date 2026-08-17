using System;
using STD.Core.Player;
using STD.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace STD.Core.UI
{
    public class PlayerHealthBar : MonoBehaviour
    {
        [SerializeField] private Slider bar;
        [SerializeField] private PlayerScript stats;

        void Start()
        {
            bar.maxValue = stats.MaxHP;
            bar.minValue = 0;
            UpdateBar(stats.MaxHP);
        }

        private void OnEnable()
        {
            Observer.Subscribe("OnHealthChanged", (Action<int>) UpdateBar);
        }

        private void OnDisable()
        {
            Observer.Unsubscribe("OnHealthChanged", (Action<int>) UpdateBar);
        }

        private void UpdateBar(int value)
        {
            bar.value = Mathf.Clamp(value, 0, stats.MaxHP);
        }
    }
}