using UnityEngine;
using System.Collections;

namespace Midterm
{
    public class BossLaserAttack : MonoBehaviour
    {
        [SerializeField] private Transform laserOrigin;
        [SerializeField] private Transform player;

        [SerializeField] private BossEnemyController boss;

        [SerializeField] private ParticleSystem chargingEffect;

        [SerializeField] private LineRenderer laserOuter;
        [SerializeField] private LineRenderer laserCore;
        [SerializeField] private LineRenderer laserWarning;

        [SerializeField] private float chargeTime = 3f;
        [SerializeField] private float fireTime = 1f;
        [SerializeField] private float cooldown = 6f;
        [SerializeField] private float laserRange = 100f;
        [SerializeField] private float damage = 25f;
        [SerializeField] private float lockTime = 0.5f;

        public float laserOffset = 0.5f;
        private Vector3 laserDirection;
        private bool hasDamagedPlayer;

        private void Start()
        {
            laserOuter.enabled = false;
            laserCore.enabled = false;
            laserWarning.enabled = false;
        }

        public void StartLaserAttack(Transform target)
        {
            player = target;

            hasDamagedPlayer = false;

            StartCoroutine(LaserAttack());
        }

        private IEnumerator LaserAttack()
        {
            // Charging effect
            chargingEffect.Play();
            AudioManager.Instance.PlaySFX(AudioManager.Instance.bossLaserCharge);

            laserWarning.enabled = true;
            
            float timer = 0f;

            // Track Player during charge, except for final lock state
            while (timer < chargeTime - lockTime)
            {
                timer += Time.deltaTime;

                laserDirection = (player.position - laserOrigin.position + Vector3.down * laserOffset).normalized;

                UpdateWarningLaser();

                yield return null;
            }

            // Locking onto Player Position when charging finishes
            // Aiming slightly below camera to enable laser visual

            laserDirection = (player.position - laserOrigin.position + Vector3.down * laserOffset).normalized;

            timer = 0f;

            // Keep warning laser visible while locked
            while (timer < lockTime)
            {
                timer += Time.deltaTime;

                UpdateWarningLaser();

                yield return null;
            }

            // Firing
            laserWarning.enabled = false;

            chargingEffect.Stop();

            AudioManager.Instance.PlaySFX(AudioManager.Instance.bossLaserAttack);

            laserOuter.enabled = true;
            laserCore.enabled = true;

            timer = 0f;

            // Keeping warning laser locked before firing
            while (timer < fireTime)
            {
                timer += Time.deltaTime;

                UpdateLaser();

                yield return null;
            }

            // Stop and cooldown

            laserOuter.enabled = false;
            laserCore.enabled = false;

            yield return new WaitForSeconds(cooldown);

            AudioManager.Instance.PlaySFX(AudioManager.Instance.laserCooldown);

            boss.LaserAttackFinished();
        }

        private void UpdateWarningLaser()
        {
            Vector3 start = laserOrigin.position;

            if (Physics.Raycast(start, laserDirection, out RaycastHit hit, laserRange))
            {
                Vector3 end = hit.point;

                laserWarning.SetPosition(0, start);
                laserWarning.SetPosition(1, end);
            }
            else
            {
                Vector3 end = start + laserDirection * laserRange;

                laserWarning.SetPosition(0, start);
                laserWarning.SetPosition(1, end);
            }
        }

        private void UpdateLaser()
        {
            Vector3 start = laserOrigin.position;

            Debug.DrawRay(start, laserDirection * laserRange, Color.red, 0.1f);

            if (Physics.Raycast(start, laserDirection, out RaycastHit hit, laserRange))
            {
                Vector3 end = hit.point;

                laserOuter.SetPosition(0, start);
                laserOuter.SetPosition(1, end);

                laserCore.SetPosition(0, start);
                laserCore.SetPosition(1, end);

                Debug.Log("Laser hit " + hit.collider.name);

                if (hit.collider.CompareTag("Player") && !hasDamagedPlayer)
                {
                    HealthScript health = hit.collider.GetComponent<HealthScript>();

                    if (health != null)
                    {
                        AudioManager.Instance.PlaySFX(AudioManager.Instance.laserExplosion);
                        health.TakeDamage(damage);
                        hasDamagedPlayer = true;
                    }
                }
            }
            else
            {
                Vector3 end = start + laserDirection * laserRange;

                laserOuter.SetPosition(0, start);
                laserOuter.SetPosition(1, end);

                laserCore.SetPosition(0, start);
                laserCore.SetPosition(1, end);
            }
        }

        public void ResetLaser()
        {
            StopAllCoroutines();

            // Turn off laser
            chargingEffect.Stop();
            laserWarning.enabled = false;
            laserOuter.enabled = false;
            laserCore.enabled = false;

            hasDamagedPlayer = false;
        }

    }
}
