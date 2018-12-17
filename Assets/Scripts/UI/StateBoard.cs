	using System.Collections.Generic;
using CenterSystem;
using Character;
using Character.Player;
using UnityEngine;
using UnityEngine.UI;
using Weapons.Gun;

namespace UI
{
	public class StateBoard : MonoBehaviour,IDashBoard,IDisplayment
	{
		public SimpleHealthBar Hpbar;
		public SimpleHealthBar Bulletbar;
		
	
		private State _state;
		private Gun _gun;
		private Text _show;


		public Color _colorHpHigh = new Color(1,1,1);
		public Color _colorHpMid = new Color(0.75f,0.5f,0.5f);
		public Color _colorHpLow = new Color(0.5f,0,0);
		// Use this for initialization
		void OnEnable ()
		{
			_show = this.gameObject.GetComponent<Text>();	
			GameObject.Find("CenterProcess").GetComponent<CenterProcess>().RegisterDashBoard(this);
			//UpdateData(CenterProcess.ModeCenterProcess);
			/*_state = null;
			_gun = null;*/
		
		}
	
		// Update is called once per frame
		void Update ()
		{
			//Display();
				
		}

		public void UpdateData(CenterProcess centerProcess)
		{
			_state = centerProcess.PlayerState;
			_gun = centerProcess.Gun;
			Display();
		}

		public void Display()
		{
			SetHpBar();
			SetOtherState();
		}

		private void SetOtherState()
		{
			if(_gun!=null)
			{
				_show.text = _gun.Gunname + "\n" + "\n"+ "\n"+ "\n"
				             +"Playername:" + _state.name + "\n" + "\n"+ "\n"
				             + "\n";
				Bulletbar.UpdateBar( _gun.IntervalTime, _gun.MaxTime);				
			}
			else
			{
				_show.text =  "No Gun In Hand" + "\n" + "\n"+ "\n"+ "\n"
				              +"Playername:" + _state.name + "\n" + "\n"+ "\n"
				              + "\n";
				Bulletbar.UpdateBar(0,0);
			}
		}

		private void SetHpBar()
		{
			if (_state != null)
			{
				Hpbar.UpdateBar( _state.Health, _state.Maxhealth );
				float per = _state.Health / _state.Maxhealth;
				if (per > 0.5)
				{
					Hpbar.UpdateColor(Color.Lerp(_colorHpMid, _colorHpHigh, (float) (per -0.5) *2));
				}
				else
				{
					Hpbar.UpdateColor(Color.Lerp(_colorHpLow, _colorHpMid, per *2));
				}
			}
			
			
		}
	}
}
