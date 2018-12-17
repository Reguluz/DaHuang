using System;
using System.Collections;
using System.Collections.Generic;
using CenterSystem;
using UnityEngine;
using Weapons.Bullets;
using Random = UnityEngine.Random;

namespace Weapons.Gun.PlayerUse
{
    [Serializable]
    public class Gun03ColtGovernment : Gun
    {
        private float _deSpeed=2;
        private float _deMaxDamage=10;
        public Gun03ColtGovernment()
        {
            Gunname = "MonsterExample";
            Damage = 30;
            Cooldown = 0.75;
            MaxTime = 30;
            IntervalTime = 30;
            ShootRange = 25;
            ShootSpeed = 40;
        }



        public override void Shoot(String name, Vector3 position, Vector3 muzzleOrientation)
        {
            if (IntervalTime > 0 && Interval <= 0)
            {
                
                Interval = Cooldown;
                //以下是花式创建子弹区域，一个Create创建一个子弹
                //cb.Create(bulletype, name, this, position + muzzleOrientation.normalized, PublicFunction.RotationMatrix(muzzleOrientation, Random.Range(-2, 2)));
                Dartle(CreateBullet.TotalScene, name, position, muzzleOrientation);
                DelayFuc(Dartle, 1000);
                Dartle(CreateBullet.TotalScene, name, position, muzzleOrientation);
            }

        }

        IEnumerator DelayFuc(Action action, float delaySeconds)
        {
            yield return new WaitForSeconds(delaySeconds);
            action();
        }

        private void Dartle()
        {

        }

        private void Dartle(CreateBullet cb, String name, Vector3 position, Vector3 muzzleOrientation)
        {
            cb.CreatePenetrate(name, this, position, PublicFunction.RotationMatrix(muzzleOrientation, 0),BulletType.Magicball,_deSpeed,_deMaxDamage);
        }
    }

}