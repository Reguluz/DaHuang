using UnityEngine;
using Weapons.Gun;
using Weapons.Gun.MonsterUse;

namespace Character.Monster
{
    public class ChassPlayerState : FSMState
    {
        public ChassPlayerState()
        {
            StateId = StateId.ChasingPlayer;
        }

        public override void DoBeforeEntering()
        {

        }

        public override void DoBeforeLeaving()
        {

        }

        //要切换状态的原因，条件  
        public override void Reason(GameObject player, GameObject target, GameObject npc)
        {
            if (Vector2.Distance(npc.transform.position, player.transform.position) > 10f)
            {
                npc.GetComponent<NPCController>().SetTransition(Transition.LostPlayer);
            }
            if (Vector2.Distance(npc.transform.position, player.transform.position) < 5f)
            {
                npc.GetComponent<NPCController>().SetTransition(Transition.AttackPlayer);
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
            npc.GetComponent<Rigidbody2D>().velocity = (player.transform.position - npc.transform.position).normalized * 1.2f;
        }
    
    }
}
