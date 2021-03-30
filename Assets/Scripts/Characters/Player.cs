using Assets.Scripts.Components;
using Assets.Scripts.Controllers;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Characters
{
    /// <summary>
    /// Player class defines the player behavior.
    /// Player extends the Character class.
    /// The class can perform action controlled from the Input such as movement, jump, run, weapon fire.
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public class Player : Character
    {
        [Tooltip("The UI component responsible for the player health points")]
        [SerializeField]
        private UIHealthComponent healthComponent;

        [Tooltip("Ground checking point.")]
        [SerializeField]
        private Transform feet;

        private SpriteRenderer spriteRenderer;
        private Color defaultColor = Color.white;
        private Color injuredColor = Color.yellow;

        // Defines the period of player protection from another injure.
        private const float injuredTimer = 2f;

        /// <summary>
        /// Defines the injure state.
        /// </summary>
        public bool IsInjured { get; set; }

        /// <summary>
        /// Extends the damage base class with the injure logic.
        /// </summary>
        public override void Damage()
        {
            base.Damage();

            healthComponent.HideElement();
            IsInjured = true;

            if (currentHealthPoints > 0)
                StartCoroutine(InjureRoutine());
        }

        /// <summary>
        /// The method controls die function of the player.
        /// </summary>
        public override void PassAway()
        {
            gameObject.SetActive(false);

            var lc = FindObjectOfType<LevelController>();
            lc.RestartLevel();
        }

        /// <summary>
        /// The method controls the way the player is moving based on Input controls.
        /// </summary>
        protected override void Move()
        {
            speedInput = Input.GetAxisRaw("Horizontal");

            if (Input.GetButtonDown("Jump") && IsGrounded(feet.position))
                isJumped = true;

            if (Input.GetButtonDown("Run") && IsGrounded(feet.position))
            {
                speedValue = model.speed * model.runIndex;
            }
            else if (Input.GetButtonUp("Run"))
            {
                speedValue = model.speed;
            }
        }

        #region MonoBehaviour Methods
        /// <summary>
        /// The method gathers the reference's and set's initial configurations.
        /// </summary>
        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            healthComponent.Initialize(model.healthPoints);
        }

        private void Update()
        {
            Move();

            if (Input.GetButtonDown("Fire1"))
                Fire();
        }
        #endregion

        /// <summary>
        /// Protects the player from another damaging event. The time is defined by the injuredTimer constant field.
        /// </summary>
        /// <returns>Returns Enumerator</returns>
        private IEnumerator InjureRoutine()
        {
            var timer = injuredTimer;
            spriteRenderer.color = injuredColor;

            while (timer > 0)
            {
                timer -= Time.deltaTime;
                yield return null;
            }

            spriteRenderer.color = defaultColor;
            IsInjured = false;
        }
    }
}