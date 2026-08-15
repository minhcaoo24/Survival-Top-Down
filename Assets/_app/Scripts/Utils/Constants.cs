namespace STD.Utils
{
    public struct Constants
    {
        public struct Player
        {
            public const int MOVE_SPEED = 2;
            public const float MAX_DEGREES_ROTATE = 720f;
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
    }
}