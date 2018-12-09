using CenterSystem;
using Character;
using Character.Player;
using UnityEngine;

namespace Weapons.Bullets
{
    public class PenetrateBullet:FloatingType,IPenetrate
    {
        private Vector3 _endposition;
        public float DeSpeed=5;
        public float DeMaxDamage=20;
        private float _extraDamage = 0;

        void Start () {
            
            
        }

        private void OnEnable()
        {
            Bullet = this.gameObject.GetComponent<Bullet>();
            AudioSource = gameObject.GetComponent<AudioSource>();  
            Animation = gameObject.GetComponent<Animation>();
        }

        // Update is called once per frame
        void Update () {
		
        }
        void FixedUpdate()
        {
            
        }
	
	
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == 10)
            {
                State state = other.GetComponent<State>();
                if (other.gameObject.layer == 10/*other.CompareTag("Player")||other.CompareTag("Monster")*/)
                {
                    
                    if (!other.CompareTag("Terrain"))
                    {
                        
                        _extraDamage = Bullet.IsPlayer
                            ? UpgradeTree.PlayerArchive.ExtraAttackLevel * SystemOption.ExPlayerAttackPerL
                            : 0;
                        state.Hurt(Bullet.Shooter.Damage + _extraDamage, Bullet.Shootername, Bullet.Shooter.Gunname.ToString(),Bullet.Shooter.Gunbuff);  
                        Penetrate();
                        Hit(Bullet.Shooter.Gunbuff);
                    }
                    else
                    {
                        Hit();
                        Bullet.Destoryself();
                    }
                }           
                
                               
            }
        }

        public void Penetrate()
        {
            Bullet.MoveSpeed -= DeSpeed - UpgradeTree.PlayerArchive.ExtraBulletSpecialLevel * SystemOption.ExAntiDeSpeedPerL;
            Bullet.Damage -= DeMaxDamage - DeSpeed + UpgradeTree.PlayerArchive.ExtraBulletSpecialLevel * SystemOption.ExAntiDeSpeedPerL;
        }

        private void Hit(Buff buff = Buff.None)
        {
            HitSoundPlay(buff);
            
            if (Bullet.Damage <= 0 || Bullet.MoveSpeed <= 10)
            {
                Bullet.Destoryself();
            }
        }
        
    }
}