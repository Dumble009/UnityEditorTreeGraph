using UnityEngine.Events;
using UnityEngine;
using System.Collections.Generic;
using XNode;

public class If_BooleanNode : BaseNode
{
   public string boolName;

   override public void Test(List<Node> nodes){
      base.Test(nodes);

      if(!string.IsNullOrEmpty(nodeName)){
         if(string.IsNullOrEmpty(boolName)){
            Debug.LogError(nodeName + ": Bool name is empty.");
         }else{
            bool isConditionExist = false;

            foreach(Node node in nodes){
               if(node is BoolNode c){
                  if(c.nodeName == boolName){
                     isConditionExist = true;
                     break;
                  }
               }
            }

            if(!isConditionExist){
               Debug.LogError(nodeName + ": Bool name \""+boolName+"\" doesn't exist.");
            }
         }

         if(!this.GetOutputPort("output").IsConnected){
            Debug.LogAssertion(nodeName + ": This If node doesn't have any children.");
         }
      }
   }

   override public string GetCode(string parentName){
      string code = "BT_If " + nodeName + " = new BT_If();\n";
      code += parentName + ".AddChild("+nodeName+");\n";
      code += nodeName + ".SetCondition(()=>{return "+boolName+";});\n";
      return code;
   }
}
