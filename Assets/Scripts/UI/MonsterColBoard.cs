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
	public class MonsterColBoard : MonoBehaviour,IDashBoard,IDisplayment
	{
		private int _monsterCol;

		public Text Number;
		// Use this for initialization
		void Start () {
			GameObject.Find("CenterProcess").GetComponent<CenterProcess>().RegisterDashBoard(this);
		}
	
		// Update is called once per frame
		void Update () {
			
		}

		public void UpdateData(CenterProcess centerProcess)
		{
			_monsterCol = centerProcess.MonsterCol;
			Display();
		}

		public void Display()
		{
			Number.text = _monsterCol.ToString();
		}
	}
}
