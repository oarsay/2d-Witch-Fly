using System.Collections;
using UnityEngine;
public class Speed : PowerUp
{
    private PlayerMovement _playerMovement;
    private readonly float _horizontalSpeedBuffAmount = 3f;
    private readonly float _verticalSpeedBuffAmount = 3f;
    private void Awake()
    {
        _playerMovement = GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponent<PlayerMovement>();
        effectDuration = 5f;
    }
    public override void Apply()
    {
        StartCoroutine(ApplyEffect());
    }
    IEnumerator ApplyEffect()
    {
        _playerMovement.BaseMoveSpeedHorizontal += _horizontalSpeedBuffAmount;
        _playerMovement.CurrentMoveSpeedVertical += _verticalSpeedBuffAmount;

        yield return new WaitForSeconds(effectDuration);

        _playerMovement.BaseMoveSpeedHorizontal -= _horizontalSpeedBuffAmount;
        _playerMovement.CurrentMoveSpeedVertical -= _verticalSpeedBuffAmount;
    }
}
