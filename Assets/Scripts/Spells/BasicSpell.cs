using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class BasicSpell : MonoBehaviour
{
    private ISpellTarget _spellTarget;

    [SerializeField] private int _damage;
    [SerializeField] private float _knockbackForce;

    [SerializeField] private float _travelSpeed;
    [SerializeField] private float _range;
    [SerializeField] private GameObject _sparksPrefab;
    [SerializeField] private AudioClip _soundEffect;
    [SerializeField] private AudioSource _soundSource;

    private Vector3 _startPosition;

    public void Start()
    {
        _spellTarget = GetComponent<ISpellTarget>();

        _startPosition = transform.position;
        _soundSource.clip = _soundEffect;
        _soundSource.Play();
    }

    public void Update()
    {
        if (Vector3.Distance(transform.position, _startPosition) >= _range) Destroy(gameObject);

        Collider2D collider = Physics2D.OverlapCircle(transform.position, 0.25f, _spellTarget.IgnoreMask());
        if (collider != null)
        {
            GameObject sparksObject = Instantiate(_sparksPrefab, transform.position, transform.rotation);
            sparksObject.GetComponent<VisualEffect>().Play();
            IHittable targetHit = collider.gameObject.GetComponent<IHittable>();
            if (targetHit != null)
            {
                Hit hit = new Hit();
                hit.damage = _damage;
                hit.knockbackDirection = (transform.position - collider.transform.position).normalized;
                hit.knockbackForce = _knockbackForce;
                targetHit.Hit(hit);
            }
            Destroy(gameObject);
        }

        transform.position += transform.up * Time.deltaTime * _travelSpeed;
    }
}
