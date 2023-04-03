using UnityEngine;

public class EmojiFollowTarget : MonoBehaviour
{
    [HideInInspector] public Transform target;

    void LateUpdate()
    {
        transform.position = target.position;
    }
}
