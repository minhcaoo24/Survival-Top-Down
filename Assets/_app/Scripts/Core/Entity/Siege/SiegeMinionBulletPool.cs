using STD.Utils;

namespace STD.Core.Entity.SiegeMinion
{
    public class SiegeMinionBulletPool : ObjectPool<SiegeMinionBullet>
    {
        public static SiegeMinionBulletPool Singleton;
        protected override void Awake() => Singleton = this;
    }
}