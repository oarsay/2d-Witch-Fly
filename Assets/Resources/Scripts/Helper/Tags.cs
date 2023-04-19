public static class Tags
{
    // ===== TAGS =====

        // Game Unit Tags
            //Player
            public const string PLAYER = "Player";
            public const string HOOK = "Hook";

            //Children Tags
            public const string CHILD = "Child";
            public const string CHILDREN = "Children";

            //Environment Tags
            public const string WALKABLE_AREA = "WalkableArea";
            public const string HIDING_SPOT = "HidingSpot";
            public const string CAULDRON_DEEP_POINT = "CauldronDeepPoint";

        // Manager Tags
        public const string SPAWN_MANAGER = "SpawnManager";
        public const string BOUNDS_MANAGER = "BoundsManager";
        

        // Other Tags
    public const string CAMERA = "MainCamera";
        public const string CAMERA_TARGET = "CameraTarget";



    // ===== GAME OBJECT NAMES =====
        
    public const string SPAWN_AREA_LEFT = "SpawnAreaLeft";
    public const string SPAWN_AREA_RIGHT = "SpawnAreaRight";
    public const string POWERUP_SPAWN_AREA = "PowerupSpawnArea";
    public const string VFX_MANAGER = "VFXManager";


    // ===== EDITOR =====
    // Address
    public const string CHILDREN_PREFABS_LOCATION = "Prefabs/Children";
    public const string POWERUP_PREFABS_LOCATION = "Prefabs/Environment/Power-Ups";

    // ===== SCENE INDEXES =====
    public const int MENU_SCENE = 0;
    public const int GAME_SCENE_SURVIVAL_MODE = 1;
    public const int GAME_SCENE_ENDLESS_MODE = 2;

    // ===== AUDIO CLIPS =====
    public const string BUBBLING_SFX = "Bubbling";
    public const string FIRE_SFX = "Fire";
    public const string GAME_OVER_SFX = "GameOver";
    public const string CHILD_DASH_SFX = "Whoosh";
    public const string POWER_UP_SFX = "Powerup";
}
