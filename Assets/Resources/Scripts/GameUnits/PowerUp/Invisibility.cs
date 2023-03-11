using System.Collections;
using UnityEngine;
public class Invisibility : PowerUp
{
    private PlayerManager _playerManager;
    private Renderer _spriteRenderer;
    private Color _colorOriginal;
    private Color _colorTransparent;
    private float _transparencyAmount = 0.3f;
    private void Awake()
    {
        _spriteRenderer = GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponent<Renderer>();
        _playerManager = GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponent<PlayerManager>();
        _colorOriginal = _spriteRenderer.material.color;
        _colorTransparent = new Color(_colorOriginal.r, _colorOriginal.g, _colorOriginal.b, _transparencyAmount);
        effectDuration = 10f;
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
        StartInvisibility();
        yield return new WaitForSeconds(effectDuration);
        EndInvisibility();
    }

    public void StartInvisibility()
    {
        _spriteRenderer.material.color = _colorTransparent;
        _playerManager.IsInvisible = true;
    }

    public void EndInvisibility()
    {
        _spriteRenderer.material.color = _colorOriginal;
        _playerManager.IsInvisible = false;
    }
}
