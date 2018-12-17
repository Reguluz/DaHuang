using CenterSystem;
using Character;
using Character.Player;
using UnityEngine;

namespace Weapons.Bullets
{
    public class ClassicalBullet:FloatingType
    {
        private float _extraDamage = 0;
        

        void OnEnable ()
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
                if (!other.CompareTag("Terrain"))
                {
                    if (Bullet.IsPlayer)
                    {
                        if (other.CompareTag("Monster"))
                        {
                            Hit(Bullet.Shooter.Gunbuff);
                            _extraDamage = Bullet.IsPlayer
                                ? UpgradeTree.PlayerArchive.ExtraAttackLevel * SystemOption.ExPlayerAttackPerL
                                : 0;

                            //other.gameObject.GetComponent<Rigidbody2D>().AddForce(Bullet.Endposition.normalized*5f,ForceMode2D.Impulse); 
                            state.Hurt(Bullet.Shooter.Damage + _extraDamage, Bullet.Shootername, Bullet.Shooter.Gunname,Bullet.Shooter.Gunbuff);
                            Bullet.Destoryself();
                        }
                    }
                    else
                    {
                        if (other.CompareTag("Player"))
                        {
                            Hit(Bullet.Shooter.Gunbuff);
                            _extraDamage = Bullet.IsPlayer
                                ? UpgradeTree.PlayerArchive.ExtraAttackLevel * SystemOption.ExPlayerAttackPerL
                                : 0;

                            //other.gameObject.GetComponent<Rigidbody2D>().AddForce(Bullet.Endposition.normalized*5f,ForceMode2D.Impulse); 
                            state.Hurt(Bullet.Shooter.Damage + _extraDamage, Bullet.Shootername, Bullet.Shooter.Gunname,Bullet.Shooter.Gunbuff);
                            Bullet.Destoryself();
                        }
                    }
                }
            }
        }

        private void Hit(Buff buff = Buff.None)
        {
            HitSoundPlay(buff);
        }
        
        
        

    }
}