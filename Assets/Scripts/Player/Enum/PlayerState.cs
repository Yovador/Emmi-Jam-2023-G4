public enum PlayerState
{
    Deactivated=0, //No Control, no movement  
    Dead=-1, // Character is dead. Negative value to allow checking Alive state by doing PlayerState > 0
    Free=1, //Can move, can interact, can die, In total player control
    Interacting=2, // Is Interacting with an object
    Grabbing=3, //Is grabbing something
    StartingLevel=4, //Is in his level starting animation
    EndingLevel=5, //Is in his level ending animation
}

