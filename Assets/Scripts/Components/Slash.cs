using UnityEngine;
using System.Collections;


namespace Assets.Scripts.Components
{
	

	public class Slash: CustomComponentBase {
		public float slashDistance = 3f;
		public static bool canSlash = false;
        public  bool slashing = false;

		public void SlashAttack(float Dir){

			if (!canSlash) {
				return;
			}

            RaycastHit hit;
			Vector3 RayDir = new Vector3 (Dir, 0f, 0f);

			if (Physics.Raycast (transform.position, RayDir, out hit, slashDistance))
			{
				if (hit.transform.tag == "Destructable") {
					hit.transform.gameObject.GetComponent<CanDestroy> ().dealDamage = 1f;
                    slashing = true;
                    IEnumerator slashWaitTime = Slashing(0.1f);
                    StartCoroutine(slashWaitTime);
                }
			}
		}

        IEnumerator Slashing(float slashTime)
        {
            yield return new WaitForSeconds(slashTime);
            slashing = false;
        }

		public bool GetCanSlash(){
			return canSlash;
		}

	}
}