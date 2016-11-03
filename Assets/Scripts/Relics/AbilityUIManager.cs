using UnityEngine;
using System.Collections;
using Assets.Scripts.Components;
using UnityEngine.UI;
using Assets.Scripts.Character;
using Assets.Scripts.Components;

public class AbilityUIManager : MonoBehaviour {

	RelicManager Manager;
	Transform Player;
	GameObject Slash;
	GameObject DoubleJump;
	GameObject WallJump;
	GameObject Dash;

	void Start () {
		Manager = FindObjectOfType<RelicManager> ();
		Player = CharController.Instance.transform;
		Slash = transform.FindChild ("Canvas/Base/Slash").gameObject;
		DoubleJump = transform.FindChild ("Canvas/Base/Double_Jump").gameObject;
		WallJump = transform.FindChild ("Canvas/Base/Wall_Jump").gameObject;
		Dash = transform.FindChild ("Canvas/Base/Dash").gameObject;
	}
		
	void Update () {

		//Still need to add in the bool that tells whether the ability is available during escape phase
		//Or may need to control this in the actual ability script
		AbilityActiveInactive (Slash, Player.GetComponent<Slash>().GetCanSlash(), Manager.slashRelic, true);
		AbilityActiveInactive (DoubleJump, Player.GetComponent<Jumping>().GetCanDoubleJump(), Manager.jumpRelic, true);
		AbilityActiveInactive (WallJump, Player.GetComponent<Jumping>().GetCanWallJump(), true);
		AbilityActiveInactive (Dash, Player.GetComponent<Dash>().GetCanDash(), Manager.dashRelic, true);
	}

	void AbilityActiveInactive(GameObject abilityIcon, bool isActive, bool abilityUnlocked, bool usableOnThisPhase){
		
		AbilityImagesActiveInactive images = abilityIcon.GetComponent<AbilityImagesActiveInactive> ();
		Image abilityIconImage = abilityIcon.GetComponent<Image> ();

		if (abilityUnlocked) {
			if (isActive && usableOnThisPhase) {
				abilityIconImage.sprite = images.abilityIcons [0];
			} else {
				abilityIconImage.sprite = images.abilityIcons [1];
			}
		} else {
			abilityIconImage.sprite = images.abilityIcons [2];
		}
	}

	//Used for the wall jump Icon because that ability is available (although limited) from the start of the game
	void AbilityActiveInactive(GameObject abilityIcon, bool isActive, bool usableOnThisPhase){

		AbilityImagesActiveInactive images = abilityIcon.GetComponent<AbilityImagesActiveInactive> ();
		Image abilityIconImage = abilityIcon.GetComponent<Image> ();

		if (isActive) {
			abilityIconImage.sprite = images.abilityIcons [0];
		} else {
			abilityIconImage.sprite = images.abilityIcons [1];
		}
	}
}
