using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "AIModel", menuName = "ScriptableObjects/Models/AI Model", order = 1)]
    public class AIModel : ScriptableObject
    {
        [Tooltip("Set min and max value for character reaction time.")]
        [SerializeField]
        private Vector2 minMaxReactionTime;

        [Tooltip("Set min and max value for fire rate.")]
        [SerializeField]
        private Vector2 minMaxFireRate;

        /// <summary>
        /// Returns a random float number between min and max reaction time.
        /// </summary>
        public float ReactionTime
        {
            get
            {
                return Random.Range(minMaxReactionTime.x, minMaxReactionTime.y);
            }
        }

        /// <summary>
        /// Returns a random float number between min and max fire rate.
        /// </summary>
        public float FireRate
        {
            get
            {
                return Random.Range(minMaxFireRate.x, minMaxFireRate.y);
            }
        }

        /// <summary>
        /// Returns a random float number for the movement direction.
        /// </summary>
        public float Direction
        {
            get
            {
                return Mathf.Clamp(Random.Range(-10, 10), -1f, 1f);
            }

        }
    }
}
