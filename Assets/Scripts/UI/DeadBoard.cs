using System;
using CenterSystem;
using Character;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class DeadBoard : MonoBehaviour,IDisplayment,IDashBoard
	{

		public Text Conditions;
		public Text CoinCollection;

		public Text MonsterKillingNum;

		private int _coins;

		private int _killingNum;
		// Use this for initialization

		void OnEnable()
		{
			GameObject.Find("CenterProcess").GetComponent<CenterProcess>().DeadBoard = this.gameObject;
		}
		void Start () {
			gameObject.SetActive(false);
		}
	
		// Update is called once per frame
		void Update () {

		}

		public void Display()
		{
			CoinCollection.text = "收集到" + _coins + "个玉符";
			MonsterKillingNum.text = "击退了" + _killingNum + "只妖异";
		}

		public void GameOver(Condition condition)
		{
			switch (condition)
			{
					case Condition.CastleDestory: Conditions.text = "部落被摧毁";
						break;
					case Condition.PlayerDead: Conditions.text = "你牺牲了";
						break;
					case Condition.MonsterClear: Conditions.text = "总算是抵挡住了妖异的进攻";
						break;
			}
			
			Display();
			//GameObject.Find("CenterProcess").GetComponent<CenterProcess>().UpdateAccount();
			
			
			//Time.timeScale = 0;
		}

		public void UpdateData(CenterProcess centerProcess)
		{
			_coins = centerProcess.IntervalCoins;
			_killingNum = centerProcess.KillingNum;
			Display();
		}
	}
}
