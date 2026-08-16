using System;
using STD.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace STD.Core.UI
{
    using static STD.Utils.Constants.Player;
    public class BulletRemainsBar : MonoBehaviour
    {
        [SerializeField] private Slider bar;
        private int currentValue;

        void Start()
        {
            bar.minValue = 0;
            bar.maxValue = BASE_ATK_MAX_CHARGE;
            UpdateBar(3);
        }

        private void OnEnable()
        {
            Observer.Subscribe("UpdateUIBulletRemains", (Action<int>)UpdateBar);
        }

        private void OnDisable()
        {
            Observer.Unsubscribe("UpdateUIBulletRemains", (Action<int>)UpdateBar);
        }

        private void UpdateBar(int value)
        {
            bar.value = Mathf.Clamp(value, 0, 3);
        }
    }
}