using System;
using CenterSystem;
using UnityEngine;
using Weapons.Bullets;

namespace Weapons.Gun.MonsterUse
{
    [Serializable]
    public class BiWeapon:Gun
    {
        public BiWeapon()
        {
            Gunname = "彘";
            Damage = 30;
            Cooldown = 2;
            ShootRange = 80;
            ShootSpeed = 20;
            BulletSize = 1f * SystemOption.BulletScale *SystemOption.SceneScale;
        }


        public override void Shoot(string name, Vector3 position, Vector3 muzzleOrientation)
        {
            if ( Interval <= 0)
            {
	            Debug.Log("Bi");
                Interval = Cooldown;
                //以下是花式创建子弹区域，一个Create创建一个子弹
                CreateBullet.TotalScene.CreateClassical(name, this, position,muzzleOrientation, BulletType.Magicball,false);
                
            }
        }

        
    }
}