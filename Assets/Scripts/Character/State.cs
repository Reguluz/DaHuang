using System;
using System.Collections;
using CenterSystem;
using Character.BuffState;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Character
{
    [RequireComponent(typeof(AudioSource))]
    public class State : MonoBehaviour
    {
        public String Name;
        public float Health;
        public float Maxhealth;
        public float Armor;
        public int CoinValue;
        private Animator _anime;
        private bool _isdead = false;

        public float Speedscale = 1;
        
        private IBuffState _internalBuffState;
        private IBuffState _normalBuffState;
        private IBuffState _fireBuffState;
        private IBuffState _freezeBuffState;
        private IBuffState _frozenBuffState;
        private IBuffState _avatarBuffState;
        
        public  GameObject     BuffSprite;
        private SpriteRenderer _buffRender;
        public Boolean IsAvatar = false;
        public AudioSource BuffFx;
        
        private readonly Object _locker = new Object();
        private CenterProcess CenterProcess;
        private Rigidbody2D _rb;
        
        void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            CenterProcess = GameObject.Find("CenterProcess").GetComponent<CenterProcess>();
            _anime = GetComponent<Animator>();                                //获取动画
            _buffRender = BuffSprite.GetComponent<SpriteRenderer>();
            if (gameObject.CompareTag("Monster"))                            //如果单位的Tag为Monster，则向CenterProcess执行添加怪物数量操作（用于当前怪物数量统计面板）
            {
                CenterProcess._monsterCol++;
            }
            _internalBuffState = new NormalBuffState(this);                  //初始化Buff状态
            if (IsAvatar)                                                    //如果当前无敌开关开启则直接转向无敌Buff状态
            {
                _internalBuffState = new AvatarBuffState(this);
            }

            StartCoroutine(BuffDamage());                                    //初始化计算Buff伤害的协程
        }

        private void Update()
        {
           
        }


        public void Hurt(float damage, String shootername, String guntype,Buff attackbuff = Buff.Normal)    //收到伤害，参数依次为：收到伤害数值，伤害来源（单位），伤害来源（武器），伤害来源的Buff（默认是普通）
        {
            lock (_locker)                                                  //本函数上进程锁，防止同时命中造成伤害计算混乱，强制依次计算
            {
                if (_isdead == false)                                        //如果是死亡状态则不进行伤害计算
                {
                    DamageCalculate(damage);                                 //计算伤害            
                    if (Health <= 0)                                        //血量小于0则进入死亡函数
                    {
                        Die(shootername, guntype);
                        
                    }

                    BuffCheck(attackbuff);                                    //识别伤害来源的Buff，并更新全面板（受伤更新）
                    CenterProcess.NotifyDashBoard();
                    /*if (isAvatar == false && attackbuff!=Buff.Normal)
                    {
                        _internalBuff = attackbuff;
                        BuffCheck();
                        CancelInvoke("InitBuff");
                        Invoke("InitBuff",time);
                    }*/
                }               
            }
            
        }

        private void Die(String shootername, String guntype)                            //传入伤害来源（单位）和伤害来源（武器种类）  （用于击杀面板显示）
        {
            if (_isdead == false)
            {
                //gameObject.GetComponent<Rigidbody2D>().enable = false;
                _isdead = true;
                //anime.SetBool("isDead",true);
                _rb.mass = 9999;
                CenterProcess.AddIntervalCoin(CoinValue);                        //如果本单位被击杀则给与对应的货币    
                CenterProcess.OneShotKilling(shootername, guntype, Name);        //并更新一条击杀信息（给击杀面板）

                if (gameObject.CompareTag("Monster"))                                                //判定本单位的Tag为Monster时减少当前场景一个怪物计数
                {
                    CenterProcess.RemoveMonster();
                    _anime.SetBool("isDead",true);
                    
                    Invoke("Destoryself",0.3f); 
                }else if (gameObject.CompareTag("Item")){                                            //判定本单位的Tag为Item时播放道具破坏的的音效
                    PlayerAudioCollection.GunCollection.Play("ItemDestory");
                    Invoke("Destoryself",1f);  
                }else if (gameObject.CompareTag("Castle") || gameObject.CompareTag("Player"))        //判定本单位的Tag为Castle或者Player时执行GameOver相关函数
                {
                    
                    CenterProcess.CheckGameOver();
                    CancelInvoke("Destoryself");
                }
                            
            }      
        }

        private void DamageCalculate(float damage)            //伤害计算方法：有护甲时收到75%的伤害来源的值，护甲减少50%的伤害来源的值
        { 
            if (Armor>0)
            {
                Health -= damage * 0.75f;
                Armor  -= damage * 0.5f;
                if (Armor <= 0)
                {
                    Armor   = 0;
                }
            }
            else
            {
                Health -= damage;
            }
           
        }

       /* public void InitBuff()
        {
            _internalBuff = Buff.Normal;            
            _BuffRender.color = Color.white;
            Speedscale = 1;
            CancelInvoke("BuffDamage");
            BuffCheck();
        }
        
        private void BuffCheck()
        {
            Debug.Log("收到" + _internalBuff + "Buff");
            switch (_internalBuff)
            {
                    case Buff.Fire:
                        _BuffRender.color = Color.red;
                        Speedscale = 1;
                        InvokeRepeating("BuffDamage",1,1);
                        break;
                    case Buff.Freeze: 
                        _BuffRender.color = Color.cyan;
                        InvokeRepeating("BuffDamage",1,1);
                        Speedscale = 0.5f;
                        break;
                    case Buff.Frozen: 
                        _BuffRender.color = Color.blue;
                        Speedscale = 0;
                        break;
                    default:
                        _BuffRender.color = Color.white;
                        Speedscale = 1;
                        break;
            }
        }*/
        

        IEnumerator  BuffDamage()            //伤害计算协程，在人物处于Buff.Fire或者Buff.Freeze状态时每秒收到5点伤害
        {
            while (true)
            {
                if (_internalBuffState.GetState().Equals(Buff.Fire)||_internalBuffState.GetState().Equals(Buff.Freeze))
                {
                    this.Hurt(5," ",_internalBuffState.ToString(),Buff.None);
                }   
                yield return new WaitForSeconds(1f);
            }
            
            
        }

        private void BuffCheck(Buff buff)    //检索伤害来源的Buff类别并更新Buff状态
        {
            
            switch (buff)
            {
                    case Buff.Normal:
                        _internalBuffState.GetNormalBuff();
                        break;
                    case Buff.Fire:
                        _internalBuffState.GetFireBuff();
                        break;
                    case Buff.Freeze:_internalBuffState.GetFreezeBuff();break;
                    case Buff.Frozen:_internalBuffState.GetFrozenBuff();break;
                    case Buff.Avatar:_internalBuffState.GetAvatarBuff();
                        break;
                    case Buff.None:_internalBuffState.GetNoneBuff();
                    break;
                    default:break;
                    
            }
        }
        public void InitBuff()        //初始化Buff状态
        {
            this.BuffCheck(Buff.Normal);     
        }
        public void SetBuff(IBuffState state)        //设置Buff状态
        {
            this._internalBuffState = state;
        }
        
        public void Destoryself()            //单位自我摧毁函数（预留摧毁前执行的代码片段）
        {
            Destroy(this.gameObject);
        }
        

        public String GetName()            //获取单位名字
        {
            return Name;
        }

        public bool Isdead        
        {
            get { return _isdead; }
        }
        
    }
}
