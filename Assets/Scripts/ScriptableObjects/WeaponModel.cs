using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "WeaponModel", menuName = "ScriptableObjects/Models/Weapon Model", order = 1)]
    public class WeaponModel : ScriptableObject
    {
        [Tooltip("The movement speed of the weapon.")]
        [Range(0f, 20f)]
        public float moveSpeed;
    }
}
