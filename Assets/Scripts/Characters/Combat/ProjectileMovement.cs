using Beehaw.Character;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    [Header("Projectile")]
    [SerializeField, Range(0, 25f)] private float projectileSpeed;
    [SerializeField, Range(1, 5)] private int damageAmount;
    private float projectileLifetime = 5f;

    private void Update()
    {
        transform.position += transform.right * projectileSpeed * Time.deltaTime;
        if (projectileLifetime < 0)
        {
            Destroy(gameObject);
        }
        projectileLifetime -= Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject hitObject = collision.gameObject;
        if (hitObject.CompareTag("IgnoreCollision"))
        {
            return;
        }
        ApplyDamageToHitObject(hitObject);
        AddKnockback(hitObject);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject hitObject = collision.gameObject;
        if (hitObject.CompareTag("IgnoreCollision"))
        {
            return;
        }
        ApplyDamageToHitObject(hitObject);
        AddKnockback(hitObject);
        Destroy(gameObject);
    }

    private void AddKnockback(GameObject hitObject)
    {
        Rigidbody2D rigidbody = hitObject.GetComponent<Rigidbody2D>();
        if (rigidbody != null)
        {
            rigidbody.AddForce((hitObject.transform.position - transform.position).normalized * projectileSpeed, ForceMode2D.Impulse);
        }
    }

    private void ApplyDamageToHitObject(GameObject hitObject)
    {
        Health health = hitObject.GetComponent<Health>();
        if (health != null)
        {
            health.applyDamage(damageAmount);
        }
    }
}
