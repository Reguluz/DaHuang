using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CenterSystem;
using Character;
using Character.Player;
using UnityEngine;
using UnityEngine.UI;
using Weapons.Gun;

namespace UI
{
	public class KillerBoard : MonoBehaviour,IDashBoard,IDisplayment
	{

		private Text[] _show = new Text[5];

		private int _temp = 0;
		private int _size = 0;

		private Queue<KillerInfo> _localist = new Queue<KillerInfo>();

		// Use this for initialization
		void Start ()
		{
			//_centerProcess = GameObject.Find("CenterProcess").GetComponent<CenterProcess>();
			GameObject.Find("CenterProcess").GetComponent<CenterProcess>().RegisterDashBoard(this);
			_localist = null;
			foreach (Transform child in gameObject.transform)
			{
				_show[_temp] = child.gameObject.GetComponent<Text>();
				_temp++;
			}
			
		}
	
		// Update is called once per frame
		void Update ()
		{
			
			//Display();			
		}

		public void UpdateData(CenterProcess centerProcess)
		{
			_localist = centerProcess.Killerlist;
			Display();
		}

		public void Display()
		{
			if (_localist == null)
			{
				_size = 0;
			}else{_size = _localist.Count;}
			
			for (int i = 0; i < 5; i++)
			{
				if (i < _size && _size!=0)
				{
					if (_localist != null)
					{
						while (_localist.ElementAt(i).Killer!="Player")
						{
							i++;
						}

						KillerInfo info = _localist.ElementAt(i);


						_show[i].text = "<color=#ff0000>" + "你用" + "</color>"+
						                "<color=#00FFff>" + info.Guntype + "</color>" + 
						                "<color=#ffffff> 击杀了 </color>" +
						                "<color=#00ff00>" + info.Victim + "</color>\n";;
					}
				}
				else
				{
					_show[i].text = " ";
				}
			}
		}
		
		
	}
}
