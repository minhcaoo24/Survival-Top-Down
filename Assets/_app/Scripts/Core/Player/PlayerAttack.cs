using STD.Core.Player.Bullet;
using STD.Core.UI;
using STD.Utils;
using UnityEngine;

namespace STD.Core.Player
{
    using static Utils.Constants.Player;
    using static Utils.Constants.Bullet;

    public class PlayerAttack : MonoBehaviour
    {
        public BaseATKButton baseATK;
        [SerializeField] private Transform placeHolder;

        private void OnEnable()
        {
            Observer.Subscribe("OnBaseATKClick", BaseATKHandle);
        }

        private void OnDisable()
        {
            Observer.Unsubscribe("OnBaseATKClick", BaseATKHandle);
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
