namespace BT
{
	public class BT_Root : BT_Node
	{
		public BT_Root() : base() { }
		override public ResultContainer Next()
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
	}
}
