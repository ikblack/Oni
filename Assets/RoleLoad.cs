using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleLoad : MonoBehaviour
{
    public GameObject Player1;
    public GameObject Player2;
    public GameObject Player3;
    // Use this for initialization
    void Awake()
    {
        int RoleID = PlayerPrefs.GetInt("Role");
        if (RoleID == 0)
        {
            //Player1 = Resources.Load("Player1") as GameObject;
            Player1.SetActive(true);
            Player2.SetActive(false);
            Player3.SetActive(false);
            Player1.tag = "Player";
            Player2.tag ="OniYama";
            Player3.tag = "OniYama";
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
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
