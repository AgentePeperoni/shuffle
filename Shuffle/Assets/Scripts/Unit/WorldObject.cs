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
        KEY
    }

    [SerializeField] 
    private EnumWorldObjectType type;
}
