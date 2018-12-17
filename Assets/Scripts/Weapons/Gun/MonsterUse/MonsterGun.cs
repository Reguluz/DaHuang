using System;
using System.Collections.Generic;
using Character;
using UnityEngine;

namespace Weapons.Gun.MonsterUse
{
    public abstract class MonsterGun:Gun
    {
        
        protected internal string Gunname;
        protected internal double Cooldown;
        protected internal float Damage;       
        protected internal float ShootRange;
        protected internal float ShootSpeed;
        protected internal double Interval;
        protected internal float BulletSize;
        protected internal String Bulletype;
        protected internal Buff Gunbuff;
        
        public override void Shoot(string name, Vector3 position, Vector3 muzzleOrientation)
        {
          
        }
    }
}