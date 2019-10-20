using System.Collections.Generic;
using XNode;
using UnityEngine;
public class EditorTreeTester{
    static public void RunTest(List<Node> nodes){
        bool isRootExist = false;
        bool isToomanyRoots = false;

        foreach(Node node in nodes){
            if(node is RootNode){
                if(!isRootExist){
                    isRootExist = true;
                }else{
                    Debug.LogError("This graph has too many root nodes.");
                    isToomanyRoots = true;
                    break;
                }
            }
        }

        if(!isRootExist){
            Debug.LogError("Root node doesn't exist. A tree needs one root node.");
        }else if(!isToomanyRoots){
            foreach(Node node in nodes){
                if(node is IBTGraphNode i){
                    i.Test(nodes);
                }
            }
        }
    }
}
