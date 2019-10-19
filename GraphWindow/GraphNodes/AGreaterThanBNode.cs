using UnityEngine.Events;
using UnityEngine;
using System.Collections.Generic;
using XNode;

public class AGreaterThanBNode : BaseNode
{
   public string A, B;
   public bool EqualAsTrue;

   override public void Test(List<Node> nodes){
      base.Test(nodes);

      if(!string.IsNullOrEmpty(nodeName)){
         if(string.IsNullOrEmpty(A) || string.IsNullOrEmpty(B)){
            Debug.LogError(nodeName + ": Parameter name is empty.");
         }else{
            bool isAExist = false, isBExist = false;

            foreach(Node node in nodes){
               if(node is IntNode i){
                  if(i.nodeName == A){
                     isAExist = true;
                  }
                  if(i.nodeName == B){
                      isBExist = true;
                  }
               }else if(node is FloatNode f){
                   if(f.nodeName == A){
                       isAExist = true;
                   }
                   if(f.nodeName == B){
                       isBExist = true;
                   }
               }
            }

            if(!isAExist){
                Debug.LogError(nodeName + ": Parameter name \""+A+"\" doesn't exist.");
            }else if(!isBExist){
                Debug.LogError(nodeName + ": Parameter name \""+B+"\" doesn't exist.");
            }
         }

         if(!this.GetOutputPort("output").IsConnected){
            Debug.LogAssertion(nodeName + ": This If node doesn't have any children.");
         }
      }
   }

   override public string GetCode(){
      string code = "BT_If " + nodeName + " = new BT_If();\n";
      string ope = EqualAsTrue ? ">=" : ">";
      code += nodeName + ".SetCondition(()=>{return "+A+ope+B+";});\n";
      return code;
   }
}
