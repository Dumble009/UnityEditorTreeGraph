# UnityEditorTreeGraph
![](https://imgur.com/SkJGTSh.png)

Writing routines of characters or enemies is tiresome work...
This Editor Extension relieve you of such painful works!

# Introduction
Writing code of specific behaviour(such as "transform.position += transform.forward;", "animator.SetTrigger("Attack");") is easy. But writing code of abstract behaviour(such as "Chase the player then attack") is difficult.
With UETG, You can create abstract behaviour of characters without writing redundant "if else if if...".
Abstract behaviours created by UETG don't depend on specific behaviours, so you can attach same abstract behaviour to different characters.
Please see example scenes for more details!

The code which UETG exports is not deterministic. You can create your own compiler of the graph. If you change the compiler of the graph, the exported code will be changed. With a proper compiler, UETG can export MonoBehaviour codes, DOTS code, js codes, lua codes and so on!

Do you want to test your code? UETG can export test codes with GUI!
![](https://imgur.com/Q9ZWJbH.png)
