using System;
using STD.Core.Interface;
using STD.Core.Player;
using STD.Utils;
using UnityEngine;

namespace STD.Core.Entity.SiegeMinion
{
    using static STD.Utils.Constants.SiegeMinion;
    using static STD.Utils.Constants.SiegeMinion.Poison;

    public class SiegeMinionBullet : MonoBehaviour
    {
        private float existTimer;
        private Vector3 moveDirection;
        private PlayerScript player;

        void Awake()
        {
            player = FindFirstObjectByType<PlayerScript>();
        }

        private void OnEnable()
        {
            existTimer = 0f;

            Initialize(player.transform.position);
        }

        public void Initialize(Vector3 targetPosition)
        {
            Vector3 direction = targetPosition - transform.position;

            direction.y = 0f;

            moveDirection = direction.normalized;

            Vector3 position = transform.position;
            position.y = 1f;
            transform.position = position;
        }

        private void Update()
        {
            // transform.position +=
            //     moveDirection *
            //     SIEGE_BULLET_MOVE_SPEED *
            //     Time.deltaTime;

            transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, SIEGE_BULLET_MOVE_SPEED * Time.deltaTime);

            // Giữ bullet ở Y = 1
            Vector3 position = transform.position;
            position.y = 1f;
            transform.position = position;

            // Lifetime = 0.5 giây
            existTimer += Time.deltaTime;

            if (existTimer >= 0.3f)
            {
                SiegeMinionBulletPool.Singleton.GoBackToPool(this);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<PlayerScript>()
                     .TakeDamage((int)POISON_DAMAGE);

                SiegeMinionBulletPool.Singleton.GoBackToPool(this);
            }
        }
    }
}

/*
private void Move()
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                playerTransform.position,
                SIEGE_BULLET_MOVE_SPEED * Time.deltaTime
            );

            if (Vector3.Distance(transform.position, siegeMinionTransform.position) >= SIEGE_BULLET_MAX_DISTANCE)
            {
                gameObject.SetActive(false);
            }
        }
*/