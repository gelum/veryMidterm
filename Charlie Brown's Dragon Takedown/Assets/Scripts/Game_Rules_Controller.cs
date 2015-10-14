using UnityEngine;
using System.Collections;

public class Game_Rules_Controller : MonoBehaviour {
    string[] warriorDice = new string[3];       //
    string[] blueDragonDice = new string[3];    //All of the game dice faceUps 
    string[] greenDragonDice = new string[3];   //
    string[] redDragonDice = new string[3];     //

    string playerTurn;
    string hunting;
    bool winner;
    bool diceRolled;
    bool diceSet;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        int availableDice;
        winner = false;
        hunting = "No";

        //game in progress
        if (winner == false) {
            //turn in progress
            availableDice = 3;
            if (availableDice > 0)
            {
                //TODO ask which dragon the player wants to hunt

                //hunting dragon
                if (hunting != "no")
                {
                    
                    if (diceRolled == true)
                    {
                        //Fire vs Shield
                        int shields = getNumSideUp(warriorDice, "Shield");
                        int fires;
                        if (hunting == "Blue") { fires = getNumSideUp(blueDragonDice, "Fire"); }        //
                        else if (hunting == "Green") { fires = getNumSideUp(greenDragonDice, "Fire"); } //each case for hunting
                        else if (hunting == "Red") { fires = getNumSideUp(redDragonDice, "Fire"); }     //
                        else fires = 0;                                                                 //

                        if (shields >= fires && diceSet == false) {
                            availableDice -= (fires - shields);
                            diceSet = true;
                        }
                        else {
                            if (hunting == "Blue")
                            {
                                if (getNumSideUp(blueDragonDice, "Head") > 0) { }
                                //TODO lock die
                                if (getNumSideUp(blueDragonDice, "Wings") > 0) { }
                                //TODO lock die
                                if (getNumSideUp(blueDragonDice, "Tail") > 0) { }
                                //TODO lock die
                            }
                        }
                    }
                }
            }
        }
    }
    int getNumSideUp(string[] array, string s) {
        int count = 0;
        for (int i = 0; i < 3; i++)
            if (array[i] == s)//or do I need .equals?
                count++;
        return count;
    }
}
