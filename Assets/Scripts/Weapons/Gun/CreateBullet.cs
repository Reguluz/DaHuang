using System;
using System.Collections;
using System.Collections.Generic;
using CenterSystem;
using Character;
using Character.Player;
using UnityEngine;
using Weapons.Bullets;


namespace Weapons.Gun
{
    public class CreateBullet : MonoBehaviour
    {
        /*private GameObject _bullet;
        public GameObject Classical;
        public GameObject Penetrate;
        public GameObject Linker;
        public GameObject Bomb;
        public GameObject Trackball;*/

        public static CreateBullet TotalScene;
        
        private void Awake()
        {
            if (TotalScene == null)
            {
                TotalScene = this;
            }
            else
            {
                Destroy(gameObject);
            }
           				
        }

        // Use this for initialization
        void Start () {
		
        }
	
        // Update is called once per frame
        void Update () {
		
        }
        
        /*public void CreateMultiply(String bulletype,String shootername,Gun gun,Vector3 position,Vector3 muzzleOrientation,BulletType bt)
        {
            _bullet = (GameObject) Resources.Load("MultiplyBullet/"+bulletype, typeof(GameObject));
            Vector3 start = position + muzzleOrientation.normalized * _bullet.transform.localScale.magnitude*SystemOption.SceneScale;
            start.z = SystemOption.ItemZPosition;

            GameObject newBullet = ObjectPool.current.GetObject(_bullet);
            
            
            Bullet bullet = newBullet.GetComponent<Bullet>();
            bullet.SetSprite(bt);
            bullet.Shootout(shootername,gun,start,muzzleOrientation);
            
        }*/
        
        public void CreateTrack(String shootername,Gun gun,Vector3 position,Vector3 muzzleOrientation,float rotateper,BulletType bt)
        {
           
                Debug.Log("PlayerTrackShoot");
                Vector3 start = position + muzzleOrientation.normalized * 
                                (SystemOption.StartBulletOffsetScale + gun.BulletSize + UpgradeTree.PlayerArchive.ExtraBulletSpecialLevel * SystemOption.ExBulletScalePerL);
                start.z = SystemOption.ItemZPosition;
                GameObject newBullet = NewObjectPool.Current.Generate("TrackBall");

                newBullet.transform.right = muzzleOrientation;
                newBullet.GetComponent<SpriteRenderer>().color = BuffColor(gun.Gunbuff);
                TrackBullet bullet = newBullet.GetComponent<TrackBullet>();
                bullet.Shootout(shootername,gun, start,rotateper);
                newBullet.GetComponent<Transform>().localScale = 
                    new Vector3(gun.BulletSize + UpgradeTree.PlayerArchive.ExtraBulletSpecialLevel * SystemOption.ExBulletScalePerL * SystemOption.SceneScale,
                                gun.BulletSize + UpgradeTree.PlayerArchive.ExtraBulletSpecialLevel * SystemOption.ExBulletScalePerL * SystemOption.SceneScale,
                                gun.BulletSize + UpgradeTree.PlayerArchive.ExtraBulletSpecialLevel * SystemOption.ExBulletScalePerL * SystemOption.SceneScale);
                //Debug.DrawLine(position,start,Color.red,2);
            
        }
	
