using System;
namespace BT
{
	public class Timer : Timing
	{
		double startTime;
		double waitTime;
		public Timer(BT_Node node, double _waitTime) : base(node)
		{
			startTime = 0;
			waitTime = _waitTime;
			UnityEngine.Debug.Log("created");
		}

		public override void Init()
		{
			base.Init();
			DateTime now = DateTime.Now;
			startTime = now.Second + (now.Millisecond / 1000.0d);
			UnityEngine.Debug.Log("init");
		}

		public override bool Check()
		{
			DateTime now = DateTime.Now;
			double currentTime = now.Second + (now.Millisecond / 1000.0d);
			UnityEngine.Debug.Log("check:" + (currentTime - startTime).ToString());
			return currentTime - startTime >= waitTime;
		}
	}
}