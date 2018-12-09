using UnityEngine;

namespace Weapons.Bullets.LazerTools
{
    public class CharacterDistance
    {
        public GameObject Character;
        public float Distance;

        public CharacterDistance(GameObject go, float dtc)
        {
            Character = go;
            Distance = dtc;
        }
    }
}