using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoosePlayer : MonoBehaviour {

    public GameObject[] players;
    public static ChoosePlayer instance;
    void Awake () {
        instance = this;
       
	}
	
	// Update is called once per frame
	void Update () {

       
    }

    public void ShowPlayer(int index)
    {
        int j = index % players.Length;
        Debug.Log(j);
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
    public void HideAllPlayer()
    {
        foreach (GameObject item in players)
        {
            item.SetActive(false);
        }

    }
  

}
