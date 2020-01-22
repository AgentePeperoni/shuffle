using System.Collections.Generic;

using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Checkpoint : MonoBehaviour
{
    public Action[] actions;
}
