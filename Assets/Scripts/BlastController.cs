using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastController : MonoBehaviour
{
    public Transform bombTransform;
    public List<ParticleSystem> blastParticles;
    public List<ParticleSystem> shockwaveParticles;

    private List<ParticleSystem> toDestroy = new List<ParticleSystem>(); 
    private Vector3 blastOrigin;

    void Update()
    {
        if(toDestroy.Count > 0)
        {
            foreach(ParticleSystem particle in toDestroy)
            {
                if (particle != null && !particle.IsAlive()) Destroy(particle.gameObject);
            }
        }
    }

    public void CreateExplosion()
    {
        this.blastOrigin = bombTransform.position;
        StartCoroutine(CreateExplosionCoroutine());
    }

    IEnumerator CreateExplosionCoroutine()
    {
        List<Object> particlesToDestory = new List<Object>();

        foreach (ParticleSystem particle in blastParticles)
        {
            toDestroy.Add(Instantiate(particle, blastOrigin, Quaternion.Euler(Vector3.zero)));
        }

        foreach (ParticleSystem particle in shockwaveParticles)
        {
            toDestroy.Add(Instantiate(particle, blastOrigin, Quaternion.Euler(new Vector3(90, 0, 0))));
        }

        yield return null;
    }
}
