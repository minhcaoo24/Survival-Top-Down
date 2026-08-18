using STD.Utils;

namespace STD.Core.Entity.SuperMinion
{
    public class SuperMinionPool : ObjectPool<SuperMinionScript>
    {
        public static SuperMinionPool Singleton;

        protected override void Awake() => Singleton = this;
    }
}