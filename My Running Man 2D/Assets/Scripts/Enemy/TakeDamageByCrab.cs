using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamageByCrab : MonoBehaviour
{
    [Header("State")]

    [SerializeField] private AIState compareState;
    public LayerMask playerMask;

    Animator myAnimation;
    public StateController myStateController;
    bool PlayerActive = false;
    bool isAttack = false;

    // Start is called before the first frame update
    void Start()
    {
        myAnimation = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (compareState == myStateController.currentState)
        {

            Attack();
            isAttack = true;
        }
        else
        {
            Idle();
            isAttack = false;

        }
    }
    void Attack()
    {
        myAnimation.Play("crab_attack");
    }
    void Idle()
    {
        myAnimation.Play("crab_idle");
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
