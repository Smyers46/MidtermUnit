using UnityEngine;

namespace Midterm
{
    public class ProjectileScript : MonoBehaviour
    {
        [SerializeField] private float bulletSpeed = 5f;
        [SerializeField] private float damage = 10f;
        [SerializeField] private float lifetime = 5f;

        private void Start()
        {
            Destroy(gameObject, lifetime);
        }

        private void Update()
        {
            
            transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime, Space.Self);
        }

        private void OnTriggerEnter(Collider other)
        {
            HealthScript health = other.GetComponent<HealthScript>();

            if (health != null)
            {
                health.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }
}
