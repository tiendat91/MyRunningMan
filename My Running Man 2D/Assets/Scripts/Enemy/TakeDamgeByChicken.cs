using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TakeDamgeByChicken : MonoBehaviour
{
    Animator myAnimation;
    public LayerMask playerMask;
    public float rayLenght;
    public StateController myStateController;

    private void Start()
    {
        myAnimation = GetComponent<Animator>();

    }
    private void Update()
    {
        if (DetectPlayer(myStateController) == true)
        {
            Run();
            Debug.Log("Attack");
        }
        else
        {
            Idle();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<Health>().LoseLife();

        }
    }
    private bool DetectPlayer(StateController controller)
    {
        Vector3 dir = controller.Path.Direction == PathFollow.MoveDirections.RIGHT ? Vector3.right : Vector3.left;
        RaycastHit2D hit = Physics2D.Raycast(controller.transform.position, dir, rayLenght, playerMask);
        controller.DebugRay(rayLenght, controller.transform.position, dir, hit);

        if (hit)
        {
            controller.Target = hit.collider.GetComponent<HeroMotor>();
            return true;
        }

        controller.Target = null;
        return false;
    }
    public void Run()
    {
        myAnimation.Play("chicken_run");
    }
    public void Idle()
    {
        myAnimation.Play("chicken_idle");
    }
}

