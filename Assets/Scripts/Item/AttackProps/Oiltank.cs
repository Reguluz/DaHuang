using System;
using Character;
using UnityEngine;
using Weapons.Bullets;

namespace Item.AttackProps
{
	public class Oiltank : AttackProp,IBoom{

		public Oiltank()
		{
			AttackPropType = AttackProps.Oiltank;
		}

		public float BoomRadius;	
		public float MaxDamage;
		public float BombForce = 10f;
		
		private String _shootername;
		private int _bombDamage;
		private SpriteRenderer _spriteshow;

		private State _state;
		// Use this for initialization
		void Start ()
		{
			_state = this.GetComponent<State>();
		}
	
		// Update is called once per frame
		void Update () {
			if (_state.Isdead)
			{
				Boom();

			}
		}

		public void Boom()
		{
			Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, BoomRadius, 1 << LayerMask.NameToLayer("Hit"));
            
			foreach(Collider2D en in enemies)
			{
				// Check if it has a rigidbody (since there is only one per enemy, on the parent).
				Rigidbody2D rb = en.GetComponent<Rigidbody2D>();
				if(rb != null)
				{
					Vector3 deltaPos = rb.transform.position - transform.position;
					_bombDamage = (int)CalculateDamage(deltaPos);
					rb.gameObject.GetComponent<State>().Hurt(_bombDamage,"OilTank",_state.Name,Buff.Fire);
                    
					Debug.DrawLine(rb.transform.position,transform.position,Color.magenta,5);
					//炸远功能
					Vector3 force = deltaPos.normalized * BombForce*100;
					rb.AddForce(force);
				
					//Destroy(this.gameObject);
				}
			}
			_state.Destoryself();
		}
		private float CalculateDamage(Vector3 deltaPos)
		{
			float distance = deltaPos.magnitude;
			return MaxDamage * Mathf.Cos(BoomRadius/distance/100 * Mathf.PI);

		}

		public string Shootername
		{
			get { return _shootername; }
			set { _shootername = value; }
		}
	}
}
