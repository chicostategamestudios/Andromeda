using UnityEngine;
using System.Collections;

public class CreditsString : MonoBehaviour {

	public string gameCredits;

	void Start () {
		myCredits ();
	}


	//Where the game credits are written
	void myCredits(){
		gameCredits = "there are\t(Pause*)\nso many\nnames. Look\nat how\n\t\t\tTHIS IS A SECTION\nmany names\nthere are.\nMy favorite\nname is\nthe name\nwith five\nletters in\nthe name.\nBecause the\nword name\nhas five\nletters in\nit. How\nmany lines\ndo I\nneed to\ntest? I\nthink this\nmany will work.";
	}
}
