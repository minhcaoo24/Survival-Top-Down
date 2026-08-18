using STD.Core.Entity.SiegeMinion;
using STD.Core.Entity.SuperMinion;
using UnityEngine;

namespace STD.Core
{
    public class WaveGenerator : MonoBehaviour
    {
        public static WaveGenerator Singleton;

        public int SiegeMinionRemains;
        public int SuperMinionRemains;

        private void Awake()
        {
            Singleton = this;
        }

        private void Start()
        {
            NewWave();
        }

        private void Update()
        {
            if(SiegeMinionRemains <= 0 && SuperMinionRemains <= 0)
            {
                NewWave();
            }    
        }

        private void NewWave()
        {
            for(int i = 0; i < 4; i++)
            {
                Vector3 randomPosition = new Vector3(Random.Range(0, 50), 1, Random.Range(0, 50));
                var super = SuperMinionPool.Singleton.Get();
                ++SuperMinionRemains;
                super.transform.position = randomPosition;
            }

            for(int i = 0; i < 2; i++)
            {
                Vector3 randomPosition = new Vector3(Random.Range(0, 50), 1, Random.Range(0, 50));
                var siege = SiegeMinionPool.Singleton.Get();
                ++SiegeMinionRemains;
                siege.transform.position = randomPosition;
            }
        }
    }
}