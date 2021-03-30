using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Components
{
    /// <summary>
    /// FollowComponent allows connecting a game object in order to follow another game object across the time of game play.
    /// </summary>
    public class FollowComponent : MonoBehaviour
    {
        [Tooltip("The target game object followed by another game object.")]
        [SerializeField]
        private Transform target;

        [Tooltip("The follower game object. If the field was not set it will take current 'transform' for follower.")]
        [SerializeField]
        private Transform follower;

        [Header("Configurations")]
        [Tooltip("Check 'True' if you want to follow the player.")]
        [SerializeField]
        private bool followPlayer;

        [Tooltip("The offset position of the follower game object.")]
        [SerializeField]
        private Vector3 offset;

        [Tooltip("The response speed of the following action. Min value is slower, Max value is faster.")]
        [Range(1f, 10f)]
        [SerializeField]
        private float speed = 1f;

        [Tooltip("The time before starting the following. The value is in seconds.")]
        [SerializeField]
        [Range(0f, 100f)]
        private float delayStart = 0f;

        /// <summary>
        /// The method set's initial configurations and start following routine.
        /// </summary>
        private void Start()
        {
            if (!follower)
                follower = transform;

            if (followPlayer)
                target = GameObject.FindGameObjectWithTag("Player").transform;

            StartCoroutine(Routin());
        }

        /// <summary>
        /// The following routine provides only position update of the follower game object.
        /// </summary>
        /// <returns>Returns Enumerator</returns>
        private IEnumerator Routin()
        {
            yield return new WaitForSeconds(delayStart);

            while (true)
            {
                var position = target.position + offset;
                follower.position = Vector3.Lerp(follower.position, position, speed * Time.deltaTime);

                yield return null;
            }
        }
    }
}