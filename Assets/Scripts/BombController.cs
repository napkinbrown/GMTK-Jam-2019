using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public float radius = 5.0F;
    public float power = 10.0F;
    private GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        GameObject gmObject = GameObject.FindGameObjectWithTag("GameManager");
        if (gmObject != null)
            gm = gmObject.GetComponent<GameManager>();
        else
            Debug.Log("Could not find game manager!");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            Explode();
        }
    }

    private void Explode() {
        gm.bombExploded = true;
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
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
