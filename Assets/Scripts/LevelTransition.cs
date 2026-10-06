using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering.Universal.Internal;
using UnityEngine.Timeline;

namespace Midterm
{
    public class LevelTransition : MonoBehaviour
    {

        [SerializeField] string currentLevelName;

        public PlayableDirector director;

        public LevelManager manager;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                GameManager.instance.currentLevel = manager;

                PlayCutscene();

                // Any other behavior needed for this level transition
                Debug.Log("Loading Level: " + currentLevelName);

                //gameObject.SetActive(false);
            }
        }

        public void PlayCutscene()
        {
            if (director != null)
            {
                director.Play();
            }
            else
            {
                Debug.LogWarning("No PlayableDirector assigned!");
            }
        }

    }
}
