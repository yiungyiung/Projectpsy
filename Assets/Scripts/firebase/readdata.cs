using System;
using System.Collections;
using System.Collections.Generic;
using Firebase;
using Firebase.Database;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class readdata : MonoBehaviour
{
    [SerializeField]
    public TMP_Dropdown tmpDropdown;

    public firebase fb;

    public IEnumerable<DataSnapshot> alldata;

    List<DataSnapshot> childlastdata = new List<DataSnapshot>();

    [SerializeField]
    string name = "";
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            fb.ReadName(name, OnReadNameComplete);
        }
    }

    private void OnReadNameComplete(IEnumerable<DataSnapshot> childrenList)
    {
        tmpDropdown.ClearOptions();
        alldata = childrenList;
        if (alldata != null)
        {
            List<string> options = new List<string>();
            childlastdata.Clear();
            tmpDropdown.options.Clear();
            foreach (var childSnapshot in alldata)
            {
                string entryId = childSnapshot.Key;
                string entryData = childSnapshot.Value.ToString();
                tmpDropdown.options.Add(new TMP_Dropdown.OptionData(entryId));
                childlastdata.Add (childSnapshot);
            }

            tmpDropdown.captionText.text = tmpDropdown.options[0].text;
            tmpDropdown.value = 0; // optional
            tmpDropdown.Select(); // optional
            tmpDropdown.RefreshShownValue();
        }
    }

   public void OnDropdownValueChanged(int index)
    {
        if (index >= 0 && index < childlastdata.Count)
        {
            Debug.Log("Selected index: " + index);

            // Access the corresponding DataSnapshot from the child list
            DataSnapshot selectedSnapshot = childlastdata[index];
            foreach (var grandChildSnapshot in childlastdata[index].Children)
                {
                    string dataEntryId = grandChildSnapshot.Key;
                    string dataEntry = grandChildSnapshot.Value.ToString();
                    Debug.Log(dataEntry);
                }
            // Now you can use 'selectedSnapshot' as needed
        }
    }
}
