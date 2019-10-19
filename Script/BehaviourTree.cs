using System.Collections;
using System.Collections.Generic;

public class BehaviourTree{
    protected BT_Node root;
    protected BT_Node continueNode;
    protected bool isContinued = false;
	protected List<BT_Interrupt> interrupts;
    public BehaviourTree(BT_Node _root){
        root = _root;
		interrupts = new List<BT_Interrupt>();
    }

    public void Tick(){
		foreach (BT_Interrupt interrupt in interrupts)
		{
			if (interrupt.IsInterrupt())
			{
				ResultContainer _result = interrupt.Next();
				if (_result.Result == BT_Result.CONTINUE)
				{
					isContinued = true;
					continueNode = _result.NextStartNode;
				}
				else
				{
					isContinued = false;
				}

				return;
			}
		}
        BT_Node nextNode = (isContinued && continueNode != null) ? continueNode : root;
        ResultContainer result = nextNode.Next();
        if(result.Result == BT_Result.CONTINUE){
            isContinued = true;
            continueNode = result.NextStartNode;
        }else{
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
}
