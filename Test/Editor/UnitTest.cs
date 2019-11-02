using NUnit.Framework;
using BT;

public class UnitTest
{
	[Test]
	public void SuccessTest()
	{
		BT_Success success = new BT_Success();
		ResultContainer result = success.Next();

		Assert.AreEqual(BT_Result.SUCCESS, result.Result);
	}

	[Test]
	public void FailureTest()
	{
		BT_Failure failure = new BT_Failure();
		ResultContainer result = failure.Next();

		Assert.AreEqual(BT_Result.FAILURE, result.Result);
	}

	[Test]
	public void RootTest()
	{
		BT_Root root = new BT_Root();
		root.AddChild(new BT_Success());

		ResultContainer result = root.Next();

		Assert.AreEqual(BT_Result.SUCCESS, result.Result);
	}

	[Test]
	public void ExecuteTest()
	{
		bool isOK = false;
		BT_Root root = new BT_Root();
		BT_Execute execute = new BT_Execute();
		execute.AddEvent(() => {
			isOK = true;
		});
		root.AddChild(execute);

		ResultContainer result = root.Next();

		Assert.AreEqual(true, isOK);
	}

	[Test]
	public void SelectorTest1()
	{
		// selector stopable test
		//
		//						1	_ failure1 _ ex1
		//root - selector-|
		//                     2  - failure2 - ex2 
		//
		// ex1: should be activated
		// ex2: should be activated

		bool isOK1 = false;
		bool isOK2 = false;

		BT_Root root = new BT_Root();

		BT_Selector s = new BT_Selector();
		BT_Failure failure1 = new BT_Failure();
		s.AddChild(failure1);
		BT_Failure failure2 = new BT_Failure();
		s.AddChild(failure2);

		BT_Execute ex1 = new BT_Execute();
		ex1.AddEvent(() => {
			isOK1 = true;
		});
		failure1.AddChild(ex1);

		BT_Execute ex2 = new BT_Execute();
		ex2.AddEvent(() => {
			isOK2 = true;
		});
		failure2.AddChild(ex2);

		root.AddChild(s);

		root.Next();

		Assert.AreEqual(true, isOK1);
		Assert.AreEqual(true, isOK2);
	}

	[Test]
	public void SelectorTest2()
	{
		// selector algorithm test
		//
		//                         1  __ failure __ ex1 
		//root - sequence -|
		//                         2 | -- ex2 -- succcess
		//                         3 ----ex3  
		//
		// ex1 : should be activated
		// ex2 : should be activated
		// ex3 : should not be activated

		BT_Root root = new BT_Root();
		BT_Selector selector = new BT_Selector();
		BT_Failure failure = new BT_Failure();
		BT_Execute ex1 = new BT_Execute();
		BT_Execute ex2 = new BT_Execute();
		BT_Execute ex3 = new BT_Execute();
		BT_Success success = new BT_Success();

		root.AddChild(selector);
		selector.AddChild(failure);
		selector.AddChild(ex2);
		selector.AddChild(ex3);
		failure.AddChild(ex1);
		ex2.AddChild(success);

		bool isOK1 = false, isOK2 = false, isOK3 = true;

		ex1.AddEvent(() => {
			isOK1 = true;
		});

		ex2.AddEvent(() => {
			isOK2 = true;
		});

		ex3.AddEvent(() => {
			isOK3 = false;
		});

		root.Next();

		Assert.AreEqual(true, isOK1);
		Assert.AreEqual(true, isOK2);
		Assert.AreEqual(true, isOK3);
	}

	[Test]
	public void SequenceTest1()
	{
		// sequence stopable test
		//
		//                         1  __ success1 __ ex1
		// root - sequence -|
		//                         2  --- success2 --- ex2
		//
		// ex1: should be activated
		// ex2: should be activated

		BT_Root root = new BT_Root();
		BT_Sequence sequence = new BT_Sequence();
		BT_Success success1 = new BT_Success();
		BT_Success success2 = new BT_Success();
		BT_Execute ex1 = new BT_Execute();
		BT_Execute ex2 = new BT_Execute();

		root.AddChild(sequence);
		sequence.AddChild(success1);
		sequence.AddChild(success2);
		success1.AddChild(ex1);
		success2.AddChild(ex2);

		bool isOK1 = false, isOK2 = false;

		ex1.AddEvent(() => {
			isOK1 = !isOK1;
		});

		ex2.AddEvent(() => {
			isOK2 = !isOK2;
		});

		root.Next();
		Assert.AreEqual(true, isOK1);
		Assert.AreEqual(false, isOK2);

		root.Next();
		Assert.AreEqual(true, isOK1);
		Assert.AreEqual(true, isOK2);

		root.Next();
		Assert.AreEqual(false, isOK1);
		Assert.AreEqual(true, isOK2);
	}

