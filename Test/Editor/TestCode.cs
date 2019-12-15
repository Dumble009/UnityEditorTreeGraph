using BT;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestCode
{
BehaviourTree behaviourTree;
Dictionary<string, bool> calledFlag;
[UnityEngine.SerializeField]
public bool b = true;[UnityEngine.SerializeField]
public bool b2 = false;[UnityEngine.SerializeField]
public UnityEngine.Events.UnityEvent e;[UnityEngine.SerializeField]
public float f = 0;[UnityEngine.SerializeField]
int i = 0;
[SetUp]
void Init()
{
BT_Root root = new BT_Root();behaviourTree = new BehaviourTree(root);BT_Execute ex1 = new BT_Execute();
BT_Timing fc1 = new BT_Timing(behaviourTree, false, false);
BT_Timing tm1 = new BT_Timing(behaviourTree, false, false);
BT_If if1 = new BT_If();
BT_While wh1 = new BT_While();
BT_Interrupt inter1 = new BT_Interrupt();
BT_Selector sl1 = new BT_Selector();
BT_Sequence sq1 = new BT_Sequence();
BT_Execute sb1 = new BT_Execute();
BT_Execute sf1 = new BT_Execute();
BT_Execute si1 = new BT_Execute();
root.AddChild(ex1);
ex1.AddEvent(()=>{
	e.Invoke();
});
ex1.AddChild(fc1);
fc1.SetTimingCreator(()=>{
	return new FrameCounter(root, 100);
});
fc1.AddChild(tm1);
tm1.SetTimingCreator(()=>{
	return new Timer(root, 10);
});
tm1.AddChild(if1);
if1.SetCondition(()=>{
	return b;
});
if1.AddChild(wh1);
wh1.SetCondition(()=>{
	return b;
});
wh1.AddChild(sl1);
inter1.SetCondition(()=>{
	return b;
});
behaviourTree.AddInterrupt(inter1);
inter1.AddChild(ex1);

sl1.AddChild(sq1);

sq1.AddChild(sb1);
sb1.AddEvent(()=>{
	b = true;
});
sb1.AddChild(sf1);
sf1.AddEvent(()=>{
	f = 10;
});
sf1.AddChild(si1);
si1.AddEvent(()=>{
	i = 10;
});


calledFlag = new Dictionary<string, bool>();
calledFlag.Add("ex1", false);
e.AddListener(()=>{
	calledFlag["ex1"] = true;
});
}

[Test]
public void TestCase1()
{


foreach(var pair in calledFlag)
{
	calledFlag[pair.Key] = false;
}
}[Test]
public void TestCase2()
{


foreach(var pair in calledFlag)
{
	calledFlag[pair.Key] = false;
}
}
}