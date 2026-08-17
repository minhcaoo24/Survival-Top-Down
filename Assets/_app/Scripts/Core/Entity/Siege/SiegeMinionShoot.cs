using System;
using STD.Utils;
using UnityEngine;

namespace STD.Core.Entity.SiegeMinion
{
    public class SiegeMinionShoot : MonoBehaviour
    {
        private SiegeMinionBullet bullet;
        [SerializeField] private Transform placeHolder;

        private void OnEnable()
        {
            Observer.Subscribe("Canon_Shoot", (Action)Shoot);
        }

        private void OnDisable()
        {
            Observer.Unsubscribe("Canon_Shoot", (Action)Shoot);
        }

        private void Shoot() 
        {
            bullet = SiegeMinionPool.Singleton.Get();
            bullet.gameObject.SetActive(true);
            bullet.transform.position = placeHolder.position;
        }
    }
}