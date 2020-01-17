using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldObject : MonoBehaviour
{
    public enum EnumWorldObjectType
    {
        NONE,
        FLOOR,
        CHECKPOINT,
        SPIKES,
        BARREL,
        PIT,
        ENEMY,
        CHEST,
        STEPS,
        DOOR,
        KEY,
        PORTAL
    }

    [SerializeField] 
    private EnumWorldObjectType type;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
