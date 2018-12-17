using System;
using System.Collections.Generic;
using CenterSystem;
using Character;
using UnityEngine;
using Weapons.Bullets;
using Random = UnityEngine.Random;

namespace Weapons.Gun.PlayerUse
{
    [Serializable]
    public class FireBow : Gun
    {

        private float _deSpeed = 10;
        private float _deMaxDamage = 20;
        
        public FireBow()
        {
            Gunname = "火焰弓";
            Damage = 80;
            Cooldown = 1;
            MaxTime = 20;
            IntervalTime = 20;
            ShootRange = 30;
            ShootSpeed = 90;
            Gunbuff = Buff.Fire;
        }



        public override void Shoot(String name, Vector3 position, Vector3 muzzleOrientation)
        {
            if (IntervalTime > 0 && Interval <= 0)
            {
                Interval = Cooldown;
                //以下是花式创建子弹区域，一个Create创建一个子弹
                CreateBullet.TotalScene.CreatePenetrate(name, this, position, muzzleOrientation,BulletType.Arrow,_deSpeed,_deMaxDamage);
                PlayerAudioCollection.GunCollection.Play("ArrowShoot3");
            }

        }
    }
}