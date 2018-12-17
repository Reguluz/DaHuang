using System;
using CenterSystem;
using Character;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Weapons.Bullets
{
    public class TrackBullet:MonoBehaviour
    {
        private String _shootername;
        private Transform _endposition;
        
        
        private bool _isChild=false; //Multiple用
        private Transform _bulletTransform;
        private SpriteRenderer _spriteRenderer;
        public Sprite BulletSprite;    
        

        private Gun.Gun _shooter;
        private float _moveSpeed;
        private float _damage;

        private Vector3 _targetdirection;

        private float _rotateper;
        

        private void Awake()
        {
            _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        }

        void OnEnable()
        {
            transform.position = Vector3.zero;      
            Invoke("Destoryself",10f);
        }


        private void FixedUpdate()
        {
            //if (_isChild == false && _shooter!=null)//Multiple用

            
            
        }

        private void Update()
        {
            
            transform.Translate(transform.right*Time.deltaTime*10); 
            if (_endposition)
            {
                _targetdirection = _endposition.position - transform.position;
                Debug.DrawLine(transform.position,_endposition.position,Color.magenta);
                transform.right = Vector3.Lerp(transform.right,_targetdirection,0.75f);
                if (transform.position == _endposition.position)
                {
                    Destoryself();
                }
            }
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(_targetdirection), _rotateper);

            
        }

        public void Shootout(String shootername,Gun.Gun shooter, Vector3 startposition,float rotateper)
        {
            this._shootername = shootername;
            _shooter = shooter;
            if (startposition != default(Vector3))
            {
                //_startposition = startposition + targetposition.normalized * 2;
                transform.position = startposition;
                _rotateper = rotateper;
                _moveSpeed = _shooter.ShootSpeed * 0.02f * SystemOption.SceneScale;
                
                SetColor(shooter.Gunbuff);
            }
             
            if (_shooter != null)
            {
                _damage = _shooter.Damage;
            }

            Collider2D[] monsters = Physics2D.OverlapCircleAll(transform.position, 200,1<< LayerMask.NameToLayer("Hit"));
            
            Debug.Log(monsters.Length);
            int t;
            do
            {
                t = Random.Range(0, monsters.Length - 1);
            } while (!monsters[t].gameObject.CompareTag("Monster"));

            _endposition = monsters[t].transform;
            Debug.DrawLine(startposition,_endposition.position,Color.cyan,2);
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
            Debug.Log("TrackDestory");
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


    }
}