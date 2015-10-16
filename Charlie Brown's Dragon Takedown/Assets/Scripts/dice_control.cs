using UnityEngine;
using System.Collections;

public class dice_control : MonoBehaviour {

	string[] warriorFaces;		// faces showing for warrior die
	string[] dragonFaces;		// faces showing for dragon die
	int updatesThisPhase;		// count the die that are finished rolling
								// technically updates because this is dealt with
								// in update methods called by dice
	int numKeepDie;				// number of dragon die we are keeping
								// need to know this to change updatesThisPhase
	int numDeadWarriors;

	int numWarriors;
	string huntedColour;

	bool headFound, tailFound, wingFound;

    // Use this for initialization
    void Start () {
		updatesThisPhase = 0;
		numKeepDie = 0;
		numDeadWarriors = 0;

		headFound = false;
		tailFound = false;
		wingFound = false;

		numWarriors = 3;
		huntedColour = "lol";

		warriorFaces = new string[3];
		dragonFaces = new string[3];
    }
	
	// Update is called once per frame
	void Update () {
        
    }

	// called to being hunting a dragon
	void beginHunt( string colour)
	{
		huntedColour = colour;
		numKeepDie = 0;
		nextPhase ();
	}

	void nextPhase()
	{
		updatesThisPhase = 0;

		// enable all die -- keep die are still marked as keep so no need to worry
		GameObject[] dice = GameObject.FindGameObjectsWithTag ("Die");

		foreach (GameObject die in dice) {
			die.GetComponent<mouse_flick>().enableMe();
		}	
	}

	// called by the die once they are rolled
	public void updateWarriorFaces( int index, string face)
	{
		if (index >= 0 && index < 3) {
			warriorFaces [index] = face;
			updatesThisPhase++;
		}

		// call assessPhase to see if we're done the phase
		assessPhase ();
	}

	// called by the die once they are rolled
	public void updateDragonFaces( string subtype, string face, mouse_flick sender)
	{
		// find appropriate index
		// current mapping: head, tail, wing to 0, 1, 2 rsp.
		// [TODO]: this functionality can be accomplished with enum type
		int index;

//		Debug.Log ("updateDragonStatus:");
//		Debug.Log ("Subtype: " + subtype + "; Face: " + face);

		switch (subtype) {
		case "head":
			index = 0;
			break;
		case "tail":
			index = 1;
			break;
		case "wing":
			index = 2;
			break;
		default:
			index = 3;
			break;
		}

		if (index != 3) {
			dragonFaces [index] = face;
			updatesThisPhase++;

			if ( face == "head" || face == "wing" || face == "tail") { 
				sender.setKeep( true);
			}
		}

		// call assessPhase to see if we're done the phase
		assessPhase ();
	}

	// method to determine if the phase is over or not
	void assessPhase()
	{
		Debug.Log ("Assessing phase... updates: " + updatesThisPhase);

		if (updatesThisPhase >= ( 6 - numKeepDie - numDeadWarriors)) {	// can never be too sure
			// phase is over, calculate winner

			Debug.Log ("Phase Over.");

			// initialize counters
			// mountains are ignored
			int numFires = 0;
			int numShields = 0;
			int numAxes = 0;

			// parse warrior results & count
			foreach ( string face in warriorFaces){
				if ( face == "shield")
				{
					numShields++;
				}
				else if ( face == "fire")
				{
					numFires++;
				}
				else if ( face == "axe")
				{
					numAxes++;
				}
			}

			// parse dragon results & count
			foreach ( string face in dragonFaces){
				if ( face == "fire")
				{
					numFires++;
				}
				else if ( face == "head")
				{
					headFound = true;
					numKeepDie++;
				}
				else if ( face == "wing")
				{
					wingFound = true;
					numKeepDie++;
				}
				else if ( face == "tail")
				{
					tailFound = true;
					numKeepDie++;
				}
			}

			// empty array to avoid spillover data
			for ( int i = 0; i < 3; i++)
			{
				warriorFaces[ i] = "";
				dragonFaces[ i] = "";
			}

			// check results

			// more fires than shields means dead warriors
			if ( numFires > numShields)
			{
				int numDestroyed = numFires - numShields;
				destroyWarriorDie (numDestroyed);
			}

			// dragon destroyed. good.
			if ( headFound && wingFound && tailFound && numAxes > 0)
			{
				succededHunt();
			}	
			else if ( numWarriors <= 0)
			{
				failedHunt();
			}
			else
			{
				nextPhase();
			}

		}
	}

	public void warriorDestroyedAtIndex( int theIndex)
	{
		warriorFaces [theIndex] = "destroyed";
	}

	// method to deal with player succeding this hunt
	// attribute points based on dragon
	void succededHunt()
	{
		Debug.Log ("A winner is you!");

		this.gameObject.GetComponent<global_control> ().succededHunt ( huntedColour);
	}

	// method to deal with players failing a hunt and thus a turn
	// go to next players turn
	void failedHunt()
	{
		Debug.Log ("fck u");
	}

	// method to deal with destruction of warrior die
	// should prefer die that are not locked
	void destroyWarriorDie( int howMany)
	{
		Debug.Log ("Need to kill " + howMany + " warriors.");
		numWarriors -= howMany;
		numDeadWarriors += howMany;

		GameObject[] dice = GameObject.FindGameObjectsWithTag ("Die");

		int needToDestroy = howMany;
		foreach (GameObject die in dice) {
			if (die.GetComponent<mouse_flick>().type == "warrior" && needToDestroy > 0)
			{
				needToDestroy--;
				Destroy( die);
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
