using Assets.Scripts.Characters;
using Assets.Scripts.Models;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    /// <summary>
    /// EnemyRespawner is a simple enemy re-spawner. Re-spawn a enemy base on initial position.
    /// </summary>
    public class EnemyRespawner : MonoBehaviour
    {
        [Tooltip("The re-spawn timer. The value is in seconds.")]
        [Range(1f, 1000f)]
        [SerializeField]
        private float timer = 1f;

        private SpawnModel[] enemies;

        /// <summary>
        /// The method gathers the reference's and set's initial configurations.
        /// </summary>
        private void Awake()
        {
            enemies = FindObjectsOfType<Character>()
                .Where(c => c.CompareTag("Enemy"))
                .Select(c => new SpawnModel
                {
                    InitialPosition = c.transform.position,
                    SpawnObject = c.transform
                })
                .ToArray();

            StartCoroutine(RespawnRoutine());
        }

        /// <summary>
        /// The routine re-spawn inactive enemies base on time interval.
        /// </summary>
        /// <returns>Returns Enumerator</returns>
        private IEnumerator RespawnRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(timer);

                foreach (var enemy in enemies)
                {
                    if (!enemy.SpawnObject.gameObject.activeSelf)
                    {
                        enemy.SpawnObject.gameObject.SetActive(true);
                        enemy.SpawnObject.position = enemy.InitialPosition;
                    }
                }
            }
        }
    }
}