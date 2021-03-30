using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "CharacterModel", menuName = "ScriptableObjects/Models/Character Model", order = 1)]
    public class CharacterModel : ScriptableObject
    {
        [Tooltip("Set movement speed value for the character. Min value for slower movement. Max value for faster movement.")]
        [Range(0.001f, 3f)]
        public float speed = 1f;

        [Tooltip("Set the run speed. The run index multiplies the speed value.")]
        [Range(1.1f, 5f)]
        public float runIndex = 1.1f;

        [Tooltip("Set the jump strength. Min value for lower jump strength. Max value for higher jump strength.")]
        [Range(1f, 10f)]
        public float jumpStrength = 1f;

        [Tooltip("Set the character health points.")]
        [Range(1, 10)]
        public int healthPoints = 1;

        [Tooltip("The prefab for the weapon.")]
        public GameObject weapon;
    }
}
