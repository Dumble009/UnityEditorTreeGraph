using System.Collections;
using System.Collections.Generic;
namespace BT
{
	public class BehaviourTree
	{
		protected BT_Node root;
		protected Queue<BT_Node> continueNodes;
		protected List<BT_Interrupt> interrupts;
		protected List<Timing> timings;

		public BehaviourTree(BT_Node _root)
		{
			root = _root;
			interrupts = new List<BT_Interrupt>();
			timings = new List<Timing>();
			continueNodes = new Queue<BT_Node>();
		}

		public void Tick()
		{
			// Check Timings
			int timingLength = timings.Count;
			bool isTimingActivated = false;
			int activatedIndex = -1;
			for (int i = 0; i < timingLength; i++)
			{
				if (timings[i].Check() && !isTimingActivated)
				{
					isTimingActivated = true;
					activatedIndex = i;
				}
			}

			if (isTimingActivated && activatedIndex >= 0)
			{
				ResultContainer _result = timings[activatedIndex].Next();
				ProcessResult(_result);
				timings.RemoveAt(activatedIndex);

				return;
			}

			bool isInterruptActivated = false;
			if (!isTimingActivated)
			{
				// Check Interrupts
				foreach (BT_Interrupt interrupt in interrupts)
				{
					if (interrupt.IsInterrupt())
					{
						ResultContainer _result = interrupt.Next();
						ProcessResult(_result);
						isInterruptActivated = true;
						return;
					}
				}
			}

			if (!isTimingActivated && !isInterruptActivated)
			{
				ResultContainer result = new ResultContainer();
				if (continueNodes.Count > 0)
				{
					do
					{
						BT_Node nextNode = continueNodes.Dequeue();
						result = nextNode.Next();
					} while (result.Result == BT_Result.FAILURE && continueNodes.Count > 0);

					if(result.Result == BT_Result.FAILURE)
					{
						result = root.Next();
					}
				}
				else
				{
					result = root.Next();
				}
				ProcessResult(result);
			}
		}

		protected void ProcessResult(ResultContainer result)
		{
			if (result.Result == BT_Result.CONTINUE)
			{
				while(result.NextStartNodesQueue.Count != 0)
				{
					continueNodes.Enqueue(result.NextStartNodesQueue.Dequeue());
				}
			}
		}

		public void AddInterrupt(BT_Interrupt interrupt)
		{
			if (!interrupts.Contains(interrupt))
			{
				interrupts.Add(interrupt);
			}
		}

		public void AddTiming(Timing timing)
		{
			if (!timings.Contains(timing))
			{
				timings.Add(timing);
			}
		}
	}
}
