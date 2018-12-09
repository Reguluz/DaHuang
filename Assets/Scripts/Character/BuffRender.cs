using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class BuffRender : MonoBehaviour
    {

        private SpriteRenderer _render;
        private float calculator;
        private int _alpha;
            // Use this for initialization
        void Start ()
        {
            _render = this.GetComponent<SpriteRenderer>();
        }
	
        // Update is called once per frame
        void Update ()
        {
            calculator += 0.02f;
            if (!_render.color.Equals(Color.white))
            {
                
                var renderColor = _render.color;
                renderColor.a = (Mathf.Cos(calculator)+1)/2*255-100;
               // Debug.Log(renderColor.a);
            }

            if (calculator > 2)
            {
                calculator = 0;
            }
        }
    }


}
