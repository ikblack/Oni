using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Globe
{
    public static string nextSceneName = "Oni";//以后需要时候可以动态改变
}
public class AsyncLoadScene : MonoBehaviour {
   
    private float loadingSpeed = 1;

    private float targetValue;

    private AsyncOperation operation;
    public GameObject[] img;
    public GameObject ImgLoading;
    private int index;
    private int RecInt=1;
    float time;
    float newtime;
    bool bIsFinish;
    public float setTime=10;
    // Use this for initialization
    void Start () {
      //  PanelMgr.instance.OpenPanel<SharePanel>("");
        StartCoroutine(AsyncLoading());
    }
    IEnumerator AsyncLoading()
    {

        operation = SceneManager.LoadSceneAsync(Globe.nextSceneName);
        //阻止当加载完成自动切换
       operation.allowSceneActivation = false;

        yield return operation;
    }

    public void Changed()
    {

        // int j = index % img.Length;
        if (RecInt > 4)
        {
            return;
        }
        int j = index % img.Length;
        for (int i = 0; i < img.Length; i++)
        {
            if (i == j)
            {
                img[j].SetActive(true);

            }
            else
                img[i].SetActive(false);
        }
        RecInt++;
        index++;
    }

    void Update()
    {
        time = time + Time.deltaTime;
        newtime = newtime + Time.deltaTime;
        if (time > 1)
        {
            Changed();
            time = 0;

        }

        targetValue = operation.progress;

        if (operation.progress >= 0.9f)
        {
            //operation.progress的值最大为0.9
            targetValue = 1.0f;
        }


        if (targetValue<1.0f)
        {
           
        }
        else
        {
            if (bIsFinish)
            {
                operation.allowSceneActivation = true;
            }
            
        }


        if (newtime>setTime)
        {
            for (int i = 0; i < img.Length; i++)
            {
                img[i].SetActive(false);
                ImgLoading.SetActive(false);
                PanelMgr.instance.OpenPanel<StartPanel>("");
            }
        }

    }

   public void FinishLoad()
    {
        bIsFinish = true;
    }
}
