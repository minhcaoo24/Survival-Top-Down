using UnityEngine;

namespace STD.Utils
{
    public class ParticleCollision : MonoBehaviour
    {
        void OnParticleCollision(GameObject other)
        {
            if (other.layer == LayerMask.NameToLayer("Enemy"))
            {
                Debug.Log($"Object {other.name} is collide with particle");
            }
        }
    }
}