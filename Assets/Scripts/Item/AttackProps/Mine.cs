using System;
using System.Collections;
using System.Collections.Generic;
using Character;
using UnityEngine;
using Weapons.Bullets;

namespace Item.AttackProps
{
    public class Mine : AttackProp,IBoom {

        public Mine()
        {
            AttackPropType = AttackProps.Mine;
        }

        public float BoomRadius;	
        public float MaxDamage;
        public float BombForce = 10f;
	
        private bool _isReady = false;
        private String _shootername;
        private float _bombDamage;
        private SpriteRenderer _spriteshow;

	
        // Use this for initialization
        void Start () {
            Invoke("CompleteInstalling",2f);
            _spriteshow.enabled = false;
        }
	
        // Update is called once per frame
        void Update () {
		
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (_isReady)
            {
                if (other.CompareTag("Monster") || other.CompareTag("Player"))
                {
                    Boom();
                }            
            }
            
        }

        private void CompleteInstalling()
        {
            _isReady = true;
            _spriteshow.enabled = true;
        }

        public void Boom()
        {
            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, BoomRadius, 1 << LayerMask.NameToLayer("Character"));
            
            foreach(Collider2D en in enemies)
            {
                // Check if it has a rigidbody (since there is only one per enemy, on the parent).
                Rigidbody2D rb = en.GetComponent<Rigidbody2D>();
                if(rb != null)
                {
                    Vector3 deltaPos = rb.transform.position - transform.position;
                    _bombDamage = CalculateDamage(deltaPos);
                    rb.gameObject.GetComponent<State>().Hurt(_bombDamage,_shootername,"Mine");
                    
                    Debug.DrawLine(rb.transform.position,transform.position,Color.magenta,5);
                    //炸远功能
                    Vector3 force = deltaPos.normalized * BombForce*100;
                    rb.AddForce(force);
                    
                    Destroy(this.gameObject);
                }
            }
        }

        private float CalculateDamage(Vector3 deltaPos)
        {
            float distance = deltaPos.magnitude;
            return MaxDamage * Mathf.Cos(distance / BoomRadius * Mathf.PI);

        }

        public string Shootername
        {
            get { return _shootername; }
            set { _shootername = value; }
        }
    }


}
