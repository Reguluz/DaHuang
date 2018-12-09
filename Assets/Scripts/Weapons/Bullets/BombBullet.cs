using CenterSystem;
using Character;
using Character.Player;
using Weapons.Bullets;
using UnityEngine;
using Weapons.Gun.MonsterUse;

namespace Weapons.Bullets
{
    public class BombBullet:FloatingType,IBoom
    {

        private float _bombRadius = 10;
        private float _bombForce = 100f;

        private float _realRadius;
        private float _extraDamage=0;
        public AudioClip ExplodeClip;
        void OnEnable () {
            Bullet = gameObject.GetComponent<Bullet>();
            AudioSource = gameObject.GetComponent<AudioSource>();
            Animation = gameObject.GetComponent<Animation>();
            if (Bullet.Shooter!=null)
            {
                if (Bullet.Shooter.GetType().BaseType == typeof(MonsterGun))
                {
                    Debug.Log(Bullet.Shooter.GetType().BaseType);
                    Boom();
                }
            }
            
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
                Boom();
                Debug.Log("DestoryBomb");
                
                
            }
        }
        public void Boom()
        {
            Debug.Log("BombBoom");
            _realRadius = Bullet.IsPlayer ?((_bombRadius + UpgradeTree.PlayerArchive.ExtraBulletSpecialLevel * SystemOption.ExBombRadiusPerL) * 0.1f * SystemOption.SceneScale )
                : (_bombRadius * 0.1f * SystemOption.SceneScale);           
            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, _realRadius, 1 << LayerMask.NameToLayer("Hit"));
            foreach(Collider2D en in enemies)
            {
                if (!en.CompareTag("Terrain"))
                {
                    _extraDamage = Bullet.IsPlayer
                        ? UpgradeTree.PlayerArchive.ExtraAttackLevel * SystemOption.ExPlayerAttackPerL
                        : 0;
                        en.gameObject.GetComponent<State>().Hurt(Bullet.Shooter.Damage + _extraDamage, Bullet.Shootername, Bullet.Shooter.Gunname.ToString(),Bullet.Shooter.Gunbuff);  

                    
                    
                    Rigidbody2D rb = en.GetComponent<Rigidbody2D>();
                    if(rb != null)
                    {
                        // Find a vector from the bomb to the enemy.
                        Vector3 deltaPos = rb.transform.position - transform.position;
                    
                        // Apply a force in this direction with a magnitude of bombForce.
                    
                        Vector3 force = deltaPos.normalized * _bombForce*100;
                        rb.AddForce(force);
                    }
                } 
                Hit();
                Bullet.Destoryself();
            }
            
        }

        private void Hit()
        {
            AudioSource.PlayOneShot(ExplodeClip);
            
        }
        public float BombRadius
        {
            get { return _bombRadius; }
            set { _bombRadius = value; }
        }

        public float BombForce
        {
            get { return _bombForce; }
            set { _bombForce = value; }
        }
    }
}