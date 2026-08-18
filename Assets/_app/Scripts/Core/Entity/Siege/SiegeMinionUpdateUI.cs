using UnityEngine;
using UnityEngine.UI;

namespace STD.Core.Entity.SiegeMinion
{
    public class SiegeMinionUpdateUI : MonoBehaviour
    {
        [SerializeField] private Slider bar;

        public void UpdateHPUI(float value)
        {
            bar.value = value;
        } 
    }
}