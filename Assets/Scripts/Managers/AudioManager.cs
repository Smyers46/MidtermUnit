using Unity.VisualScripting;
using UnityEngine;

namespace Midterm
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        [SerializeField] private AudioSource sfxSource;
        [SerializeField] private AudioSource musicSource;

        [Header("Audio Clips")]

        [Header("Player")]
        public AudioClip takeDamage;
        public AudioClip shootBullet;
        public AudioClip shootRocket;
        public AudioClip die;
        public AudioClip switchWeapon;
        public AudioClip walking;

        [Header("Enemy")]
        public AudioClip enemyDamage;
        public AudioClip attackState;
        public AudioClip followState;
        public AudioClip onDeath;
        public AudioClip enemyShoot;
        public AudioClip onSpawn;

        [Header("Boss")]
        public AudioClip bossAttackState;
        public AudioClip bossDeath;
        public AudioClip bossLaserCharge;
        public AudioClip bossLaserAttack;
        public AudioClip bossSpawn;
        public AudioClip laserExplosion;
        public AudioClip laserCooldown;

        [Header("General")]
        public AudioClip buttonClick;
        public AudioClip lightIndicator;
        public AudioClip doorOpen;
        public AudioClip doorClose;
        public AudioClip collectItem;
        public AudioClip useItem;
        public AudioClip ropeBroken;
        public AudioClip healthPickup;

        [Header("Robot Friend")]
        public AudioClip greeting;
        public AudioClip gameHelp;
        public AudioClip moveCommand;
        public AudioClip victory;
        public AudioClip death;

        [Header("Levels")]
        public AudioClip newLevel;
        public AudioClip levelEnd;
        public AudioClip pause;

        [Header("Main Menu")]
        public AudioClip mainMenuMusic;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Debug.LogWarning("Duplicate AudioManager destroyed: " + gameObject.name);
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }

        public void PlayMainMenuMusic()
        {
            if (musicSource.clip == mainMenuMusic && musicSource.isPlaying)
                return;

            musicSource.clip = mainMenuMusic;
            musicSource.loop = true;
            musicSource.Play();
        }

        public void PlaySFX(AudioClip clip)
        {
            if (clip != null)
            {
                sfxSource.PlayOneShot(clip);
            }
        }

        public void PlayMusic(AudioClip clip)
        {
            if (clip == null) return;

            if (musicSource.clip == clip && musicSource.isPlaying)
                return;

            musicSource.clip = clip;
            musicSource.loop = true;
            musicSource.Play();
        }

        public void PauseMusic()
        {
            musicSource.Pause();
        }

        public void ResumeMusic()
        {
            musicSource.UnPause();
        }

        public void StopMusic()
        {
            musicSource.Stop();
        }

    }
}
