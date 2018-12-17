using System;
using System.Collections.Generic;
using CenterSystem;
using Character;
using UnityEngine;
using Weapons.Bullets;
using Weapons.Gun;

namespace Weapons.Gun
{
    [Serializable]
    public abstract class Gun:IGun
    {
        protected internal String Gunname;
        protected internal double Cooldown;
        protected internal float Damage;       
        protected internal float IntervalTime;
        protected internal float MaxTime;
        protected internal float ShootRange;
        protected internal float ShootSpeed;
        protected internal double Interval;
        protected internal float BulletSize = 0.1f * SystemOption.BulletScale;
        protected internal String Bulletype;//仅限怪物等需要Multiple的武器
        protected internal Buff Gunbuff;
        

        public void SetTime()
        {
            IntervalTime = MaxTime;
        }

        public virtual void Shoot(string name, Vector3 position, Vector3 muzzleOrientation)
        {
            
        }
        public virtual void Create(string name, Vector3 position, Vector3 muzzleOrientation, GameObject target)
        {
            
        }
    }
    
}