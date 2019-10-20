public class EnemyBehaviour:BehaviourTreeComponent{
[UnityEngine.SerializeField]
int attack_distance=0;
[UnityEngine.SerializeField]
int current_distance=0;
[UnityEngine.SerializeField]
UnityEngine.Events.UnityEvent attack_event;
[UnityEngine.SerializeField]
UnityEngine.Events.UnityEvent chase_event;
override public void MakeTree(){
base.MakeTree();
BT_Root root = new BT_Root();
behaviourTree = new BehaviourTree(root);
BT_Selector selector = new BT_Selector();
root.AddChild(selector);
BT_If chase_branch = new BT_If();
chase_branch.SetCondition(()=>{return current_distance>=attack_distance;});
selector.AddChild(chase_branch);
BT_Execute chase= new BT_Execute();
chase.AddEvent(()=>{chase_event.Invoke();});
chase_branch.AddChild(chase);
BT_Execute attack= new BT_Execute();
attack.AddEvent(()=>{attack_event.Invoke();});
selector.AddChild(attack);
attack.AddChild(chase);
}
}