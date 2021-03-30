using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Controllers
{
    public class LevelController : MonoBehaviour
    {
        /// <summary>
        /// The method reloads the scene.
        /// </summary>
        public void RestartLevel()
        {
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }
    }
}