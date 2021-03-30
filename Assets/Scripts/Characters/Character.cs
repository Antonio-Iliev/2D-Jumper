using Assets.Scripts.Constants;
using Assets.Scripts.Items;
using Assets.Scripts.ScriptableObjects;
using UnityEngine;

namespace Assets.Scripts.Characters
{
    /// <summary>
    /// Character base class allows fundamental capabilities of a game character.
    /// Character provides move, fire, take damage and die functionality.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Character : MonoBehaviour
    {
        [SerializeField]
        protected CharacterModel model;

        [Tooltip("The character weapon position.")]
        [SerializeField]
        protected Transform firePoint;

        protected LayerMask groundMask;

        protected float speedInput;
        protected float speedValue;
        protected bool isJumped;
        protected int currentHealthPoints;

        private bool isFacingLeft;
        private Rigidbody2D rb;

        /// <summary>
        /// The method allows character to use a weapon if there is any added in the Character Model.
        /// </summary>
        protected void Fire()
        {
            if (model.weapon)
            {
                var weapon = Instantiate(model.weapon, firePoint.position, firePoint.rotation);
                var script = weapon.GetComponent<Weapon>();
                Debug.Assert(script, $"Can not found a {typeof(Weapon)} from component {weapon.name}");

                script.Fire(tag);
            }
        }

        /// <summary>
        /// The character receives damage.
        /// </summary>
        public virtual void Damage()
        {
            if (--currentHealthPoints <= 0)
                PassAway();
        }

        /// <summary>
        /// The method controls die functionality.
        /// </summary>
        public abstract void PassAway();

        /// <summary>
        /// The method controls the way the character is moving.
        /// </summary>
        protected abstract void Move();

        /// <summary>
        /// The method allows a character to check if it stands on the surface of a ground object with the mask set as Ground.
        /// </summary>
        /// <param name="targetPosition">The point against a check is performed.</param>
        /// <returns>Return true if the character is on a ground object</returns>
        protected bool IsGrounded(Vector3 targetPosition)
        {
            return Physics2D.OverlapCircle(targetPosition, CommonConstant.OVERLAP_CHECK_RADIOS, groundMask);
        }

        #region MonoBehaviour Methods
        /// <summary>
        /// The method gathers the reference's and set's initial configurations.
        /// </summary>
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            groundMask = LayerMask.GetMask("Ground");

            speedValue = model.speed;
            currentHealthPoints = model.healthPoints;
        }

        /// <summary>
        /// The movement of the character. 
        /// </summary>
        private void FixedUpdate()
        {
            var velocity = rb.velocity;

            velocity.x = speedInput * speedValue;
            velocity.y = isJumped ? model.jumpStrength : velocity.y;
            rb.velocity = velocity;

            isJumped = false;

            CharacterOrientation();
        }
        #endregion

        /// <summary>
        /// Set the orientation of the character base on the direction of the movement.
        /// </summary>
        private void CharacterOrientation()
        {
            if (!Mathf.Approximately(speedInput, 0f))
            {
                if ((speedInput > 0 && isFacingLeft) || (speedInput < 0 && !isFacingLeft))
                {
                    isFacingLeft = !isFacingLeft;

                    // Use rotation to maintain the direction of the weapon.
                    transform.Rotate(0f, 180f, 0f);
                }
            }
        }
    }
}