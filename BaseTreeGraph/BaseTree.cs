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
public bool case11 = false;[UnityEngine.SerializeField]
public bool setParameterBool1 = false;[UnityEngine.SerializeField]
public bool setParameterBool2 = true;[UnityEngine.SerializeField]
public bool while_bool2 = false;[UnityEngine.SerializeField]
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
public UnityEngine.Events.UnityEvent setParameterEv1 = new UnityEngine.Events.UnityEvent();
[UnityEngine.SerializeField]
public UnityEngine.Events.UnityEvent setParameterEv2 = new UnityEngine.Events.UnityEvent();
[UnityEngine.SerializeField]
public UnityEngine.Events.UnityEvent setParameterEv3 = new UnityEngine.Events.UnityEvent();
[UnityEngine.SerializeField]
public UnityEngine.Events.UnityEvent setParameterEv4 = new UnityEngine.Events.UnityEvent();
[UnityEngine.SerializeField]
public UnityEngine.Events.UnityEvent setParameterEv5 = new UnityEngine.Events.UnityEvent();
[UnityEngine.SerializeField]
public UnityEngine.Events.UnityEvent setParameterEv6 = new UnityEngine.Events.UnityEvent();
[UnityEngine.SerializeField]
public UnityEngine.Events.UnityEvent setParameterEv7 = new UnityEngine.Events.UnityEvent();
[UnityEngine.SerializeField]
public UnityEngine.Events.UnityEvent while_ev3 = new UnityEngine.Events.UnityEvent();
[UnityEngine.SerializeField]
public float if_float1 = 100.5f;[UnityEngine.SerializeField]
public float setParameterFloat1 = 0f;[UnityEngine.SerializeField]
public float setParameterFloat2 = 0f;[UnityEngine.SerializeField]
int if_int1 = 0;[UnityEngine.SerializeField]
int setParameterInt1 = 0;[UnityEngine.SerializeField]
int setParameterInt2 = 0;

