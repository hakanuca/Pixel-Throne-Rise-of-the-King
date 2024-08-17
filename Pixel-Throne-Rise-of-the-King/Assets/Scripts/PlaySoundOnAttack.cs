using UnityEngine;

public class PlaySoundOnAttack : MonoBehaviour
{
    public AudioSource runSound;  // Assign the AudioSource component with the run sound
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Check if the current animation state is "Main_Character_Run"
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Main_Character_Attack"))
        {
            // Play the sound if it's not already playing
            if (!runSound.isPlaying)
            {
                runSound.Play();
            }
        }
        else
        {
            // Stop the sound if the animation state changes
            if (runSound.isPlaying)
            {
                runSound.Stop();
            }
        }
    }
}
