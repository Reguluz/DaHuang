using CenterSystem;
using UnityEngine;

namespace Character
{
    public class ShootDirection : MonoBehaviour
    {

        
        void Start () {

        }

        void Update()
        {
            /*Vector3 mouse = Input.mousePosition;
            Vector3 obj = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 direction = mouse - obj;*/
            
                
            
            

        }

        public void TurnTo()
        {
            Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, pos.z);
            Vector3 direction = Camera.main.ScreenToWorldPoint(mousePos) - this.transform.position;


            direction.z = 0f;
            direction = direction.normalized;
            transform.up = PublicFunction.RotationMatrix(direction, -90f);
        }


    }


}
