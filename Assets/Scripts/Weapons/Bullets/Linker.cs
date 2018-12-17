using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using CenterSystem;
using Character;
using Character.Player;
using UnityEditor;
using UnityEngine;
using Weapons.Bullets.LazerTools;

namespace Weapons.Bullets
{
    public class Linker:MonoBehaviour
    {
        private LazerState _lazerState;  
        private int _count;
        private float _realRadius;
        
        private float _nextFindTime;
        private float _deMaxDamagePer=0.1f;
        private int _maxNum;
        private float _searchRadius = 20;

        private Vector3 _nextTarget;
        



        private void Start()
        {
            _lazerState = this.gameObject.GetComponent<LazerState>();

        }

        void OnEnable ()
        {
            _count = 0;
            _nextTarget = Vector3.zero;         
            
        }

        private void OnDisable()
        {
            
            CancelInvoke("FindNext");
        }

        // Update is called once per frame
        void Update () {
            if ((_nextTarget - transform.position).magnitude > 0)
            {
                transform.position = Vector3.Lerp(transform.position,_nextTarget,_nextFindTime);
            }
            
            if (_lazerState.Damage<5 || _count>= _maxNum + UpgradeTree.PlayerArchive.ExtraBulletSpecialLevel)
            {                
                Debug.Log("伤害降低或超出数量销毁");

                _lazerState.Destoryself();
            }
            
        }
        void FixedUpdate()
        {
            
        }

        public void Set(float nextFindTime,float deMaxDamagePer,int maxNum,float searchRadius)
        {
            _nextFindTime = nextFindTime;
            _deMaxDamagePer = deMaxDamagePer;
            _maxNum = maxNum;
            _searchRadius = searchRadius;
            InvokeRepeating("FindNext", 0, _nextFindTime);
        }
        
        private void FindNext()
        {
            
                _realRadius =     
                    _searchRadius + UpgradeTree.PlayerArchive.ExtraBulletSpecialLevel * SystemOption.ExBombRadiusPerL *
                    0.1f                                                              * SystemOption.SceneScale;
                
                Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, _realRadius, 1 << LayerMask.NameToLayer("Hit"));
                    
                List<CharacterDistance> cdList = new List<CharacterDistance>();
                Debug.Log(enemies.Length);
                if (enemies.Length > 1)
                {
                    for(int i=0;i <enemies.Length;i++)
                    {                
                        
                        if(enemies[i].CompareTag("Monster"))
                        {      
                            Vector3 deltaPos = enemies[i].gameObject.transform.position - transform.position;
                            CharacterDistance cd = new CharacterDistance(enemies[i].gameObject,deltaPos.magnitude);
                            cdList.Add(cd);
                        }                    
                    }

                    if (cdList.Count > 1)
                    {
                        cdList.Sort(new DistanceComparer());
                                                
                        _nextTarget = cdList[1].Character.transform.position;
                        
                        //transform.parent = cdList[1].Character.gameObject.transform;

                        cdList[0].Character.gameObject.GetComponent<State>().Hurt(_lazerState.Damage + UpgradeTree.PlayerArchive.ExtraAttackLevel, _lazerState.Shootername, _lazerState.Shooter.Gunname.ToString());
                        Attenuation();
                    }
                    else
                    {
                        _nextTarget = cdList[0].Character.transform.position;
                        //transform.parent = cdList[0].Character.gameObject.transform;
                        cdList[0].Character.gameObject.GetComponent<State>().Hurt(_lazerState.Damage + UpgradeTree.PlayerArchive.ExtraAttackLevel, _lazerState.Shootername, _lazerState.Shooter.Gunname.ToString());
                    }
                    
                    
                    _count++;
                   
                
                  
                }
                else
                {          
                    _lazerState.Destoryself();
                    
                }
            

        }

         


        private void Attenuation()
        {
            _lazerState.Damage = _lazerState.Damage*(1-_deMaxDamagePer);
        }

       
    }
}