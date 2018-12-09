using System.Collections.Generic;
using CenterSystem;
using UnityEngine;

namespace Weapons.Bullets
{
	public class InfoDeliver : MonoBehaviour
	{
	
		private Bullet    _parentBullet;
		public  GameObject ChildType;

		public List<GameObject> Child;

		// Use this for initialization
		private void Start()
		{
			_parentBullet = this.gameObject.GetComponent<Bullet>();
		}

		void OnEnable ()
		{			
			for (int i=0;i <Child.Count;i++)
			{

				GameObject go   = ObjectPool.current.GetObject(ChildType);
				go.transform.parent = this.gameObject.transform;
				if (go)
				{
					Debug.Log(go.GetComponent<Bullet>().Shooter);
					go.GetComponent<Bullet>().Shootout(_parentBullet.Shootername,_parentBullet.Shooter,Vector3.zero);
					/*go.GetComponent<Bullet>().SetChild(Child[i].transform.position);*/
				}
			}
		}
	
		// Update is called once per frame
		void Update () {
		
		}
	}
}
