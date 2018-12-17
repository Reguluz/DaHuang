using System;
using System.Collections.Generic;
using CenterSystem;
using Character;
using Character.Player;
using UnityEngine;
using Weapons.Gun;

namespace Weapons.Bullets
{
    public class Bullet : MonoBehaviour
    {
        private String _shootername;
        private Vector3 _endposition;

        private bool _isChild=false; //Multiple用
        private Transform _bulletTransform;
        private SpriteRenderer _spriteRenderer;
        public Sprite[] BulletSprites;    
        

        private Gun.Gun _shooter;
        private float _moveSpeed;
        private float _damage;

        private bool _isPlayer = false;

        private void Awake()
        {
            _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            SetSprite(BulletType.Magicball);
        }

        void OnEnable()
        {
            transform.position = Vector3.zero;      
        }


        private void FixedUpdate()
        {
            //if (_isChild == false && _shooter!=null)//Multiple用
            
            transform.position = Vector3.MoveTowards(transform.position, _endposition, _moveSpeed);
            

            if (transform.position == _endposition)
            {
                Destoryself();
            }
            
        }

        private void Update()
        {
            Debug.DrawLine(transform.position,_endposition);
        }

        public void Shootout(String shootername,Gun.Gun shooter, Vector3 startposition,Vector3 targetposition= default(Vector3),bool isPlayer = false)
        {
            this._shootername = shootername;
            _shooter = shooter;
            this._isPlayer = isPlayer;
            if (startposition != default(Vector3))
            {
                //_startposition = startposition + targetposition.normalized * 2;
                transform.position = startposition;
                _endposition = startposition + _shooter.ShootRange * targetposition.normalized * SystemOption.SceneScale;
                _moveSpeed = _shooter.ShootSpeed * 0.02f * SystemOption.SceneScale;
                Debug.DrawLine(startposition,_endposition,Color.cyan,2);
                SetColor(shooter.Gunbuff);
            }
             
            if (_shooter != null)
            {
                _damage = _shooter.Damage;
            }
            
            
            
        }

        public void SetSprite(BulletType bt)
        {
            foreach (Sprite sp in BulletSprites)
            {
                if (sp.name.Equals(bt.ToString()))
                {
                    _spriteRenderer.sprite = sp;
                }
            }
        }

        private void SetColor(Buff buff)
        {
            switch (buff)
            {
                    case Buff.Fire:
                        _spriteRenderer.color = Color.red;
                        break;
                    case Buff.Freeze:
                        _spriteRenderer.color = Color.blue;
                        break;
                    case Buff.Frozen:
                        _spriteRenderer.color = Color.cyan;
                        break;
                    default: break;
            }
        }

        /*public void SetChild(Vector3 shootposition)//Multiple用
        {
            _isChild = true;

            transform.position = shootposition;
            transform.localScale = new Vector3(_shooter.BulletSize, _shooter.BulletSize, _shooter.BulletSize);
        }*/

        public void Destoryself()
        {
            NewObjectPool.Current.Destroy(gameObject);
            
        }

       
        
        void OnDisable()
        {
            CancelInvoke ("Destoryself");
        }


    
        public string Shootername
        {
            get { return _shootername; }
            set { _shootername = value; }
        }   


        /*public bool IsChild
        {
            get { return _isChild; }
            set { _isChild = value; }
        }*/

        public Transform BulletTransform
        {
            get { return _bulletTransform; }
            set { _bulletTransform = value; }
        }

        public Gun.Gun Shooter
        {
            get { return _shooter; }
            set { _shooter = value; }
        }

        public float MoveSpeed
        {
            get { return _moveSpeed; }
            set { _moveSpeed = value; }
        }

        public float Damage
        {
            get { return _damage; }
            set { _damage = value; }
        }

        public bool IsPlayer
        {
            get { return _isPlayer; }
            set { _isPlayer = value; }
        }

        public Vector3 Endposition
        {
            get { return _endposition; }
            set { _endposition = value; }
        }
    }
}