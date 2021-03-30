using Assets.Scripts.Characters;
using Assets.Scripts.Constants;
using Assets.Scripts.ScriptableObjects;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Items
{
    /// <summary>
    /// Weapon class allows the character to fire a projectile
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class Weapon : MonoBehaviour
    {
        [SerializeField]
        private WeaponModel model;

        private Rigidbody2D rb;
        private string weaponOwner;

        /// <summary>
        /// The method configures the weapon based on the loaded model and starts a routine for self-destruction.
        /// </summary>
        /// <param name="ownerName">The name of the weapon owner.</param>
        internal void Fire(string ownerName)
        {
            rb = GetComponent<Rigidbody2D>();
            rb.velocity = transform.right * model.moveSpeed;

            weaponOwner = ownerName;

            StartCoroutine(AutoDestroyRoutine());
        }

        /// <summary>
        /// Responsible for detecting a vulnerable target and take damage.
        /// </summary>
        /// <param name="collision">Collider details returned by 2D physics callback functions</param>
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag(weaponOwner))
            {
                if (collision.TryGetComponent<Character>(out var script))
                    script.Damage();

                StopAllCoroutines();
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// Self-destruction routine for optimization.
        /// </summary>
        /// <returns>Returns Enumerator</returns>
        private IEnumerator AutoDestroyRoutine()
        {
            yield return new WaitForSeconds(CommonConstant.WEAPON_LIFE_CYCLE);
            Destroy(gameObject);
        }
    }
}