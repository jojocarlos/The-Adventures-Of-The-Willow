using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveObjectPositionID : MonoBehaviour, IDataPersistence
{
    [SerializeField] private string id;
    private Vector3 thisObjectPosition;
    private Vector3 previousPosition;
    private bool isMoved;

    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    void Start()
    {
        if(isMoved)
        {
            transform.position = thisObjectPosition;
        }
        previousPosition = transform.position;
    }
    void Update()
    {
        thisObjectPosition = transform.position; 

        if (transform.position != previousPosition)
        {
            isMoved = true;
        }
        previousPosition = transform.position;
    }

    public void LoadData(GameData data)
    {
        data.ObjectsPosition.TryGetValue(id, out thisObjectPosition);
        data.ObjectsPositionBool.TryGetValue(id, out isMoved);
    }

    public void SaveData(GameData data)
    {
        if (data.ObjectsPosition.ContainsKey(id))
        {
            data.ObjectsPosition.Remove(id);
        }
        data.ObjectsPosition.Add(id, thisObjectPosition); 

        if (data.ObjectsPositionBool.ContainsKey(id))
        {
            data.ObjectsPositionBool.Remove(id);
        }
        data.ObjectsPositionBool.Add(id, isMoved);
    }
}
