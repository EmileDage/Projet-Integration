using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Dave.ScriptDave
{
	public abstract class State
	{
		protected CreatureBehavior CreatureBehavior;

        protected State(CreatureBehavior creatureBehavior)
        {
            CreatureBehavior = creatureBehavior;
        }

        public virtual void Neutre()
		{
			
		}

		public virtual void Pacifique()
		{
			
		}

		public virtual void Afraid()
		{
			
		}

		public virtual void FoodSearch()
		{

		}

		public virtual void Agressif()
		{

		}

		public virtual void Captured()
		{

		}
	}
}
