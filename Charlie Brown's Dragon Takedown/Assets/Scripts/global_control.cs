﻿using UnityEngine;
using System.Collections;

public class global_control : MonoBehaviour {

	int curPlayer, numPlayers;
	int[] points;
	string[] names;

	bool redSlain, blueSlain, greenSlain;

	bool lastRound;
	int[] playersWithMaxPoints;
	int numPlayersWithMaxPoints;

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

	// Update is called once per frame
	void Update () {
	
	}


}
