using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //All children models
    [SerializeField] private GameObject[] _childrenPrefabs;

    //Chosen position and child model for the next instantiation
    private Vector3 _spawnPosition;
    private GameObject _childPrefab;

    //"Children" gameobject on the scene, parent object for all children
    private Transform _childrenParentTransform;
    private readonly string _childrenParentObjectTag = "Children";

    private void Awake()
    {
        InitChildrenParentGameObject();
    }

    private void Start()
    {
        //EDIT
        InvokeRepeating(nameof(SpawnAChild), 3f, 5f);
    }

    private void SpawnAChild()
    {
        SelectRandomChildPrefab();
        GenerateSpawnPosition();
        Instantiate(_childPrefab, _spawnPosition, Quaternion.identity, _childrenParentTransform);
    }

    private void GenerateSpawnPosition()
    {
        //EDIT
        _spawnPosition = Vector3.zero;
    }

    private void SelectRandomChildPrefab()
    {
        int childPrefabIndex = Random.Range(0, _childrenPrefabs.Length);
        _childPrefab = _childrenPrefabs[childPrefabIndex];
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
}
