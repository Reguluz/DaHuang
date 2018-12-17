using System.Collections;
using System.Collections.Generic;
using CenterSystem;
using Character;
using Character.Player;
using UnityEngine;
using Weapons.Gun;

namespace Character
{
    [RequireComponent(typeof(State))]
    public class Castle : MonoBehaviour {

        private State _state;

        private CenterProcess CenterProcess;
        // Use this for initialization
        void Awake ()
        {
            CenterProcess = GameObject.Find("CenterProcess").GetComponent<CenterProcess>();
            _state = this.gameObject.GetComponent<State>();
            _state.Maxhealth += UpgradeTree.PlayerArchive.ExtraCastleHpLevel * SystemOption.ExCastleHpPerL;
            _state.Health += UpgradeTree.PlayerArchive.ExtraCastleHpLevel * SystemOption.ExCastleHpPerL;
            CenterProcess.SetTargetState(this.gameObject);
            
        }
	
        // Update is called once per frame
        void Update () {
            CenterProcess.SetTargetState(this.gameObject);     
        }
    }


}
