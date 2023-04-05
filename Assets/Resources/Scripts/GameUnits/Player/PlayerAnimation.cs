using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;
    [HideInInspector] public List<Renderer> renderers;

    // Animation parameters
    const string ON_SPEED = "onSpeed";
    const string LAUGH = "laugh";

    // AllinOneShader shine effect property names
    private readonly string SHINE_POSITION = "_ShineLocation";
    private readonly string SHINE_INTENSITY = "_ShineGlow";
    [SerializeField] private float _shineEffectSpeed = 1.2f;

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
        StartCoroutine(nameof(PlayShineEffect));
    }

    public void EndSpeedBuffAnimation()
    {
        _animator.SetBool(ON_SPEED, false);
    }

    private IEnumerator PlayShineEffect()
    {
        // init shine values
        float currentPosition = 0;
        float currentIntensity = 0;
        foreach (Renderer renderer in renderers)
        {
            renderer.material.SetFloat(SHINE_INTENSITY, 1);
            renderer.material.SetFloat(SHINE_POSITION, 0);
        }

        //while(currentIntensity < 1)
        //{
        //    foreach (Renderer renderer in renderers)
        //    {
        //        renderer.material.SetFloat(SHINE_INTENSITY, currentIntensity);
        //    }
        //    currentIntensity += _shineEffectSpeed * Time.deltaTime * 2;
        //    yield return null;
        //}

        // move shine effect
        while (currentPosition < 1)
        {
            foreach (Renderer renderer in renderers)
            {
                renderer.material.SetFloat(SHINE_POSITION, currentPosition);
            }
            currentPosition += _shineEffectSpeed * Time.deltaTime;
            yield return null;
        }

        //while (currentIntensity > 0)
        //{
        //    foreach (Renderer renderer in renderers)
        //    {
        //        renderer.material.SetFloat(SHINE_INTENSITY, currentIntensity);
        //    }
        //    currentIntensity -= _shineEffectSpeed * Time.deltaTime * 2;
        //    yield return null;
        //}

        // end shine effect
        foreach (Renderer renderer in renderers)
        {
            renderer.material.SetFloat(SHINE_INTENSITY, 0);
            renderer.material.SetFloat(SHINE_POSITION, 1);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(nameof(PlayShineEffect));
        }
    }

}
