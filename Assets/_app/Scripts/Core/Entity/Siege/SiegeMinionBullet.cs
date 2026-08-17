using STD.Core.Interface;
using STD.Core.Player;
using UnityEngine;

namespace STD.Core.Entity.SiegeMinion
{
    using static STD.Utils.Constants.SiegeMinion;
    using static STD.Utils.Constants.SiegeMinion.Poison;

    public class SiegeMinionBullet : MonoBehaviour
    {
        private Transform playerTransform;

        private void Start()
        {
            playerTransform = FindFirstObjectByType<PlayerScript>().transform;
        }

        private void LateUpdate()
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                new Vector3(playerTransform.position.x, 1, playerTransform.position.z),
                SIEGE_BULLET_MOVE_SPEED * Time.deltaTime
            );
        }

        private async void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                SiegeMinionPool.Singleton.GoBackToPool(this);
                playerTransform.GetComponent<PlayerScript>().TakeDamage((int)POISON_DAMAGE);
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