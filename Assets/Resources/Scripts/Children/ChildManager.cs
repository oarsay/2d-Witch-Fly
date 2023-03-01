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

        ChildManager.numberOfChildren++;
        state = ChildState.Walk;

        // EDIT
        //Destroy(gameObject, Random.Range(5, 15));
    }

    private void Update()
    {
        switch(state)
        {
            case ChildState.Walk:
                if(IsWitchSeen())
                {
                    transform.GetComponentInChildren<SpriteRenderer>().color = Color.red;
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

    private void OnDestroy()
    {
        ChildManager.numberOfChildren--;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(state == ChildState.Flee)
            {
                state = ChildState.Hunted;
                _gameEventOnChildHunted.TriggerEvent();
                OnHunted();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _fleeRemainingTime = _fleeDuration;
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
            transform.GetComponentInChildren<SpriteRenderer>().color = Color.green;
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
        transform.GetComponentInChildren<SpriteRenderer>().color = Color.cyan;
    }
}
