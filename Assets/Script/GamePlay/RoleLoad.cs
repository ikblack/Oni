using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleLoad : MonoBehaviour
{
    public GameObject Player1;
    public GameObject Player2;
    public GameObject Player3;
    public static RoleLoad instance;
    // Use this for initialization
    void Awake()
    {
        Load();
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }
   public void Load()
    {
        int RoleID = PlayerPrefs.GetInt("Role");
        if (RoleID == 0)
        {
            //Player1 = Resources.Load("Player1") as GameObject;
            Player1.SetActive(true);
            Player2.SetActive(false);
            Player3.SetActive(false);
            Player1.tag = "Player";
            Player2.tag = "OniYama";
            Player3.tag = "OniYama";
            Player1.GetComponent<OniPlayerController>().enabled = true;
            Player2.GetComponent<OniPlayerController>().enabled = false;
            Player3.GetComponent<OniPlayerController>().enabled = false;
        }
        else if (RoleID == 1)
        {
            //Player2= Resources.Load("Player2") as GameObject;
            Player2.SetActive(true);
            Player1.SetActive(false);
            Player3.SetActive(false);
            Player1.tag = "OniYama";
            Player2.tag = "Player";
            Player3.tag = "OniYama";
            Player2.GetComponent<OniPlayerController>().enabled = true;
            Player1.GetComponent<OniPlayerController>().enabled = false;
            Player3.GetComponent<OniPlayerController>().enabled = false;
        }
        else if (RoleID == 2)
        {
            //Player2 = Resources.Load("Player3") as GameObject;
            Player3.SetActive(true);
            Player1.SetActive(false);
            Player2.SetActive(false);
            Player1.tag = "OniYama";
            Player2.tag = "OniYama";
            Player3.tag = "Player";
            Player3.GetComponent<OniPlayerController>().enabled = true;
            Player2.GetComponent<OniPlayerController>().enabled = false;
            Player1.GetComponent<OniPlayerController>().enabled = false;
           
        }
        if (TagMark.instance)
        {
            TagMark.instance.ChangPlayer();
            TagMark.instance.Player.GetComponent<OniPlayerController>().step = PLAYERSTEP.RUN;
        }
       
    }
}
