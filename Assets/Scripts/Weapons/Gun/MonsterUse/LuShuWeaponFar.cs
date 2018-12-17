using System;
using System.Collections;
using System.Collections.Generic;
using CenterSystem;
using UnityEngine;
using Weapons.Bullets;

namespace Weapons.Gun.MonsterUse
{
    [Serializable]
    public class LuShuWeaponFar:Gun
    {
        public LuShuWeaponFar()
        {
            Gunname = "鹿蜀_远";
            Damage = 30;
            Cooldown = 4;
            ShootRange = 30;
            ShootSpeed = 1;
            BulletSize = 0.3f * SystemOption.BulletScale;
        }


        public override void Shoot(string name, Vector3 position, Vector3 muzzleOrientation)
        {
            if ( Interval <= 0)
            {
	
                Interval = Cooldown;
                //以下是花式创建子弹区域，一个Create创建一个子弹
                CreateBullet.TotalScene.CreateClassical(name, this, position,Vector3.up, BulletType.Magicball);
                CreateBullet.TotalScene.CreateClassical(name, this, position,PublicFunction.RotationMatrix(Vector3.up, 30), BulletType.Magicball);
                CreateBullet.TotalScene.CreateClassical(name, this, position,PublicFunction.RotationMatrix(Vector3.up, 60), BulletType.Magicball);
                CreateBullet.TotalScene.CreateClassical(name, this, position,PublicFunction.RotationMatrix(Vector3.up, 90), BulletType.Magicball);
                CreateBullet.TotalScene.CreateClassical(name, this, position,PublicFunction.RotationMatrix(Vector3.up, 120), BulletType.Magicball);
                CreateBullet.TotalScene.CreateClassical(name, this, position,PublicFunction.RotationMatrix(Vector3.up, 150), BulletType.Magicball);
                CreateBullet.TotalScene.CreateClassical(name, this, position,PublicFunction.RotationMatrix(Vector3.up, 180), BulletType.Magicball);
                CreateBullet.TotalScene.CreateClassical(name, this, position,PublicFunction.RotationMatrix(Vector3.up, 210), BulletType.Magicball);
                CreateBullet.TotalScene.CreateClassical(name, this, position,PublicFunction.RotationMatrix(Vector3.up, 240), BulletType.Magicball);
                CreateBullet.TotalScene.CreateClassical(name, this, position,PublicFunction.RotationMatrix(Vector3.up, 270), BulletType.Magicball);
                CreateBullet.TotalScene.CreateClassical(name, this, position,PublicFunction.RotationMatrix(Vector3.up, 300), BulletType.Magicball);
                CreateBullet.TotalScene.CreateClassical(name, this, position,PublicFunction.RotationMatrix(Vector3.up, 330), BulletType.Magicball);
            }
        }
    }
}