using Assets.Scripts.ScriptableObjects;
using UnityEngine;

namespace Assets.Scripts.Characters
{
    /// <summary>
    /// Monster class defines the Enemy behavior.
    /// Monster extends the Character class.
    /// The class can perform basic random movements, fire capabilities, and basic spatial awareness.
    /// </summary>
    public class Monster : Character
    {
        [Tooltip("The AI model for controlling the character behavior.")]
        [SerializeField]
        private AIModel aiModel;

        [Tooltip("The point transform component for checking for the slopes.")]
        [SerializeField]
        private Transform blindCane;

        private float timer;

        /// <summary>
        /// The method controls die function of the monster character.
        /// </summary>
        public override void PassAway()
        {
            gameObject.SetActive(false);
        }

        /// <summary>
        /// The method controls the way the monster character is moving.
        /// The method depends on the AI model for movement controls.
        /// </summary>
        protected override void Move()
        {
            timer -= Time.deltaTime;
            if (timer < 0f)
            {
                timer = aiModel.ReactionTime;
                speedInput = model.speed * aiModel.Direction;
            }

            if (!IsGrounded(blindCane.position))
                ReversDirection();
        }

        #region MonoBehaviour Methods
        private void OnEnable()
        {
            // Restoring the health points when the character is spawned.
            currentHealthPoints = model.healthPoints;
        }

        private void Update()
        {
            Move();

            if (IsReadyToFire())
                Fire();
        }

        /// <summary>
        /// Responsible for the Monster spatial awareness.
        /// </summary>
        /// <param name="collision">Collision details returned by 2D physics callback functions</param>
        private void OnCollisionEnter2D(Collision2D collision)
        {
            // Change direction if there are obstacles. 
            if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
                ReversDirection();
        }

        /// <summary>
        /// Responsible for damaging the player.
        /// </summary>
        /// <param name="collision">Collider details returned by 2D physics callback functions</param>
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (gameObject.activeSelf && collision.gameObject.CompareTag("Player")
                && collision.TryGetComponent<Player>(out var script) && !script.IsInjured)
            {
                script.Damage();
            }
        }
        #endregion

        /// <summary>
        /// The method checks if the character is ready to fire.
        /// </summary>
        /// <returns>Returns true if the random number is positive.</returns>
        private bool IsReadyToFire()
        {
            return aiModel.FireRate > 0f;
        }

        /// <summary>
        /// The method revers the movement value and reset the reaction timer.
        /// </summary>
        private void ReversDirection()
        {
            timer = aiModel.ReactionTime;
            speedInput *= -1f;
        }
    }
}