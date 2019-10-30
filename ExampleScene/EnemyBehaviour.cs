public class EnemyBehaviour:BehaviourTreeComponent{
[UnityEngine.SerializeField]
public bool IsFound = false;[UnityEngine.SerializeField]
public bool IsAttackable = false;[UnityEngine.SerializeField]
public bool IsMoveable = false;[UnityEngine.SerializeField]
public bool IsEscape = false;[UnityEngine.SerializeField]
public UnityEngine.Events.UnityEvent attack_event;[UnityEngine.SerializeField]
public UnityEngine.Events.UnityEvent chase_event;[UnityEngine.SerializeField]
public UnityEngine.Events.UnityEvent patrol_event;[UnityEngine.SerializeField]
public UnityEngine.Events.UnityEvent attackablecheck_event;[UnityEngine.SerializeField]
public UnityEngine.Events.UnityEvent moveablecheck_event;[UnityEngine.SerializeField]
public UnityEngine.Events.UnityEvent escapecheck_event;[UnityEngine.SerializeField]
public UnityEngine.Events.UnityEvent escape_event;override public void MakeTree(){
base.MakeTree();
BT_Root root = new BT_Root();
behaviourTree = new BehaviourTree(root);BT_Execute moveableCheck = new BT_Execute();
moveableCheck.AddEvent(()=>{
	moveablecheck_event.Invoke();
});root.AddChild(moveableCheck);
BT_Selector selector1 = new BT_Selector();moveableCheck.AddChild(selector1);
BT_If if_found = new BT_If();
if_found.SetCondition(()=>{
	return IsFound;
});selector1.AddChild(if_found);
BT_Selector selector2 = new BT_Selector();if_found.AddChild(selector2);
BT_Execute esacpe_check = new BT_Execute();
esacpe_check.AddEvent(()=>{
	escapecheck_event.Invoke();
});selector2.AddChild(esacpe_check);
BT_If If_escape = new BT_If();
If_escape.SetCondition(()=>{
	return IsEscape && IsMoveable;
});esacpe_check.AddChild(If_escape);
BT_Execute escape = new BT_Execute();
escape.AddEvent(()=>{
	escape_event.Invoke();
});If_escape.AddChild(escape);
BT_Execute attackable_check = new BT_Execute();
attackable_check.AddEvent(()=>{
	attackablecheck_event.Invoke();
});selector2.AddChild(attackable_check);
BT_If If_attack = new BT_If();
If_attack.SetCondition(()=>{
	return IsAttackable;
});attackable_check.AddChild(If_attack);
BT_Execute attack = new BT_Execute();
attack.AddEvent(()=>{
	attack_event.Invoke();
});If_attack.AddChild(attack);
BT_If If_chase = new BT_If();
If_chase.SetCondition(()=>{
	return IsMoveable;
});selector2.AddChild(If_chase);
BT_Execute chase = new BT_Execute();
chase.AddEvent(()=>{
	chase_event.Invoke();
});If_chase.AddChild(chase);
BT_If if_patrol = new BT_If();
if_patrol.SetCondition(()=>{
	return IsMoveable;
});selector1.AddChild(if_patrol);
BT_Execute patrol = new BT_Execute();
patrol.AddEvent(()=>{
	patrol_event.Invoke();
});if_patrol.AddChild(patrol);
}
}