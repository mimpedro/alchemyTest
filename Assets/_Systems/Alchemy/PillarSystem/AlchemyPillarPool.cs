using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlchemyPillarPool : MonoBehaviour
{
    #region Singleton logic
    public static AlchemyPillarPool instance { get; private set; }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("There's more than one AlchemyPillarPool present!");
            Destroy(this);
        }
    }
    #endregion
    public int poolSize;
    public GameObject[] pillarTypes;
    //public List<AlchemyPillar> pool;

    void Start()
    {
        
    }

    public GameObject GetPillarBySurfaceTag(string surfaceTag)
    {
        switch (surfaceTag)
        {
            case "Stone":
                return pillarTypes[1];
            default:
                return pillarTypes[0];
        }
    }

    public GameObject CreatePillar(Vector3 position, Vector3 normal, string tag)
    {
        GameObject newPillarObj = Instantiate(GetPillarBySurfaceTag(tag));
        newPillarObj.transform.position = position;
        newPillarObj.transform.up = normal;
        return newPillarObj;
    }
}
