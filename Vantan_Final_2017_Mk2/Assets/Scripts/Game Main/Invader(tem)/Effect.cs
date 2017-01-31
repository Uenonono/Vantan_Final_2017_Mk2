using UnityEngine;
using System.Collections;

//エフェクト
public class Effect : MonoBehaviour
{
    public float time;
    public bool isParticle;

    void Start()
    {
        ParticleSystem particle = GetComponent<ParticleSystem>();
        if (isParticle)
        {
            Destroy(this.gameObject, particle.duration);
        }
        else
        {
            Destroy(this.gameObject, time);
        }
    }


    void Update()
    {

    }
}
