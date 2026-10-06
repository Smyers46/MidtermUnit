using UnityEngine;

namespace Midterm
{
    public class MenuManager : MonoBehaviour
    {
        private void Start()
        {
            AudioManager.Instance.PlayMusic(AudioManager.Instance.mainMenuMusic);
        }
    }
}
