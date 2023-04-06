using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private ParticleSystem[] _tailParticleSystems;
    [SerializeField] private float _tailStartLifetimeOnIdle;
    [SerializeField] private float _tailStartLifetimeOnSpeed;
    [HideInInspector] public List<Renderer> renderers;

    // Animation parameters
    const string ON_SPEED = "onSpeed";
    const string LAUGH = "laugh";

    void Start()
    {
        _animator = GetComponent<Animator>();
        renderers = GetComponentsInChildren<Renderer>().ToList();
    }

    public void OnChildHunt()
    {
        _animator.SetTrigger(LAUGH);
    }

    public void StartSpeedBuffAnimation()
    {
        _animator.SetBool(ON_SPEED, true);
        foreach(ParticleSystem tailParticleSystem in _tailParticleSystems)
        {
            var main = tailParticleSystem.main;
            main.startLifetime = _tailStartLifetimeOnSpeed;
        }
    }

    public void EndSpeedBuffAnimation()
    {
        _animator.SetBool(ON_SPEED, false);
        foreach (ParticleSystem tailParticleSystem in _tailParticleSystems)
        {
            var main = tailParticleSystem.main;
            main.startLifetime = _tailStartLifetimeOnIdle;
        }
    }

    public void StartInvisibilityAnimation()
    {
        foreach (ParticleSystem tailParticleSystem in _tailParticleSystems)
        {
            var main = tailParticleSystem.main;
            var currentColor = main.startColor.color;
            currentColor.a = 0.012f;
            main.startColor = currentColor;
        }
    }

    public void EndInvisibilityAnimation()
    {
        foreach (ParticleSystem tailParticleSystem in _tailParticleSystems)
        {
            var main = tailParticleSystem.main;
            var currentColor = main.startColor.color;
            currentColor.a = 1f;
            main.startColor = currentColor;
        }
    }
}