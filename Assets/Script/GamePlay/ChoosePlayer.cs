using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoosePlayer : MonoBehaviour {

    public GameObject[] players;
    public static ChoosePlayer instance;
    void Start () {
        instance = this;
       
	}
	
	// Update is called once per frame
	void Update () {

       
    }

    public void ShowPlayer(int index)
    {
        int j = index % players.Length;
        for (int i = 0; i < players.Length; i++)
        {
            if (i == j)
            {
                players[j].SetActive(true);
                LoadData.instance.SaveIdData(j);
            }
            else
                players[i].SetActive(false);
        }
    }

    public void OnGUI()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            ShowPlayer(0);
        }
    }


}
