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
        private float _clamp1;
        private float _clamp2;

        public Camera cam;
        
        // Use this for initialization
        void Start ()
		{
            float orthographicSize = cam.orthographicSize;
            float aspectRatio = Screen.width * 1.0f / Screen.height;
            _clamp1 = cam.transform.position.x - orthographicSize * 2f * aspectRatio / 2.0f;
            _clamp2 = cam.transform.position.x + orthographicSize * 2f * aspectRatio / 2.0f;

            _fastspeed = 2 * Speed;
            anime = GetComponent<Animator>();
			_state = this.GetComponent<State>();
			rb = GetComponent<Rigidbody2D>();
			anime.SetBool("shift", false);
		}
	
		// Update is called once per frame
		void FixedUpdate () {
			if (!_state.Isdead)
			{
				if (Input.GetKey(SystemOption.SpeedFaster))
				{
					if (cd == 0)
					{
                        anime.SetBool("shift", true);
                        cd = 0.5f;
						GetMousePosition();          
						Debug.Log(rb.velocity);
						Vector3 force = Vector3.zero;
						if (Input.GetKey(KeyCode.A))
						{
							force += Vector3.left * _fastspeed*80;
						}else if (Input.GetKey(KeyCode.D))
						{
							force += Vector3.right * _fastspeed*80;
						}
						else
						{
							force += Vector3.right * _fastspeed*80;
						}
						
						if (Input.GetKey(KeyCode.W))
						{
							force += Vector3.up * _fastspeed*80;
						}else if (Input.GetKey(KeyCode.S))
						{
							force += Vector3.down * _fastspeed*80;
						}
						
						rb.AddForce(force);
					}
					
					//_local = _fastspeed * SystemOption.SceneScale * _state.Speedscale;
				}
				else
				{
					anime.SetBool("shift", false);
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
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, _clamp1, _clamp2), transform.position.y, transform.position.z);
            //anime.SetFloat("speed", Mathf.Abs(h) + Mathf.Abs(v));
		}

		private void GetMousePosition()
		{
            
			Vector3 pos  = Camera.main.WorldToScreenPoint(transform.position);
			Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, pos.z);
			_muzzleOrientation = Camera.main.ScreenToWorldPoint(mousePos) - transform.position;
		}
		
	}
}
