using System.Collections.Generic;
using XNode;
using UnityEngine;
public class EditorTreeTester{
    static public void RunTest(List<Node> nodes){
        bool isRootExist = false;
        
        foreach(Node node in nodes){
            if(node is IBTGraphNode bt){
                bt.Test(nodes);
            }
            if(node is RootNode){
                if(!isRootExist){
                    isRootExist = true;
                }else{
                    Debug.LogError("This graph has too many root nodes.");
                }
            }
        }

        if(!isRootExist){
            Debug.LogError("Root node doesn't exist. A tree needs one root node.");
        }
    }
}
