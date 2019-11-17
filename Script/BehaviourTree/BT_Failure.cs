namespace BT
{
	public class BT_Failure : BT_Node
	{
		public BT_Failure() : base() { }

		public override ResultContainer Next()
		{
			if (children != null && children.Count > 0)
			{
				children[0].Next();
			}
			return new ResultContainer(BT_Result.FAILURE);
		}
	}
}
