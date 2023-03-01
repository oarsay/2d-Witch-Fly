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
    private Transform _witch;
    private Transform _hook;
    private PlayerManager _witchManager;
    private Rigidbody2D _rigidbody;
    [HideInInspector] public ChildMovement _childMovement;
    [SerializeField] private GameEvent _gameEventOnChildFlee;
    [SerializeField] private GameEvent _gameEventOnChildHunted;

    public static int numberOfChildren = 0;
    [HideInInspector] public ChildState state;
    private static readonly float _sightRange = 6f;
    private static readonly float _fleeDuration = 3f;
    private float _fleeRemainingTime = 0;

    public void Awake()
    {
        //Init children here but not in the constructor, because
        //when children prefabs are assigned to the SpawnManager on the scene
        //it triggers their constructors even though they weren't instantiated on the scene.

        _childMovement = GetComponent<ChildMovement>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _hook = GameObject.FindGameObjectWithTag("Hook").transform;
        _witch = GameObject.FindGameObjectWithTag("Player").transform;
        _witchManager = _witch.GetComponent<PlayerManager>();

        ChildManager.numberOfChildren++;
        state = ChildState.Walk;
    }

    private void Update()
    {
        switch(state)
        {
            case ChildState.Walk:
                if(IsWitchSeen())
                {
                    state = ChildState.Flee;
                    ResetFleeTimer();
                    _gameEventOnChildFlee.TriggerEvent();
                }
                break;
            case ChildState.Flee:
                OnFlee();
                break;
            case ChildState.Hunted:
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && _witchManager.IsHookEmpty)
        {
            if(state == ChildState.Flee)
            {
                state = ChildState.Hunted;
                _gameEventOnChildHunted.TriggerEvent();
                OnHunted();
            }
        }
    }
    private void OnFlee()
    {
        if(IsWitchSeen())
        {
            ResetFleeTimer();
        }

        if (_fleeRemainingTime > 0)
        {
            _fleeRemainingTime -= Time.deltaTime;
        }
        else
        {
            state = ChildState.Walk;
        }
    }
    private bool IsWitchSeen()
    {
        float distanceToWitch = Vector2.Distance(transform.position, _witch.position);

        return distanceToWitch < _sightRange;
    }

    private void ResetFleeTimer()
    {
        _fleeRemainingTime = _fleeDuration;
    }

    private void OnHunted()
    {
        _rigidbody.gravityScale = 0;
        transform.SetParent(_hook);
        transform.position = _hook.position;
        transform.Rotate(new(0, 0, 90));
    }

    public void OnFall()
    {
        // Before falling, need to check if it is the hunted child.
        // Because all children listen for the falling event
        // If we don't check, all children will respond to this event.

        if(state == ChildState.Hunted)
        {
            state = ChildState.Fall;
            transform.SetParent(null);
        }
    }

    public void OnDeep()
    {
        // Before deep, need to check if it is the falling child.
        // Because all children listen for the deep event
        // If we don't check, all children will respond to this event.

        if (state == ChildState.Fall)
        {
            state = ChildState.Die;
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        ChildManager.numberOfChildren--;
    }
}
