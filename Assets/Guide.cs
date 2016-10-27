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
    private RelicManager myMan;
    private GameObject Player;
    private Light Lighton;

    // Use this for initialization
    IEnumerator Start()
    {
        yield return new WaitForSeconds(.05f);
        Lighton = GetComponent<Light>();
        Player = CharController.Instance.transform.gameObject;
        myMan = Player.GetComponent<RelicManager>();

        if (GuideBehavior == GuideType.WallJump)
        {
            Lighton.color = Color.green;
        }

        if (GuideBehavior == GuideType.DoubleJump)
        {
            Lighton.color = Color.yellow;
        }

        if (GuideBehavior == GuideType.Dash)
        {
            Lighton.color = Color.red;
        }

        if (GuideBehavior == GuideType.Slash)
        {
            Lighton.color = Color.cyan;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject == Player)
        {
            if (GuideBehavior == GuideType.DoubleJump)
            {
                if (myMan.jumpRelic == true)
                {
                    Lighton.range = 3;
                }
            }
        
            if (GuideBehavior == GuideType.WallJump)
            {
                if (myMan.wallJumpRelic == true)
                {
                    Lighton.range = 3; ;
                }
            }
            if (GuideBehavior == GuideType.Dash)
            {
                if (myMan.dashRelic == true)
                {
                    Lighton.range = 3;
                }
            }
            if (GuideBehavior == GuideType.Slash)
            {
                if (myMan.slashRelic == true)
                {
                    Lighton.range = 3;
                }
            }
        }
    }
    void OnTriggerExit(Collider col)
    {
        Lighton.range = 2;
    }
}