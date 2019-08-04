using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastController : MonoBehaviour
{
    public Transform bombTransform;
    public List<ParticleSystem> blastParticles;
    public List<ParticleSystem> shockwaveParticles;

    public float flashIntensity = 2.0f;
    public float flashRange = 10.0f;
    public float flashDimDropoff = 0.05f;

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
        StartCoroutine(CreateExplosionLightFlash());
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

    IEnumerator CreateExplosionLightFlash()
    {
        GameObject lightGameObject = new GameObject("Explosion Flash");
        Light flash = lightGameObject.AddComponent<Light>();
        flash.type = LightType.Point;
        flash.intensity = flashIntensity;
        flash.range = flashRange;
        flash.transform.position = blastOrigin;

        while (flash.intensity > .05f)
        {
            yield return null;
            flash.intensity = Mathf.Lerp(flash.intensity, 0, flashDimDropoff);
        }

        Destroy(lightGameObject);
    }
}
