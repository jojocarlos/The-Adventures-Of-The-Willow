using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ignorecolliderphysics : MonoBehaviour
{

    void Update(){Physics.IgnoreLayerCollision(0, 4);}
   
}