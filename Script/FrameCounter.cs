namespace BT
{
	public class FrameCounter : Timing
	{
		int waitCount;
		uint currentFrameCount;

		public FrameCounter(BT_Node node, int _waitCount) : base(node)
		{
			currentFrameCount = 0;
			waitCount = _waitCount;
		}

		public override void Init()
		{
			currentFrameCount = 0;
		}

		public override bool Check()
		{
			currentFrameCount++;
			return currentFrameCount > waitCount;
		}
	}
}
