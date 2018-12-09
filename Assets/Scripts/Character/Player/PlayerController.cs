using System;
using CenterSystem;
using UnityEngine;
using Weapons.Gun;
using Weapons.Gun.PlayerUse;
using Object = UnityEngine.Object;

namespace Character.Player
{
    [RequireComponent(typeof(State))]
    [RequireComponent(typeof(PlayerBag))]
    [RequireComponent(typeof(Gun))]
    public class PlayerController : MonoBehaviour
    {
        private PlayerBag _bag;
        public Gun        Gun;
        private double    _intervaltime;
        private Vector3   _shootposition;
        private Vector3   _muzzleOrientation;

        private State _state;


        public GameObject GunShow;
        private SpriteRenderer _gunsprite;
        private ShootDirection _shootDirection;

        private static float _playerFlipScale=1.2f;
        private static float _gunShowFlipScale=0.8f;

        // Use this for initialization
        void Awake ()
        {
            _gunsprite = GunShow.GetComponent<SpriteRenderer>();
            _state = this.gameObject.GetComponent<State>();          
            CenterProcess.ModeCenterProcess.SetPlayerState(this.gameObject,Gun,_bag);
            _bag = this.gameObject.GetComponent<PlayerBag>();
            _shootDirection = GunShow.GetComponent<ShootDirection>();
            _bag.init();

            _state.Maxhealth += UpgradeTree.PlayerArchive.ExtraHpLevel * SystemOption.ExPlayerHpPerL;
            _state.Health += UpgradeTree.PlayerArchive.ExtraHpLevel * SystemOption.ExPlayerHpPerL;
            _state.Armor += UpgradeTree.PlayerArchive.ExtraArmorLevel * SystemOption.ExPlayerArmorPerL;
            
        }

        // Update is called once per frame
        void Update ()
        {
            if (!_state.Isdead)
            {
                GetFunctionKeyDown(); 
            }         
            CenterProcess.ModeCenterProcess.SetPlayerState(this.gameObject,Gun,_bag);
        }

        


        private void FixedUpdate()
        {
            if (Gun.Gunname!=Gunname.Default.ToString())
            {
                if (Gun.Interval > 0)
                {
                    Gun.Interval -= Time.fixedDeltaTime;
                    Gun.IntervalTime -= Time.fixedDeltaTime;
                }            
            }
            Shoot();
        }
        

        
        
        private void GetFunctionKeyDown()
        {
            GetMousePosition();
            Flip();
            _shootDirection.TurnTo();
            /*if (Input.GetKeyDown(SystemOption.ThrowAway))
            {
                if (Gun != null)
                {
                    GunThrowAway();
                }
                
            }*/
            if (Input.GetKeyDown(SystemOption.PreviousItem))
            {                
                _bag.Now --; 
                BagEdgeCheck();
                Debug.Log("现在是第" + (_bag.Now + 1) + "个道具：" + _bag.list[_bag.Now]);    
            }else if(Input.GetKeyDown(SystemOption.NextItem)){
                _bag.Now ++; 
                BagEdgeCheck();
                Debug.Log("现在是第" + (_bag.Now + 1) + "个道具：" + _bag.list[_bag.Now]);
            }

            if (Input.GetKeyDown(SystemOption.PutDownItem))
            {
                _bag.Use(_state.name,this.transform.position,_muzzleOrientation);
            }
            CenterProcess.ModeCenterProcess.NotifyDashBoard();
        }

        private void BagEdgeCheck()
        {
            if (_bag.Now >= _bag.ItemCount.Length)
            {
                _bag.Now = 0;
            }
            else if(_bag.Now<0)
            {
                _bag.Now = _bag.ItemCount.Length - 1;
            }
        }
        
        private void GetMousePosition()
        {
            
            Vector3 pos  = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, pos.z);
            _muzzleOrientation = Camera.main.ScreenToWorldPoint(mousePos) - GunShow.transform.position;
        }
        
        private void Flip()
        {            
                this.gameObject.transform.localScale = new Vector3(Math.Sign(_muzzleOrientation.x) * -_playerFlipScale,_playerFlipScale,_playerFlipScale);
                GunShow.transform.localScale  = new Vector3(Math.Sign(_muzzleOrientation.x) * -_gunShowFlipScale, Math.Sign(_muzzleOrientation.x)*_gunShowFlipScale, _gunShowFlipScale);            
        }

        

        void Shoot()
        {
            if (Gun.Gunname != Gunname.Default.ToString())
            {
                if (Gun.IntervalTime > 0)
                {
               
                    Gun.Shoot(_state.name, GunShow.transform.position+_muzzleOrientation.normalized*gameObject.GetComponent<SpriteRenderer>().bounds.size.x * SystemOption.SceneScale, _muzzleOrientation);
                    Debug.DrawLine(GunShow.transform.position,GunShow.transform.position+_muzzleOrientation.normalized*gameObject.GetComponent<SpriteRenderer>().bounds.size.x * SystemOption.SceneScale,Color.blue);
                }
                else if(Gun.IntervalTime <= 0)
                {
                    GunDestory();
                }
            }
            
            CenterProcess.ModeCenterProcess.NotifyDashBoard();
        }
        
        
        
        private void GunDestory()
        {
            GunShow.GetComponent<SpriteRenderer>().sprite = null;
            Gun = new DefaultGun();
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("PlayerSprite/PlayerWithPunch");
        }
        /*
        private void GunThrowAway()
        {
            GameObject usedGun = Instantiate((GameObject) Resources.Load("Gun", typeof(GameObject)), this.transform.position+_muzzleOrientation.normalized, Quaternion.identity);
            usedGun.transform.localScale = new Vector3(SystemOption.NormalGunScale,SystemOption.NormalGunScale,SystemOption.NormalGunScale);
            GunAction action = usedGun.GetComponent<GunAction>();
            action.StringGuname = Gun.GetType().Name;          
            action.Init();
            action.Gun.SetTime();
            GunShow.GetComponent<SpriteRenderer>().sprite = null;
            Gun = null;
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("PlayerSprite/PlayerWithPunch");
        }*/

        public void ChangeSprite()
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("PlayerSprite/PlayerWithGun");
        }

    }


}
