using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
	public class BT_Interrupt : BT_Node
	{
		public BT_Interrupt() : base() { }

		public override ResultContainer Next()
		{
			if (children != null && children.Count > 0)
			{
				if (children[0] != null)
				{
					return children[0].Next();
				}
			}
			return new ResultContainer(BT_Result.FAILURE);
		}

		public bool IsInterrupt()
		{
			return condition.Invoke();
		}
	}
}
