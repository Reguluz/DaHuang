using UnityEngine;

namespace Character
{
	public class MonsterTracking : MonoBehaviour {
		private Vector3    _distance;
		private Vector3    _target;
		public  GameObject Nearest;
		public  Rigidbody  Rb;

		// Use this for initialization
		void Start () {
		
		}
	
		// Update is called once per frame
		void Update () {
			Vector3 self  = transform.position;
			Vector3 light = Nearest.transform.position;
			_distance   = transform.position - light;
			_distance.y = 0;

			_target = light - _distance;

			Debug.DrawLine (Vector3.zero,       light,                       Color.red);
			Debug.DrawLine (Vector3.zero,       _distance,                    Color.blue);
			Debug.DrawLine (light,              _distance,                    Color.green);
			Debug.DrawLine (transform.position, transform.position + _target, Color.cyan);
			transform.LookAt (transform.position                   +_target);
			if (_target.magnitude < 100) {
				Rb.AddForce (_target.normalized *4f);

			}
			
		}
	}
}
