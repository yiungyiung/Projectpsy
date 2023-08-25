using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firebase : MonoBehaviour
{
   void Start() {
    // Get the root reference location of the database.
    DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
  }

  
}
