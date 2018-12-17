using System;
using System.Collections.Generic;
using CenterSystem;
using UnityEngine;
using Weapons.Bullets;
using Random = UnityEngine.Random;

namespace Weapons.Gun.PlayerUse
{
    [Serializable]
    public class Gun02Colt1911A : Gun
    {
        public Gun02Colt1911A()
        {
            Gunname = "MonsterExample";
            Damage = 30;
            Cooldown = 0.5;
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
                CreateBullet.TotalScene.CreateClassical(name, this, position, PublicFunction.RotationMatrix(muzzleOrientation, Random.Range(-2, 2)),BulletType.Magicball);

            }

        }
    }
}