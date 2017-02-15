using UnityEngine;
using System.Collections;

//エフェクト
public class Effect : MonoBehaviour
{
    public bool isParticle;     //もしチェックが入ってればエフェクトの時間で消える
    public float time;


    void Start()
    {
        //エフェクトの時間で消える 入ってなければ時間設定
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
