using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTreeBase : MonoBehaviour
{
    BehaviourTree behaviourTree;
    void Start()
    {
        MakeTree();
    }

    public void Tick(){
        if(behaviourTree != null){
            behaviourTree.Tick();
        }
    }

    virtual public void MakeTree(){

    }
}
