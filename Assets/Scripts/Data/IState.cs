using System.Collections.Generic;
using State;

    public interface IState{
        Dictionary<StateType, bool> states { get; set;}
        void SetStates((StateType type, bool status)newState);
        Dictionary<StateType, bool> ReturnStatesDictionary();
    }
