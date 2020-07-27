using System;
using System.Collections;
using UnityEngine;

public class RotationSystem : MonoBehaviour
{

    [SerializeField]
    private Transform root;

    [SerializeField, Range(0.1f, 20)]
    private float speed;

    private void Start()
    {
        StartCoroutine(RotateRoot());
    }

    private IEnumerator RotateRoot()
    {
        while(true)
        {
            Vector3 rotation = this.root.eulerAngles;

            rotation.y += speed * Time.deltaTime;

            this.root.eulerAngles = rotation;

            yield return new WaitForEndOfFrame();
        }
    }
}
