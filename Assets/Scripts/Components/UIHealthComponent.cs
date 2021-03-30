using UnityEngine;
using System.Linq;

namespace Assets.Scripts.Components
{
    /// <summary>
    /// UIHealthComponent provides the player health points visualization.
    /// </summary>
    public class UIHealthComponent : MonoBehaviour
    {
        [Tooltip("The visual representation of the player health points.")]
        [SerializeField]
        private GameObject healthPointPrefab;

        private GameObject[] elements;
        private int index;

        /// <summary>
        /// The method instantiates the necessary game objects for health points visualization.
        /// </summary>
        /// <param name="healthPoints"></param>
        internal void Initialize(int healthPoints)
        {
            index = healthPoints - 1;

            elements = new GameObject[healthPoints]
                .Select(hp => Instantiate(healthPointPrefab, transform))
                .ToArray();
        }

        /// <summary>
        /// The method hides a health point element in reversed order.
        /// </summary>
        internal void HideElement()
        {
            index--;
            if (index < elements.Length && index >= 0)
            {
                elements[index].SetActive(false);
            }
        }
    }
}