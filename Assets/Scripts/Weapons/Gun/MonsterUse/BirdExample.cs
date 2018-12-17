using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons.Bullets;

namespace Weapons.Gun.MonsterUse
{
	[Serializable]
	public class BirdExample : Gun
	{
		public BirdExample()
		{
			Gunname = "MonsterExample";
			Damage = 20;
			Cooldown = 2;
			ShootRange = 75;
			ShootSpeed = 10;
			Bulletype = "ClassicalBullet";
		}



		public override void Shoot(String name, Vector3 position, Vector3 muzzleOrientation)
		{
			if ( Interval <= 0)
			{
	
				Interval = Cooldown;
				//以下是花式创建子弹区域，一个Create创建一个子弹
				CreateBullet.TotalScene.CreateClassical(name, this, position,muzzleOrientation,BulletType.Magicball);
			}

		}
	}
}