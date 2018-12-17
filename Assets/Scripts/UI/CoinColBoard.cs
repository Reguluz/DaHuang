using System.Collections.Generic;
using CenterSystem;
using Character;
using Character.Player;
using UnityEngine;
using UnityEngine.UI;
using Weapons.Gun;

namespace UI
{
	[RequireComponent(typeof(Text))]
	public class CoinColBoard : MonoBehaviour,IDashBoard,IDisplayment
	{
		private int _coinCol;

		public Text Number;
		// Use this for initialization
		void OnEnable () {
			GameObject.Find("CenterProcess").GetComponent<CenterProcess>().RegisterDashBoard(this);
		}
	
		// Update is called once per frame
		void Update () {
			
		}

		public void UpdateData(CenterProcess centerProcess)
		{
			_coinCol = centerProcess.IntervalCoins;
			Display();
		}

		public void Display()
		{
			Number.text = _coinCol.ToString();
		}
	}
}
