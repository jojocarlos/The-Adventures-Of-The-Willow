using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusicStop : MonoBehaviour
{
    void Awake()
    {
        BGMusic.instance.destroyObject();
    }
}
