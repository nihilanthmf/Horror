using UnityEngine;

public class Radio : MonoBehaviour
{
    AudioSource audioSource;
    [HideInInspector] public bool muted = false;
    [SerializeField] AudioClip[] songs;
    [SerializeField] Player player;
    Animator animator;
    int songIndex = 0;
    float timeSinceSongStarted;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = songs[songIndex];
        audioSource.Play();
        animator = GetComponent<Animator>();

        timeSinceSongStarted = Time.time;
    }

    private void Update()
    {
        animator.SetBool("turnOn", !muted);


        if (muted)
        {
            audioSource.volume = 0;
        }
        else
        {
            audioSource.volume = (2 / Vector3.Distance(transform.position, player.transform.position)) - 0.085f;
        }

        if (Time.time > timeSinceSongStarted + audioSource.clip.length) // if the current song is over
        {
            if (songIndex < songs.Length - 1)
            {
                songIndex++;
            }
            else
            {
                songIndex = 0;
            }

            timeSinceSongStarted = Time.time;

            audioSource.clip = songs[songIndex];
            audioSource.Play();
        }
    }

    public void UnMute()
    {
        muted = false;
    }

    public void Mute()
    {
        muted = true;
    }
}
