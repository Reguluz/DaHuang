using System;
using System.Collections.Generic;
using CenterSystem;
using Character;
using Character.Player;
using UnityEngine;
using Weapons.Gun;

namespace Weapons.Bullets
{
    public class LazerState : MonoBehaviour
    {

        private String _shootername;
        private float _damage;

        private Vector3 _startposition;
        private Vector3 _targetposition;
        private Vector3 _endposition;
        
        private float _range;


        private Transform _bulletTransform;
        private Buff _buff;


        private Gun.Gun _shooter;

        
        private void OnEnable()
        {
            transform.position = Vector3.zero;            
        }
        private void Start()
        {

    
        }

        private void FixedUpdate()
        {
            
        }

        public void Shootout(String shootername, Gun.Gun shooter,Vector3 shootposition, Vector3 targetposition)
        {
            this._shootername = shootername;
            _shooter = shooter;
            _startposition = shootposition + targetposition.normalized * 2*SystemOption.SceneScale;
            transform.position = _startposition;
            _targetposition = targetposition;
            _endposition = _startposition+ _range * _targetposition.normalized;
            _damage = shooter.Damage;
        }
       
       
        public void Destoryself()
        {
            Debug.Log("回对象池");
            ObjectPool.current.PoolObject (gameObject);
            //Destroy(this.gameObject);
        }


        public string Shootername
        {
            get { return _shootername; }
            set { _shootername = value; }
        }

        public Vector3 Startposition
        {
            get { return _startposition; }
            set { _startposition = value; }
        }

        public Vector3 Targetposition
        {
            get { return _targetposition; }
            set { _targetposition = value; }
        }

        public float Range
        {
            get { return _range; }
            set { _range = value; }
        }

        public Transform BulletTransform
        {
            get { return _bulletTransform; }
            set { _bulletTransform = value; }
        }

        public Buff Buff
        {
            get { return _buff; }
            set { _buff = value; }
        }

        public Gun.Gun Shooter
        {
            get { return _shooter; }
            set { _shooter = value; }
        }

        public float Damage
        {
            get { return _damage; }
            set { _damage = value; }
        }
    }
}