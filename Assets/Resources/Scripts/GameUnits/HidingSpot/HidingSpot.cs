using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

enum Type
{
    Bush,
    Tree,
    Fence,
    Cross,
    Bench,
    Rock
}
public class HidingSpot : MonoBehaviour
{
    public int duration, amount, vibrato;
    public Vector3 strength;
    
    private VFXManager _vfxManager;

    [SerializeField] private Type _type;
    private bool _isEmpty = true;
    public bool IsEmpty { get { return _isEmpty; } set { _isEmpty = value; } }
    private readonly float _childYPositionWhileHiding = -5.6f;

    private void Start()
    {
        _vfxManager = GameObject.Find(Tags.VFX_MANAGER).GetComponent<VFXManager>();
    }
    public void Hide(Transform child)
    {
        // Type spesific actions
        switch (_type)
        {
            case Type.Bush:
                transform.DOShakePosition(0.7f, new Vector3(0.1f, 0f, 0.1f), 10, 10);
                _vfxManager.CreateLeafEffect(transform.position);
                break;

            case Type.Tree:
                transform.DOShakePosition(0.7f, new Vector3(0.1f, 0f, 0.1f), 10, 10);
                break;

            case Type.Fence:
                transform.DOShakePosition(0.7f, new Vector3(0.1f, 0f, 0.1f), 10, 10);
                break;

            case Type.Cross:
                // ADD GLOW FLASH
                break;

            case Type.Bench:
                transform.DOShakePosition(0.7f, new Vector3(0.1f, 0f, 0.1f), 10, 10);
                break;

            case Type.Rock:
                transform.DOShakePosition(0.7f, new Vector3(0.1f, 0f, 0.1f), 10, 10);
                
                break;
        }

        // Common actions for all hiding spots
        // Create VFXs
        _vfxManager.CreateDashEffect(child.position, transform.position);
        _vfxManager.CreateSmokeEffect(child.position, transform.position);

        // Set child properties
        child.GetComponent<Rigidbody2D>().gravityScale = 0;
            child.position = new Vector3(transform.position.x, _childYPositionWhileHiding, transform.position.z);
        
            // Update hiding spot
            _isEmpty = false;
    }
}
