using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

public class LoadData : MonoBehaviour
{

    public static LoadData instance;
    public int PlayerID;
    public int Score;
    public int Rank;
    public Dictionary<string, XmlElement> roleDic = new Dictionary<string, XmlElement>();
    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }


    void Update()
    {

    }
    public void SaveIdData(int index)
    {
        PlayerPrefs.SetInt("Role", index);
        //Debug.Log(index);
    }

    public void SavePlayerData(int PlayerID, int Score, int Rank)
    {
        PlayerPrefs.SetInt("Role", PlayerID);
        PlayerPrefs.SetInt("Score", Score);
        PlayerPrefs.SetInt("Rank", Rank);
    }
    public void LoadPlayerData(int PlayerID, int Score, int Rank)
    {
        this.PlayerID = PlayerPrefs.GetInt("Role");
        this.Score = PlayerPrefs.GetInt("Score");
        this.Rank = PlayerPrefs.GetInt("Rank");
    }

    public void createXml(List<Data> data)
    {
        string filepath = Application.persistentDataPath + @"/data.xml";
        //if(!File.Exists (filepath))
        //{	
        XmlDocument xmlDoc = new XmlDocument();
        XmlElement root = xmlDoc.CreateElement("broard");


        //设置节点的属性

        for (int i = 0; i <= data.Count - 1; i++)
        {
            //创建子节点
            XmlElement element = xmlDoc.CreateElement("messages");
            element.SetAttribute("top", (i + 1).ToString());
            XmlElement elementChild0 = xmlDoc.CreateElement("id");
            elementChild0.InnerText = data[i].Id.ToString();
            XmlElement elementChild1 = xmlDoc.CreateElement("dis");
            elementChild1.InnerText = data[i].Distence.ToString();
            XmlElement elementChild2 = xmlDoc.CreateElement("score");
            elementChild2.InnerText = data[i].Score.ToString();
            //把节点一层一层的添加至xml中，注意他们之间的先后顺序，这是生成XML文件的顺序
            element.AppendChild(elementChild0);
            element.AppendChild(elementChild1);
            element.AppendChild(elementChild2);
            root.AppendChild(element);
        }

        xmlDoc.AppendChild(root);

        xmlDoc.Save(filepath);
        Debug.Log(filepath);
        roleDic.Clear();
        Debug.Log("createXml OK!");
        //}
    }

   public void LoadXml()
    {
        int  id;
        float dis;
        int score;
        List<Data> datalist = new List<Data>();
       List<string> arr=new List<string>();
        string filepath = Application.persistentDataPath + @"/data.xml";
        if (File.Exists(filepath))
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filepath);
            XmlNodeList nodeList = xmlDoc.SelectSingleNode("broard").ChildNodes;
            //遍历所有子节点
            foreach (XmlElement xl1 in nodeList)
            {
               // if (xl1.GetAttribute("top") == "1")
                {
                    //继续遍历节点下的子节点
                    foreach (XmlElement xl2 in xl1.ChildNodes)
                    {
                        // Debug.Log(xl2.InnerText);
                                            arr.Add(xl2.InnerText);
}
                    id = int.Parse(arr[0]);
                    dis = float.Parse(arr[1]);
                    score = int.Parse(arr[2]);
                    arr.Clear();
                    print(id+"  "+dis+"  "+score);
                    Data data = new Data(id,dis,score);
                    datalist.Add(data);
                }
               
            }
            foreach (var item in datalist)
            {
                Debug.Log(item.Id+"     "+item.Distence+"       "+item.Score);
            }
           
        }
    }
}
