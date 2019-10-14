public class EnemyBehaviour:BehaviourTreeBase{
[UnityEngine.SerializeField]
int attack_distance=0;
[UnityEngine.SerializeField]
int current_distance=0;
[UnityEngine.SerializeField]
UnityEngine.Events.UnityEvent attack_event;
[UnityEngine.SerializeField]
UnityEngine.Events.UnityEvent chaice_event;
override public void MakeTree(){
base.MakeTree();
BT_Root root = new BT_Root();
behaviourTree = new BehaviourTree(root);
BT_Selector selector = new BT_Selector();
root.AddChild(selector);
BT_If chaice_branch = new BT_If();
selector.AddChild(chaice_branch);
chaice_branch.SetCondition(()=>{return current_distance>=attack_distance;});
BT_Execute chaice= new BT_Execute();
chaice_branch.AddChild(chaice);
chaice.AddEvent(()=>{chaice_event.Invoke();});
BT_Execute attack= new BT_Execute();
selector.AddChild(attack);
attack.AddEvent(()=>{attack_event.Invoke();});
}
}