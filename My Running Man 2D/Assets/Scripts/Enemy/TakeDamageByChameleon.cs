using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamageByChameleon : MonoBehaviour
{
    [Header("State")]

    [SerializeField] private AIState replaceState;
    public LayerMask playerMask;
    public float rayLenght;
    Animator myAnimation;
    public StateController myStateController;
    bool PlayerActive = false;
    bool isAttack = false;
    private void Start()
    {
        myAnimation = GetComponent<Animator>();

    }
    private void Update()
    {
        if (GameObject.Find("Player(Clone)") != null)
        {
            PlayerActive = true;
        }
        else
        {
            PlayerActive = false;
        }
        if (DetectPlayer(myStateController) == true && PlayerActive == true)
        {
            Attack();
            isAttack = true;
            Debug.Log("Attack");
        }
        if (DetectPlayer(myStateController) == false && PlayerActive == false)
        {
            Idle();
            isAttack = false;
        }
        if (DetectPlayer(myStateController) == false && PlayerActive == true)
        {
            Idle();
            isAttack = false;
        }
        if (isAttack == true)
        {

            myStateController.TransitionToState(replaceState);
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
    void Attack()
    {

        myAnimation.Play("Chameleon-Attack");
    }
    void Idle()
    {

        myAnimation.Play("Chameleon-Idle");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && DetectPlayer(myStateController) == true)
        {

            Debug.Log("LoseLife");
            collision.GetComponent<Health>().LoseLife();
        }

    }

}
