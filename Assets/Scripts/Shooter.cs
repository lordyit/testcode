using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    private string targetTag = "target";

    [SerializeField] Animator shootAnim;
    [SerializeField] Camera myCam;


    [Header("Particles")]
    [SerializeField] ParticleSystem blast;

    void Shoot()
    {
        if (Input.GetButton("Fire1"))
        {
            shootAnim.SetTrigger("shoot");
            shootAnim.SetBool("shoot", true);
            RaycastHit hit;

            if (Physics.Raycast(myCam.transform.position, myCam.transform.forward, out hit))
            {
                if (hit.collider.CompareTag(targetTag))
                {
                    blast.transform.position = hit.point;
                    blast.Play();
                    UIManager.Instance.UpdateHitsCount(1);
                    hit.collider.gameObject.SetActive(false);
                    UIManager.Instance.CheckCubesList();
                }
            }
        }
        else
        {
            shootAnim.SetBool("shoot", false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        Shoot();
    }
}
