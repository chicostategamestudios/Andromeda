using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.Components
{


    public class Health : CustomComponentBase
    {
        public float CurStuff = 0f;
        float pointValue;
        List<GameObject> collectedLoot;
        Death myDeathObj;
        public BoxCollider playerCollider;
        bool invincible;

        public void TotalStuff()
        {
            playerCollider = GetComponent<BoxCollider>();
            GameObject[] tempList = GameObject.FindGameObjectsWithTag("Loot");
            float maxStuff = tempList.Length;
            pointValue = 1f / maxStuff;
            collectedLoot = new List<GameObject>();

        }

        void OnTriggerEnter(Collider col)
        {
            if (col.gameObject.tag == "Loot")
            {
                CurStuff += pointValue;
                collectedLoot.Add(col.gameObject);
                col.gameObject.SetActive(false);
            }

        }

        public void TakeDamage(float damage)
        {
            for (int relicsLost = 0; relicsLost < damage; relicsLost++)
            {
                //	GameObject turnOn = collectedLoot [Random.Range (0, collectedLoot.Count)];
                //	collectedLoot.Remove (turnOn);
                //	turnOn.SetActive (true);
                CurStuff -= pointValue;

            }
            if (invincible == false)
            StartCoroutine("Invincible");
        }

        public void Death()
        {
            //	Debug.LogError ("Death is being called on the Health Script, Make sure whatever is calling it is changed to be calling the death script instead");
            if (myDeathObj == null)
            {
                myDeathObj = this.gameObject.GetComponent<Death>();
            }

            myDeathObj.Respawn();

        }

        public void OnParticleCollision(GameObject particle)
        {
            if (particle.name == "LavaSpew")
            {
                TakeDamage(0);
            }
        }

        IEnumerator Invincible()
        {
            invincible = true;
            Debug.Log("yay");
            playerCollider.enabled = false;
            yield return new WaitForSeconds (3f);
            playerCollider.enabled = true;
            invincible = false;
        }
    }
}