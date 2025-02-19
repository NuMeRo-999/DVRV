 using Fusion;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [Networked] public int ColorIndex { get; set; } // Sync player color across network
    public Material[] playerMaterials;
    public Camera Camera;

    public override void Render()
    {
        GetComponent<Renderer>().material = playerMaterials[ColorIndex]; // Apply correct material
    }
    
    public override void Spawned()
    {
        if (HasStateAuthority)
        {
            Camera = Camera.main;
            GetComponent<PlayerMovement>().SetCamera(Camera);
        }
    }
}