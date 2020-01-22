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
        PORTAL,
        BUTTON
    }

    [SerializeField] 
    private EnumWorldObjectType type;
}
