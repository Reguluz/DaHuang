using System;
using System.Collections.Generic;
using CenterSystem;
using Character;
using UnityEngine;
using Weapons.Bullets;

namespace Weapons.Gun.PlayerUse
{
    [Serializable]
    public class ExplodeFireWand:Gun
    {
        private float _bombRadius = 3;
        private float _bombForce = 100;
        public ExplodeFireWand()
        {
            Gunname = "爆炎法杖";
            Damage = 60;
            Cooldown = 1.25;
            MaxTime = 10;
            IntervalTime = 10;
            ShootRange = 30;
            ShootSpeed = 60;
            BulletSize = 0.3f * SystemOption.BulletScale;
            Gunbuff = Buff.Fire;
        }


        public override void Shoot(String name,Vector3 position,Vector3 muzzleOrientation)
        {            
            if (IntervalTime > 0 && Interval <=0)
            {
                Interval = Cooldown;
                
                //以下是花式创建子弹区域，一个Create创建一个子弹
                CreateBullet.TotalScene.CreateBomb(name,this,position,muzzleOrientation,BulletType.Magicball,_bombForce,_bombRadius);			
                PlayerAudioCollection.GunCollection.Play("FireBallShoot");
            }

        }
    }
}