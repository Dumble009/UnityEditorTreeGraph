using BT;
public class BaseTree : BehaviourTreeComponent 
{
[UnityEngine.SerializeField]
public bool case2 = false;[UnityEngine.SerializeField]
public bool case3 = false;[UnityEngine.SerializeField]
public bool if_bool1 = false;[UnityEngine.SerializeField]
public bool case4 = false;[UnityEngine.SerializeField]
public bool while_bool1 = false;[UnityEngine.SerializeField]
public bool case5 = false;[UnityEngine.SerializeField]
public bool selector_bool1 = false;[UnityEngine.SerializeField]
public bool case6 = false;[UnityEngine.SerializeField]
public bool sequence_bool1 = false;[UnityEngine.SerializeField]
public bool case7 = false;[UnityEngine.SerializeField]
public bool case8 = false;[UnityEngine.SerializeField]
public bool case9 = false;[UnityEngine.SerializeField]
public bool case10 = false;[UnityEngine.SerializeField]
public UnityEngine.Events.UnityEvent first_ev = new UnityEngine.Events.UnityEvent();
[UnityEngine.SerializeField]
public UnityEngine.Events.UnityEvent itr_ev = new UnityEngine.Events.UnityEvent();
[UnityEngine.SerializeField]
public UnityEngine.Events.UnityEvent if_ev1 = new UnityEngine.Events.UnityEvent();
[UnityEngine.SerializeField]
public UnityEngine.Events.UnityEvent if_ev2 = new UnityEngine.Events.UnityEvent();
[UnityEngine.SerializeField]
public UnityEngine.Events.UnityEvent if_ev3 = new UnityEngine.Events.UnityEvent();
[UnityEngine.SerializeField]
public UnityEngine.Events.UnityEvent while_ev1 = new UnityEngine.Events.UnityEvent();
[UnityEngine.SerializeField]
public UnityEngine.Events.UnityEvent while_ev2 = new UnityEngine.Events.UnityEvent();
[UnityEngine.SerializeField]
public UnityEngine.Events.UnityEvent selector_ev1 = new UnityEngine.Events.UnityEvent();
[UnityEngine.SerializeField]
public UnityEngine.Events.UnityEvent selector_ev2 = new UnityEngine.Events.UnityEvent();
[UnityEngine.SerializeField]
public UnityEngine.Events.UnityEvent selector_ev3 = new UnityEngine.Events.UnityEvent();
[UnityEngine.SerializeField]
public UnityEngine.Events.UnityEvent sequence_ev1 = new UnityEngine.Events.UnityEvent();
[UnityEngine.SerializeField]
public UnityEngine.Events.UnityEvent sequence_ev2 = new UnityEngine.Events.UnityEvent();
[UnityEngine.SerializeField]
public UnityEngine.Events.UnityEvent sequence_ev3 = new UnityEngine.Events.UnityEvent();
[UnityEngine.SerializeField]
public UnityEngine.Events.UnityEvent frameCounter_ev1 = new UnityEngine.Events.UnityEvent();
[UnityEngine.SerializeField]
public UnityEngine.Events.UnityEvent frameCounter_ev2 = new UnityEngine.Events.UnityEvent();
[UnityEngine.SerializeField]
public UnityEngine.Events.UnityEvent frameCounter_ev3 = new UnityEngine.Events.UnityEvent();
[UnityEngine.SerializeField]
public UnityEngine.Events.UnityEvent frameCounter_ev4 = new UnityEngine.Events.UnityEvent();
[UnityEngine.SerializeField]
public UnityEngine.Events.UnityEvent frameCounter_ev5 = new UnityEngine.Events.UnityEvent();
[UnityEngine.SerializeField]
public UnityEngine.Events.UnityEvent timerTest_ev1 = new UnityEngine.Events.UnityEvent();
[UnityEngine.SerializeField]
public UnityEngine.Events.UnityEvent timerTest_ev2 = new UnityEngine.Events.UnityEvent();
[UnityEngine.SerializeField]
public UnityEngine.Events.UnityEvent timerTest_ev3 = new UnityEngine.Events.UnityEvent();
[UnityEngine.SerializeField]
public UnityEngine.Events.UnityEvent timerTest_ev4 = new UnityEngine.Events.UnityEvent();
[UnityEngine.SerializeField]
public UnityEngine.Events.UnityEvent timerTest_ev5 = new UnityEngine.Events.UnityEvent();
[UnityEngine.SerializeField]
public float if_float1 = 100.5f;[UnityEngine.SerializeField]
int if_int1 = 0;

override public void MakeTree()
{
base.MakeTree();
BT_Root root = new BT_Root();behaviourTree = new BehaviourTree(root);BT_Execute firstEx = new BT_Execute();
BT_Interrupt itr_1 = new BT_Interrupt();
BT_Execute itrEx = new BT_Execute();
BT_Interrupt itr_2 = new BT_Interrupt();
BT_If if1 = new BT_If();
BT_Execute ifEx1 = new BT_Execute();
BT_If if2 = new BT_If();
BT_Execute ifEx2 = new BT_Execute();
BT_If if3 = new BT_If();
BT_Execute ifEx3 = new BT_Execute();
BT_Interrupt itr_3 = new BT_Interrupt();
BT_While while1 = new BT_While();
BT_Execute whileEx1 = new BT_Execute();
BT_Execute whileEx2 = new BT_Execute();
BT_Interrupt itr_4 = new BT_Interrupt();
BT_Selector selector1 = new BT_Selector();
BT_Execute selectorEx1 = new BT_Execute();
BT_If selectorIf1 = new BT_If();
BT_Execute selectorEx2 = new BT_Execute();
BT_Execute selectorEx3 = new BT_Execute();
BT_Interrupt SequenceTest = new BT_Interrupt();
BT_Sequence sequence1 = new BT_Sequence();
BT_Execute sequenceEx1 = new BT_Execute();
BT_If sequenceIf1 = new BT_If();
BT_Execute sequenceEx2 = new BT_Execute();
BT_Execute sequenceEx3 = new BT_Execute();
BT_Interrupt frameCounterTest = new BT_Interrupt();
BT_Execute frameCounterEx1 = new BT_Execute();
BT_Timing frameCounter1 = new BT_Timing(behaviourTree, false, false);
BT_Interrupt frameCounterTest2 = new BT_Interrupt();
BT_Execute frameCounterEx2 = new BT_Execute();
BT_Interrupt frameCounterTest3 = new BT_Interrupt();
BT_Execute frameCounterEx3 = new BT_Execute();
BT_Timing frameCounter2 = new BT_Timing(behaviourTree, false, true);
BT_Interrupt frameCounter4 = new BT_Interrupt();
BT_Execute frameCounterEx4 = new BT_Execute();
BT_Timing frameCounter3 = new BT_Timing(behaviourTree, true, false);
BT_Interrupt frameCounter5 = new BT_Interrupt();
BT_Execute frameCounterEx5 = new BT_Execute();
BT_Interrupt timerTest = new BT_Interrupt();
BT_Execute timerTestEx1 = new BT_Execute();
BT_Timing timer1 = new BT_Timing(behaviourTree, false, false);
BT_Interrupt timerTest2 = new BT_Interrupt();
BT_Execute timerTestEx2 = new BT_Execute();
BT_Interrupt timerTest3 = new BT_Interrupt();
BT_Execute timerTestEx3 = new BT_Execute();
BT_Timing timer2 = new BT_Timing(behaviourTree, false, true);
BT_Interrupt timerTest4 = new BT_Interrupt();
BT_Execute timerTestEx4 = new BT_Execute();
BT_Timing timer3 = new BT_Timing(behaviourTree, true, false);
BT_Interrupt timerTest5 = new BT_Interrupt();
BT_Execute timerTestEx5 = new BT_Execute();
firstEx.AddEvent(()=>{
	first_ev.Invoke();
});
root.AddChild(firstEx);
itr_1.SetCondition(()=>{
	return case2;
});
behaviourTree.AddInterrupt(itr_1);
itr_1.AddChild(itrEx);
itrEx.AddEvent(()=>{
	itr_ev.Invoke();
});
itr_2.SetCondition(()=>{
	return case3;
});
behaviourTree.AddInterrupt(itr_2);
itr_2.AddChild(if1);
if1.SetCondition(()=>{
	return if_bool1;
});
if1.AddChild(ifEx1);
ifEx1.AddEvent(()=>{
	if_ev1.Invoke();
});
ifEx1.AddChild(if2);
if2.SetCondition(()=>{
	return if_int1 > 10;
});
if2.AddChild(ifEx2);
ifEx2.AddEvent(()=>{
	if_ev2.Invoke();
});
ifEx2.AddChild(if3);
if3.SetCondition(()=>{
	return if_float1 < 10.0f;
});
if3.AddChild(ifEx3);
ifEx3.AddEvent(()=>{
	if_ev3.Invoke();
});
while1.SetCondition(()=>{
	return while_bool1;
});
while1.AddChild(whileEx2);
itr_3.SetCondition(()=>{
	return case4;
});
behaviourTree.AddInterrupt(itr_3);
itr_3.AddChild(whileEx1);
whileEx1.AddEvent(()=>{
	while_ev1.Invoke();
});
whileEx1.AddChild(while1);
whileEx2.AddEvent(()=>{
	while_ev2.Invoke();
});
selectorEx1.AddEvent(()=>{
	selector_ev1.Invoke();
});
itr_4.SetCondition(()=>{
	return case5;
});
behaviourTree.AddInterrupt(itr_4);
itr_4.AddChild(selector1);

selector1.AddChild(selectorEx1);
selector1.AddChild(selectorIf1);
selector1.AddChild(selectorEx3);
selectorIf1.SetCondition(()=>{
	return selector_bool1;
});
selectorIf1.AddChild(selectorEx2);
selectorEx2.AddEvent(()=>{
	selector_ev2.Invoke();
});
selectorEx3.AddEvent(()=>{
	selector_ev3.Invoke();
});
sequenceEx1.AddEvent(()=>{
	sequence_ev1.Invoke();
});
SequenceTest.SetCondition(()=>{
	return case6;
});
behaviourTree.AddInterrupt(SequenceTest);
SequenceTest.AddChild(sequence1);

sequence1.AddChild(sequenceEx1);
sequence1.AddChild(sequenceIf1);
sequence1.AddChild(sequenceEx3);
sequenceIf1.SetCondition(()=>{
	return sequence_bool1
;
});
sequenceIf1.AddChild(sequenceEx2);
sequenceEx2.AddEvent(()=>{
	sequence_ev2.Invoke();
});
sequenceEx3.AddEvent(()=>{
	sequence_ev3.Invoke();
});
frameCounterEx2.AddEvent(()=>{
	frameCounter_ev2.Invoke();
});
frameCounterTest.SetCondition(()=>{
	return case7;
});
behaviourTree.AddInterrupt(frameCounterTest);
frameCounterTest.AddChild(frameCounterEx1);
frameCounter1.SetTimingCreator(()=>{
	return new FrameCounter(frameCounterTest2, 2);
});
frameCounterTest2.SetCondition(()=>{
	return false;
});
behaviourTree.AddInterrupt(frameCounterTest2);
frameCounterTest2.AddChild(frameCounterEx2);
frameCounterEx1.AddEvent(()=>{
	frameCounter_ev1.Invoke();
});
frameCounterEx1.AddChild(frameCounter1);
frameCounterTest3.SetCondition(()=>{
	return case8;
});
behaviourTree.AddInterrupt(frameCounterTest3);
frameCounterTest3.AddChild(frameCounterEx3);
frameCounterEx3.AddEvent(()=>{
	frameCounter_ev3.Invoke();
});
frameCounterEx3.AddChild(frameCounter2);
frameCounter2.SetTimingCreator(()=>{
	return new FrameCounter(frameCounter4, 2);
});
frameCounter4.SetCondition(()=>{
	return false;
});
behaviourTree.AddInterrupt(frameCounter4);
frameCounter4.AddChild(frameCounterEx4);
frameCounterEx4.AddEvent(()=>{
	frameCounter_ev4.Invoke();
});
frameCounterEx4.AddChild(frameCounter3);
frameCounter3.SetTimingCreator(()=>{
	return new FrameCounter(frameCounter5, 2);
});
frameCounter5.SetCondition(()=>{
	return false;
});
behaviourTree.AddInterrupt(frameCounter5);
frameCounter5.AddChild(frameCounterEx5);
frameCounterEx5.AddEvent(()=>{
	frameCounter_ev5.Invoke();
});
timerTest.SetCondition(()=>{
	return case9;
});
behaviourTree.AddInterrupt(timerTest);
timerTest.AddChild(timerTestEx1);
timerTestEx1.AddEvent(()=>{
	timerTest_ev1.Invoke();
});
timerTestEx1.AddChild(timer1);
timer1.SetTimingCreator(()=>{
	return new Timer(timerTest2, 3);
});
timerTest2.SetCondition(()=>{
	return false;
});
behaviourTree.AddInterrupt(timerTest2);
timerTest2.AddChild(timerTestEx2);
timerTestEx2.AddEvent(()=>{
	timerTest_ev2.Invoke();
});
timerTest3.SetCondition(()=>{
	return case10;
});
behaviourTree.AddInterrupt(timerTest3);
timerTest3.AddChild(timerTestEx3);
timerTestEx3.AddEvent(()=>{
	timerTest_ev3.Invoke();
});
timerTestEx3.AddChild(timer2);
timer2.SetTimingCreator(()=>{
	return new Timer(timerTest4, 1.5);
});
timerTest4.SetCondition(()=>{
	return false;
});
behaviourTree.AddInterrupt(timerTest4);
timerTest4.AddChild(timerTestEx4);
timerTestEx4.AddEvent(()=>{
	timerTest_ev4.Invoke();
});
timerTestEx4.AddChild(timer3);
timer3.SetTimingCreator(()=>{
	return new Timer(timerTest5, 3);
});
timerTest5.SetCondition(()=>{
	return false;
});
behaviourTree.AddInterrupt(timerTest5);
timerTest5.AddChild(timerTestEx5);
timerTestEx5.AddEvent(()=>{
	timerTest_ev5.Invoke();
});

}
}