using System.Collections.Generic;

namespace BT
{
	public enum BT_Result
	{
		SUCCESS,
		FAILURE,
		CONTINUE
	}

	public struct ResultContainer
	{
		BT_Node nextStartNode;
		public BT_Node NextStartNode {
			get {
				return nextStartNode;
			}
		}
		BT_Result result;
		public BT_Result Result {
			get {
				return result;
			}
		}
		public ResultContainer(BT_Result _r)
		{
			nextStartNode = null;
			result = _r;
		}

		public ResultContainer(BT_Node _n, BT_Result _r)
		{
			nextStartNode = _n;
			result = _r;
		}
	}
	public class BT_Node
	{

		protected List<BT_Node> children;
		protected BT_Node parent;
		protected NodeEvent nodeEvent;
		protected IfEvent condition;
		public BT_Node()
		{
			children = new List<BT_Node>();
			parent = null;
		}

		virtual public ResultContainer Next()
		{
			return new ResultContainer(BT_Result.SUCCESS);
		}

		virtual public void AddChild(BT_Node c)
		{
			if (children == null)
			{
				children = new List<BT_Node>();
			}
			children.Add(c);
		}

		virtual public void AddEvent(NodeEvent _e)
		{
			nodeEvent = _e;
		}

		virtual public void SetCondition(IfEvent _e)
		{
			condition = _e;
		}
	}
}
