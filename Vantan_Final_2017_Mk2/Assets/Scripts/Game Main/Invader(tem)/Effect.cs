using UnityEngine;
using System.Collections;

//エフェクト
public class Effect : MonoBehaviour
{
    public bool isParticle;
    public float time;


    void Start()
    {
        //もしisParticleならエフェクトの時間で消える
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
}