override public void MakeTree()
{
base.MakeTree();
BT_Root root = new BT_Root();behaviourTree = new BehaviourTree(root);BT_Execute firstEx = new BT_Execute();
BT_Interrupt InterruptTest = new BT_Interrupt();
BT_Execute itrEx = new BT_Execute();
BT_Interrupt IfTest = new BT_Interrupt();
BT_If if1 = new BT_If();
BT_Execute ifEx1 = new BT_Execute();
BT_If if2 = new BT_If();
BT_Execute ifEx2 = new BT_Execute();
BT_If if3 = new BT_If();
BT_Execute ifEx3 = new BT_Execute();
BT_Interrupt WhileTest = new BT_Interrupt();
BT_While while1 = new BT_While();
BT_Execute whileEx1 = new BT_Execute();
BT_Execute whileEx2 = new BT_Execute();
BT_Interrupt SelectorTest = new BT_Interrupt();
BT_Selector selector1 = new BT_Selector();
BT_Execute selectorEx2 = new BT_Execute();
BT_If selectorIf1 = new BT_If();
BT_Execute selectorEx1 = new BT_Execute();
BT_Execute selectorEx3 = new BT_Execute();
BT_Interrupt SequenceTest = new BT_Interrupt();
BT_Sequence sequence1 = new BT_Sequence();
BT_Execute sequenceEx1 = new BT_Execute();
BT_If sequenceIf1 = new BT_If();
BT_Execute sequenceEx2 = new BT_Execute();
BT_Execute sequenceEx3 = new BT_Execute();
BT_Interrupt frameCounterTest = new BT_Interrupt();
BT_Execute frameCounterEx1 = new BT_Execute();
BT_Timing frameCounter1 = new BT_Timing(behaviourTree, false, false, "frameCounter1");
BT_Interrupt frameCounterTest2 = new BT_Interrupt();
BT_Execute frameCounterEx2 = new BT_Execute();
BT_Interrupt frameCounterTest3 = new BT_Interrupt();
BT_Execute frameCounterEx3 = new BT_Execute();
BT_Timing frameCounter2 = new BT_Timing(behaviourTree, false, true, "frameCounter2");
BT_Interrupt frameCounterTest4 = new BT_Interrupt();
BT_Execute frameCounterEx4 = new BT_Execute();
BT_Timing frameCounter3 = new BT_Timing(behaviourTree, true, false, "frameCounter3");
BT_Interrupt frameCounterTest5 = new BT_Interrupt();
BT_Execute frameCounterEx5 = new BT_Execute();
BT_Interrupt timerTest = new BT_Interrupt();
BT_Execute timerTestEx1 = new BT_Execute();
BT_Timing timer1 = new BT_Timing(behaviourTree, false, false, "timer1");
BT_Interrupt timerTest2 = new BT_Interrupt();
BT_Execute timerTestEx2 = new BT_Execute();
BT_Interrupt timerTest3 = new BT_Interrupt();
BT_Execute timerTestEx3 = new BT_Execute();
BT_Timing timer2 = new BT_Timing(behaviourTree, false, true, "timer2");
BT_Interrupt timerTest4 = new BT_Interrupt();
BT_Execute timerTestEx4 = new BT_Execute();
BT_Timing timer3 = new BT_Timing(behaviourTree, true, false, "timer3");
BT_Interrupt timerTest5 = new BT_Interrupt();
BT_Execute timerTestEx5 = new BT_Execute();
BT_Success NextSequence = new BT_Success();
BT_Interrupt setParameterTest = new BT_Interrupt();
BT_Execute SetBool1 = new BT_Execute();
BT_Execute SetBool2 = new BT_Execute();
BT_Sequence sequence = new BT_Sequence();
BT_If setParameterIf1 = new BT_If();
BT_Execute setParameterEx1 = new BT_Execute();
BT_If setParameterIf2 = new BT_If();
BT_Execute setParameterEx2 = new BT_Execute();
BT_Execute SetBool3 = new BT_Execute();
BT_If setParameterIf3 = new BT_If();
BT_Execute setParameterEx3 = new BT_Execute();
BT_Execute SetInt1 = new BT_Execute();
BT_If setParameterIf4 = new BT_If();
BT_Execute setParameterEx4 = new BT_Execute();
BT_Execute SetInt2 = new BT_Execute();
BT_If setParameterIf5 = new BT_If();
BT_Execute setParameterEx5 = new BT_Execute();
BT_Execute SetFloat1 = new BT_Execute();
BT_If setParameterIf6 = new BT_If();
BT_Execute setParameterEx6 = new BT_Execute();
BT_Execute SetFloat2 = new BT_Execute();
BT_If setParameterIf7 = new BT_If();
BT_Execute setParameterEx7 = new BT_Execute();
BT_While while2 = new BT_While();
BT_Execute whileEx3 = new BT_Execute();
firstEx.AddEvent(()=>{
	first_ev.Invoke();
});
root.AddChild(firstEx);
InterruptTest.SetCondition(()=>{
	return case2;
});
behaviourTree.AddInterrupt(InterruptTest);
InterruptTest.AddChild(itrEx);
itrEx.AddEvent(()=>{
	itr_ev.Invoke();
});
IfTest.SetCondition(()=>{
	return case3;
});
behaviourTree.AddInterrupt(IfTest);
IfTest.AddChild(if1);
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
while2.SetCondition(()=>{
	return while_bool2;
});
while2.AddChild(whileEx3);
whileEx3.AddEvent(()=>{
	while_ev3.Invoke();
});
while1.SetCondition(()=>{
	return while_bool1;
});
while1.AddChild(whileEx2);
WhileTest.SetCondition(()=>{
	return case4;
});
behaviourTree.AddInterrupt(WhileTest);
WhileTest.AddChild(whileEx1);
whileEx1.AddEvent(()=>{
	while_ev1.Invoke();
});
whileEx1.AddChild(while1);
whileEx2.AddEvent(()=>{
	while_ev2.Invoke();
});
whileEx2.AddChild(while2);
selectorIf1.SetCondition(()=>{
	return selector_bool1;
});
selectorIf1.AddChild(selectorEx1);
selectorEx1.AddEvent(()=>{
	selector_ev1.Invoke();
});
SelectorTest.SetCondition(()=>{
	return case5;
});
behaviourTree.AddInterrupt(SelectorTest);
SelectorTest.AddChild(selector1);
selectorEx2.AddEvent(()=>{
	selector_ev2.Invoke();
});

selector1.AddChild(selectorIf1);
selector1.AddChild(selectorEx2);
selector1.AddChild(selectorEx3);
selectorEx3.AddEvent(()=>{
	selector_ev3.Invoke();
});
sequenceEx1.AddEvent(()=>{
	sequence_ev1.Invoke();
});
sequenceEx1.AddChild(NextSequence);

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
	return new FrameCounter(frameCounterTest2, 1);
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
	return new FrameCounter(frameCounterTest4, 1);
});
frameCounterTest4.SetCondition(()=>{
	return false;
});
behaviourTree.AddInterrupt(frameCounterTest4);
frameCounterTest4.AddChild(frameCounterEx4);
frameCounterEx4.AddEvent(()=>{
	frameCounter_ev4.Invoke();
});
frameCounterEx4.AddChild(frameCounter3);
frameCounter3.SetTimingCreator(()=>{
	return new FrameCounter(frameCounterTest5, 1);
});
frameCounterTest5.SetCondition(()=>{
	return false;
});
behaviourTree.AddInterrupt(frameCounterTest5);
frameCounterTest5.AddChild(frameCounterEx5);
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
SetBool1.AddEvent(()=>{
	setParameterBool1 = true;
});
SetBool1.AddChild(setParameterIf1);
SetBool2.AddEvent(()=>{
	setParameterBool2 = false;
});
SetBool2.AddChild(setParameterIf2);
setParameterIf1.SetCondition(()=>{
	return setParameterBool1;
});
setParameterIf1.AddChild(setParameterEx1);
setParameterIf2.SetCondition(()=>{
	return !setParameterBool2;
});
setParameterIf2.AddChild(setParameterEx2);
SetBool3.AddEvent(()=>{
	setParameterBool2 = setParameterBool1;
});
SetBool3.AddChild(setParameterIf3);
setParameterIf3.SetCondition(()=>{
	return setParameterBool2;
});
setParameterIf3.AddChild(setParameterEx3);
setParameterEx3.AddEvent(()=>{
	setParameterEv3.Invoke();
});
setParameterEx1.AddEvent(()=>{
	setParameterEv1.Invoke();
});
setParameterEx1.AddChild(SetBool2);
setParameterEx2.AddEvent(()=>{
	setParameterEv2.Invoke();
});
setParameterEx2.AddChild(SetBool3);
setParameterTest.SetCondition(()=>{
	return case11;
});
behaviourTree.AddInterrupt(setParameterTest);
setParameterTest.AddChild(sequence);
setParameterIf4.SetCondition(()=>{
	return setParameterInt1 == 1;
});
setParameterIf4.AddChild(setParameterEx4);
setParameterIf5.SetCondition(()=>{
	return setParameterInt2 == 1;
});
setParameterIf5.AddChild(setParameterEx5);

