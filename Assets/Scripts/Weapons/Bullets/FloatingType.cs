using CenterSystem;
using Character;
using UnityEngine;

namespace Weapons.Bullets
{
	[RequireComponent(typeof(Bullet))]
	public class FloatingType : MonoBehaviour
	{
		protected Bullet Bullet;
		protected AudioSource AudioSource;
		protected Animation Animation;
		public AudioClip IceHit;
		public AudioClip ArrowHit;
		public AudioClip BallHit;
		public AudioClip PipeHit;

		// Use this for initialization
		void Start ()
		{
			
		}
	
		// Update is called once per frame
		void Update () {
		
		}
		void FixedUpdate()
		{
            

		}

		protected void HitSoundPlay(Buff buff = Buff.None)
		{
            
			
			if (buff.Equals(Buff.Freeze)||buff.Equals(Buff.Frozen))
			{
				AudioSource.PlayOneShot(IceHit);  
			}
			else
			{
				if (gameObject.GetComponent<SpriteRenderer>().sprite.name == "Arrow")
				{
					AudioSource.PlayOneShot(ArrowHit);
				}else if (gameObject.GetComponent<SpriteRenderer>().sprite.name == "Magicball")
				{
					AudioSource.PlayOneShot(BallHit);  

				}else if (gameObject.GetComponent<SpriteRenderer>().sprite.name == "Pipe")
				{      
					AudioSource.PlayOneShot(PipeHit);
					
				}
			}
			
			
			
		}
		
	}
}
