using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyFollow : MonoBehaviour
{
    public Transform KeyFollowPoint;
    public Spirit followingSpirit;

    public List<Key> followingKeys = new List<Key>();

    void Update()
    {
        foreach (Key key in followingKeys)
        {
            if (key != null)
            {
                key.transform.position = KeyFollowPoint.position;
            }
        }
    }

    public void AddKey(Key key)
    {
        followingKeys.Add(key);
    }
    public void RemoveKey(Key key)
    {
        followingKeys.Remove(key);
    }
}

