
using System;
using CenterSystem;
using Item;
using Item.AttackProps;
using UnityEngine;

namespace Character.Player
{
	public class PlayerBag:MonoBehaviour
	{
		
		public GameObject BoxObj;
		public GameObject MineObj;
		public GameObject BandDeviceObj;
		public GameObject OilTankObj;

		public AttackProps[] list;
		public int[] itemCount;

		private int now=0;
		

		public void init()
		{
			itemCount = new int[list.Length];
		}


		public void Use(String name,Vector3 characterpos,Vector3 muzzleOrientation)
		{

			if (itemCount[now] > 0)
			{
				itemCount[now]--;
				switch (list[now])
				{
					case AttackProps.Box: PutDown(BoxObj,name,characterpos,muzzleOrientation);
						break;
					case AttackProps.Mine:PutDownMine(name,characterpos,muzzleOrientation);
						break;
					case AttackProps.BandDevice:PutDown(BandDeviceObj,name,characterpos,muzzleOrientation);
						break;
					case AttackProps.Oiltank:PutDown(OilTankObj,name,characterpos,muzzleOrientation);
						break;
					default: ;break;
				}
			}
			
		}

		public void GetItem(AttackProps chosen)
		{
			for (int i = 0; i < list.Length; i++)
			{
				if (list[i].Equals(chosen))
				{
					itemCount[i]++;
				}
			}
		}

		private void PutDownMine(String name,Vector3 characterpos,Vector3 muzzleOrientation)
		{
			Debug.Log("放置地雷");
			GameObject mine = Instantiate(MineObj, characterpos+muzzleOrientation.normalized*SystemOption.SceneScale * 2, Quaternion.identity);
			mine.GetComponent<Mine>().Shootername = name;
		}
		private void ThrowGrenade(String name,Vector3 characterpos,Vector3 muzzleOrientation)
		{
			Debug.Log("扔手雷");
			
		}

		private void PutDown(GameObject obj,String name,Vector3 characterpos,Vector3 muzzleOrientation)
		{
			Debug.Log("放置场景物体:" + obj.ToString());
			Instantiate(obj, characterpos+muzzleOrientation.normalized*SystemOption.SceneScale * 2, Quaternion.identity);	
		}

		
		
		

		public int[] ItemCount
		{
			get { return itemCount; }
			set { itemCount = value; }
		}

		public int Now
		{
			get { return now; }
			set { now = value; }
		}

		public AttackProps[] List
		{
			get { return list; }
		}
	}
}
