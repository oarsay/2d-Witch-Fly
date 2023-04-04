using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Invisibility : PowerUp
{
    private PlayerManager _playerManager;
    //private PlayerAnimation _playerAnimation;

    // Hologram effect property names
    private readonly string HOLOGRAM = "_HologramBlend";
    private void Awake()
    {
        var player = GameObject.FindGameObjectWithTag(Tags.PLAYER);
        _playerManager = player.GetComponent<PlayerManager>();
        _playerAnimation = player.GetComponent<PlayerAnimation>();
        effectDuration = new WaitForSeconds(10f);
        StartCoroutine(nameof(base.AutoDestroy));
    }
    public override void Apply()
    {
        if(!_playerManager.IsInvisible)
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
        if (!_playerManager.IsInvisible)
        {
            UpdatePlayerRenderers(HOLOGRAM, 1);
            _playerManager.IsInvisible = true;
        }
    }
    public override void EndEffect()
    {
        if (_playerManager.IsInvisible)
        {
            UpdatePlayerRenderers(HOLOGRAM, 0);
            _playerManager.IsInvisible = false;
            Destroy(gameObject);
        }
    }
}
