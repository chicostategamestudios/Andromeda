using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.Components
{
	public class Health: CustomComponentBase {
		public float CurStuff= 0f;
		float pointValue;
		List<GameObject> collectedLoot;
		Death myDeathObj;

		public static List<Transform> myLoot = new List<Transform> ();

		public void TotalStuff(){
		//	GameObject[] tempList = GameObject.FindGameObjectsWithTag ("Loot");
			float maxStuff = myLoot.Count;
			pointValue = 1f / maxStuff;
			collectedLoot = new List<GameObject> ();
		}

		void OnTriggerEnter(Collider col){
			if (col.gameObject.tag == "Loot") {
				CurStuff += pointValue;
				collectedLoot.Add (col.gameObject);
				col.gameObject.SetActive (false);

				int Active = 0;
				for (int cube = 0; cube < myLoot.Count; cube++) {
					if (myLoot [cube].gameObject.activeInHierarchy) {
						Active++;
					}
				}
			}
		}

		public void TakeDamage(float damage){
			for (int relicsLost = 0; relicsLost < damage; relicsLost++) {
			//	GameObject turnOn = collectedLoot [Random.Range (0, collectedLoot.Count)];
			//	collectedLoot.Remove (turnOn);
			//	turnOn.SetActive (true);
				CurStuff -= pointValue;
			}
		}

		public void Death(){
			//	Debug.LogError ("Death is being called on the Health Script, Make sure whatever is calling it is changed to be calling the death script instead");
			if (myDeathObj == null) {
				myDeathObj = this.gameObject.GetComponent<Death> ();
			}

			myDeathObj.Respawn ();
		}
	}
}