using System;
using CenterSystem;
using Character.Player;
using UnityEngine;

namespace Item.AttackProps
{
    public class AttackPropsAction:MonoBehaviour
    {
        public AttackProps Chosen;
		private String _createname;
        private String _currentGun;
		private Sprite _sp;

		// Use this for initialization
		private void Awake()
		{
			
		}


	    public void Init()
		{
			_createname = Chosen.ToString();
			_sp = Resources.Load<Sprite>("ItemSprite/" + _createname);
			this.GetComponent<SpriteRenderer>().sprite = _sp;
		}

		

		// Update is called once per frame
		void Update()
		{

		}

		private void OnTriggerEnter2D(Collider2D other)
		{

			if (other.CompareTag("Player"))
			{
				PlayerController controller = other.GetComponent<PlayerController>();
				controller.GetComponent<PlayerBag>().GetItem(Chosen);

				PlayerAudioCollection.GunCollection.Play("Pickup");//这里播放获取特效
				Destroy(this.gameObject);
			}
		}

    }
}