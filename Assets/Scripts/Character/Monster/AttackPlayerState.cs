using UnityEngine;
using Weapons.Gun;
using Weapons.Gun.MonsterUse;
using Weapons.Gun.PlayerUse;

namespace Character.Monster
{
    public class AttackPlayerState : FSMState
    {
        public float Speed = 5f;
        private Gun _gun = new BiWeapon();

        public AttackPlayerState()
        {
            StateId = StateId.AttackingPlayer;
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
            if (Vector2.Distance(npc.transform.position, player.transform.position) > 5f)
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
            Debug.Log(_gun.Interval);
            anime.SetBool("attack", true);
            npc.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);

            Vector3 direction = (player.transform.position - npc.transform.position).normalized;
            _gun.Shoot("Monster", npc.transform.position + direction, direction);
            if (_gun != null)
            {
                if (_gun.Interval > 0) { _gun.Interval -= 0.02; }
            }
        }

    }
}
