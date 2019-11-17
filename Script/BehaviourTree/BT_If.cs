using System.Collections.Generic;

namespace BT
{
	public delegate bool IfEvent();
	public class BT_If : BT_Node
	{

		public BT_If() : base() { }
		public BT_If(BT_Node _c, BT_Node _p, IfEvent _e)
		{
			children = new List<BT_Node>();
			children.Add(_c);
			parent = _p;
			condition = _e;
		}


		override public ResultContainer Next()
		{
			if (children != null && children.Count > 0 && children[0] != null)
			{
				if (condition.Invoke())
				{
					return children[0].Next();
				}
				else
				{
					return new ResultContainer(BT_Result.FAILURE);
				}
			}
			return new ResultContainer(BT_Result.FAILURE);
		}
	}
}
