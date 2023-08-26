using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using System;
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
        string userId = name; // Replace with your method to obtain user identifier
        DatabaseReference userReference = reference.Child(userId);
        DatabaseReference timeReference = userReference.Child(""+time+" "+age+" "+gender+" ");
        
        foreach (string s in yourList)
        {
            string entryId = System.Guid.NewGuid().ToString();
            timeReference.Child(entryId).SetValueAsync(s);
        }
    }
    
}