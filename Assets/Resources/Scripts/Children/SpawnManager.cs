using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //All children models (will be loaded from Resources folder)
    private static GameObject[] _childrenPrefabs;
    private static int _maxChildrenNumber = 5;

    //Chosen position and child model for the next instantiation
    private static Vector3 _spawnPosition;
    private static GameObject _childPrefab;
    private static Vector3 _leftSpawnAreaLeftBoundary;
    private static Vector3 _leftSpawnAreaRightBoundary;
    private static Vector3 _rightSpawnAreaLeftBoundary;
    private static Vector3 _rightSpawnAreaRightBoundary;

    //"Children" gameobject on the scene, parent (container) object for all children
    private static Transform _childrenParentTransform;
    private static readonly string _childrenParentObjectTag = "Children";
    private static PlayerMovement _playerMovement;

    private void Awake()
    {
        _playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        InitSpawnAreaBoundaries();
        InitChildrenParentGameObject();
        LoadChildrenPrefabs();
    }
    private void InitSpawnAreaBoundaries()
    {
        var leftArea = transform.Find("SpawnAreaLeft").GetComponent<SpriteRenderer>();
        var rightArea = transform.Find("SpawnAreaRight").GetComponent<SpriteRenderer>();
        _leftSpawnAreaLeftBoundary = leftArea.bounds.min;
        _leftSpawnAreaRightBoundary = leftArea.bounds.max;
        _rightSpawnAreaLeftBoundary = rightArea.bounds.min;
        _rightSpawnAreaRightBoundary = rightArea.bounds.max;
    }
    private void InitChildrenParentGameObject()
    {
        //Search for Children gameobject on the scene
        var _childrenParent = GameObject.FindWithTag(_childrenParentObjectTag);

        //If it doesn't already exist, create one and set its tag.
        if (!_childrenParent)
        {
            var emptyChildrenParentObject = new GameObject(_childrenParentObjectTag);
            emptyChildrenParentObject.tag = _childrenParentObjectTag;
            _childrenParent = emptyChildrenParentObject;
        }

        //Finally, get its transform.
        _childrenParentTransform = _childrenParent.transform;
    }
    private void LoadChildrenPrefabs()
    {
        _childrenPrefabs = Resources.LoadAll<GameObject>("Prefabs/Children");
        if (_childrenPrefabs == null) ExceptionHandler.Throw(_childrenPrefabs);
    }
    //This method is called by the event
    public void SpawnNewChildren()
    {
        while (IsSpawnNeeded())
        {
            SpawnAChild();
        }
    }
    private bool IsSpawnNeeded()
    {
        return Child.numberOfChildren < SpawnManager._maxChildrenNumber;
    }
    private void SpawnAChild()
    {
        SelectRandomChildPrefab();
        GenerateSpawnPosition();
        Instantiate(_childPrefab, _spawnPosition, Quaternion.identity, _childrenParentTransform);
    }
    private void SelectRandomChildPrefab()
    {
        int childPrefabIndex = Random.Range(0, _childrenPrefabs.Length);
        _childPrefab = _childrenPrefabs[childPrefabIndex];
    }
    private void GenerateSpawnPosition()
    {
        if (_playerMovement.DirectionHorizontal == PlayerMovement.Direction.Right)
        {
            float randomPointInSpawnArea = Random.Range(_rightSpawnAreaLeftBoundary.x, _rightSpawnAreaRightBoundary.x);
            _spawnPosition = new Vector3(randomPointInSpawnArea, _rightSpawnAreaLeftBoundary.y, 0);
        }
        else if (_playerMovement.DirectionHorizontal == PlayerMovement.Direction.Left)
        {
            float randomPointInSpawnArea = Random.Range(_leftSpawnAreaLeftBoundary.x, _leftSpawnAreaRightBoundary.x);
            _spawnPosition = new Vector3(randomPointInSpawnArea, _leftSpawnAreaLeftBoundary.y, 0);
        }
        else
        {
            ExceptionHandler.Throw(_playerMovement.DirectionHorizontal);
        }
    }
}
