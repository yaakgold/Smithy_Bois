using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public string throwerName;
    public float damage;
    public string tagHit, tagIgnore;
    public float speed;

    public void Fire(float _speed, Vector2 target, float _damage, string tagOfObjectToHit, string tagOfObjectToIgnore)
    {
        damage = _damage;
        tagHit = tagOfObjectToHit;
        tagIgnore = tagOfObjectToIgnore;
        speed = _speed;

        GetComponent<Rigidbody2D>().AddForce((position - transform.position).normalized * speed, ForceMode2D.Impulse);
    }

    private void Update()
    {
        transform.Rotate(Vector3.forward, speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(tagHit))
        {
            if (collision.TryGetComponent(out Health h))
            {
                h.TakeDamage(damage);
            }
        }

        if (!collision.CompareTag(tagIgnore) && !collision.CompareTag("Untagged"))
        {
            AudioManager.Instance.Play($"{throwerName} Proj Hit");
            Destroy(gameObject);
        }
    }
}