	[Test]
	public void SequenceTest2()
	{
		// sequence algorithm test
		//
		//                          1 __ success1 __ ex1
		// root - sequence -|
		//                          2 --- failure __ ex2
		//                             |
		//                          3 --- success2 --- ex3
		//
		// ex1 : should be activated twice
		// ex2 : should be activated
		// ex3 : should not be activated

		BT_Root root = new BT_Root();
		BT_Sequence sequence = new BT_Sequence();
		BT_Success success1 = new BT_Success();
		BT_Success success2 = new BT_Success();
		BT_Failure failure = new BT_Failure();
		BT_Execute ex1 = new BT_Execute();
		BT_Execute ex2 = new BT_Execute();
		BT_Execute ex3 = new BT_Execute();

		root.AddChild(sequence);
		sequence.AddChild(success1);
		sequence.AddChild(failure);
		sequence.AddChild(success2);
		success1.AddChild(ex1);
		failure.AddChild(ex2);
		success2.AddChild(ex3);

		bool isOK1 = false, isOK2 = false, isOK3 = false;

		ex1.AddEvent(() => {
			isOK1 = !isOK1;
		});

		ex2.AddEvent(() => {
			isOK2 = !isOK2;
		});

		ex3.AddEvent(() => {
			isOK3 = !isOK3;
		});

		root.Next();
		Assert.AreEqual(true, isOK1);
		Assert.AreEqual(false, isOK2);
		Assert.AreEqual(false, isOK3);

		root.Next();
		Assert.AreEqual(true, isOK1);
		Assert.AreEqual(true, isOK2);
		Assert.AreEqual(false, isOK3);

		root.Next();
		Assert.AreEqual(false, isOK1);
		Assert.AreEqual(true, isOK2);
		Assert.AreEqual(false, isOK3);
	}

	[Test]
	public void IfTest()
	{
		// if test
		//
		// root - if1 - ex1 - if2 - ex2
		//
		// ex1 : should be activated
		// ex2 : should not be activated
		// if1 : return true
		// if2 : return false

		BT_Root root = new BT_Root();
		BT_If if1 = new BT_If();
		BT_If if2 = new BT_If();
		BT_Execute ex1 = new BT_Execute();
		BT_Execute ex2 = new BT_Execute();

		root.AddChild(if1);
		if1.AddChild(ex1);
		ex1.AddChild(if2);
		if2.AddChild(ex2);

		if1.SetCondition(() => {
			return true;
		});

		if2.SetCondition(() => {
			return false;
		});

		bool isOK1 = false, isOK2 = true;

		ex1.AddEvent(() => {
			isOK1 = true;
		});

		ex2.AddEvent(() => {
			isOK2 = false;
		});

		ResultContainer result = root.Next();

		Assert.AreEqual(true, isOK1);
		Assert.AreEqual(true, isOK2);
		Assert.AreEqual(BT_Result.FAILURE, result.Result);
	}

	[Test]
	public void WhileTest()
	{
		// while  test
		//
		//                                1  __ while1 __ ex2 
		// root - ex1 - selector -|
		//                                2  --- ex3
		//
		// ex1 : should be activated twice, at first tick and at fourth tick
		// ex2 : should be activated three times
		// ex3 : should be activated only once at fourth tick
		// while1 : should loop three times

		BT_Root root = new BT_Root();
		BT_Execute ex1 = new BT_Execute();
		BT_Selector selector = new BT_Selector();
		BT_While while1 = new BT_While();
		BT_Execute ex2 = new BT_Execute();
		BT_Execute ex3 = new BT_Execute();

		BehaviourTree tree = new BehaviourTree(root);

		root.AddChild(ex1);
		ex1.AddChild(selector);
		selector.AddChild(while1);
		selector.AddChild(ex3);
		while1.AddChild(ex2);

		bool isOK1 = false, isOK3 = false;
		int loopCount = 3;

		ex1.AddEvent(() => {
			isOK1 = !isOK1;
		});

		ex2.AddEvent(() => {
			loopCount--;
		});

		ex3.AddEvent(() => {
			isOK3 = true;
		});

		while1.SetCondition(() => {
			return loopCount > 0;
		});

		tree.Tick();
		Assert.AreEqual(true, isOK1);
		Assert.AreEqual(false, isOK3);

		tree.Tick();
		Assert.AreEqual(true, isOK1);
		Assert.AreEqual(false, isOK3);

		tree.Tick();
		Assert.AreEqual(true, isOK1);
		Assert.AreEqual(false, isOK3);

		tree.Tick();
		Assert.AreEqual(true, isOK1);
		Assert.AreEqual(false, isOK3);
		Assert.AreEqual(0, loopCount);

		tree.Tick();
		Assert.AreEqual(false, isOK1);
		Assert.AreEqual(true, isOK3);
		Assert.AreEqual(0, loopCount);
	}

