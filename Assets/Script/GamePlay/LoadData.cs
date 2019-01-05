using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadData : MonoBehaviour {

    public static LoadData instance;
    public int PlayerID;
    public int Score;
    public int Rank;

	void Awake () {
        instance = this;
	}
	
	
	void Update () {
       
	}
    public void SaveIdData(int index)
    {
        PlayerPrefs.SetInt("Role", index);
        Debug.Log(index);
    }

    public void SavePlayerData(int PlayerID, int Score, int Rank)
    {
        PlayerPrefs.SetInt("Role", PlayerID);
        PlayerPrefs.SetInt("Score", Score);
        PlayerPrefs.SetInt("Rank", Rank);
    }
    public void LoadPlayerData(int PlayerID,int Score,int Rank)
    {
        this.PlayerID = PlayerPrefs.GetInt("Role");
        this.Score = PlayerPrefs.GetInt("Score");
        this.Rank = PlayerPrefs.GetInt("Rank");
    }
}