        public void CreateClassical(String shootername,Gun gun,Vector3 position,Vector3 muzzleOrientation,BulletType bt,Boolean isPlayer = true)
        {
            if (isPlayer)
            {
                Debug.Log("PlayerClassicalShoot");
                Vector3 start = position + muzzleOrientation.normalized * 
                                (SystemOption.StartBulletOffsetScale + gun.BulletSize + UpgradeTree.PlayerArchive.ExtraBulletSpecialLevel * SystemOption.ExBulletScalePerL);
                start.z = SystemOption.ItemZPosition;
                GameObject newBullet = NewObjectPool.Current.Generate("ClassicalBullet");

                newBullet.transform.right = muzzleOrientation;
                newBullet.GetComponent<SpriteRenderer>().color = BuffColor(gun.Gunbuff);
                Bullet bullet = newBullet.GetComponent<Bullet>();
                bullet.SetSprite(bt);
                bullet.Shootout(shootername,gun, start,muzzleOrientation,true);
                newBullet.GetComponent<Transform>().localScale = 
                    new Vector3(gun.BulletSize + UpgradeTree.PlayerArchive.ExtraBulletSpecialLevel * SystemOption.ExBulletScalePerL * SystemOption.SceneScale,
                                gun.BulletSize + UpgradeTree.PlayerArchive.ExtraBulletSpecialLevel * SystemOption.ExBulletScalePerL * SystemOption.SceneScale,
                                gun.BulletSize + UpgradeTree.PlayerArchive.ExtraBulletSpecialLevel * SystemOption.ExBulletScalePerL * SystemOption.SceneScale);
                //Debug.DrawLine(position,start,Color.red,2);
            }
            else
            {
                Vector3 start = position + muzzleOrientation.normalized * 
                                (SystemOption.StartBulletOffsetScale + gun.BulletSize);
                start.z = SystemOption.ItemZPosition;
                GameObject newBullet =  NewObjectPool.Current.Generate("ClassicalBullet");
                
                newBullet.transform.right = muzzleOrientation;
                newBullet.GetComponent<SpriteRenderer>().color = BuffColor(gun.Gunbuff);
                Bullet bullet = newBullet.GetComponent<Bullet>();
                bullet.SetSprite(bt);
                bullet.Shootout(shootername, gun, start, muzzleOrientation, false);
                newBullet.GetComponent<Transform>().localScale =
                    new Vector3(gun.BulletSize, gun.BulletSize, gun.BulletSize);
                //Debug.DrawLine(position,bullet.transform.position,Color.red,2);
            }
        }
        
        
        public void CreatePenetrate(String shootername,Gun gun,Vector3 position,Vector3 muzzleOrientation, BulletType bt,float deSpeed,float deMaxDamage)
        {
            Vector3 start = position + muzzleOrientation.normalized * 
                            (SystemOption.StartBulletOffsetScale + gun.BulletSize + UpgradeTree.PlayerArchive.ExtraBulletSpecialLevel * SystemOption.ExBulletScalePerL * SystemOption.SceneScale);
            start.z = SystemOption.ItemZPosition;
            GameObject newBullet = NewObjectPool.Current.Generate("PentrateBullet");
            
            
            newBullet.transform.right = muzzleOrientation;
            Bullet bullet = newBullet.GetComponent<Bullet>();
            bullet.SetSprite(bt);
            bullet.Shootout(shootername,gun, start,muzzleOrientation);  
            newBullet.GetComponent<SpriteRenderer>().color = BuffColor(gun.Gunbuff);
            newBullet.GetComponent<Transform>().localScale = 
                    new Vector3(gun.BulletSize + UpgradeTree.PlayerArchive.ExtraBulletSpecialLevel * SystemOption.ExBulletScalePerL * SystemOption.SceneScale,
                                gun.BulletSize + UpgradeTree.PlayerArchive.ExtraBulletSpecialLevel * SystemOption.ExBulletScalePerL * SystemOption.SceneScale,
                                gun.BulletSize + UpgradeTree.PlayerArchive.ExtraBulletSpecialLevel * SystemOption.ExBulletScalePerL * SystemOption.SceneScale);
            newBullet.GetComponent<PenetrateBullet>().DeMaxDamage = deMaxDamage;
            newBullet.GetComponent<PenetrateBullet>().DeSpeed = deSpeed;
            Debug.DrawLine(position,bullet.transform.position,Color.red,2);
        }
        
