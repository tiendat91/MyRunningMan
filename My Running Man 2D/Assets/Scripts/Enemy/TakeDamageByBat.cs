using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamageByBat : MonoBehaviour
{
    [Header("State")]

    private Collider2D playerCollider;
    public LayerMask playerMask;
    public float radius;
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
        if (DetectPlayer(myStateController) == true)
        {
            isAttack = true;
        }
    }

    private bool DetectPlayer(StateController controller)
    {
        playerCollider = Physics2D.OverlapCircle(controller.transform.position, radius, playerMask);
        controller.SetRediusDetectionValues(radius, controller.transform.position, playerCollider);

        if (playerCollider)
        {
            controller.Target = playerCollider.GetComponent<HeroMotor>();
            return true;
        }

        controller.Target = null;
        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && isAttack == true)
        {

            Debug.Log("LoseLife");
            collision.GetComponent<Health>().LoseLife();
        }

    }
}
