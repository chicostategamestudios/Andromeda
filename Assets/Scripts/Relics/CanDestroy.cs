using UnityEngine;
using System.Collections;

public class CanDestroy : LevelElement {
	public float hp;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	
    public float dealDamage
    {
        set
        {
            hp -= value;
            if(hp <= 0)
            {
                this.DestroyMe();
            }
        }
    }

    public virtual void DestroyMe()
    {
        Destroy(this.gameObject); //default if not overriden into child classes.
    }
}
