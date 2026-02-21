using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 15f;
    [SerializeField] GameObject playerTargetPoint;
    [SerializeField] GameObject projectileHitVFX;

    Rigidbody rigidbody;
    int damage = 0;

    public void Init(int damage)
    {
        this.damage = damage;
    }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        rigidbody.linearVelocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().TakeDamage(damage);
        }

        Instantiate(projectileHitVFX, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
