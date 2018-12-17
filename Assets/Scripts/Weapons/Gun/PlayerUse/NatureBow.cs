using System;
using System.Collections.Generic;
using CenterSystem;
using UnityEngine;
using Weapons.Bullets;
using Random = UnityEngine.Random;

namespace Weapons.Gun.PlayerUse
{
    [Serializable]
    public class NatureBow :Gun
    {
        public NatureBow()
        {
            Gunname = "郡之弓";
            Damage = 60;
            Cooldown = 0.2;
            MaxTime = 40;
            IntervalTime = 40;
            ShootRange = 25;
            ShootSpeed = 60;
        }



        public override void Shoot(String name,Vector3 position,Vector3 muzzleOrientation)
        {            
            if (IntervalTime > 0 && Interval <=0)
            {
                Interval = Cooldown;
                //以下是花式创建子弹区域，一个Create创建一个子弹
                CreateBullet.TotalScene.CreateClassical(name,this,position,PublicFunction.RotationMatrix(muzzleOrientation,Random.Range(-5,5)),BulletType.Arrow);

                PlayerAudioCollection.GunCollection.Play("ArrowShoot");
            }

        }
        

        
    }
}