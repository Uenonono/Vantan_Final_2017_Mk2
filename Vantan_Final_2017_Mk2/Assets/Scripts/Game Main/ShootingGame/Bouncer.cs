using UnityEngine;
using System.Collections;

//ギミック バウンサー
public class Bouncer : MonoBehaviour
{
    public float speed; //跳ね返す速度


    void OnCollisionEnter(Collision coll)
    {
        this.GetComponent<Rigidbody>().AddForce(
    (transform.right) * speed, ForceMode.VelocityChange);
    }
}
