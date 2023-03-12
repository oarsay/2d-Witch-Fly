using System.Collections;
using UnityEngine;
public class Invisibility : PowerUp
{
    private PlayerManager _playerManager;
    private Renderer _spriteRenderer;
    private Color _colorOriginal;
    private Color _colorTransparent;
    private float _transparencyAmount = 0.1f;
    private void Awake()
    {
        _spriteRenderer = GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponent<Renderer>();
        _playerManager = GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponent<PlayerManager>();
        _colorOriginal = _spriteRenderer.material.color;
        _colorTransparent = new Color(_colorOriginal.r, _colorOriginal.g, _colorOriginal.b, _transparencyAmount);
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
            _spriteRenderer.material.color = _colorTransparent;
            _playerManager.IsInvisible = true;
        }
    }
    public override void EndEffect()
    {
        if (_playerManager.IsInvisible)
        {
            _spriteRenderer.material.color = _colorOriginal;
            _playerManager.IsInvisible = false;
            Destroy(gameObject);
        }
    }
}
