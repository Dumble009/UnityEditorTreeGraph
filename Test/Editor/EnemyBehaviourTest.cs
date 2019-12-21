using BT;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class EnemyBehaviourTest
{
BehaviourTree behaviourTree;
Dictionary<string, bool> calledFlag;
[UnityEngine.SerializeField]
public bool IsFound = false;[UnityEngine.SerializeField]
public bool IsAttackable = false;[UnityEngine.SerializeField]
public bool IsMoveable = false;[UnityEngine.SerializeField]
public bool IsEscape = false;[UnityEngine.SerializeField]
public bool IsGotDamage = false;[UnityEngine.SerializeField]
public UnityEngine.Events.UnityEvent attack_event = new UnityEngine.Events.UnityEvent();
[UnityEngine.SerializeField]
public UnityEngine.Events.UnityEvent chase_event = new UnityEngine.Events.UnityEvent();
[UnityEngine.SerializeField]
public UnityEngine.Events.UnityEvent patrol_event = new UnityEngine.Events.UnityEvent();
[UnityEngine.SerializeField]
public UnityEngine.Events.UnityEvent attackablecheck_event = new UnityEngine.Events.UnityEvent();
[UnityEngine.SerializeField]
public UnityEngine.Events.UnityEvent moveablecheck_event = new UnityEngine.Events.UnityEvent();
[UnityEngine.SerializeField]
public UnityEngine.Events.UnityEvent escape_event = new UnityEngine.Events.UnityEvent();

[SetUp]
public void Init()
{
BT_Root root = new BT_Root();behaviourTree = new BehaviourTree(root);BT_Selector selector1 = new BT_Selector();
BT_Execute attack = new BT_Execute();
BT_Execute chase = new BT_Execute();
BT_Execute patrol = new BT_Execute();
BT_Selector selector2 = new BT_Selector();
BT_If if_found = new BT_If();
BT_If If_attack = new BT_If();
BT_Execute attackable_check = new BT_Execute();
BT_Execute moveableCheck = new BT_Execute();
BT_If if_patrol = new BT_If();
BT_If If_chase = new BT_If();
BT_If If_escape = new BT_If();
BT_Execute escape = new BT_Execute();
BT_Interrupt DamageInterrupt = new BT_Interrupt();
BT_Execute ResetDamageFlag = new BT_Execute();
BT_Execute ActivateEscape = new BT_Execute();
BT_Timing EscapeTimer = new BT_Timing(behaviourTree, true, false);
BT_Interrupt EscapeResetInterrupt = new BT_Interrupt();
BT_Execute EscapeResetter = new BT_Execute();
BT_Execute SetFound = new BT_Execute();
root.AddChild(moveableCheck);

selector1.AddChild(if_found);
selector1.AddChild(if_patrol);
attack.AddEvent(()=>{
	attack_event.Invoke();
});
chase.AddEvent(()=>{
	chase_event.Invoke();
});
patrol.AddEvent(()=>{
	patrol_event.Invoke();
});

selector2.AddChild(If_escape);
selector2.AddChild(attackable_check);
selector2.AddChild(If_chase);
if_found.SetCondition(()=>{
	return IsFound;
});
if_found.AddChild(selector2);
If_attack.SetCondition(()=>{
	return IsAttackable;
});
If_attack.AddChild(attack);
attackable_check.AddEvent(()=>{
	attackablecheck_event.Invoke();
});
attackable_check.AddChild(If_attack);
moveableCheck.AddEvent(()=>{
	moveablecheck_event.Invoke();
});
moveableCheck.AddChild(selector1);
if_patrol.SetCondition(()=>{
	return IsMoveable;
});
if_patrol.AddChild(patrol);
If_chase.SetCondition(()=>{
	return IsMoveable;
});
If_chase.AddChild(chase);
If_escape.SetCondition(()=>{
	return IsEscape && IsMoveable;
});
If_escape.AddChild(escape);
escape.AddEvent(()=>{
	escape_event.Invoke();
});
DamageInterrupt.SetCondition(()=>{
	return IsGotDamage;
});
behaviourTree.AddInterrupt(DamageInterrupt);
DamageInterrupt.AddChild(SetFound);
ResetDamageFlag.AddEvent(()=>{
	IsGotDamage = false;
});
ResetDamageFlag.AddChild(ActivateEscape);
ActivateEscape.AddEvent(()=>{
	IsEscape = true;
});
ActivateEscape.AddChild(EscapeTimer);
EscapeTimer.SetTimingCreator(()=>{
	return new Timer(EscapeResetInterrupt, 3.0);
});
EscapeTimer.AddChild(moveableCheck);
EscapeResetInterrupt.SetCondition(()=>{
	return false;
});
behaviourTree.AddInterrupt(EscapeResetInterrupt);
EscapeResetInterrupt.AddChild(EscapeResetter);
EscapeResetter.AddEvent(()=>{
	IsEscape = false;
});
EscapeResetter.AddChild(moveableCheck);
SetFound.AddEvent(()=>{
	IsFound = true;
});
SetFound.AddChild(ResetDamageFlag);


calledFlag = new Dictionary<string, bool>();
calledFlag.Add("attack", false);
attack_event.AddListener(()=>{
	calledFlag["attack"] = true;
});calledFlag.Add("chase", false);
chase_event.AddListener(()=>{
	calledFlag["chase"] = true;
});calledFlag.Add("patrol", false);
patrol_event.AddListener(()=>{
	calledFlag["patrol"] = true;
});calledFlag.Add("attackable_check", false);
attackablecheck_event.AddListener(()=>{
	calledFlag["attackable_check"] = true;
});calledFlag.Add("moveableCheck", false);
moveablecheck_event.AddListener(()=>{
	calledFlag["moveableCheck"] = true;
});calledFlag.Add("escape", false);
escape_event.AddListener(()=>{
	calledFlag["escape"] = true;
});
}

[Test]
public void Test1()
{
IsFound=false;
IsAttackable=true;
IsMoveable=true;
IsEscape=true;
IsGotDamage=false;

behaviourTree.Tick();
Assert.AreEqual(true, calledFlag["patrol"]);
var keys = calledFlag.Keys.ToArray();
foreach(var key in keys)
{
	calledFlag[key] = false;
}
}[Test]
public void Test2()
{
IsFound=true;
IsAttackable=true;
IsMoveable=true;
IsEscape=false;
IsGotDamage=false;

behaviourTree.Tick();
Assert.AreEqual(true, calledFlag["attack"]);Assert.AreEqual(true, IsAttackable && !IsEscape);
var keys = calledFlag.Keys.ToArray();
foreach(var key in keys)
{
	calledFlag[key] = false;
}
}[Test]
public void Test3()
{
IsFound=true;
IsAttackable=false;
IsMoveable=true;
IsEscape=true;
IsGotDamage=false;

behaviourTree.Tick();
Assert.AreEqual(true, calledFlag["escape"]);
var keys = calledFlag.Keys.ToArray();
foreach(var key in keys)
{
	calledFlag[key] = false;
}
}
}