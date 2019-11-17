using System.Collections.Generic;
namespace BT
{
	public class BT_While : BT_Node
	{
		public BT_While() : base() { }
		public BT_While(BT_Node _c, BT_Node _p, IfEvent _e)
		{
			children = new List<BT_Node>();
			children.Add(_c);
			parent = _p;
			condition = _e;
		}


		override public ResultContainer Next()
		{
			if (condition != null)
			{
				if (condition.Invoke())
				{
					ResultContainer result = new ResultContainer(this, BT_Result.CONTINUE);
					if (children != null && children.Count > 0)
					{
						ResultContainer childResult = children[0].Next();
						if (childResult.Result == BT_Result.CONTINUE && childResult.NextStartNode != null)
						{
							result = childResult;
						}
					}
					return result;
				}
				else
				{
					return new ResultContainer(BT_Result.FAILURE);
				}
			}
			return new ResultContainer(BT_Result.SUCCESS);
		}
	}
}
