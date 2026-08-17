using STD.Utils;

namespace STD.Core.Entity.SiegeMinion
{
    public class SiegeMinionPool : ObjectPool<SiegeMinionBullet>
    {
        public static SiegeMinionPool Singleton;
        protected override void Awake() => Singleton = this;
    }
}