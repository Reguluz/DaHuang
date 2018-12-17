using UnityEngine;

namespace UI
{
	public class SpriteRotate : MonoBehaviour
	{
		public bool Statics;

		public float RotateAnglePerS;

		public bool Counterclockwise;
		// Use this for initialization
		void Start ()
		{
			if (!Counterclockwise)
			{
				transform.Rotate(0, 0, -RotateAnglePerS*Time.deltaTime);
			}
			else
			{
				transform.Rotate(0, 0, RotateAnglePerS*Time.deltaTime);
			}
		}
	
		// Update is called once per frame
		void Update () {
			if (Statics)
			{
				if (!Counterclockwise)
				{
					transform.Rotate(0, 0, -RotateAnglePerS*Time.deltaTime);
				}else
				{
					transform.Rotate(0, 0, RotateAnglePerS*Time.deltaTime);
				}
			}
		}
	}
}
