using Balto.Domain.Common;
using System;

namespace Balto.Domain.Aggregates.Objective
{
    public class ObjectiveFinishState : Value<ObjectiveFinishState>
    {
        public bool State { get; private set; }
        public DateTime? Date { get; private set; }

        protected ObjectiveFinishState() { }
        protected ObjectiveFinishState(bool state)
        {
            State = state;

            if (state)
            {
                Date = DateTime.Now;
            }
            else
            {
                Date = null;
            }
        }

        public static implicit operator bool(ObjectiveFinishState state) => state.State;
        public static implicit operator DateTime?(ObjectiveFinishState state) => state.Date.Value;
        public static implicit operator string(ObjectiveFinishState state) => state.Date.Value.ToString();

        public static ObjectiveFinishState Unfinished => new ObjectiveFinishState(false);
        public static ObjectiveFinishState Finished => new ObjectiveFinishState(true);
        public static ObjectiveFinishState Set(bool state) => new ObjectiveFinishState(state);
    }
}
