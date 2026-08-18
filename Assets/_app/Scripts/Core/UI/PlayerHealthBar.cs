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
            bar.maxValue = stats.MaxHp;
            bar.minValue = 0;
            UpdateBar(stats.MaxHp);
        }

        private void OnEnable()
        {
            Observer.Subscribe("OnHealthChanged", (Action<float>) UpdateBar);
        }

        private void OnDisable()
        {
            Observer.Unsubscribe("OnHealthChanged", (Action<float>) UpdateBar);
        }

        private void UpdateBar(float value)
        {
            bar.value = Mathf.Clamp(value, 0, stats.MaxHp);
        }
    }
}