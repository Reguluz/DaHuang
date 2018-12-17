using CenterSystem;
using Character;
using Character.Player;
using UnityEngine;

namespace Weapons.Bullets
{
    public class Track:FloatingType
    {
        private float _extraDamage = 0;
        

        void OnEnable ()
        {
            TrackBullet = this.gameObject.GetComponent<TrackBullet>();
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
                   
                    if (other.CompareTag("Monster"))
                    {
                        Hit(TrackBullet.Shooter.Gunbuff);
                        _extraDamage = UpgradeTree.PlayerArchive.ExtraAttackLevel * SystemOption.ExPlayerAttackPerL;

                        //other.gameObject.GetComponent<Rigidbody2D>().AddForce(Bullet.Endposition.normalized*5f,ForceMode2D.Impulse); 
                        state.Hurt(TrackBullet.Shooter.Damage + _extraDamage, TrackBullet.Shootername, TrackBullet.Shooter.Gunname,TrackBullet.Shooter.Gunbuff);
                        Debug.Log("TrackHurt");
                        TrackBullet.Destoryself();
                            
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