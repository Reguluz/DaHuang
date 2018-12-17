using System;
using CenterSystem;
using UnityEngine;
using Weapons.Bullets;
using Random = UnityEngine.Random;

namespace Weapons.Gun.PlayerUse
{
    [Serializable]
    public class TrackGun:Gun
    {
        public TrackGun()
        {
            Gunname = "追踪导弹";
            Damage = 99999999;
            Cooldown = 2;
            MaxTime = 10;
            IntervalTime = 10;
            ShootRange = 100;
            ShootSpeed = 50;
            BulletSize = 1;
        }
        public override void Shoot(string name, Vector3 position, Vector3 muzzleOrientation)
        {
            if (IntervalTime > 0 && Interval <=0)
            {
                Interval = Cooldown;
                //以下是花式创建子弹区域，一个Create创建一个子弹
                CreateBullet.TotalScene.CreateTrack(name,this,position,PublicFunction.RotationMatrix(muzzleOrientation,Random.Range(-25,25)),1f,BulletType.Magicball);
                CreateBullet.TotalScene.CreateTrack(name,this,position,PublicFunction.RotationMatrix(muzzleOrientation,Random.Range(-25,25)),1f,BulletType.Magicball);
                CreateBullet.TotalScene.CreateTrack(name,this,position,PublicFunction.RotationMatrix(muzzleOrientation,Random.Range(-25,25)),1f,BulletType.Magicball);
                PlayerAudioCollection.GunCollection.Play("ArrowShoot");
            }
        }
    }
}