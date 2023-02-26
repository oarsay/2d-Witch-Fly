using UnityEngine;
using Random = UnityEngine.Random;

public enum ChildState
{
    Walk,
    Flee,
    Hide,
    Hunted,
    Fall,
    Die
}
public class ChildManager : MonoBehaviour
{
    public ChildMovement _childMovement;

    public static int numberOfChildren = 0;
    public ChildState state;

    public void Awake()
    {
        //Init children here but not in the constructor, because
        //when children prefabs are assigned to the SpawnManager on the scene
        //it triggers their constructors even though they weren't instantiated on the scene.

        _childMovement = GetComponent<ChildMovement>();

        ChildManager.numberOfChildren++;
        state = ChildState.Walk;

        // EDIT
        Destroy(gameObject, Random.Range(5, 15));
    }

    private void OnDestroy()
    {
        ChildManager.numberOfChildren--;
    }
}
