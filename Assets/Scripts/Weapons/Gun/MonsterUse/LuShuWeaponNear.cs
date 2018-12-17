using System;
using CenterSystem;
using UnityEngine;
using Weapons.Bullets;

namespace Weapons.Gun.MonsterUse
{
    [Serializable]
    public class LuShuWeaponNear:Gun
    {
        public LuShuWeaponNear()
        {
            Gunname = "鹿蜀_近";
            Damage = 5;
            Cooldown = 2;
            ShootRange = 2.5f;
            ShootSpeed = 1;
            BulletSize = 3f * SystemOption.SceneScale;
        }


        public override void Shoot(string name, Vector3 position, Vector3 muzzleOrientation)
        {
            if ( Interval <= 0)
            {
	            
                Interval = Cooldown;
                //以下是花式创建子弹区域，一个Create创建一个子弹
                CreateBullet.TotalScene.CreateBomb(name, this, position,muzzleOrientation, BulletType.Magicball,4f,0f);              
            }
        }

        
        
    }
}