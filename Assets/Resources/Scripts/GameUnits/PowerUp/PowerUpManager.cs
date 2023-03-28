using UnityEngine;
using Random = UnityEngine.Random;

public class PowerUpManager : MonoBehaviour
{
    //All power-up models (will be loaded from Resources folder)
    private static GameObject[] _powerupPrefabs;
    private Transform _player;
    private float _spawnRateInSeconds = 30;
    private float _minSpawnDistanceToPlayer = 15f;
    private void Awake()
    {
        LoadPowerupPrefabs();
    }

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag(Tags.PLAYER).transform;
        InvokeRepeating(nameof(SpawnPowerUp), _spawnRateInSeconds/2, _spawnRateInSeconds);
    }

    private void LoadPowerupPrefabs()
    {
        _powerupPrefabs = Resources.LoadAll<GameObject>(Tags.POWERUP_PREFABS_LOCATION);
        if (_powerupPrefabs == null) ExceptionHandler.Throw("PowerUpManager/LoadPowerupPrefabs/Power-up prefabs cannot be loaded!");
    }

    private void SpawnPowerUp()
    {
        // Generate spawn position
        Vector3 candidateSpawnPosition;
        do
        {
            candidateSpawnPosition = GenerateSpawnPosition();
        } while (Vector3.Distance(_player.position, candidateSpawnPosition) > _minSpawnDistanceToPlayer);

        // Select power-up type
        int prefabIndex = SelectRandomPowerupPrefabIndex();

        Instantiate(_powerupPrefabs[1], candidateSpawnPosition, Quaternion.identity);
    }

    private int SelectRandomPowerupPrefabIndex()
    {
        return Random.Range(0, _powerupPrefabs.Length);
    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPositionX = Random.Range(BoundsManager.powerUpSpawnAreaLeftBoundary, BoundsManager.powerUpSpawnAreaRightBoundary);
        float spawnPositionY = Random.Range(BoundsManager.powerUpSpawnAreaBottomBoundary, BoundsManager.powerUpSpawnAreaTopBoundary);
        return new Vector3(spawnPositionX, spawnPositionY, 0f);
    }
}
