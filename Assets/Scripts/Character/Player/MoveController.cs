using System;
using CenterSystem;
using UnityEngine;

namespace Character.Player
{
	public class MoveController : MonoBehaviour
	{
		public float Speed;
        private float _fastspeed;
        private float _local;
		private State _state;

        private Animator anime;
		private Vector3 _muzzleOrientation;
		private Rigidbody2D rb;
		private float cd;
		// Use this for initialization
		void Start ()
		{
			_fastspeed = 2 * Speed;
            anime = GetComponent<Animator>();
			_state = this.GetComponent<State>();
			rb = GetComponent<Rigidbody2D>();
		}
	
		// Update is called once per frame
		void FixedUpdate () {
			if (!_state.Isdead)
			{
				if (Input.GetKey(SystemOption.SpeedFaster))
				{
					if (cd == 0)
					{
						cd = 1f;
						GetMousePosition();               
						Vector3 force = _muzzleOrientation.normalized * _fastspeed*80;
						rb.AddForce(force);
					}
	            
					//_local = _fastspeed * SystemOption.SceneScale * _state.Speedscale;
				}
				else
				{
					_local = Speed * SystemOption.SceneScale * _state.Speedscale;
				}

				cd -= 0.02f;
				if (cd < 0)
				{
					cd = 0;
				}

				MoveByVector3Lerp();
			}

        }
	
		private void MoveByVector3Lerp()
		{
            //1、获得当前位置
            //Vector3 curenPosition = this.transform.position;
            //2、获得方向

            /*if (Input.GetKey(KeyCode.W))
			{
				this.GetComponent<Rigidbody2D>().AddForce(Vector2.up *local);
			}else if (Input.GetKey(KeyCode.S)){
				this.GetComponent<Rigidbody2D>().AddForce(Vector2.down *local);
			}
			if (Input.GetKey(KeyCode.A)){
				this.GetComponent<Rigidbody2D>().AddForce(Vector2.left *local);
			}else if (Input.GetKey(KeyCode.D)){
				this.GetComponent<Rigidbody2D>().AddForce(Vector2.right *local);
			}*/


            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            Vector3 vector = new Vector3(h, v, 0).normalized;
            transform.Translate(vector * _local * Time.deltaTime);
            anime.SetFloat("speed", Mathf.Abs(h) + Mathf.Abs(v));
		}

		private void GetMousePosition()
		{
            
			Vector3 pos  = Camera.main.WorldToScreenPoint(transform.position);
			Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, pos.z);
			_muzzleOrientation = Camera.main.ScreenToWorldPoint(mousePos) - transform.position;
		}
		
	}
}
