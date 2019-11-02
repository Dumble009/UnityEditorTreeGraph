using System;
namespace BT
{
	public class Timer : Timing
	{
		float startTime;
		float waitTime;
		public Timer(BT_Node node, float _waitTime) : base(node)
		{
			startTime = 0;
			waitTime = _waitTime;
		}

		public override void Init()
		{
			base.Init();
			DateTime now = DateTime.Now;
			startTime = now.Second + (now.Millisecond / 1000.0f);
		}

		public override bool Check()
		{
			DateTime now = DateTime.Now;
			float currentTime = now.Second + (now.Millisecond / 1000.0f);

			return currentTime - startTime >= waitTime;
		}
	}
}