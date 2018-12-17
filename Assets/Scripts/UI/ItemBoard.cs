using System.Collections.Generic;
using CenterSystem;
using Character;
using Character.Player;
using UnityEngine;
using UnityEngine.UI;
using Weapons.Gun;


namespace UI
{
	public class ItemBoard : MonoBehaviour,IDashBoard,IDisplayment
	{

		public Image Image;
		public Text Text;
		private PlayerBag _bag;

		// Use this for initialization
		void Start ()
		{
			Image.sprite = null;
			Text.text = " ";	
			GameObject.Find("CenterProcess").GetComponent<CenterProcess>().RegisterDashBoard(this);
		}
	
		// Update is called once per frame
		void Update () {
		
		}

		public void UpdateData(CenterProcess centerProcess)
		{
			_bag = centerProcess.Bag;
			Display();
		}

		public void Display()
		{
			if (_bag!=null)
			{
				Image.sprite = Resources.Load<Sprite>("ItemSprite/" + _bag.list[_bag.Now]);
				Text.text = _bag.itemCount[_bag.Now].ToString();
			}
		}
	}
}
