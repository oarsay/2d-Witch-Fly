using System.Collections;
using UnityEngine;

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
    private HidingSpot _hidingSpot;
    private Rigidbody2D _rigidbody;
    [HideInInspector] public ChildMovement _childMovement;
    [SerializeField] private GameEvent _gameEventOnChildFlee;
    [SerializeField] private GameEvent _gameEventOnChildHunted;

    public static int numberOfChildren = 0;
    [HideInInspector] public ChildState state;
    private static readonly float _sightRange = 6f;
    private static readonly float _fleeDuration = 3f;
    private float _fleeRemainingTime = 0;
    private WaitForSeconds _refreshHidingDuration = new(3);

    public void Awake()
    {
        //Init children here but not in the constructor, because
        //when children prefabs are assigned to the SpawnManager on the scene
        //it triggers their constructors even though they weren't instantiated on the scene.

        _childMovement = GetComponent<ChildMovement>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _hook = GameObject.FindGameObjectWithTag(Tags.HOOK).transform;
        _witch = GameObject.FindGameObjectWithTag(Tags.PLAYER).transform;
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
            case ChildState.Hide:
                
                break;
            case ChildState.Hunted:
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Tags.PLAYER) && _witchManager.IsHookEmpty)
        {
            if(state == ChildState.Flee)
            {
                state = ChildState.Hunted;
                _gameEventOnChildHunted.TriggerEvent();
                OnHunted();
            }
        }
        else if(collision.CompareTag(Tags.HIDING_SPOT) && state == ChildState.Flee && collision.gameObject.GetComponent<HidingSpot>().IsEmpty)
        {
            _hidingSpot = collision.gameObject.GetComponent<HidingSpot>();
            state = ChildState.Hide;
            _hidingSpot.Hide(transform);
            StartCoroutine(OnHide());
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
        if (_witchManager.IsInvisible) return false;

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

    private IEnumerator OnHide()
    {
        // do-while was used instead of while loop
        // because I want a child to hide for a certain time
        // once he/she just starts hiding. Otherwise, for some cases
        // the child would hide and leave immediately.

        // Hide as long as you are safe
        do
        {
            yield return _refreshHidingDuration;
        } while (IsWitchSeen());

        // Now you can leave
        state = ChildState.Walk;
        _hidingSpot.IsEmpty = true;
    }
    private void OnDestroy()
    {
        numberOfChildren--;
    }
}
