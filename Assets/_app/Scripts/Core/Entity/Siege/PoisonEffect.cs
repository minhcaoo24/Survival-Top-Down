using System.Threading;
using STD.Core.Player;
using UnityEngine;

namespace STD.Core.Entity.SiegeMinion
{
    public class PoisonEffect : MonoBehaviour
    {
        private float damagePerTick;
        private float duration;
        private float tickInterval;

        private CancellationTokenSource poisonCts;

        public async Awaitable Apply(
            float damagePerTick,
            float duration,
            float tickInterval)
        {
            this.damagePerTick = damagePerTick;
            this.duration = duration;
            this.tickInterval = tickInterval;

            // Nếu đang bị poison → reset poison
            poisonCts?.Cancel();
            poisonCts?.Dispose();

            poisonCts = CancellationTokenSource.CreateLinkedTokenSource(
                destroyCancellationToken
            );

            CancellationToken token = poisonCts.Token;

            try
            {
                await PoisonRoutine(token);
            }
            catch (System.OperationCanceledException e)
            {
                Debug.LogError($"error: {e}");
            }
            finally
            {
                if (poisonCts != null && poisonCts.Token == token)
                {
                    poisonCts.Dispose();
                    poisonCts = null;
                }
            }
        }

        private async Awaitable PoisonRoutine(CancellationToken token)
        {
            DealDamage();

            await Awaitable.WaitForSecondsAsync(
                tickInterval,
                token
            );

            DealDamage();

            await Awaitable.WaitForSecondsAsync(
                tickInterval,
                token
            );

            DealDamage();

            await Awaitable.WaitForSecondsAsync(
                tickInterval,
                token
            );

            DealDamage();
        }

        private void DealDamage()
        {
            Debug.Log($"Poison damage: {damagePerTick}");

            // TODO:
            FindFirstObjectByType<PlayerScript>().TakeDamage((int)damagePerTick);
        }

        private void OnDestroy()
        {
            poisonCts?.Cancel();
            poisonCts?.Dispose();
            poisonCts = null;
        }
    }
}
