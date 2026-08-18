using System;
using STD.Utils;
using TMPro;
using UnityEngine;

namespace STD.Core.UI
{
    public class LevelText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;

        private void OnEnable()
        {
            Observer.Subscribe("OnLevelUp", (Action<int>)SetText);
        }

        private void OnDisable()
        {
            Observer.Unsubscribe("OnLevelUp", (Action<int>)SetText);
        }

        private void SetText(int content) => text.text = content.ToString();
    }
}