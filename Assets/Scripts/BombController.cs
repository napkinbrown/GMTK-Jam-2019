using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BombController : MonoBehaviour
{
    public UnityEvent exploadEvent;

    public AudioSource explosionSound;

    public bool hasExploaded = false;

    public float radius = 5.0F;
    public float power = 10.0F;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E) && !hasExploaded)
        {
            Explode();
        }
    }

    private void Explode() {
        hasExploaded = true;
        exploadEvent.Invoke();
        explosionSound.Play();


        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            if (hit.gameObject.tag == "Bomb")
                continue;

            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
        }

    }

    public void onBombPickup()
    {
        Destroy(this.gameObject);
    }

}
