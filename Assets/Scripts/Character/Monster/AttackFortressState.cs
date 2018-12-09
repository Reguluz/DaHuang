using UnityEngine;
using Weapons.Gun;
using Weapons.Gun.MonsterUse;

namespace Character.Monster
{
    public class AttackFortressState : FSMState
    {
        private Gun _gun = new BirdExample();

        public AttackFortressState()
        {
            StateId = StateId.AttackingFortress;
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

        }
        //切换状态后的行动  
        public override void Act(GameObject player, GameObject target, GameObject npc, Animator anime)
        {
            anime.SetBool("attack", true);
            npc.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
            Vector2 direction = (target.transform.position - npc.transform.position).normalized;
            _gun.Shoot("Monster", npc.transform.position, direction);
            if (_gun != null)
            {
                if (_gun.Interval > 0) { _gun.Interval -= 0.02; }
            }
        }
    }
}
