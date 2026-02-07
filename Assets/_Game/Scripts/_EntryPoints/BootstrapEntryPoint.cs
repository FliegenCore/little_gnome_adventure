using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace _Game.Scripts.GameInitializeSystems
{
    public class BootstrapEntryPoint : IStartable
    {
        public BootstrapEntryPoint()
        {
        }

        public void Start()
        {
            SceneManager.LoadScene(1);
        }
    }
}