using UnityEngine;

public class VFXManager : MonoBehaviour
{
    [Header("Children's Dash VFX")]
        [SerializeField] private GameObject _dashPrefab;
        [Tooltip("Angle from ground")]
        [SerializeField] [Range(0, 10)] private float angleZ;
    [Header("Bursting Leaf VFX")]
        [SerializeField] private GameObject _leafPrefab;
    [Header("Bursting Smoke VFX")]
        [SerializeField] private GameObject _smokePrefab;
    [Header("Speed Lines VFX")]
        [SerializeField] private ParticleSystem _speedLinesParticleSystem;
    [Header("Cauldron Splash VFX")]
        [SerializeField] private ParticleSystem _splashPrefab;
    [Header("Emoji VFXs")]
        [SerializeField] private GameObject[] _devilEmojiPrefabs;
        [SerializeField] private GameObject[] _screamEmojiPrefabs;
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

    public void CreateSmokeEffect(Vector3 childPosition, Vector3 hidingPosition)
    {
        GameObject smoke = Instantiate(_smokePrefab, hidingPosition, Quaternion.identity);

        if (childPosition.x < hidingPosition.x)
        {
            // if child is coming from left, burst smoke to the right
            smoke.transform.localEulerAngles = new Vector3(0, 0, -90);
        }
        else
        {
            // if child is coming from right, burst smoke to the left
            smoke.transform.localEulerAngles = new Vector3(0, 0, 90);
        }
    }

    /// <summary>
    /// Enable or disable SpeedLineVFX Particle System attached to main camera.
    /// </summary>
    /// <param name="enable">True to enable, false to disable</param>
    public void SetSpeedLineVFX(bool enable)
    {
        if(enable)
        {
            if(!_speedLinesParticleSystem.isPlaying)
                _speedLinesParticleSystem.Play();
        }
        else
        {
            if (_speedLinesParticleSystem.isPlaying)
                _speedLinesParticleSystem.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }

    public void CreateSplashEffect()
    {
        Instantiate(_splashPrefab);
    }

    public void CreateDevilEmoji(Transform witchTransform)
    {
        int prefabIndex = SelectRandomIndexFromPrefabsArray(_devilEmojiPrefabs.Length);
        var emoji = Instantiate(_devilEmojiPrefabs[prefabIndex], witchTransform.position, Quaternion.identity);
        emoji.GetComponent<EmojiFollowTarget>().target = witchTransform;
    }
    public void CreateScreamEmoji(Transform childTransform)
    {
        int prefabIndex = SelectRandomIndexFromPrefabsArray(_screamEmojiPrefabs.Length);
        var emoji = Instantiate(_screamEmojiPrefabs[prefabIndex], childTransform.position, Quaternion.identity);
        emoji.GetComponent<EmojiFollowTarget>().target = childTransform;
    }
    private int SelectRandomIndexFromPrefabsArray(int length)
    {
        return Random.Range(0, length);
    }
}
