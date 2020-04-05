using UnityEngine;
using XNode;

[CreateNodeMenu("Comment")]
public class CommentNode : Node
{
    [SerializeField, TextArea(3, 5)]
    string comment;
}
