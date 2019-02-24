using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

public class SaveTool : MonoBehaviour {
    public GameObject[] obj;
    public string[] names;
    public Dictionary<string, XmlElement> roleDic = new Dictionary<string, XmlElement>();
    //public Dictionary<string, XmlElement> readRoleDic = new Dictionary<string, XmlElement>();
    public List<float> list = new List<float>();
    public Vector3 pos;
    public Vector3 rot;
    public List<Vector3> posList = new List<Vector3>();
    public List<Vector3> rotList = new List<Vector3>();
    public static SaveTool instance;
    void Awake () {
      
        instance = this;
       // obj[6] = TagParamter.target.gameObject;
       // Debug.Log(obj[6].name);
        //ReadTask();//读取任务进度
        //showXml();//读取位置信息
    }
    void Start()
    {

      
        //ReadTask();//读取任务进度
       // showXml();//读取位置信息
    }
    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 200, 30), "CREATE XML"))
        {
            createXml();
        }

        if (GUI.Button(new Rect(10, 170, 200, 30), "SHOW XML"))
        {
           
            showXml();
        }

    }
    public void ReadTask()
    {
        Debug.LogError("ONCE");
        string TASKNAME = PlayerPrefs.GetString("Role");
        string TASKINDEX = PlayerPrefs.GetString("taskIndex");
        Debug.LogError("TASKNAME       " + TASKNAME);
        Debug.LogError("TASKINDEX      " + TASKINDEX);
        string[] str = TASKNAME.Split(',');
        string[] str1 = TASKINDEX.Split(',');
        int i = 0;
        foreach (var item in str)
        {
            if (item != "")
            {
                Debug.LogError("              " + item);
                i++;
            }


        }
    }
    public void createXml()
    {
        string filepath = Application.persistentDataPath + @"/data.xml";
        //if(!File.Exists (filepath))
        //{	
        XmlDocument xmlDoc = new XmlDocument();
        XmlElement root = xmlDoc.CreateElement("broard");
       


        for (int i = 0; i <6; i++)
        {
            XmlElement elmNewRole = xmlDoc.CreateElement("Role");
            XmlElement elmNewDis = xmlDoc.CreateElement("Distence");
            XmlElement elmNewScore= xmlDoc.CreateElement("Score");
            elmNewRole.SetAttribute("name", names[i]);
           
            roleDic.Add(names[i], elmNewRole);


            XmlElement dis= xmlDoc.CreateElement("val");
            dis.InnerText = obj[i].transform.position.x.ToString();
            XmlElement position_Y = xmlDoc.CreateElement("y");
            position_Y.InnerText = obj[i].transform.position.y.ToString();
            XmlElement position_Z = xmlDoc.CreateElement("z");
            position_Z.InnerText = obj[i].transform.position.z.ToString();

            Debug.Log(obj[i].transform.localEulerAngles);
            XmlElement rotation_X = xmlDoc.CreateElement("x");
            rotation_X.InnerText = obj[i].transform.localEulerAngles.x.ToString();
            XmlElement rotation_Y = xmlDoc.CreateElement("y");
            Debug.Log(obj[i].transform.localEulerAngles.y.ToString());
            rotation_Y.InnerText = obj[i].transform.localEulerAngles.y.ToString();
            XmlElement rotation_Z = xmlDoc.CreateElement("z");
            rotation_Z.InnerText = obj[i].transform.localEulerAngles.z.ToString();

            //elmNewPos.AppendChild(position_X);
            //elmNewPos.AppendChild(position_Y);
            //elmNewPos.AppendChild(position_Z);
            //elmNewRot.AppendChild(rotation_X);
            //elmNewRot.AppendChild(rotation_Y);
            //elmNewRot.AppendChild(rotation_Z);

            //elmNewRole.AppendChild(elmNewPos);
            //elmNewRole.AppendChild(elmNewRot);
        }


        for (int i = 0; i < roleDic.Count; i++)
        {
            root.AppendChild(roleDic[obj[i].name]);
        }

        //root.AppendChild(elmNewRole);
        xmlDoc.AppendChild(root);
        xmlDoc.Save(filepath);
        Debug.Log(filepath);
        roleDic.Clear();
        Debug.Log("createXml OK!");
        //}
    }


    public  void showXml()
    {
        Debug.Log("xml");
        string filepath = Application.persistentDataPath + @"/data.xml";
        if (File.Exists(filepath))
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filepath);
            XmlNodeList nodeList = xmlDoc.SelectSingleNode("transforms").ChildNodes;

            foreach (XmlElement xe in nodeList)
            {
                Debug.Log("name :" + xe.GetAttribute("name") + "       ----       " + xe.GetAttribute("level"));
                if (xe.GetAttribute("name")== "Hero")
                {
                    int lv;
                    int.TryParse(xe.GetAttribute("level").ToString(),out lv) ;
                    int at;
                    int.TryParse(xe.GetAttribute("attack").ToString(), out at);
                    int df;
                    int.TryParse(xe.GetAttribute("defand").ToString(), out df);
                    int bj;
                    int.TryParse(xe.GetAttribute("baoji").ToString(), out bj);
                    int bs;
                    int.TryParse(xe.GetAttribute("baoshang").ToString(), out bs);
                    int sa;
                    int.TryParse(xe.GetAttribute("study").ToString(), out sa);
                    int ca;
                    int.TryParse(xe.GetAttribute("communicaty").ToString(), out ca);
                    int pa;
                    int.TryParse(xe.GetAttribute("profession").ToString(), out pa);
                    int cat;
                    int.TryParse(xe.GetAttribute("comprehensive").ToString(), out cat);
                  
                }
                foreach (XmlElement x1 in xe.ChildNodes)
                {
                    if (x1.Name == "position")
                    {
                        foreach (XmlElement item in x1.ChildNodes)
                        {

                            float index;
                            float.TryParse(item.InnerText.ToString(), out index);
                            list.Add(index);
                            Debug.Log(item.InnerText + "    -----      ");
                        }
                        pos = new Vector3(list[0], list[1], list[2]);

                        list.Clear();
                        posList.Add(pos);
                        Debug.Log(pos);
                    }
                    if (x1.Name == "rotation")
                    {
                        foreach (XmlElement item in x1.ChildNodes)
                        {
                            float index;
                            float.TryParse(item.InnerText.ToString(), out index);
                            list.Add(index);
                            Debug.Log(item.InnerText + "    -----      ");
                        }
                        rot = new Vector3(list[0], list[1], list[2]);
                        list.Clear();
                        rotList.Add(rot);
                        Debug.Log(rot);
                    }
                }
            }
        }
        for (int i = 0; i < obj.Length; i++)
        {
            obj[i].transform.position = posList[i];
            obj[i].transform.localEulerAngles = rotList[i];
        }
      //  obj[7].transform.localEulerAngles = new Vector3(0,180,0);
    }


    public void deleteXML()
    {
        string filepath = Application.persistentDataPath + @"/my.xml";
        File.Delete(filepath);
    }
}
