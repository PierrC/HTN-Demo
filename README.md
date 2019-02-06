# HTN_Demo
This is a demo of a Hierarchical task network.

Created with Unity version 2018.2.10 

The demo is a small game. The objective is to collect 5 items (purple) before the The other agent, or have more items if both get caught. 
The player is the blue agent.
The AI is the green agent.

Player controls:
A,S,D,W: movement
Space: use trap (2 uses)


![alt text](https://github.com/PierrC/HTN-Demo/blob/master/HTNDemoPicture1.png)


There is one item in each alcove, both agent start in random alcoves and must move from alcove to alcove while avoiding the view of
the red agents (yellowish area).
The red agents will patrol the hallways at random speeds (the interval is small). They appear at one of the doors (orange) and will walk towards the other side. Once they reach a door they disapear and will reappear shortlty at a random door.
When the red agents reach the small wall in the middle, it loses its sight while in the wall and 3 things can happend:
  - the agent will continue his partol to the other side
  - the agent will turn around and walk back
  - the agent will disapear and reappear at one of the 4 doors
If the player or the AI is caught they disappear and are no longer able to collect items.
