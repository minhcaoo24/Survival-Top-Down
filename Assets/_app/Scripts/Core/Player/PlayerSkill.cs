using STD.Core.Player.Bullet;
using STD.Core.Player.Bomb;
using STD.Core.UI;
using STD.Utils;
using UnityEngine;
using System;

namespace STD.Core.Player
{
    using static Utils.Constants.Player;
    using static Utils.Constants.Bomb;
    using static Utils.Constants.Bullet;

    public class PlayerSkill : MonoBehaviour
    {
        private int baseATK_BulletRemains = 3;
        private bool canShoot = true;
        private bool isReloading = false;
        [SerializeField] private Transform placeHolder;
        [SerializeField] private PlayerController controller;

        private bool isDashing;
        private bool canDash = true;
        [SerializeField] private DashBombScript dashBomb;
        [SerializeField] private ESkillCooldown eCooldown;

        private bool canPlaceBoommpppppp = true;
        [SerializeField] private BombScript bombPrefab;
        [SerializeField] private WSkillCooldown wCooldown;


        private void OnEnable()
        {
            Observer.Subscribe("OnBaseATKClick", (Action)BaseATKHandle);
            Observer.Subscribe("OnEClick", (Action)ESkill);
            Observer.Subscribe("OnWClick", (Action)WSkill);
        }

        private void OnDisable()
        {
            Observer.Unsubscribe("OnBaseATKClick", (Action)BaseATKHandle);
            Observer.Unsubscribe("OnEClick", (Action)ESkill);
            Observer.Unsubscribe("OnWClick", (Action)WSkill);
        }

        private async void LateUpdate()
        {
            await ReloadBulletAsync();
        }

        private async void BaseATKHandle()
        {
            if (baseATK_BulletRemains <= 0)
                return;

            if(!canShoot) 
                return;

            canShoot = false;
            Vector3 direction = placeHolder.transform.forward;

            Vector3 left = Quaternion.Euler(0f, -BULLET_ANGLE, 0f) * direction;
            Vector3 center = direction;
            Vector3 right = Quaternion.Euler(0f, BULLET_ANGLE, 0f) * direction;

            Observer.Publish("SHOOOOT");
            Observer.Publish("UpdateUIBulletRemains", baseATK_BulletRemains);

            ShootBullet(left);
            ShootBullet(center);
            ShootBullet(right);

            --baseATK_BulletRemains;
            Observer.Publish("UpdateUIBulletRemains", baseATK_BulletRemains);
            _ = ReloadBulletAsync();

            await Awaitable.WaitForSecondsAsync(PLAYER_SHOOT_DELAY);
            canShoot = true;
        }

        private async Awaitable ReloadBulletAsync()
        {
            if (isReloading)
                return;

            isReloading = true;

            while (baseATK_BulletRemains < BASE_ATK_MAX_CHARGE)
            {
                Observer.Publish("RELOADDDDDD");
                await Awaitable.WaitForSecondsAsync(BASE_ATK_RELOAD_TIME);

                // Sau 3 giây hồi 1 charge
                if (baseATK_BulletRemains < BASE_ATK_MAX_CHARGE)
                {
                    ++baseATK_BulletRemains;
                    Observer.Publish("UpdateUIBulletRemains", baseATK_BulletRemains);
                }
            }

            isReloading = false;
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
            PlayerAudio.Singleton.PlayE();

            transform.position = targetPosition;

            isDashing = false;

            Vector3 place = transform.position;
            Vector3 behind = place - transform.forward * DASH_DISTANCE;

            dashBomb.transform.position = new Vector3(behind.x, 1, behind.z);
            dashBomb.gameObject.SetActive(true);
            eCooldown.EnableCooldown();
            await Awaitable.WaitForSecondsAsync(DASH_COOLDOWN);

            canDash = true;
        }
        private async void ESkill() => await DashHandle();

        private async Awaitable Bombbb()
        {
            if (!canPlaceBoommpppppp)
                return;

            Vector3 place = this.transform.position;

            bombPrefab.transform.position = new Vector3(place.x, 0, place.z);
            bombPrefab.gameObject.SetActive(true);
            canPlaceBoommpppppp = false;
            PlayerAudio.Singleton.PlayW();

            wCooldown.EnableCooldown();
            await Awaitable.WaitForSecondsAsync(BOMBBB_COOLDOWN + BOMBBB_EXPLOSION_DELAY);
            canPlaceBoommpppppp = true;
        }
        private async void WSkill() => await Bombbb();

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
