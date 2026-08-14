using STD.Utils;

namespace STD.Core.Player.Bullet
{
    public class BulletPool : ObjectPool<BulletScript>
    {
        public static BulletPool Singleton;
        protected override void Awake() => Singleton = this;
    }
}
