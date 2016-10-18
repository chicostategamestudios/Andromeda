using UnityEngine;
using System.Collections;

    public enum GuideType{
        DoubleJump,
        WallJump,
        Dash,
        Slash
    }

public class Guide : MonoBehaviour
{

    public GuideType GuideBehavior;
    public GameObject Player;

    // Use this for initialization
    void Start()
    {
        Player.GetComponent<Relics>();
    }
    void Update()
    {
        
        if (GuideBehavior == GuideType.DoubleJump)
        {
            GetComponent<Light>().color = Color.green;
        }

        if (GuideBehavior == GuideType.WallJump)
        {
            GetComponent<Light>().color = Color.yellow;
        }

        if (GuideBehavior == GuideType.Dash)
        {
            GetComponent<Light>().color = Color.red;
        }

        if (GuideBehavior == GuideType.Slash)
        {
            GetComponent<Light>().color = Color.cyan;
        }
    }
/*.PlayerMovement _movement;
Components.RelicManager _relics;
Components.Slash _slash;
Components.Health _health;
Components.Death _death;
*/}