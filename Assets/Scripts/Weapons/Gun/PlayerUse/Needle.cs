using System;
using System.Collections.Generic;
using CenterSystem;
using UnityEngine;
using Weapons.Bullets;
using Random = UnityEngine.Random;

namespace Weapons.Gun.PlayerUse
{
    [Serializable]
    public class Needle :Gun
    {
        private float _random = 0;
        public Needle()
        {
            Gunname = "飞针";
            Damage = 20;
            Cooldown = 0.1;
            MaxTime = 80;
            IntervalTime = 80;
            ShootRange = 20;
            ShootSpeed = 70;
            BulletSize = 0.2f * SystemOption.BulletScale;
        }



        public override void Shoot(String name,Vector3 position,Vector3 muzzleOrientation)
        {            
            if (IntervalTime > 0 && Interval <=0)
            {
                Interval = Cooldown;
                //以下是花式创建子弹区域，一个Create创建一个子弹
                CreateBullet.TotalScene.CreateClassical(name,this,position,PublicFunction.RotationMatrix(muzzleOrientation,Random.Range(-5,5)),BulletType.Pipe);
                _random = _random + 4;
                if (_random > 7.5)
                {
                    _random = _random - 15;
                }
                PlayerAudioCollection.GunCollection.Play("PreArrowShoot");
            }

        }
        

        
    }
}