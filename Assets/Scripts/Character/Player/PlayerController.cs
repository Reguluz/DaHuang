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
        private static Vector3   _muzzleOrientation = Vector3.right;

        private State _state;
        private Animator _anime;

        public GameObject GunShow;
        private SpriteRenderer _gunsprite;
        private ShootDirection _shootDirection;

        private static float _playerFlipScale=1f;
        private static float _gunShowFlipScale=0.8f;
        private CenterProcess CenterProcess;

        private bool _tipswitch = true;

        // Use this for initialization
        void Awake ()
        {
            _anime = GetComponent<Animator>();

            CenterProcess = GameObject.Find("CenterProcess").GetComponent<CenterProcess>();
            _gunsprite = GunShow.GetComponent<SpriteRenderer>();
            _state = this.gameObject.GetComponent<State>();          
            
            _bag = this.gameObject.GetComponent<PlayerBag>();
            _shootDirection = GunShow.GetComponent<ShootDirection>();
            _bag.init();
            
            _state.Maxhealth += UpgradeTree.PlayerArchive.ExtraHpLevel * SystemOption.ExPlayerHpPerL;
            _state.Health += UpgradeTree.PlayerArchive.ExtraHpLevel * SystemOption.ExPlayerHpPerL;
            _state.Armor += UpgradeTree.PlayerArchive.ExtraArmorLevel * SystemOption.ExPlayerArmorPerL;

            this.gameObject.transform.localScale = new Vector3( -_playerFlipScale, _playerFlipScale, _playerFlipScale);
        }

        void OnEnable()
        {
            Debug.Log("SetState");
            Gun = new DefaultGun();
            CenterProcess.SetPlayerState(this.gameObject,Gun,_bag);
        }

        // Update is called once per frame
        void Update ()
        {
            switch (Gun.Gunname)
            {
                case "破旧的手枪":
                    _anime.SetInteger("gun", 0);
                    break;
                case "追踪导弹":
                    _anime.SetInteger("gun", 3);
                    break;

            }
            if (!_state.Isdead)
            {
                GetFunctionKeyDown(); 
            }         
            //CenterProcess.ModeCenterProcess.SetPlayerState(this.gameObject,Gun,_bag);
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.tag.Equals("Monster"))
            {
                _state.Hurt(99999, other.gameObject.name, " ");
            }
        }
        


        private void FixedUpdate()
        {
            if (Gun!=null)
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
            //Flip();
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
            CenterProcess.NotifyDashBoard();
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
            //_muzzleOrientation = Camera.main.ScreenToWorldPoint(mousePos) - GunShow.transform.position;
        }
        
        /*private void Flip()
        {            
                this.gameObject.transform.localScale = new Vector3(Math.Sign(_muzzleOrientation.x) * -_playerFlipScale,_playerFlipScale,_playerFlipScale);
                GunShow.transform.localScale  = new Vector3(Math.Sign(_muzzleOrientation.x) * -_gunShowFlipScale, Math.Sign(_muzzleOrientation.x)*_gunShowFlipScale, _gunShowFlipScale);            
        }*/

        

        void Shoot()
        {
            if (Gun!=null)
            {
                if (Gun.IntervalTime > 0)
                {
               
                    Gun.Shoot(_state.name, GunShow.transform.position+_muzzleOrientation.normalized*gameObject.GetComponent<SpriteRenderer>().bounds.size.x * SystemOption.SceneScale, _muzzleOrientation);
                    Debug.DrawLine(GunShow.transform.position,GunShow.transform.position+_muzzleOrientation.normalized*gameObject.GetComponent<SpriteRenderer>().bounds.size.x * SystemOption.SceneScale,Color.blue);
                    if (Gun.IntervalTime <= 5 && _tipswitch)
                    {
                        CenterProcess.SendMessage(Gun.Gunname+" 即将消失");
                        _tipswitch = false;
                    }
                }
                else if(Gun.IntervalTime <= 0)
                {
                    GunDestory();
                    _tipswitch = true;
                }
            }
            
            CenterProcess.NotifyDashBoard();
        }
        
        
        
        private void GunDestory()
        {
            GunShow.GetComponent<SpriteRenderer>().sprite = null;
            Gun = new DefaultGun();
            //清屏功能带效果
            GunBrokenBoom();
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

        public void SetGun(Gun newgun)
        {
            this.Gun = newgun;
            CenterProcess.SendMessage("获得新武器 " + Gun.Gunname);
        }
        
        private void GunBrokenBoom()
        {
            Debug.Log("BombBoom");
            int _realRadius = 50;
            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, _realRadius, 1 << LayerMask.NameToLayer("Hit"));
            foreach(Collider2D en in enemies)
            {
                if (en.CompareTag("Monster"))
                {
                    int _extraDamage = 9999;
                    en.gameObject.GetComponent<State>().Hurt(_extraDamage, name, Gun.Gunname);  
                    Rigidbody2D rb = en.GetComponent<Rigidbody2D>();
                    
                } 
            }
            
        }

    }


}
