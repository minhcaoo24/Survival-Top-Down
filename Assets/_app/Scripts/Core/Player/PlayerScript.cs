using STD.Core.Entity;

namespace STD.Core.Player
{
    using static STD.Utils.Constants.Player;
    public class PlayerScript : AbstractEntity
    {
        protected override void Awake()
        {
            MaxHP = PLAYER_HP;
        }

        public override void TakeDamage(int inDamage)
        {
            var damage = inDamage - Armor;
            SetHP(-damage);
        }

        private void LevelUp()
        {
            MaxHP += 40;
            SetHP(40);
            SetArmor();
            SetDamageMultiplier();
        }
    }
}
