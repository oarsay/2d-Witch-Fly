using System.Collections;
using UnityEngine;

public abstract class PowerUp: MonoBehaviour
{
    protected WaitForSeconds effectDuration;
    private WaitForSeconds _lifeSpan = new(25f);

    // we shouldn't destroy a power-up while it is performing its effect
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
}
