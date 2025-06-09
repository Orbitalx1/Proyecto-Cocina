using UnityEngine;

public class Respawn : MonoBehaviour
{
    public GameObject prefab;
    public float tiempoRespawn = 5f;

    public void IniciarRespawn()
    {
        Invoke(nameof(Respawnear), tiempoRespawn);
    }

    void Respawnear()
    {
        Instantiate(prefab, transform.position, transform.rotation);
    }
}




