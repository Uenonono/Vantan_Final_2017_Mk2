using UnityEngine;
using System.Collections;

public class Reflection : MonoBehaviour
{
    ReflectionProbe probe;

    public GameObject subCam;

    void Start()
    {
        this.probe = GetComponent<ReflectionProbe>();
    }


    void Update()
    {
        this.probe.transform.position = new Vector3(
            transform.position.x,
            transform.position.y,
            transform.position.z);

        probe.RenderProbe();
    }
}