sequence.AddChild(SetBool1);
sequence.AddChild(SetInt1);
sequence.AddChild(SetFloat1);
SetInt1.AddEvent(()=>{
	setParameterInt1 = 1;
});
SetInt1.AddChild(setParameterIf4);
setParameterEx4.AddEvent(()=>{
	setParameterEv4.Invoke();
});
setParameterEx4.AddChild(SetInt2);
SetInt2.AddEvent(()=>{
	setParameterInt2 = setParameterInt1;
});
SetInt2.AddChild(setParameterIf5);
setParameterEx5.AddEvent(()=>{
	setParameterEv5.Invoke();
});
setParameterIf6.SetCondition(()=>{
	return setParameterFloat1 > 1.0f;
});
setParameterIf6.AddChild(setParameterEx6);
setParameterEx6.AddEvent(()=>{
	setParameterEv6.Invoke();
});
setParameterEx6.AddChild(SetFloat2);
setParameterIf7.SetCondition(()=>{
	return setParameterFloat1 < 1.0f;
});
setParameterIf7.AddChild(setParameterEx7);
SetFloat1.AddEvent(()=>{
	setParameterFloat1 = 1.5f;
});
SetFloat1.AddChild(setParameterIf6);
SetFloat2.AddEvent(()=>{
	setParameterFloat1 = setParameterFloat2;
});
SetFloat2.AddChild(setParameterIf7);
setParameterEx7.AddEvent(()=>{
	setParameterEv7.Invoke();
});

}
}