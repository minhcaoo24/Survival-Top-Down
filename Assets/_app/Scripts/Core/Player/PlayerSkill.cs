using STD.Core.Player.Bullet;
using STD.Core.UI;
using STD.Utils;
using UnityEngine;

namespace STD.Core.Player
{
    using static Utils.Constants.Player;
    using static Utils.Constants.Bullet;

    public class PlayerSkill : MonoBehaviour
    {
        private BaseATKButton baseATK;
        [SerializeField] private Transform placeHolder;
        [SerializeField] private PlayerController controller;

        private bool isDashing;
        private bool canDash = true;

        private bool canPlaceBoommpppppp;
        private GameObject bombPrefab;

        private void OnEnable()
        {
            Observer.Subscribe("OnBaseATKClick", BaseATKHandle);
            Observer.Subscribe("OnEClick", async () =>
            {
                await DashHandle();
            });
        }

        private void OnDisable()
        {
            Observer.Unsubscribe("OnBaseATKClick", BaseATKHandle);
            Observer.Unsubscribe("OnEClick", async () =>
            {
                await DashHandle();
            });
        }

        private void BaseATKHandle()
        {
            print("This is base atk");
            Vector3 direction = placeHolder.transform.forward;

            Vector3 left = Quaternion.Euler(0f, -BULLET_ANGLE, 0f) * direction;
            Vector3 center = direction;
            Vector3 right = Quaternion.Euler(0f, BULLET_ANGLE, 0f) * direction;

            ShootBullet(left);
            ShootBullet(center);
            ShootBullet(right);
        }

        private async Awaitable DashHandle()
        {
            if (!canDash || isDashing)
                return;

            isDashing = true;
            canDash = false;

            Vector3 startPosition = transform.position;
            Vector3 direction = transform.forward;
            Vector3 targetPosition = startPosition + direction * DASH_DISTANCE;

            float elapsed = 0f;

            while (elapsed < DASH_DURATION)
            {
                elapsed += Time.deltaTime;

                float t = Mathf.Clamp01(elapsed / DASH_DURATION);

                transform.position = Vector3.Lerp(
                    startPosition,
                    targetPosition,
                    t
                );

                Observer.Publish("OnDash");

                await Awaitable.NextFrameAsync();
            }

            transform.position = targetPosition;

            isDashing = false;

            await Awaitable.WaitForSecondsAsync(DASH_COOLDOWN);
            Debug.Log("Can dash");

            canDash = true;
        }

        private async Awaitable Bombbb()
        {
            if(!canPlaceBoommpppppp)
                return;

            Vector3 place = this.transform.position;

            bombPrefab.transform.position = place;
            bombPrefab.SetActive(true);
            canPlaceBoommpppppp = false;

            //TODO: Update UI cooldown
            await Awaitable.WaitForSecondsAsync(BOMBBB_COOLDOWN + BOMBBB_EXPLOSION_DELAY);
            canPlaceBoommpppppp = true;
        }

        private void ShootBullet(Vector3 direction)
        {

            var bullet = BulletPool.Singleton.Get();
            if (!bullet)
            {
                Debug.LogError($"<color=red>Oooops, Something went wrong</color>, check it out");
                return;
            }
            Rigidbody body = bullet.GetComponentInChildren<Rigidbody>();
            if (!body)
            {
                Debug.LogError($"<color=red>RigidBody is null</color>, add body and try again");
                return;
            }
            body.transform.position = placeHolder.position;
            body.transform.rotation = placeHolder.rotation;
            body.linearVelocity = direction.normalized * BUTLLET_SPEED;
        }
    }
}
