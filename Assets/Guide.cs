using UnityEngine;
using System.Collections;
using Assets.Scripts.Components;
using Assets.Scripts.Character;

    public enum GuideType{
        DoubleJump,
        WallJump,
        Dash,
        Slash
    }

public class Guide : MonoBehaviour
{

    public GuideType GuideBehavior;
    RelicManager myMan;
    GameObject Player;
    Light Lighton;


    // Use this for initialization
    void Start()
    {
        Lighton = GetComponent<Light>();
        Player = CharController.Instance.transform.gameObject;
        myMan = Player.GetComponent<RelicManager>();

        Lighton.enabled = false;
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject == Player)
        {
            if (GuideBehavior == GuideType.DoubleJump)
            {
                if (myMan.jumpRelic == true)
                {
                    Lighton.enabled = true;
                    GetComponent<Light>().color = Color.green;
                }
            }
        
            if (GuideBehavior == GuideType.WallJump)
            {
                if (myMan.wallJumpRelic == true)
                {
                    Lighton.enabled = true;
                    GetComponent<Light>().color = Color.yellow;
                }
            }
            if (GuideBehavior == GuideType.Dash)
            {
                if (myMan.dashRelic == true)
                {
                    Lighton.enabled = true;
                    GetComponent<Light>().color = Color.red;
                }
            }
            if (GuideBehavior == GuideType.Slash)
            {
                if (myMan.slashRelic == true)
                {
                    Lighton.enabled = true;
                    GetComponent<Light>().color = Color.cyan;
                }
            }
        }
    }
    void OnTriggerExit(Collider col)
    {
        Lighton.enabled = false;
    }
}