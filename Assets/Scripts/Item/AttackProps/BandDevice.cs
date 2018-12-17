using CenterSystem;
using UnityEngine;

namespace Item.AttackProps
{
    public class BandDevice : AttackProp
    {
        public BandDevice()
        {
            AttackPropType = AttackProps.BandDevice;
        }
        
        public float Radius;
        public float Force;
        public float InternalTime;


        // Use this for initialization
        void Start()
        {
            Invoke("Destoryself",InternalTime);
        }

        // Update is called once per frame
        void Update()
        {
            Attract();
        }


   

        public void Attract()
        {
            Collider2D[] enemies =
                Physics2D.OverlapCircleAll(transform.position, Radius * SystemOption.SceneScale, 1 << LayerMask.NameToLayer("Hit"));

            foreach (Collider2D en in enemies)
            {
                // Check if it has a rigidbody (since there is only one per enemy, on the parent).
                Rigidbody2D rb = en.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector3 deltaPos = rb.transform.position - transform.position;
                
                    Debug.DrawLine(rb.transform.position,transform.position,Color.blue,5);
                    //炸远功能
                    Vector3 force = deltaPos.normalized * Force * 20;
                    rb.AddForce(-force);               
                }
            }
        }

        private void Destoryself()
        {
            Destroy(this.gameObject);
        }

    }
}


