using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class painarray : MonoBehaviour
{   
    int i=0;
    bool up=false;
    public List<string> data = new List<string>();
    [SerializeField]
    TMP_Text Text;
    [SerializeField]
    TMP_InputField register_username;
    [SerializeField]
    TMP_InputField age;
    [SerializeField]
    TMP_InputField gender;
    public firebase fb;
    public void singledata(float init)
    {   
        i=0;
        string s = "acute pain at "+init+"°";
        data.Add(s);
        up=true;
    }

    public void rangedata(float init,float last)
    {
        i=0;
        string s = "ranged pain at "+init+"° to "+last+"°";
        data.Add(s);
        up=true;
    }

    void Update()
    {
       if (up)
       {
        up=false;
        string s="";
        if(data.Count>5)
        {
            for (int i=data.Count-1;i>=data.Count-5;i--)
            {
                s=s+data[i]+"\n";
            }
        }
        else{
            for (int i=data.Count-1;i>=0;i--)
            {
                s=s+data[i]+"\n";
            }
        }
            Text.text=s;
       }
    }

    public void senddata()
    {   GameObject[] gameObjects;
        gameObjects = GameObject.FindGameObjectsWithTag("Finish");
        foreach (GameObject gameObject in gameObjects)
        {
            string s = "Pain Location at  "+gameObject.transform.position;
             data.Add(s);
        }
        fb.AddDataEntry(register_username.text,data,age.text,gender.text);
        data.Clear();
        Text.text="";
    }
}