	[Test]
	public void InterruptTest()
	{
		// interrupt test
		//
		//  root - ex1
		//
		//  interrupt - ex2
		//
		// ex1 : should be activated twice, at fist tick and at third tick
		// ex2 : should be activated only once at second tick

		BT_Root root = new BT_Root();
		BT_Execute ex1 = new BT_Execute();
		BT_Interrupt interrupt = new BT_Interrupt();
		BT_Execute ex2 = new BT_Execute();

		root.AddChild(ex1);
		interrupt.AddChild(ex2);

		bool isOK1 = false, isOK2 = false;
		ex1.AddEvent(() => {
			isOK1 = !isOK1;
		});

		ex2.AddEvent(() => {
			isOK2 = !isOK2;
		});

		interrupt.SetCondition(() => {
			return isOK1 && !isOK2;
		});

		BehaviourTree tree = new BehaviourTree(root);
		tree.AddInterrupt(interrupt);

		tree.Tick();
		Assert.AreEqual(true, isOK1);
		Assert.AreEqual(false, isOK2);

		tree.Tick();
		Assert.AreEqual(true, isOK1);
		Assert.AreEqual(true, isOK2);

		tree.Tick();
		Assert.AreEqual(false, isOK1);
		Assert.AreEqual(true, isOK2);
	}

	[Test]
	public void TimingTest()
	{
		//Timing Test
		//
		// root - timing - ex1 - ex_target
		//
		// ex1 : should be called once at first tick.
		// failure1 : should be called once at first tick.
		// ex_target : should be called twice. Second tick should start from this node.
		// success 1 : should be called twice.

		BT_Root root = new BT_Root();
		BehaviourTree tree = new BehaviourTree(root);
		BT_Timing timing = new BT_Timing(tree, false, false);
		BT_Execute ex1 = new BT_Execute();
		BT_Execute ex_target = new BT_Execute();

		root.AddChild(timing);
		timing.AddChild(ex1);
		ex1.AddChild(ex_target);

		timing.SetTimingCreator(() => {
			return new Timing(ex_target);
		});

		bool isOK1 = false, isOK2 = false;

		ex1.AddEvent(() => {
			isOK1 = !isOK1;
		});

		ex_target.AddEvent(() => {
			isOK2 = !isOK2;
		});

		tree.Tick();
		Assert.AreEqual(true, isOK1);
		Assert.AreEqual(true, isOK2);

		tree.Tick();
		Assert.AreEqual(true, isOK1);
		Assert.AreEqual(false, isOK2);
	}

	[Test]
	public void TimerTest()
	{
		//Timer Test
		//
		// root - timing - ex1 - ex_target
		//
		// ex1 : should be called once at first tick.
		// failure1 : should be called once at first tick.
		// ex_target : should be called twice. Second tick should start from this node.
		// success 1 : should be called twice.

		BT_Root root = new BT_Root();
		BehaviourTree tree = new BehaviourTree(root);
		BT_Timing timing = new BT_Timing(tree, false, false);
		BT_Execute ex1 = new BT_Execute();
		BT_Execute ex_target = new BT_Execute();

		root.AddChild(timing);
		timing.AddChild(ex1);
		ex1.AddChild(ex_target);

		float waitTime = 0.1f;
		timing.SetTimingCreator(() => {
			return new Timer(ex_target, waitTime);
		});

		bool isOK1 = false, isOK2 = false;

		ex1.AddEvent(() => {
			isOK1 = !isOK1;
		});

		ex_target.AddEvent(() => {
			isOK2 = !isOK2;
		});

		tree.Tick();
		Assert.AreEqual(true, isOK1);
		Assert.AreEqual(true, isOK2);

		System.Threading.Thread.Sleep(50);

		tree.Tick();
		Assert.AreEqual(false, isOK1);
		Assert.AreEqual(false, isOK2);

		System.Threading.Thread.Sleep(100);

		tree.Tick();
		Assert.AreEqual(false, isOK1);
		Assert.AreEqual(true, isOK2);
	}
}
