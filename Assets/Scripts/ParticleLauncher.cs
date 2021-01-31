using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleLauncher : MonoBehaviour
{

    public ParticleSystem particleLauncher;

    private Material m_Material;
    private ParticleSystemRenderer psr;

    public void EmitParticles()
    {
        particleLauncher.Play();
    }

    public void ParticlesJump()
    {
        particleLauncher.Emit(1);
    }


    public void EmitParticlesDifMaterial() // no se esta usando, no pilla el material
    {

        var ps = GetComponent<ParticleSystem>();
        m_Material = GetComponent<Renderer>().material;
        psr = GetComponent<ParticleSystemRenderer>();



        particleLauncher.Play();
    }

}
