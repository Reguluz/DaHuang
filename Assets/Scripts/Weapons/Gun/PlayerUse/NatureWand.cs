using System;
using System.Collections.Generic;
using CenterSystem;
using UnityEngine;
using Weapons.Bullets;

namespace Weapons.Gun.PlayerUse
{
	[Serializable]
	public class NatureWand:Gun
	{
		public NatureWand()
		{
			Gunname = "自然法杖";
			Damage = 20;
			Cooldown = 0.5;
			MaxTime = 20;
			IntervalTime = 20;
			ShootRange = 10;
			ShootSpeed = 30;
		}


		public override void Shoot(String name,Vector3 position,Vector3 muzzleOrientation)
		{            
			if (IntervalTime > 0 && Interval <=0)
			{
				Interval = Cooldown;
                
				//以下是花式创建子弹区域，一个Create创建一个子弹
				CreateBullet.TotalScene.CreateClassical(name,this,position,muzzleOrientation,BulletType.Magicball);
				CreateBullet.TotalScene.CreateClassical(name,this,position,PublicFunction.RotationMatrix(muzzleOrientation,-10), BulletType.Magicball);
				CreateBullet.TotalScene.CreateClassical(name,this,position,PublicFunction.RotationMatrix(muzzleOrientation,10), BulletType.Magicball);
				CreateBullet.TotalScene.CreateClassical(name,this,position,PublicFunction.RotationMatrix(muzzleOrientation,-5), BulletType.Magicball);
				CreateBullet.TotalScene.CreateClassical(name,this,position,PublicFunction.RotationMatrix(muzzleOrientation,5), BulletType.Magicball);
			
				PlayerAudioCollection.GunCollection.Play("WandShoot");
			}

		}
		
		
		
	}
}