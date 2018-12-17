using System;
using System.Collections.Generic;
using CenterSystem;
using UnityEngine;
using Weapons.Bullets;
using Random = UnityEngine.Random;

namespace Weapons.Gun.PlayerUse
{
    [Serializable]
    public class CrossBow :Gun
    {
        public CrossBow()
        {
            Gunname = "连弩";
            Damage = 40;
            Cooldown = 0.1;
            MaxTime = 20;
            IntervalTime = 20;
            ShootRange = 100;
            ShootSpeed = 50;   
        }



        public override void Shoot(String name,Vector3 position,Vector3 muzzleOrientation)
        {            
            if (IntervalTime > 0 && Interval <=0)
            {
                Interval = Cooldown;
                //以下是花式创建子弹区域，一个Create创建一个子弹
                Debug.Log("CrossBowShoot");
                CreateBullet.TotalScene.CreateClassical(name,this,position,PublicFunction.RotationMatrix(muzzleOrientation,Random.Range(-2,2)),BulletType.Arrow);

                PlayerAudioCollection.GunCollection.Play("ArrowShoot");
            }

        }
        

        
    }
}