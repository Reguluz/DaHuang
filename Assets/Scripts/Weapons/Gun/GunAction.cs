using System;
using System.Collections;
using System.Collections.Generic;
using CenterSystem;
using Character.Player;
using UnityEngine;
using Weapons.Gun.PlayerUse;

namespace Weapons.Gun
{
	
	public class GunAction : MonoBehaviour
	{
		public Gunname Chosen;						//用作自动生成脚本时，提供一个枚举类来随机生成的武器
		private String _createname;
        private String _currentGun;
		private Gun _gun;
		private Sprite _sp;

		// Use this for initialization
		private void Awake()
		{
			Init();
		}


		public void Init()
		{
			if (_createname == null)									//用作自动生成脚本时此项为空，则使用Chosen的选择，用作枪支丢弃时，此项已经有值，则忽略Chosen的选择
			{
				_createname = Chosen.ToString();
			}
			_sp = Resources.Load<Sprite>("GunSprite/" + _createname);	//将本单位设置成所选武器对应的贴图
			this.GetComponent<SpriteRenderer>().sprite = _sp;
			CheckGunType();												//核对武器类型并生成
			
		}

		

		// Update is called once per frame
		void Update()
		{

		}

		private void OnTriggerEnter2D(Collider2D other)				
		{

			if (other.CompareTag("Player"))						//判定碰撞体Tag为Player时执行相关函数
			{
				PlayerController controller = other.GetComponent<PlayerController>();		//获取Player的控制器
				/*if (controller.Gun == null)													//当玩家此时没有手持武器时
				{*/
					Debug.Log("SetGun "+_createname);
					controller.GunShow.GetComponent<SpriteRenderer>().sprite = 
						Resources.Load<Sprite>("GunSprite/" + _createname);		//设置武器贴图为本武器
                    controller.ChangeSprite();				//设置人物贴图为持有武器贴图
                    controller.GunShow.GetComponent<Transform>().transform.localScale = 
	                    new Vector3(SystemOption.GunShowScale,
		                    SystemOption.GunShowScale,
		                    SystemOption.GunShowScale); //设置武器贴图比例

                    controller.SetGun(CheckGunType());//设置当前Player所持武器类为该武器
					Destroy(this.gameObject);	//销毁地上武器
				/*}else if (controller.Gun.Equals(CheckGunType()))				//当玩家此时持有武器名与该武器相同（同种武器）
				{
					if (controller.Gun.IntervalTime + CheckGunType().IntervalTime > controller.Gun.MaxTime)		//如果本武器子弹与手持子弹叠加大于子弹上限
					{
						controller.Gun.IntervalTime = controller.Gun.MaxTime;		//则设置子弹数量为子弹上限
					}
					else
					{
						controller.Gun.IntervalTime += CheckGunType().IntervalTime;		//否则直接叠加子弹数量
					}
					Destroy(this.gameObject);		//并销毁
				}*/

				PlayerAudioCollection.GunCollection.Play("Pickup");//这里播放获取特效

			}
		}
		
		private Gun CheckGunType()
		{
            if (_createname == "FireBow")				//枪支丢弃时不能通过Gunname枚举获取信息，只能通过Gun.gunname这个字符串来获取信息，所以这里直接用String类来判断
            {
                return new FireBow();
                _currentGun = "PlayerGun";
            }
            else if (_createname == "CrossBow")
            {
	            return new CrossBow();
                _currentGun = "PlayerGun";
            }
            else if (_createname == "NatureWand")
            {
	            return new NatureWand();
	            _currentGun = "PlayerShotgun";
            }
            else if (_createname == "ExplodeFireWand")
            {
	            return new ExplodeFireWand();
	            _currentGun = "RPG";
            }else if (_createname == "FreezeBow")
            {
	            return new FreezeBow();
                _currentGun = "PlayerGun";
            }else if (_createname == "NatureBow")
            {
	            return new NatureBow();
	            _currentGun = "PlayerGun";
            }else if (_createname == "Needle")
            {
	            return new Needle();
	            _currentGun = "PlayerGun";
            }else if (_createname == "FreezeStar")
            {
	            return new FreezeStar();
	            _currentGun = "PlayerGun";
            }else if (_createname == "TrackGun")
            {
	            return new TrackGun();
	            _currentGun = "PlayerGun";
            }

			return null;
		}

		public string StringGuname
		{
			get { return _createname; }
			set { _createname = value; }
		}

		public Gun Gun
		{
			get { return _gun; }
			set { _gun = value; }
		}
	}
}