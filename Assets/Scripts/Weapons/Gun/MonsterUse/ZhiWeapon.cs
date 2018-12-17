using CenterSystem;
using UnityEngine;
using Weapons.Bullets;

namespace Weapons.Gun.MonsterUse
{
    public class ZhiWeapon:Gun
    {
        public ZhiWeapon()
        {
            Gunname = "彘";
            Damage = 50;
            Cooldown = 4;
            ShootRange = 10f;
            ShootSpeed = 10;
            BulletSize = 1f * SystemOption.SceneScale * SystemOption.BulletScale;
        }


        public override void Shoot(string name, Vector3 position, Vector3 muzzleOrientation)
        {
            if ( Interval <= 0)
            {
                Debug.Log("zhi");
                Interval = Cooldown;
                //以下是花式创建子弹区域，一个Create创建一个子弹
                CreateBullet.TotalScene.CreateClassical(name, this, position+muzzleOrientation*0.5f,muzzleOrientation, BulletType.Near,false);
                CreateBullet.TotalScene.CreateClassical(name, this, position+muzzleOrientation*0.5f,PublicFunction.RotationMatrix(muzzleOrientation,30), BulletType.Near,false);
                CreateBullet.TotalScene.CreateClassical(name, this, position+muzzleOrientation*0.5f,PublicFunction.RotationMatrix(muzzleOrientation,-30), BulletType.Near,false);
            }
        }
    }
}