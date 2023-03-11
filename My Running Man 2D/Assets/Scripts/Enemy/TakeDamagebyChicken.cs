using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamagebyChicken : MonoBehaviour
{
    [Header("State")]
    [SerializeField] private AIState replaceState;
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

            Run();
            isAttack = true;
        }
        else
        {

            isAttack = false;
            Idle();
        }
        if (isAttack == true)
        {

            myStateController.TransitionToState(replaceState);
        }
    }
    void Run()
    {
        myAnimation.Play("chicken_run");
    }
    void Idle()
    {
        myAnimation.Play("chicken_idle");
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
