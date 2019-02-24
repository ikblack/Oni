using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data  {

    private int id;
    private float distence;
    private int score;
    

    public float Distence
    {
        get
        {
            return distence;
        }

        set
        {
            distence = value;
        }
    }

    public int Score
    {
        get
        {
            return score;
        }

        set
        {
            score = value;
        }
    }

    public int Id
    {
        get
        {
            return id;
        }

        set
        {
            id = value;
        }
    }

    public Data(int m_id,float m_distence,int m_score)
    {
        this.id = m_id;
        this.distence = m_distence;
        this.score = m_score;
    }
}