        public void CreateLinker(String shootername,Gun gun,Vector3 position,Vector3 muzzleOrientation,float nextFindTime,float deMaxDamagePer,int maxNum,float searchRadius, BulletType bt)
        {
            Vector3 start = position + muzzleOrientation.normalized * 
                            (SystemOption.StartBulletOffsetScale + gun.BulletSize + UpgradeTree.PlayerArchive.ExtraBulletSpecialLevel * SystemOption.ExBulletScalePerL * SystemOption.SceneScale);
            start.z = SystemOption.ItemZPosition;
            GameObject newBullet = NewObjectPool.Current.Generate("BulletT");
            
            
            newBullet.transform.right = muzzleOrientation;
            newBullet.GetComponent<SpriteRenderer>().color = BuffColor(gun.Gunbuff);
            LazerState lazerState = newBullet.GetComponent<LazerState>();
            lazerState.Shootout(shootername,gun, start,muzzleOrientation);
            newBullet.GetComponent<Transform>().localScale = new Vector3(0.5f*SystemOption.SceneScale,0.5f*SystemOption.SceneScale,0.5f*SystemOption.SceneScale);
            newBullet.GetComponent<Linker>().Set(nextFindTime, deMaxDamagePer, maxNum, searchRadius);
        }
        
        public void CreateBomb(String shootername,Gun gun,Vector3 position,Vector3 muzzleOrientation,BulletType bt,float radius,float force,Boolean isPlayer = true)
        {
            if (isPlayer)
            {
                Vector3 start = position + muzzleOrientation.normalized * 
                                (SystemOption.StartBulletOffsetScale + gun.BulletSize + UpgradeTree.PlayerArchive.ExtraBulletSpecialLevel * SystemOption.ExBulletScalePerL * SystemOption.SceneScale);
                start.z = SystemOption.ItemZPosition;
                GameObject newBullet = NewObjectPool.Current.Generate("BombBullet");
            
            
                newBullet.transform.right = muzzleOrientation;
                newBullet.GetComponent<SpriteRenderer>().color = BuffColor(gun.Gunbuff);
                Bullet bullet = newBullet.GetComponent<Bullet>();
                bullet.SetSprite(bt);
                
                bullet.Shootout(shootername,gun, start,muzzleOrientation);
                newBullet.GetComponent<Transform>().localScale = new Vector3(gun.BulletSize + UpgradeTree.PlayerArchive.ExtraBulletSpecialLevel * SystemOption.ExBulletScalePerL * SystemOption.SceneScale,
                    gun.BulletSize + UpgradeTree.PlayerArchive.ExtraBulletSpecialLevel * SystemOption.ExBulletScalePerL * SystemOption.SceneScale,
                    gun.BulletSize + UpgradeTree.PlayerArchive.ExtraBulletSpecialLevel * SystemOption.ExBulletScalePerL * SystemOption.SceneScale);
                newBullet.GetComponent<BombBullet>().BombRadius = radius;
                newBullet.GetComponent<BombBullet>().BombForce = force;
                //Debug.DrawLine(position,bullet.transform.position,Color.red,2);
            }
            else
            {
                Vector3 start = position + muzzleOrientation.normalized * 
                                (SystemOption.StartBulletOffsetScale + gun.BulletSize);
                start.z = SystemOption.ItemZPosition;
                GameObject newBullet = NewObjectPool.Current.Generate("BombBullet");
            
            
                newBullet.transform.right = muzzleOrientation;
                newBullet.GetComponent<SpriteRenderer>().color = BuffColor(gun.Gunbuff);
                Bullet bullet = newBullet.GetComponent<Bullet>();
                bullet.SetSprite(bt);
                bullet.Shootout(shootername,gun, start,muzzleOrientation);
                newBullet.GetComponent<Transform>().localScale = new Vector3(gun.BulletSize,gun.BulletSize,gun.BulletSize);
                newBullet.GetComponent<BombBullet>().BombRadius = radius;
                newBullet.GetComponent<BombBullet>().BombForce = force;
                Debug.DrawLine(position,bullet.transform.position,Color.red,2);
            }
        }
        
        
        public void CreateRed(String shootername,Gun gun,Vector3 position,Vector3 muzzleOrientation,BulletType bt,Boolean isPlayer = true)
        {
            if (isPlayer)
            {
                Debug.Log("PlayerClassicalShoot");
                Vector3 start = position + muzzleOrientation.normalized * 
                                (SystemOption.StartBulletOffsetScale + gun.BulletSize + UpgradeTree.PlayerArchive.ExtraBulletSpecialLevel * SystemOption.ExBulletScalePerL);
                start.z = SystemOption.ItemZPosition;
                GameObject newBullet = NewObjectPool.Current.Generate("BulletF");

                newBullet.transform.right = muzzleOrientation;
                newBullet.GetComponent<SpriteRenderer>().color = BuffColor(gun.Gunbuff);
                Bullet bullet = newBullet.GetComponent<Bullet>();
                bullet.SetSprite(bt);
                bullet.Shootout(shootername,gun, start,muzzleOrientation,true);
                newBullet.GetComponent<Transform>().localScale = 
                    new Vector3(gun.BulletSize + UpgradeTree.PlayerArchive.ExtraBulletSpecialLevel * SystemOption.ExBulletScalePerL * SystemOption.SceneScale,
                                gun.BulletSize + UpgradeTree.PlayerArchive.ExtraBulletSpecialLevel * SystemOption.ExBulletScalePerL * SystemOption.SceneScale,
                                gun.BulletSize + UpgradeTree.PlayerArchive.ExtraBulletSpecialLevel * SystemOption.ExBulletScalePerL * SystemOption.SceneScale);
                //Debug.DrawLine(position,start,Color.red,2);
            }
            else
            {
                Vector3 start = position + muzzleOrientation.normalized * 
                                (SystemOption.StartBulletOffsetScale + gun.BulletSize);
                start.z = SystemOption.ItemZPosition;
                GameObject newBullet =  NewObjectPool.Current.Generate("BulletF");
                
                newBullet.transform.right = muzzleOrientation;
                newBullet.GetComponent<SpriteRenderer>().color = BuffColor(gun.Gunbuff);
                Bullet bullet = newBullet.GetComponent<Bullet>();
                bullet.SetSprite(bt);
                bullet.Shootout(shootername, gun, start, muzzleOrientation, false);
                newBullet.GetComponent<Transform>().localScale =
                    new Vector3(gun.BulletSize, gun.BulletSize, gun.BulletSize);
                //Debug.DrawLine(position,bullet.transform.position,Color.red,2);
            }
        }
        
