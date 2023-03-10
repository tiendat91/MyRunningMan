using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamageBySnail : MonoBehaviour
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
        Enemy enemy = myAnimation.GetComponent<Enemy>();
        if (compareState == myStateController.currentState)
        {
            enemy.enabled = false;
            Defense();
            isAttack = true;
        }
        else
        {
            enemy.enabled = true;
            isAttack = false;
            Idle();
        }
    }
    void Defense()
    {
        myAnimation.Play("snail_shield");
    }
    void Idle()
    {
        myAnimation.Play("snail_idle");
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
