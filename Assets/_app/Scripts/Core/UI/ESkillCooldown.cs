using TMPro;
using UnityEngine;

namespace STD.Core.UI
{
    using static STD.Utils.Constants.Player;
    public class ESkillCooldown : MonoBehaviour
    {
        private float elapsedTime;
        private float cooldown;

        [SerializeField] private TextMeshProUGUI text;

        private void Start()
        {
            cooldown = DASH_COOLDOWN;
            gameObject.SetActive(false);
        }

        private void Update()
        {
            elapsedTime += Time.deltaTime;

            float remainingTime = cooldown - elapsedTime;

            text.text = Mathf.CeilToInt(remainingTime).ToString();

            if (elapsedTime >= cooldown)
            {
                elapsedTime = 0f;
                DisableCooldown();
            }
        }

        public void EnableCooldown()
        {
            elapsedTime = 0f;
            text.text = Mathf.CeilToInt(cooldown).ToString();
            gameObject.SetActive(true);
        }

        public void DisableCooldown()
        {
            gameObject.SetActive(false);
        }
    }
}