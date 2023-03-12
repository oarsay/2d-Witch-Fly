using System.Collections;
using UnityEngine;
public class Speed : PowerUp
{
    private PlayerManager _playerManager;
    private PlayerMovement _playerMovement;
    private readonly float _horizontalSpeedBuffAmount = 3f;
    private readonly float _verticalSpeedBuffAmount = 2f;
    private void Awake()
    {
        _playerManager = GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponent<PlayerManager>();
        _playerMovement = GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponent<PlayerMovement>();
        effectDuration = new WaitForSeconds(5f);
        StartCoroutine(nameof(base.AutoDestroy));
    }
    public override void Apply()
    {
        if (!_playerManager.OnSpeedBuff)
        {
            StartCoroutine(ApplyEffect());
        }
    }
    IEnumerator ApplyEffect()
    {
        StartEffect();
        yield return effectDuration;
        EndEffect();
    }
    public override void StartEffect()
    {
        if(!_playerManager.OnSpeedBuff)
        {
            _playerMovement.BaseMoveSpeedHorizontal += _horizontalSpeedBuffAmount;
            _playerMovement.CurrentMoveSpeedVertical += _verticalSpeedBuffAmount;
            _playerManager.OnSpeedBuff = true;
        }
    }
    public override void EndEffect()
    {
        if (_playerManager.OnSpeedBuff)
        {
            _playerMovement.BaseMoveSpeedHorizontal -= _horizontalSpeedBuffAmount;
            _playerMovement.CurrentMoveSpeedVertical -= _verticalSpeedBuffAmount;
            _playerManager.OnSpeedBuff = false;
            Destroy(gameObject);
        }
    }
}
