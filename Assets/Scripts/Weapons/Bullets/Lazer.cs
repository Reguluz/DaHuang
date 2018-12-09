using Character;
using Character.Player;
using UnityEditor;
using UnityEngine;

namespace Weapons.Bullets
{
    public class Lazer:MonoBehaviour,IPenetrate
    {
        private LazerState LazerState;
        private Vector3 _endposition;
        public float DeMaxDamage=20;
        private GameObject player;
        
        // 激光朝向
        private Vector3 _direction;
        // 射线方向
        private Vector3 rayDirection;
        // 射线碰撞体
        RaycastHit2D hitsStorage = new RaycastHit2D();
        Vector3 rayOriginPoint;
        public LayerMask PlatformMask = 10;
        // 距离
        
        
        private float distance = 20;
        private Transform theray;


        private void Start()
        {
            LazerState = this.gameObject.GetComponent<LazerState>();
            theray = this.gameObject.GetComponentInChildren<Transform>();
        }

        void OnEnable () {
            player = GameObject.FindGameObjectWithTag("Player");
            this.transform.parent = player.transform;
            
            
            rayOriginPoint = transform.position;
            _direction = transform.forward;
            rayDirection = _direction;
        }
	
        // Update is called once per frame
        void Update () {
            
            hitsStorage = Physics2D.Raycast(rayOriginPoint, rayDirection, 30f, PlatformMask);
            // 如果射线长度变化才赋值
            if (hitsStorage && distance != hitsStorage.distance)
            {
                distance = hitsStorage.distance;
                theray.localScale = new Vector3(1, distance, 1);
            }

            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
              /*  Destroy(this.gameObject);*/
            }
        }
        void FixedUpdate()
        {
            
        }
	
	
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == 10)
            {
                State state = other.GetComponent<State>();
                if (other.CompareTag("Player")||other.CompareTag("Monster"))
                {                       
                    state.Hurt(LazerState.Damage, LazerState.Shootername, LazerState.Shooter.Gunname.ToString(),LazerState.Shooter.Gunbuff);   
                    Penetrate();
                }           
                
                               
            }
        }

        public void Penetrate()
        {
            LazerState.Damage -= DeMaxDamage;
        }
    }
}