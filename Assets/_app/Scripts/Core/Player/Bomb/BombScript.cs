using STD.Core.Interface;
using UnityEngine;

namespace STD.Core.Player.Bomb
{
    using static STD.Utils.Constants.Bomb;
    public class BombScript : MonoBehaviour
    {
        [SerializeField] private LayerMask enemyLayer;
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
            Collider[] hits = Physics.OverlapSphere(
                transform.position,
                BOMBBB_EXPLOSION_RADIUS,
                enemyLayer
            );

            foreach (Collider hit in hits)
            {
                if (hit.TryGetComponent<IDamageable>(out IDamageable damageable))
                {
                    damageable.TakeDamage(DASHBOMB_EXPLOSION_DAMAGE);
                }
            }

            if (!this || !gameObject.activeInHierarchy)
                return;

            explosionEffect.Play();

            await Awaitable.WaitForSecondsAsync(1f);

            explosionEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

            isExploding = false;
            gameObject.SetActive(false);
        }
    }
}