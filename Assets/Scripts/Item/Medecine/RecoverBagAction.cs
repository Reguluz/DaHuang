using Character;
using Character.Player;
using UnityEngine;

namespace Item.Medecine
{
	public class RecoverBagAction : MonoBehaviour
	{
		// Use this for initialization
		void Start () {
		}
	
		// Update is called once per frame
		void Update () {
		
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.CompareTag("Player"))
			{

				State state = other.GetComponent<State>();
				state.Health += 50;
				if (state.Health > state.Maxhealth)
				{
					state.Health = state.Maxhealth;
				}
				//这里播放获取特效
				Destroy(this.gameObject);
			}
		}
	}
}
