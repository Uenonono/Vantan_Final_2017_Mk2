using UnityEngine;
using System.Collections;

//エフェクト
public class Effect : MonoBehaviour
{
    void Start()
    {
        ParticleSystem particle = GetComponent<ParticleSystem>();

        Destroy(this.gameObject, particle.duration);
    }


    void Update()
    {

    }
}
