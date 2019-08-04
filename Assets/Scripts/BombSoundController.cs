using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSoundController : MonoBehaviour
{
    public AudioSource explosionSound;

    // Start is called before the first frame update
    void Start()
    {
        explosionSound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.explosionSound.isPlaying) Destroy(this.gameObject);
    }
}
