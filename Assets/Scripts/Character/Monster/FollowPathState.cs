using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character.Monster
{
    public class FollowPathState : FSMState
    {
        private List<Vector2> _path;
        private float         _time = 0f;

        public FollowPathState()
        {
            StateId = StateId.FollowingPath;
        }

        public override void DoBeforeEntering()
        {
        }

        public override void DoBeforeLeaving()
        {
        }

        //要切换状态的条件  
        public override void Reason(GameObject player, GameObject target, GameObject npc)
        {
            if (Vector2.Distance(npc.transform.position, player.transform.position) < 10f)
            {
                npc.GetComponent<NPCController>().SetTransition(Transition.SawPlayer);
            }
            if (Vector2.Distance(npc.transform.position, target.transform.position) < 1.5f)
            {
                npc.GetComponent<NPCController>().SetTransition(Transition.SawFortress);
            }
        }

        //切换状态后的行动  
        public override void Act(GameObject player, GameObject target, GameObject npc, Animator anime)
        {
            anime.SetBool("attack", false);
            Vector2 rdDirection = new Vector2(Random.value, Random.value).normalized;
            while(_time > 2f)
            {
                if (_path != null)
                {
                    _path.RemoveAt(0);
                }
                _path = NavMesh2D.GetSmoothedPath(npc.transform.position, target.transform.position);
                _time = 0f;
            }
            if (_path != null && _path.Count != 0)
            {
                npc.transform.position = Vector2.MoveTowards(npc.transform.position, _path[0], 1.5f * Time.deltaTime);
                //npc.GetComponent<Rigidbody2D>().velocity = new Vector2(npc.transform.position.x - _path[0].x, npc.transform.position.y - _path[0].y).normalized;
                if (Vector2.Distance(npc.transform.position, _path[0]) < 0.8f)
                {
                    _path.RemoveAt(0);
                }
            }
            _time += Time.fixedDeltaTime;

        }

    }
}
