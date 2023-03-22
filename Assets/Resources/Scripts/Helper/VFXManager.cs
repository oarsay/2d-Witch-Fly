using UnityEngine;

public class VFXManager : MonoBehaviour
{
    [Header("Children's Dash VFX")]
        [SerializeField] private GameObject _dashPrefab;
        [Tooltip("Angle from ground")]
        [SerializeField] [Range(0, 10)] private float angleZ;
    [Header("Bursting Leaf VFX")]
        [SerializeField] private GameObject _leafPrefab;
    public void CreateDashEffect(Vector3 startPosition, Vector3 endPosition)
    {
        GameObject dash = Instantiate(_dashPrefab, startPosition, Quaternion.identity);

        if(startPosition.x < endPosition.x)
        {
            dash.transform.localEulerAngles = new Vector3(0, 0, angleZ);
        }
        else
        {
            dash.transform.localEulerAngles = new Vector3(0, 0, 180 - angleZ);
        }
    }

    public void CreateLeafEffect(Vector3 startPosition)
    {
        Instantiate(_leafPrefab, startPosition, Quaternion.identity);
    }
}
