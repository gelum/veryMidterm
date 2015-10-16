using UnityEngine;
using System.Collections;

public class global_control : MonoBehaviour {

	int curPlayer, numPlayers;
	int[] points;
	string[] names;

	bool redSlain, blueSlain, greenSlain;

	bool lastRound;
	int[] playersWithMaxPoints;
	int numPlayersWithMaxPoints;

	public Rigidbody test_0;
	public Rigidbody test_1;
	public Rigidbody test_2;
	public Rigidbody test_head;
	public Rigidbody test_tail;
	public Rigidbody test_wing;

	// Use this for initialization
	void Start () {

		// [TODO]: placeholder inits
		numPlayers = 2;
		curPlayer = 0;

		points = new int[ numPlayers];
		for ( int i = 0; i < numPlayers; i++)
		{
			points[ i] = 0;
		}

		names = new string[ numPlayers];
		names [0] = "Mat";
		names [1] = "Alexi";

		redSlain = false;
		blueSlain = false;
		greenSlain = false;

		lastRound = false;

		playersWithMaxPoints = new int[ numPlayers];
		numPlayersWithMaxPoints = 0;

		Instantiate (test_0, new Vector3 (0, 7, 3), Quaternion.identity);
		Instantiate (test_1, new Vector3 (2, 7, 5), Quaternion.identity);
		Instantiate (test_2, new Vector3 (-2, 7, 5), Quaternion.identity);
		Instantiate (test_head, new Vector3 (-2, 7, 1), Quaternion.identity);
		Instantiate (test_wing, new Vector3 (0, 7, -1), Quaternion.identity);
		Instantiate (test_tail, new Vector3 (2, 7, 1), Quaternion.identity);
	}

	void nextTurn()
	{
		// need to flag the final round
		int maxPoints = 0;
		if (points [curPlayer] >= 40) {
			maxPoints = points [curPlayer];
			lastRound = true;
		}

		// attempting to go past the last turn of the game, catch this
		if (lastRound && (curPlayer + 1) == numPlayers) {
			// find who has the most points
			for ( int i = 0; i < numPlayers; i++) {
				if ( points [i] == maxPoints) {
					playersWithMaxPoints [ numPlayersWithMaxPoints] = i;
					numPlayersWithMaxPoints++;
				}
				else if ( points [i] > maxPoints){
					numPlayersWithMaxPoints = 1;
					playersWithMaxPoints[ 0] = i;
				}
			}
		}

		curPlayer = (curPlayer + 1) % numPlayers;

		redSlain = false;
		greenSlain = false;
		blueSlain = false;
	}

	void onPressEndTurn()
	{
		// play any relevant animations
		nextTurn ();
	}

	void onPressHuntRed()
	{

	}

	void onPressHuntGreen()
	{

	}

	void onPressHuntBlue()
	{

	}

	void resetDragonStatus()
	{
		blueSlain = false;
		redSlain = false;
		greenSlain = false;

		// play animations on buttons
	}

	public void succededHunt( string colour)
	{
		// mark dragons as slain and increment points
		if (colour == "blue") {
			blueSlain = true;
			points[ curPlayer] += 2;
		} else if (colour == "green") {
			greenSlain = true;
			points[ curPlayer] += 4;
		} else if (colour == "red") {
			redSlain = true;
			points[ curPlayer] += 6;
		}

		// if all 3 dragons slain this turn, reset all dragons
		if (blueSlain && redSlain && greenSlain) {
			blueSlain = false;
			greenSlain = false;
			redSlain = false;

			// play relevant animations between
		}
	}

	// Update is called once per frame
	void Update () {
	
	}


}
