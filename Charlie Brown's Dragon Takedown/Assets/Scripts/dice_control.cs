using UnityEngine;
using System.Collections;

public class dice_control : MonoBehaviour {

	bool shouldCheck;			// should we be checking for updoots... might be
								// irrelephant

	string[] warriorFaces;		// faces showing for warrior die
	string[] dragonFaces;		// faces showing for dragon die
	int updatesThisPhase;		// count the die that are finished rolling
								// technically updates because this is dealt with
								// in update methods called by dice

	int numWarriors;
	string huntedColour;

	bool headFound, tailFound, wingFound;

    // Use this for initialization
    void Start () {
		shouldCheck = false;
		updatesThisPhase = 0;

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
		nextPhase ();
	}

	void nextPhase()
	{
		shouldCheck = true;		// is this relevant?
		updatesThisPhase = 0;
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
	public void updateDragonFaces( string subtype, string face)
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

//		Debug.Log ("Index this round: " + index);

		if (index != 3) {
			dragonFaces [index] = face;
			updatesThisPhase++;
		}

		// call assessPhase to see if we're done the phase
		assessPhase ();
	}

	// method to determine if the phase is over or not
	void assessPhase()
	{
		Debug.Log ("Assessing phase... updates: " + updatesThisPhase);

		if (updatesThisPhase >= 6) {	// can never be too sure
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
				}
				else if ( face == "wing")
				{
					wingFound = true;
				}
				else if ( face == "tail")
				{
					tailFound = true;
				}
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

	}

	// method to deal with destruction of warrior die
	// should prefer die that are not locked
	void destroyWarriorDie( int howMany)
	{
		Debug.Log ("Need to kill " + howMany + " warriors.");
		numWarriors -= howMany;
	}

    int getNumSideUp(string[] array, string s) {
        int count = 0;
        for (int i = 0; i < 3; i++)
            if (array[i] == s)//or do I need .equals?
                count++;
        return count;
    }
}
