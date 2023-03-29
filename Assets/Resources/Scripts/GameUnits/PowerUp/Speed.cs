using System.Collections;
using UnityEngine;
public class Speed : PowerUp
{
    [SerializeField] private GameEvent _gameEventOnSpeedBuffStart;
    [SerializeField] private GameEvent _gameEventOnSpeedBuffEnd;
    private PlayerManager _playerManager;
    private PlayerMovement _playerMovement;
    private VFXManager _vfxManager;
    private readonly float _horizontalSpeedBuffAmount = 3f;
    private readonly float _verticalSpeedBuffAmount = 2f;
    private void Awake()
    {
        _playerManager = GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponent<PlayerManager>();
        _playerMovement = GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponent<PlayerMovement>();
        _vfxManager = GameObject.Find(Tags.VFX_MANAGER).GetComponent<VFXManager>();
        effectDuration = new WaitForSeconds(5f);
        StartCoroutine(nameof(base.AutoDestroy));
    }
    public override void Apply()
    {
        StartCoroutine(ApplyEffect());
    }
    IEnumerator ApplyEffect()
    {
        StartEffect();
        yield return effectDuration;
        EndEffect();
    }
    public override void StartEffect()
    {
        _gameEventOnSpeedBuffStart.TriggerEvent();
        _playerMovement.BaseMoveSpeedHorizontal += _horizontalSpeedBuffAmount;
        _playerMovement.CurrentMoveSpeedVertical += _verticalSpeedBuffAmount;
        _vfxManager.SetSpeedLineVFX(true);
    }
    public override void EndEffect()
    {
        _gameEventOnSpeedBuffEnd.TriggerEvent();
        _playerMovement.BaseMoveSpeedHorizontal -= _horizontalSpeedBuffAmount;
        _playerMovement.CurrentMoveSpeedVertical -= _verticalSpeedBuffAmount;
        _vfxManager.SetSpeedLineVFX(false);
        Destroy(gameObject);
    }
}
