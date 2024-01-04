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
        var time = DateTime.Now;
Debug.Log(time);

// Format the DateTime according to the specified pattern
string tim = time.ToString("M-d-yyyy h:mm:ss tt");

// Now tim should have the formatted timestamp
Debug.Log(tim);

        string userId = name; // Replace with your method to obtain user identifier
        DatabaseReference userReference = reference.Child(userId);
        DatabaseReference timeReference = userReference.Child(" "+tim+" "+age+" "+gender+" "+SceneManager.GetActiveScene().name+" ");
        
        foreach (string s in yourList)
        {
            string entryId = System.Guid.NewGuid().ToString();
            timeReference.Child(entryId).SetValueAsync(s);
        }
    }
    
 public void ReadName(string name,Action<IEnumerable<DataSnapshot>> onComplete)
    {
        reference.Child(name).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                IEnumerable<DataSnapshot> childrenList = snapshot.Children;
                onComplete?.Invoke(childrenList);
                // Iterate through the children of the snapshot
                /*foreach (var childSnapshot in snapshot.Children)
                {   
                    string entryId = childSnapshot.Key;
                    string entryData = childSnapshot.Value.ToString();

                    Debug.Log(entryId);
                foreach (var grandChildSnapshot in childSnapshot.Children)
                {
                    string dataEntryId = grandChildSnapshot.Key;
                    string dataEntry = grandChildSnapshot.Value.ToString();
                    Debug.Log(dataEntry);
                }
                Debug.Log("*************************************************************");
            }
            */
            }
        });
    }

}