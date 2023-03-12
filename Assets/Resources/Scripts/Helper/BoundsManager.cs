using UnityEngine;

public class BoundsManager : MonoBehaviour
{
    // Walkable Area Boundaries
    public static float walkableAreaLeftBoundary;
    public static float walkableAreaRightBoundary;

    // Children Spawn Area Boundaries
    public static float leftSpawnAreaLeftBoundary;
    public static float leftSpawnAreaRightBoundary;
    public static float rightSpawnAreaLeftBoundary;
    public static float rightSpawnAreaRightBoundary;

    // Power-Up Spawn Area Boundaries
    public static float powerUpSpawnAreaLeftBoundary;
    public static float powerUpSpawnAreaRightBoundary;
    public static float powerUpSpawnAreaTopBoundary;
    public static float powerUpSpawnAreaBottomBoundary;

    private void Awake()
    {
        // Witch and children can patrol in the walkable area
        SpriteRenderer walkableAreaSR = GameObject.FindGameObjectWithTag(Tags.WALKABLE_AREA).GetComponent<SpriteRenderer>();
        walkableAreaLeftBoundary = walkableAreaSR.bounds.min.x;
        walkableAreaRightBoundary = walkableAreaSR.bounds.max.x;

        // Children can be spawned in the children spawn area
        SpriteRenderer leftAreaSR = GameObject.Find(Tags.SPAWN_AREA_LEFT).GetComponent<SpriteRenderer>();
        SpriteRenderer rightAreaSR = GameObject.Find(Tags.SPAWN_AREA_RIGHT).GetComponent<SpriteRenderer>();
        leftSpawnAreaLeftBoundary = leftAreaSR.bounds.min.x;
        leftSpawnAreaRightBoundary = leftAreaSR.bounds.max.x;
        rightSpawnAreaLeftBoundary = rightAreaSR.bounds.min.x;
        rightSpawnAreaRightBoundary = rightAreaSR.bounds.max.x;

        // Power-ups can be spawned in the power-up spawn area
        SpriteRenderer powerUpSpawnArea = GameObject.Find(Tags.POWERUP_SPAWN_AREA).GetComponent<SpriteRenderer>();
        powerUpSpawnAreaLeftBoundary = powerUpSpawnArea.bounds.min.x;
        powerUpSpawnAreaRightBoundary = powerUpSpawnArea.bounds.max.x;
        powerUpSpawnAreaTopBoundary = powerUpSpawnArea.bounds.max.y;
        powerUpSpawnAreaBottomBoundary = powerUpSpawnArea.bounds.min.y;
    }
}
