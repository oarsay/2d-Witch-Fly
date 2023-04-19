using System.Collections;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    protected static PlayerAnimation _playerAnimation;

    protected WaitForSeconds effectDuration;
    private readonly WaitForSeconds _lifeSpan = new(25f);

    // we shouldn't destroy a power-up while it is performing its effect on the player
    // therefore before destroying. we need to check if this power-up is in use or not
    private bool _onDestroyAvailable = true;
    public abstract void Apply();
    public abstract void StartEffect();
    public abstract void EndEffect();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Tags.PLAYER))
        {
            _onDestroyAvailable = false;
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            GetComponentInChildren<ParticleSystem>().Stop();
            Apply();
        }
    }

    public IEnumerator AutoDestroy()
    {
        yield return _lifeSpan;
        if (_onDestroyAvailable)
        {
            Destroy(gameObject);
        }
    }

    protected void UpdatePlayerRenderers(string propertyName, float newValue)
    {
        foreach (Renderer renderer in _playerAnimation.renderers)
        {
            renderer.material.SetFloat(propertyName, newValue);
        }
    }

    protected void UpdatePlayerRenderers(string propertyName, float newValue, float duration, bool boomerang)
    {
        StartCoroutine(Transition(propertyName, newValue, duration, boomerang));
    }

    IEnumerator Transition(string propertyName, float endPoint, float transitionDuration, bool boomerang)
    {
        if (boomerang)
        {
            float elapsedTime = 0;
            float startPoint = _playerAnimation.renderers[0].material.GetFloat(propertyName);

            // Make transition by using half of the total time
            while (elapsedTime < transitionDuration / 2)
            {
                float currentLerpValue = Mathf.Lerp(startPoint, endPoint, elapsedTime / (transitionDuration / 2));
                foreach (Renderer renderer in _playerAnimation.renderers)
                {
                    renderer.material.SetFloat(propertyName, currentLerpValue);
                }
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Make REVERSE transition by using the other half of the time
            elapsedTime = 0;
            while (elapsedTime < transitionDuration / 2)
            {
                float currentLerpValue = Mathf.Lerp(endPoint, startPoint, elapsedTime / (transitionDuration / 2));
                foreach (Renderer renderer in _playerAnimation.renderers)
                {
                    renderer.material.SetFloat(propertyName, currentLerpValue);
                }
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            foreach (Renderer renderer in _playerAnimation.renderers)
            {
                renderer.material.SetFloat(propertyName, startPoint);
            }
        }
        else
        {
            float elapsedTime = 0;
            float startPoint = _playerAnimation.renderers[0].material.GetFloat(propertyName);

            while (elapsedTime < transitionDuration)
            {
                float currentLerpValue = Mathf.Lerp(startPoint, endPoint, (elapsedTime / transitionDuration));
                foreach (Renderer renderer in _playerAnimation.renderers)
                {
                    renderer.material.SetFloat(propertyName, currentLerpValue);
                }
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            foreach (Renderer renderer in _playerAnimation.renderers)
            {
                renderer.material.SetFloat(propertyName, endPoint);
            }
        }
    }
}
