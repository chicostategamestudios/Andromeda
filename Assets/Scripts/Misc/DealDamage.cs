using UnityEngine;
using System.Collections;
using Assets.Scripts.Components;
public class DealDamage : MonoBehaviour {
	public float damage;

    void OnTriggerEnter(Collider col){

        if (col.gameObject.tag == "Player") {
            Debug.Log("damage");

            col.GetComponent<Health> ().TakeDamage (damage);
		}
	
	}
}
