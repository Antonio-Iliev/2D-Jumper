using Assets.Scripts.Characters;
using UnityEngine;

namespace Assets.Scripts.Components
{
    /// <summary>
    /// AbyssComponent is responsible for detecting and 'killing' the character while interacting with the component.
    /// </summary>
    public class AbyssComponent : MonoBehaviour
    {

        /// <summary>
        /// Responsible for detecting the collision with any character.
        /// </summary>
        /// <param name="collision">Collider details returned by 2D physics callback functions</param>
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<Character>(out var character))
                character.PassAway();
        }
    }
}