using System;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons.Gun
{
    public interface IGun
    {
        void Shoot(String name,Vector3 position,Vector3 muzzleOrientation);
    }
}