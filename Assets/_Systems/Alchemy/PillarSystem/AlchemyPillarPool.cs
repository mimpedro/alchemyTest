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
    public List<AlchemyPillar> pool;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
