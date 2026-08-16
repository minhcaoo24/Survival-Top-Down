namespace STD.Utils
{
    public struct Constants
    {
        public struct Player
        {
            public const int PLAYER_HP = 500;
            public const float PLAYER_MOVE_SPEED = 2;
            public const float MAX_DEGREES_ROTATE = 720f;
            public const float PLAYER_SHOOT_DELAY = 1f;
            public const int BASE_ATK_MAX_CHARGE = 3;
            public const float BASE_ATK_RELOAD_TIME = 3f;

            public const float DASH_DISTANCE = 3f;
            public const float DASH_DURATION = 0.5f;
            public const float DASH_COOLDOWN = 6f;
        }

        public struct Bullet
        {
            public const float BUTLLET_SPEED = 5f;
            public const float BULLET_ANGLE = 15f;
            public const float BULLET_DESTROY_TIME = 0.25f;
            public const float BULLET_COLLIDER_DISABLE_TIME = 0.05f;
        }

        public struct Bomb
        {
            public const float BOMBBB_EXPLOSION_DELAY = 2f;
            public const float BOMBBB_EXPLOSION_RADIUS = 5f;
            public const float BOMBBB_EXPLOSION_DAMAGE = 50f;
            public const float BOMBBB_COOLDOWN = 12f;

            public const float DASHBOMB_EXPLOSION_RADIUS = 3f;
            public const int DASHBOMB_EXPLOSION_DAMAGE = 15;
        }

        public struct SiegeMinion //con xe
        {
            public const int SIEGE_MAX_HP = 180;
            public const float SIEGE_MOVE_SPEED = 2.7f;
        }

        public struct SuperMinion //con chuy`
        {
            public const int SUPER_MAX_HP = 220;
            public const float SUPER_MOVE_SPEED = 3f;
        }
    }
}