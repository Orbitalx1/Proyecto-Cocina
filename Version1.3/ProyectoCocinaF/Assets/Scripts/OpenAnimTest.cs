using UnityEngine;

public class OpenAnimTest : MonoBehaviour
{

	public GameObject objetoAEntregar; 
    public Transform puntoDeSpawn;     

    private Animator anim;
    private bool open = false;

    void Start()
    {
        anim = GetComponent<Animator>();

    }

    void OnTriggerEnter(Collider other)
    {
        if (open) return;

        if (other.CompareTag("Player"))
        {
            ControladorObjetos jugador = other.GetComponent<ControladorObjetos>();
			if (jugador != null && jugador.TieneLlave)
			{
				open = true;
				anim.SetBool("open", true);
				jugador.OcultarIconoLlave();
				
				if (objetoAEntregar != null && puntoDeSpawn != null)
    {
        Instantiate(objetoAEntregar, puntoDeSpawn.position, puntoDeSpawn.rotation);
    }
            }
        }
    }
}
