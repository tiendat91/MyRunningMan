using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TakeDamegeBySlime : MonoBehaviour
{
    [Header("State")]

    [SerializeField] private AIState compareState;
    public LayerMask playerMask;

    Animator myAnimation;
    public StateController myStateController;

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

    }
    void Run()
    {
        myAnimation.Play("slime_idle_run");
    }
    void Idle()
    {
        myAnimation.Play("slime_idle");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("LoseLife");
            collision.GetComponent<Health>().LoseLife();
        }

    }
}
