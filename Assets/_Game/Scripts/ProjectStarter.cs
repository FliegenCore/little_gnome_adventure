using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Game.Scripts
{
    public class ProjectStarter
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void OnRuntimeMethodLoad()
        {
            if (SceneManager.GetActiveScene().name != "BootstrapScene")
            {
                SceneManager.LoadScene("BootstrapScene");
            }
        }
    }
}