using System.Collections.Generic;
using CenterSystem;
using Character;
using Character.Player;
using UnityEngine;
using UnityEngine.UI;
using Weapons.Gun;

namespace UI
{
	public class WeaponBoard : MonoBehaviour,IDashBoard,IDisplayment
	{

		public Image WeaponImage;
		private Gun _gun;
		//private SpriteRenderer _spriteRenderer;

		// Use this for initialization
		void Start ()
		{
			
			GameObject.Find("CenterProcess").GetComponent<CenterProcess>().RegisterDashBoard(this);    
		
        	_gun = null;
			//Display();
		}
	
		// Update is called once per frame
		void Update () {
		
		}

		public void UpdateData(CenterProcess centerProcess)
		{
			_gun = centerProcess.Gun;
			Display();
		}

		public void Display()
		{
			if (_gun!=null)
			{
				WeaponImage.sprite = Resources.Load<Sprite>("GunSprite/" + _gun.GetType().Name);
			}
			else
			{
				WeaponImage.sprite = Resources.Load<Sprite>("GunSprite/null");
			}
			
		}
	}
}
