﻿namespace BT
{
	public class BT_Success : BT_Node
	{
		public BT_Success() : base() { }

		public override ResultContainer Next()
		{
			if (children != null && children.Count > 0)
			{
				children[0].Next();
			}
			return new ResultContainer(BT_Result.SUCCESS);
		}
	}
}
