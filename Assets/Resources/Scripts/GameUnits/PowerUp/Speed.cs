using System;
using System.Collections;
using UnityEngine;
public class Speed : PowerUp
{
    [SerializeField] private GameEvent _gameEventOnSpeedBuffStart;
    [SerializeField] private GameEvent _gameEventOnSpeedBuffEnd;
    private PlayerMovement _playerMovement;
    private PlayerManager _playerManager;
    private CameraZoom _mainCamera;
    private VFXManager _vfxManager;
    private readonly float _horizontalSpeedBuffAmount = 3f;
    private readonly float _verticalSpeedBuffAmount = 2f;

    // Motion blur effect property
    private readonly string BLUR = "_BlurIntensity";
    private readonly float _blurAmountOnIdle = 0f;
    private readonly float _blurAmountOnSpeed = 2.2f;

    // Camera zoom effect properties
    private readonly float _projectionSizeOnIdle = 9f;
    private readonly float _projectionSizeOnSpeed = 10f;
    private readonly float _cameraZoomTransitionDuration = 0.3f;
    private void Awake()
    {
        _mainCamera = GameObject.FindGameObjectWithTag(Tags.CAMERA).GetComponent<CameraZoom>();
        var player = GameObject.FindGameObjectWithTag(Tags.PLAYER);
        _playerManager =  player.GetComponent<PlayerManager>();
        _playerMovement = player.GetComponent<PlayerMovement>();
        _playerAnimation = player.GetComponent<PlayerAnimation>();
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
        UpdatePlayerRenderers(BLUR, _blurAmountOnSpeed);
        _mainCamera.ZoomEffect(_projectionSizeOnSpeed, _cameraZoomTransitionDuration);
    }

    public override void EndEffect()
    {
        _gameEventOnSpeedBuffEnd.TriggerEvent();
        _playerMovement.BaseMoveSpeedHorizontal -= _horizontalSpeedBuffAmount;
        _playerMovement.CurrentMoveSpeedVertical -= _verticalSpeedBuffAmount;
        _vfxManager.SetSpeedLineVFX(false);
        UpdatePlayerRenderers(BLUR, _blurAmountOnIdle);
        _mainCamera.ZoomEffect(_projectionSizeOnIdle, _cameraZoomTransitionDuration);
        Destroy(gameObject);
    }
}
