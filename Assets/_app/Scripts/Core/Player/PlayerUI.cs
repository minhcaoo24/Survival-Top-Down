using UnityEngine;
using UnityEngine.UI;

namespace STD.Core.Player
{
    public class PlayerUI : MonoBehaviour
    {
        [SerializeField] private GameObject health, shotRemains, levelText, canvas;
        private Camera cam;

        private void Awake()
        {
            cam = Camera.main;
        }

        private void LateUpdate()
        {
            // health.transform.rotation = Quaternion.LookRotation(transform.position - cam.transform.position);
            // shotRemains.transform.rotation = Quaternion.LookRotation(transform.position - cam.transform.position);
            // levelText.transform.rotation = Quaternion.LookRotation(transform.position - cam.transform.position);
            canvas.transform.rotation = Quaternion.LookRotation(transform.position - cam.transform.position);
        }
    }
}