        public void CreateYellow(String shootername,Gun gun,Vector3 position,Vector3 muzzleOrientation,BulletType bt,Boolean isPlayer = true)
        {
            if (isPlayer)
            {
                Debug.Log("PlayerClassicalShoot");
                Vector3 start = position + muzzleOrientation.normalized * 
                                (SystemOption.StartBulletOffsetScale + gun.BulletSize + UpgradeTree.PlayerArchive.ExtraBulletSpecialLevel * SystemOption.ExBulletScalePerL);
                start.z = SystemOption.ItemZPosition;
                GameObject newBullet = NewObjectPool.Current.Generate("BulletC");

                newBullet.transform.right = muzzleOrientation;
                newBullet.GetComponent<SpriteRenderer>().color = BuffColor(gun.Gunbuff);
                Bullet bullet = newBullet.GetComponent<Bullet>();
                bullet.SetSprite(bt);
                bullet.Shootout(shootername,gun, start,muzzleOrientation,true);
                newBullet.GetComponent<Transform>().localScale = 
                    new Vector3(gun.BulletSize + UpgradeTree.PlayerArchive.ExtraBulletSpecialLevel * SystemOption.ExBulletScalePerL * SystemOption.SceneScale,
                                gun.BulletSize + UpgradeTree.PlayerArchive.ExtraBulletSpecialLevel * SystemOption.ExBulletScalePerL * SystemOption.SceneScale,
                                gun.BulletSize + UpgradeTree.PlayerArchive.ExtraBulletSpecialLevel * SystemOption.ExBulletScalePerL * SystemOption.SceneScale);
                //Debug.DrawLine(position,start,Color.red,2);
            }
            else
            {
                Vector3 start = position + muzzleOrientation.normalized * 
                                (SystemOption.StartBulletOffsetScale + gun.BulletSize);
                start.z = SystemOption.ItemZPosition;
                GameObject newBullet =  NewObjectPool.Current.Generate("BulletC");
                
                newBullet.transform.right = muzzleOrientation;
                newBullet.GetComponent<SpriteRenderer>().color = BuffColor(gun.Gunbuff);
                Bullet bullet = newBullet.GetComponent<Bullet>();
                bullet.SetSprite(bt);
                bullet.Shootout(shootername, gun, start, muzzleOrientation, false);
                newBullet.GetComponent<Transform>().localScale =
                    new Vector3(gun.BulletSize, gun.BulletSize, gun.BulletSize);
                //Debug.DrawLine(position,bullet.transform.position,Color.red,2);
            }
        }
        
        
        public void CreateBlue(String shootername,Gun gun,Vector3 position,Vector3 muzzleOrientation,BulletType bt,Boolean isPlayer = true)
        {
            if (isPlayer)
            {
                Debug.Log("PlayerClassicalShoot");
                Vector3 start = position + muzzleOrientation.normalized * 
                                (SystemOption.StartBulletOffsetScale + gun.BulletSize + UpgradeTree.PlayerArchive.ExtraBulletSpecialLevel * SystemOption.ExBulletScalePerL);
                start.z = SystemOption.ItemZPosition;
                GameObject newBullet = NewObjectPool.Current.Generate("BulletT");

                newBullet.transform.right = muzzleOrientation;
                newBullet.GetComponent<SpriteRenderer>().color = BuffColor(gun.Gunbuff);
                Bullet bullet = newBullet.GetComponent<Bullet>();
                bullet.SetSprite(bt);
                bullet.Shootout(shootername,gun, start,muzzleOrientation,true);
                newBullet.GetComponent<Transform>().localScale = 
                    new Vector3(gun.BulletSize + UpgradeTree.PlayerArchive.ExtraBulletSpecialLevel * SystemOption.ExBulletScalePerL * SystemOption.SceneScale,
                                gun.BulletSize + UpgradeTree.PlayerArchive.ExtraBulletSpecialLevel * SystemOption.ExBulletScalePerL * SystemOption.SceneScale,
                                gun.BulletSize + UpgradeTree.PlayerArchive.ExtraBulletSpecialLevel * SystemOption.ExBulletScalePerL * SystemOption.SceneScale);
                //Debug.DrawLine(position,start,Color.red,2);
            }
            else
            {
                Vector3 start = position + muzzleOrientation.normalized * 
                                (SystemOption.StartBulletOffsetScale + gun.BulletSize);
                start.z = SystemOption.ItemZPosition;
                GameObject newBullet =  NewObjectPool.Current.Generate("BulletT");
                
                newBullet.transform.right = muzzleOrientation;
                newBullet.GetComponent<SpriteRenderer>().color = BuffColor(gun.Gunbuff);
                Bullet bullet = newBullet.GetComponent<Bullet>();
                bullet.SetSprite(bt);
                bullet.Shootout(shootername, gun, start, muzzleOrientation, false);
                newBullet.GetComponent<Transform>().localScale =
                    new Vector3(gun.BulletSize, gun.BulletSize, gun.BulletSize);
                //Debug.DrawLine(position,bullet.transform.position,Color.red,2);
            }
        }

        private Color BuffColor(Buff buff)
        {
            switch (buff)
            {
                    case Buff.Avatar:return Color.yellow;
                    case Buff.Fire:return Color.red;
                    case Buff.Freeze:return Color.cyan;
                    case Buff.Frozen:return Color.blue;
                    default:return Color.white;
            }
        }
    }
    

}

