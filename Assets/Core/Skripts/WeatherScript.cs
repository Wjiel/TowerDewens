using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class WeatherScript : MonoBehaviour
{
    [Header("ParticleSystem")]
    [SerializeField] private ParticleSystem RainParticl;
    [SerializeField] private ParticleSystem fireflies;
    [SerializeField] private Light sun;

    [Header("Lamps")]
    [SerializeField] private GameObject[] GlassForLamps;
    [SerializeField] private Light[] lightOfLamp;

    [Header("other")]
    [SerializeField] private GameObject pumpkin;
    public bool _sun = false;

    [Header("Sounds")]
    [SerializeField] private AudioSource RainSonds;
    void Start()
    {
        StartCoroutine(Sun());
        StartCoroutine(Rain());
        StartCoroutine(Fireflies());
    }
    private void Update()
    {
        if (_sun)
        {
            sun.intensity -= 0.07f * Time.deltaTime;

            if (sun.intensity <= 0)
            {
                pumpkin.transform.rotation = Quaternion.Lerp(pumpkin.transform.rotation, Quaternion.Euler(-90, -146, 0), Time.deltaTime);

                for (int i = 0; i < GlassForLamps.Length; i++) { GlassForLamps[i].GetComponent<Renderer>().material.color = new Color(0, 0, 0, 0); }

                if (lightOfLamp[0].intensity < 1)
                    for (int i = 0; i < GlassForLamps.Length; i++) { lightOfLamp[i].intensity += 0.1f * Time.deltaTime; }
            }
        }
        else if (_sun == false && sun.intensity <= 1)
        {
            sun.intensity += 0.07f * Time.deltaTime;

            pumpkin.transform.rotation = Quaternion.Lerp(pumpkin.transform.rotation, Quaternion.Euler(-90, 0, 0), Time.deltaTime);

            for (int i = 0; i < GlassForLamps.Length; i++) { GlassForLamps[i].GetComponent<Renderer>().material.color = new Color(0, 0, 0, 0.9f); }

            if (lightOfLamp[0].intensity > 0)
                for (int i = 0; i < GlassForLamps.Length; i++) { lightOfLamp[i].intensity -= 0.1f * Time.deltaTime; }
        }
    }
    private IEnumerator Fireflies()
    {
        if (RainParticl.isPlaying != true && _sun == true)
        {
            yield return new WaitForSeconds(5);
            fireflies.Play();
        }
        else
        {
            fireflies.Stop();
        }
        yield return new WaitForSeconds(5);
        StartCoroutine(Fireflies());
    }
    private IEnumerator Sun()
    {
        while (true)
        {
            yield return new WaitForSeconds(100);
            _sun = true;
            yield return new WaitForSeconds(100);
            _sun = false;
        }
    }
    private IEnumerator Rain()
    {
        if (fireflies.isPlaying != true)
        {
            yield return new WaitForSeconds(Random.Range(60, 300));
            RainSonds.Play();
            RainParticl.Play();
            yield return new WaitForSeconds(Random.Range(30, 60));
            RainParticl.Stop();
            RainSonds.Stop();
        }
        yield return new WaitForSeconds(5);
        StartCoroutine(Rain());
    }

}
