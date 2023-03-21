using UnityEngine;

public class VFXManager : MonoBehaviour
{
    [SerializeField] private GameObject _dashPrefab;
    public float angleX;
    public float angleY;
    public float angleZ;
    public static void CreateDashEffect(Vector3 startPosition, Vector3 endPosition)
    {
        //Transform dashTransform = Instantiate(_dashEffect, position, Quaternion.identity);
        //dashTransform.localEulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(dir));
        //dashTransform.localScale = new Vector3(dashSize / 35f, 1, 1);
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
            GameObject dash = Instantiate(_dashPrefab, worldPosition, Quaternion.identity);
            dash.transform.localEulerAngles = new Vector3(angleX, angleY, angleZ);
        }
    }
}
