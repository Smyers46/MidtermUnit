using UnityEngine;

namespace Midterm
{
    public class BulletScript : MonoBehaviour
    {

        private string ownerShipTag = "";
        private float damage = 10f;

        public void Initialize(string _ownerShipTag)
        {
            ownerShipTag = _ownerShipTag;
        }

        private void OnCollisionEnter(Collision collision)
        {

            if (!collision.gameObject.CompareTag(ownerShipTag))
            {
                HealthScript health = collision.gameObject.GetComponent<HealthScript>();
                if (health != null)
                {
                    health.TakeDamage(damage);
                }
            }

            PooledObject pooledObject = GetComponent<PooledObject>();
            if (pooledObject != null)
            {
                pooledObject.ResetObject();
            }
            else
            {
                Destroy(this.gameObject);
            }

        }
    }
}
