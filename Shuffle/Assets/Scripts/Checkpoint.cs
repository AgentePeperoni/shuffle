using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Checkpoint : MonoBehaviour
{
    public bool CheckpointReached { get; set; } = false;

    public Transform startPosition;
    public Action[] actions;
    public int frames;
}
