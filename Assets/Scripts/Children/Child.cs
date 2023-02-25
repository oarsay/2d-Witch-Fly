using UnityEngine;
using Random = UnityEngine.Random;

public class Child : MonoBehaviour
{
    public static int numberOfChildren = 0;

    private static readonly float _minMoveSpeed = 1f;
    private static readonly float _maxMoveSpeed = 3f;

    private float _moveSpeed;
    public bool IsHiding { get; private set; }

    public void Awake()
    {
        //Init children here but not in the constructor, because
        //when children prefabs are assigned to the SpawnManager on the scene
        //it triggers their constructors even though they weren't instantiated on the scene.

        Child.numberOfChildren++;
        this._moveSpeed = GenerateRandomMoveSpeed();
        this.IsHiding = false;
    }

    private void Update()
    {

    }

    private void Move()
    {

    }

    private float GenerateRandomMoveSpeed()
    {
        return Random.Range(_minMoveSpeed, _maxMoveSpeed);
    }
}
