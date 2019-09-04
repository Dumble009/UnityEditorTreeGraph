using NUnit.Framework;
using Assert = UnityEngine.Assertions.Assert;

public class BT_UnitTest {
	[Test]
	public void RootTest(){
		BT_Root root1 = new BT_Root();
		root1.AddChild(new SuccessNode());
		Assert.AreEqual(root1.Next().Result, NodeResult.SUCCESS);

		BT_Root root2 = new BT_Root();
		root2.AddChild(new FailureNode());
		Assert.AreEqual(root2.Next().Result, NodeResult.FAILURE);

		BT_Root root3 = new BT_Root();
		root3.AddChild(new ContinueNode());
		Assert.AreEqual(root3.Next().Result, NodeResult.CONTINUE);
	}

	[Test]
	public void ExecuteNodeTest(){
		BT_ExecuteNode ex = new BT_ExecuteNode();
		BT_ExecuteNode child = new BT_ExecuteNode();
		ex.AddChild(child);
		int x = 0;
		ex.AddEvent(()=>{x++;});
		child.AddEvent(()=>{x++;});
		Assert.AreEqual(ex.Next().Result, NodeResult.SUCCESS);
		Assert.AreEqual(x, 2);
	}

	[Test]
	public void IfTest(){
		BT_If ifNode = new BT_If();
		int x=0;
		ifNode.SetCondition(()=>{
			return false;
		});

		BT_ExecuteNode n = new BT_ExecuteNode();
		n.AddEvent(()=>{x++;});
		ifNode.AddChild(n);
		Assert.AreEqual(ifNode.Next().Result, NodeResult.FAILURE);
		Assert.AreEqual(x, 0);

		ifNode.SetCondition(()=>{
			return true;
		});

		Assert.AreEqual(ifNode.Next().Result, NodeResult.SUCCESS);
		Assert.AreEqual(x, 1);
	}

	

	[Test]
	public void SelectorTest(){
		BT_Selector selector = new BT_Selector();
		selector.AddChild(new FailureNode());
		selector.AddChild(new SuccessNode());
		selector.AddChild(new ContinueNode());

		Assert.AreEqual(selector.Next().Result, NodeResult.SUCCESS);
	}

	[Test]
	public void SequenceTest(){
		BT_Sequence sequence1 = new BT_Sequence();
		sequence1.AddChild(new SuccessNode());
		sequence1.AddChild(new ContinueNode());
		sequence1.AddChild(new FailureNode());
		sequence1.AddChild(new ContinueNode());
		Assert.AreEqual(sequence1.Next().Result, NodeResult.SUCCESS);
		Assert.AreEqual(sequence1.Next().Result, NodeResult.CONTINUE);
		Assert.AreEqual(sequence1.Next().Result, NodeResult.FAILURE);
		Assert.AreEqual(sequence1.Next().Result, NodeResult.SUCCESS);
	}

	[Test]
	public void WhileTest(){
		BT_While w = new BT_While();
		int i = 0;
		w.SetCondition(()=>{
			i++;
			return i <= 10;
		});
		int x = 0;
		BT_ExecuteNode n = new BT_ExecuteNode();
		n.AddEvent(()=>{
			x++;
		});
		w.AddChild(n);

		for(int j = 0; j < 10; j++){
			Assert.AreEqual(w.Next().Result, NodeResult.CONTINUE);
			Assert.AreEqual(x - 1, j);
		}
		Assert.AreEqual(w.Next().Result, NodeResult.FAILURE);
		Assert.AreEqual(x, 10);
	}
}

public class FailureNode : BT_Node{
	public FailureNode():base(){}
	override public ResultContainer Next(){
		return new ResultContainer(NodeResult.FAILURE);
	}
}

public class SuccessNode : BT_Node{
	public SuccessNode():base(){}
	override public ResultContainer Next(){
		return new ResultContainer(NodeResult.SUCCESS);
	}
}

public class ContinueNode : BT_Node{
	public ContinueNode():base(){}
	override public ResultContainer Next(){
		return new ResultContainer(this, NodeResult.CONTINUE);
	}
}


