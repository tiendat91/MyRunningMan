using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamageByDuck : MonoBehaviour
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

            Idle();
            isAttack = true;
        }
        else
        {

            isAttack = false;
            Idle();
        }
    }
    void Run()
    {
        myAnimation.Play("duck_attack");
    }
    void Idle()
    {
        myAnimation.Play("duck_idle");
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
