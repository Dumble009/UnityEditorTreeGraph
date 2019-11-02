﻿using System.Collections;
using System.Collections.Generic;
namespace BT
{
	public class BehaviourTree
	{
		protected BT_Node root;
		protected BT_Node continueNode;
		protected bool isContinued = false;
		protected List<BT_Interrupt> interrupts;
		protected List<Timing> timings;

		public BehaviourTree(BT_Node _root)
		{
			root = _root;
			interrupts = new List<BT_Interrupt>();
			timings = new List<Timing>();
		}

		public void Tick()
		{
			foreach (BT_Interrupt interrupt in interrupts)
			{
				if (interrupt.IsInterrupt())
				{
					ResultContainer _result = interrupt.Next();
					ProcessResult(_result);

					return;
				}
			}

			int timingLength = timings.Count;
			bool isTimingActivated = false;
			int activatedIndex = -1;
			for (int i = 0; i < timingLength; i++)
			{
				if (timings[i].Check())
				{
					ResultContainer _result = timings[i].Next();
					ProcessResult(_result);
					isTimingActivated = true;
					activatedIndex = i;
					break;
				}
			}

			if (isTimingActivated && activatedIndex >= 0)
			{
				timings.RemoveAt(activatedIndex);

				return;
			}

			BT_Node nextNode = (isContinued && continueNode != null) ? continueNode : root;
			ResultContainer result = nextNode.Next();
			ProcessResult(result);
		}

		protected void ProcessResult(ResultContainer result)
		{
			if (result.Result == BT_Result.CONTINUE && result.NextStartNode != null)
			{
				isContinued = true;
				continueNode = result.NextStartNode;
			}
			else
			{
				isContinued = false;
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
