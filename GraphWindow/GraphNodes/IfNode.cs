using UnityEngine.Events;
using UnityEngine;
using System.Collections.Generic;
using XNode;

public class IfNode : BaseNode
{
   public string conditionName;

   override public void Test(List<Node> nodes){
      base.Test(nodes);

      if(string.IsNullOrEmpty(nodeName)){
         if(string.IsNullOrEmpty(conditionName)){
            Debug.LogError(nodeName + ": Condition name is empty.");
         }else{
            bool isConditionExist = false;

            foreach(Node node in nodes){
               if(node is ConditionNode c){
                  if(c.nodeName == conditionName){
                     isConditionExist = true;
                     break;
                  }
               }
            }

            if(!isConditionExist){
               Debug.LogError(nodeName + ": Condition name \""+conditionName+"\" doesn't exist.");
            }
         }

         if(!this.GetOutputPort("output").IsConnected){
            Debug.LogAssertion(nodeName + ": This If node doesn't have any children.");
         }
      }
   }
}
