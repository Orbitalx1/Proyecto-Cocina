using UnityEngine;

public class ControladorSonidoPasos : MonoBehaviour
{
    public AudioSource audioSource;
    private int jugadoresCaminando = 0;

    public void EmpezarCaminar()
    {
        jugadoresCaminando++;
        if (jugadoresCaminando == 1)
        {
            audioSource.Play();
        }
    }

    public void DejarDeCaminar()
    {
        jugadoresCaminando = Mathf.Max(0, jugadoresCaminando - 1);
        if (jugadoresCaminando == 0)
        {
            audioSource.Stop();
        }
    }
}
