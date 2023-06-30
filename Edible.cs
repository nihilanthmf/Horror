using UnityEngine;

public class Edible : MonoBehaviour
{
    [SerializeField] GameObject piece;
    [SerializeField] ParticleSystem eatingParticles;

    public void Eat()
    {
        if (piece.activeSelf)
        {
            piece.SetActive(false);
        }
        else
        {
            eatingParticles.transform.SetParent(null);
            Destroy(gameObject);
        }
        eatingParticles.Play();
    }
}
