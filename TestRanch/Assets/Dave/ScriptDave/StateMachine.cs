using Pathfinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Dave.ScriptDave
{
    public abstract class StateMachine : VersionedMonoBehaviour
    {
        protected State State;

        public void SetState(State state)
        {
            State = state;
            State.Neutre();
        }
    }
}
