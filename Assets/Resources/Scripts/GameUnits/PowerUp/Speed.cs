using System;
using System.Collections;
using UnityEngine;
public class Speed : PowerUp
{
    [SerializeField] private GameEvent _gameEventOnSpeedBuffStart;
    [SerializeField] private GameEvent _gameEventOnSpeedBuffEnd;
    private PlayerMovement _playerMovement;
    private PlayerManager _playerManager;
    private PlayerAnimation _playerAnimation;
    private VFXManager _vfxManager;
    private readonly float _horizontalSpeedBuffAmount = 3f;
    private readonly float _verticalSpeedBuffAmount = 2f;

    // Motion blur effect property names
    private readonly string MOTION_BLUR = "_MotionBlurDist";
    private void Awake()
    {
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
        UpdateRenderersForMotionBlur(true);
    }

    public override void EndEffect()
    {
        _gameEventOnSpeedBuffEnd.TriggerEvent();
        _playerMovement.BaseMoveSpeedHorizontal -= _horizontalSpeedBuffAmount;
        _playerMovement.CurrentMoveSpeedVertical -= _verticalSpeedBuffAmount;
        _vfxManager.SetSpeedLineVFX(false);
        UpdateRenderersForMotionBlur(false);
        Destroy(gameObject);
    }

    private void UpdateRenderersForMotionBlur(bool onStartMotionBlur)
    {
        if (_playerAnimation == null) Debug.Log("null");

        if (onStartMotionBlur)
        {
            foreach (Renderer renderer in _playerAnimation.renderers)
            {
                MaterialPropertyBlock materialProperty = new();
                renderer.GetPropertyBlock(materialProperty);
                materialProperty.SetFloat(MOTION_BLUR, 1);
                renderer.SetPropertyBlock(materialProperty);
            }
        }
        else
        {
            foreach (Renderer renderer in _playerAnimation.renderers)
            {
                MaterialPropertyBlock materialProperty = new();
                renderer.GetPropertyBlock(materialProperty);
                materialProperty.SetFloat(MOTION_BLUR, 0);
                renderer.SetPropertyBlock(materialProperty);
            }
        }
    }
}
