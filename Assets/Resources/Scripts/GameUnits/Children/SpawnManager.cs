using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //All children models (will be loaded from Resources folder)
    private static GameObject[] _childrenPrefabs;
    private static int _maxChildrenNumber = 5;

    //Chosen position and child model for the next instantiation
    private static float _spawnPositionX;
    private static readonly float _spawnPositionY = -3.4696f;
    private static GameObject _childPrefab;

    //"Children" gameobject on the scene, parent (container) object for all children
    private static Transform _childrenParentTransform;
    private static readonly string _childrenParentObjectTag = Tags.CHILDREN;
    private static PlayerMovement _playerMovement;

    private void Awake()
    {
        _playerMovement = GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponent<PlayerMovement>();
        InitChildrenParentGameObject();
        LoadChildrenPrefabs();
    }

    private void Start()
    {
        //EDIT
        SpawnNewChildren();
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
        _childrenPrefabs = Resources.LoadAll<GameObject>(Tags.CHILDREN_PREFABS_LOCATION);
        if (_childrenPrefabs == null) ExceptionHandler.Throw("SpawnManager/LoadChildrenPrefabs/Children prefabs cannot be loaded!");
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
        return ChildManager.numberOfChildren < SpawnManager._maxChildrenNumber;
    }
    private void SpawnAChild()
    {
        SelectRandomChildPrefab();
        GenerateSpawnPositionX();
        Vector3 spawnPos = new(_spawnPositionX, _spawnPositionY, -1);
        Instantiate(_childPrefab, spawnPos, Quaternion.identity, _childrenParentTransform);
    }
    private void SelectRandomChildPrefab()
    {
        int childPrefabIndex = Random.Range(0, _childrenPrefabs.Length);
        _childPrefab = _childrenPrefabs[childPrefabIndex];
    }
    private void GenerateSpawnPositionX()
    {
        if (_playerMovement.DirectionHorizontal == PlayerMovement.Direction.Right)
        {
            _spawnPositionX = Random.Range(BoundsManager.rightSpawnAreaLeftBoundary, BoundsManager.rightSpawnAreaRightBoundary);
        }
        else if (_playerMovement.DirectionHorizontal == PlayerMovement.Direction.Left)
        {
            _spawnPositionX = Random.Range(BoundsManager.leftSpawnAreaLeftBoundary, BoundsManager.leftSpawnAreaRightBoundary);
        }
        else
        {
            ExceptionHandler.Throw("SpawnManager.cs/GenerateSpawnPositionX/Unknown direction state in if-else!");
        }
    }
}
