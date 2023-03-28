using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Invisibility : PowerUp
{
    private List<Renderer> _playerRenderers;
    private PlayerManager _playerManager;

    // Hologram effect property names
    private readonly string HOLOGRAM = "_HologramBlend";
    private void Awake()
    {
        _playerManager = GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponent<PlayerManager>();
        _playerRenderers = GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponentsInChildren<Renderer>().ToList();
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
            UpdateRenderersForInvisibility(true);
            _playerManager.IsInvisible = true;
        }
    }
    public override void EndEffect()
    {
        if (_playerManager.IsInvisible)
        {
            UpdateRenderersForInvisibility(false);
            _playerManager.IsInvisible = false;
            Destroy(gameObject);
        }
    }

    private void UpdateRenderersForInvisibility(bool onStartInvisibility)
    {
        if (onStartInvisibility)
        {
            foreach(Renderer renderer in _playerRenderers)
            {
                renderer.material.SetFloat(HOLOGRAM, 1);
            }
        }
        else
        {
            foreach (Renderer renderer in _playerRenderers)
            {
                renderer.material.SetFloat(HOLOGRAM, 0);
            }
        }
    }
}
