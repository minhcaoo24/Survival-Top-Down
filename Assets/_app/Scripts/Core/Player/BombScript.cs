using UnityEngine;

namespace STD.Core.Player
{
    using static STD.Utils.Constants.Player;
    public class BombScript : MonoBehaviour
    {
        [SerializeField] private ParticleSystem explosionEffect;

        private bool isExploding;

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        private async void OnEnable()
        {
            if (isExploding)
                return;

            isExploding = true;

            explosionEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

            await Awaitable.WaitForSecondsAsync(BOMBBB_EXPLOSION_DELAY);

            if (!this || !gameObject.activeInHierarchy)
                return;

            explosionEffect.Play();

            // while (explosionEffect.IsAlive(true))
            // {
            //     await Awaitable.NextFrameAsync();

            //     if (!this || !gameObject.activeInHierarchy)
            //         return;
            // }

            await Awaitable.WaitForSecondsAsync(1f);

            explosionEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

            isExploding = false;
            gameObject.SetActive(false);
        }
    }
}