using System.Collections.Generic;
using UnityEngine;

namespace Character.Monster
{
    public class FSMSystem
    {
        private List<FSMState> states;

        // 当前的状态ID
        private StateId _currentStateId;
        // 当前的状态类
        private FSMState _currentFsmState;

        public StateId CurrentStateID
        {
            get
            {
                return _currentStateId;
            }

        }

        public FSMState CurrentFSMState
        {
            get
            {
                return _currentFsmState;
            }

        }

        public FSMSystem()
        {
            states = new List<FSMState>();
        }
    
        // 这个方法为有限状态机置入新的状态或在改状态已经存在列表时打印错误信息
        public void AddState(FSMState s)
        { 
            if (s == null)
            {
                Debug.LogError("FSM error:Null reference is not allowed");
            }
 
            if (states.Count == 0)
            {
                states.Add(s);
                _currentFsmState = s;
                _currentStateId  = s.ID;
                return;
            }
        
            foreach (FSMState state in states)
            {
                if (state.ID == s.ID)
                {
                    Debug.LogError("FSM error: There already exist " + s.ID.ToString());
                    return;
                }

            }
            states.Add(s);
        }
    
        // 删除一个已存在状态机中的状态
        public void DeleteState(StateId id)
        {
            if (id == StateId.NullStateId)
            {
                Debug.LogError("FSM error:NullStateID is not allowed for a real state");
                return;
            }
            foreach (FSMState state in states)
            {
                if (state.ID == id)
                {
                    states.Remove(state);
                    return;
                }
            }
            Debug.LogError("FSM error:Can't delete state " + id.ToString() + ",it was not in list");
        }
    
        // 该方法基于当前状态和过渡状态是否通过来尝试改变状态机的状态，当前状态没有目标状态过渡时则打印错误信息  
        // 切换当前状态到下一个要转换的状态
        public void PerformTransition(Transition trans)
        {
            if (trans == Transition.NullTransition)
            {
                Debug.LogError("FSM error:NullTransition is not allowed");
                return;
            }
        
            StateId id = _currentFsmState.GetOutputState(trans);
            if (id == StateId.NullStateId)
            {
                Debug.LogError("FSM error: State " + _currentFsmState.ID.ToString() + "does not allowed");
                return;
            }

            //更新当前的状态机和状态编号  
            _currentStateId = id;
            foreach (FSMState state in states)
            {
                if (state.ID == _currentStateId)
                {
                    _currentFsmState.DoBeforeLeaving();
                    _currentFsmState = state; 
                    _currentFsmState.DoBeforeEntering();
                    break;
                }
            }

        }
    }
}
