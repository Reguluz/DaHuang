using System;
using System.Collections.Generic;
using CenterSystem;
using Character;
using UnityEngine;
using Weapons.Bullets;
using Random = UnityEngine.Random;

namespace Weapons.Gun.PlayerUse
{
    [Serializable]
    public class FreezeStar : Gun
    {
        private float _nextFindTime = 0.25f;
        private float _deMaxDamagePer = 0.1f;
        private int _maxNum = 5;
        private float _searchRadius = 5;
        
        public FreezeStar()
        {
            Gunname = "霜星";
            Damage = 60;
            Cooldown = 1f;
            MaxTime = 100;
            IntervalTime = 100;
            Gunbuff = Buff.Frozen;
        }



        public override void Shoot(String name, Vector3 position, Vector3 muzzleOrientation)
        {
            if (IntervalTime > 0 && Interval <= 0)
            {
                Interval = Cooldown;
                //以下是花式创建子弹区域，一个Create创建一个子弹

                    CreateBullet.TotalScene.CreateLinker(name, this, position, muzzleOrientation,_nextFindTime,_deMaxDamagePer,_maxNum,_searchRadius,BulletType.Magicball);

            }
        }
    }
}