using UnityEngine;

public class TerrainZone : MonoBehaviour
{
    public enum TerrainType
    {
        Sticky,
        Icy,
        Windy
    }

    [SerializeField]
    private TerrainType terrainType;

    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement player = other.GetComponent<PlayerMovement>();

        if (player != null)
        {
            player.SetTerrain(terrainType);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerMovement player = other.GetComponent<PlayerMovement>();

        if (player != null)
        {
            player.SetNormalMovement();
        }
    }
}