using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        // Common actions for all hiding spots
            // Create VFXs
            _vfxManager.CreateDashEffect(child.position, transform.position);

            // Set child properties
            child.GetComponent<Rigidbody2D>().gravityScale = 0;
            child.position = new Vector3(transform.position.x, _childYPositionWhileHiding, transform.position.z);
        
            // Update hiding spot
            _isEmpty = false;

        // Type spesific actions
        switch(_type)
        {
            case Type.Bush:
                _vfxManager.CreateLeafEffect(transform.position);
                break;
            case Type.Tree: break;
            case Type.Fence: break;
            case Type.Cross: break;
            case Type.Bench: break;
            case Type.Rock: break;
        }
    }
}
