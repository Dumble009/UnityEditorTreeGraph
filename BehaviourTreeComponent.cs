using UnityEngine;
using System;

public class BehaviourTreeComponent : MonoBehaviour
{
    public BehaviourTree behaviourTree;
	public bool isActive = true;
    void Start()
    {
        MakeTree();
    }

    public void Tick(){
        if(behaviourTree != null && isActive){
            behaviourTree.Tick();
        }
    }

    virtual public void MakeTree(){
        
    }

    protected IfEvent UnityEvent2IfEvent(UnityEngine.Events.UnityEvent e){
        object target = e.GetPersistentTarget(0);
        Type t = target.GetType();
        System.Reflection.MethodInfo mi = t.GetMethod(e.GetPersistentMethodName(0));
        IfEvent ie = ()=>{
            object o = mi.Invoke(target, new object[]{});
            return (bool)o;
        };

        return ie;
    }
}
