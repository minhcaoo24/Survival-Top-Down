using UnityEngine;
using UnityEngine.UI;

public class SuperMinionUpdateUI : MonoBehaviour
{
    [SerializeField] private Slider bar;

    public void UpdateHPUI(float value)
    {
        bar.value = value;
    }
}
