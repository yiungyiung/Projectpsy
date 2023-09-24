using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using System;
using UnityEngine.SceneManagement;
public class firebase : MonoBehaviour
{
 private DatabaseReference reference;
    
    private void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            reference = FirebaseDatabase.DefaultInstance.RootReference;
        });
    }
    
    public void AddDataEntry(string name,List<string> yourList,string age,string gender)
    {   
        var time=DateTime.Now;
        Debug.Log(time);
        string tim=time.ToString();
        tim=tim.Replace("/","-");
        string userId = name; // Replace with your method to obtain user identifier
        DatabaseReference userReference = reference.Child(userId);
        DatabaseReference timeReference = userReference.Child(" "+tim+" "+age+" "+gender+" "+SceneManager.GetActiveScene().name+" ");
        
        foreach (string s in yourList)
        {
            string entryId = System.Guid.NewGuid().ToString();
            timeReference.Child(entryId).SetValueAsync(s);
        }
    }
    
}