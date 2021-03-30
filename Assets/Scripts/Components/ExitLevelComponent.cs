using UnityEngine;

namespace Assets.Scripts.Components
{
    /// <summary>
    /// ExitLevelComponent allows the player to complete the level.
    /// </summary>
    public class ExitLevelComponent : MonoBehaviour
    {
        [Tooltip("The UI panel represent the level completion.")]
        [SerializeField]
        private GameObject winPanel;

        /// <summary>
        /// Responsible for detecting the collision with the player and invoking the level completion logic.
        /// </summary>
        /// <param name="collision">Collider details returned by 2D physics callback functions</param>
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                // Set player to be invisible.
                collision.gameObject.SetActive(false);

                winPanel.SetActive(true);
            }
        }
    }
}
