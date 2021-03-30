using Assets.Scripts.Characters;
using UnityEngine;

namespace Assets.Scripts.Components
{
    /// <summary>
    /// WeekPointComponent provides a way for instant 'killing' character on interaction.
    /// </summary>
    public class WeekPointComponent : MonoBehaviour
    {
        /// <summary>
        /// Detect collision with a character.
        /// </summary>
        /// <param name="collision">Collider details returned by 2D physics callback functions</param>
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag(tag) && transform.parent.TryGetComponent<Character>(out var script))
                script.PassAway();
        }
    }
}