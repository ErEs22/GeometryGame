using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    public bool resetOnEnable = true;
    public bool looping = false;
    public bool playOnAwake = false;
    public float particleDisableTime = 2.0f;

    private List<ParticleSystem> particles = new List<ParticleSystem>();

    private void Awake() {
        foreach (var particle in GetComponentsInChildren<ParticleSystem>())
        {
            particles.Add(particle);
        }
        InitAllParticles();
    }

    private void OnEnable() {
        foreach(var particle in particles)
        {
            particle.Play();
        }
        Invoke(nameof(Deativate),particleDisableTime);
    }

    private void OnDisable()
    {
        foreach (var particle in particles)
        {
            particle.Stop();
        }
    }

    private void InitAllParticles()
    {
        foreach (var particle in particles)
        {
            var main = particle.main;
            main.loop = looping;
            main.playOnAwake = playOnAwake;
        }
    }

    private void Deativate()
    {
        gameObject.SetActive(false);
    }
}