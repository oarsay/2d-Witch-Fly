using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Invisibility : PowerUp
{
    private PlayerManager _playerManager;
    private VFXManager _vfxManager;

    // Shader effect property names
    private readonly string HOLOGRAM = "_HologramBlend";
    private readonly string ROUND_WAVE = "_RoundWaveStrength";
    private void Awake()
    {
        var player = GameObject.FindGameObjectWithTag(Tags.PLAYER);
        _playerManager = player.GetComponent<PlayerManager>();
        _playerAnimation = player.GetComponent<PlayerAnimation>();
        _vfxManager = GameObject.Find(Tags.VFX_MANAGER).GetComponent<VFXManager>();
        effectDuration = new WaitForSeconds(10f);
        StartCoroutine(nameof(base.AutoDestroy));
    }
    public override void Apply()
    {
        if(!_playerManager.IsInvisible)
        {
            _vfxManager.CreateInvisibilityPowerupEffect(transform.position);
            AudioManager.Instance.PlaySoundWithName(Tags.POWER_UP_INVISIBILITY_SFX);
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
        if (!_playerManager.IsInvisible)
        {
            UpdatePlayerRenderers(HOLOGRAM, 1, 0.8f, false);
            UpdatePlayerRenderers(ROUND_WAVE, 1, 0.4f, true);
            _playerManager.IsInvisible = true;
            _playerAnimation.StartInvisibilityAnimation();
        }
    }
    public override void EndEffect()
    {
        if (_playerManager.IsInvisible)
        {
            UpdatePlayerRenderers(HOLOGRAM, 0);
            _playerManager.IsInvisible = false;
            _playerAnimation.EndInvisibilityAnimation();
            Destroy(gameObject);
        }
    }
}
