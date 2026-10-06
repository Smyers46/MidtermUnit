using UnityEngine;
using UnityEngine.SceneManagement;

namespace Midterm
{
    public class SceneChanger : MonoBehaviour
    {
        public void ChangeScene(string scenename)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(scenename);
        }
    }
}
