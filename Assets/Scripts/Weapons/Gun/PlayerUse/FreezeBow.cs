using System;
using CenterSystem;
using Character;
using UnityEngine;
using Weapons.Bullets;

namespace Weapons.Gun.PlayerUse
{
    [Serializable]
    public class FreezeBow : Gun
    {

        private float _deSpeed = 2;
        private float _deMaxDamage = 10;

        public FreezeBow()
        {
            Gunname = "冰霜弓";
            Damage = 150;
            Cooldown = 1.25;
            MaxTime = 10;
            IntervalTime = 10;
            ShootRange = 35;
            ShootSpeed = 100;
            Gunbuff = Buff.Freeze;
        }



        public override void Shoot(String name, Vector3 position, Vector3 muzzleOrientation)
        {
            if (IntervalTime > 0 && Interval <= 0)
            {
                Interval = Cooldown;
                //以下是花式创建子弹区域，一个Create创建一个子弹
                CreateBullet.TotalScene.CreatePenetrate(name, this, position, muzzleOrientation,BulletType.Arrow, _deSpeed,
                    _deMaxDamage);
                PlayerAudioCollection.GunCollection.Play("ArrowShoot2");
            }

        }

    }
}