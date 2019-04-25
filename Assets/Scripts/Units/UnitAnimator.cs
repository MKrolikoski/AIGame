using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class UnitAnimator : MonoBehaviour
{
    //Navigation mesh agent
    private NavMeshAgent agent;
    //Unit animator
    private Animator animator;

	void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        GetComponent<UnitCombat>().OnAttack += OnAttack;
        GetComponent<UnitStats>().OnHit += OnHit;

    }

    void Update ()
    {
        float speedPercent = agent.velocity.magnitude / agent.speed;
        animator.SetFloat("speedPercent", speedPercent, 1f, Time.deltaTime);
	}

    public void OnAttack()
    {
        if(GetComponent<UnitCombat>().rangedAttack)
        {
            animator.SetTrigger("rangedAttack");
        }
        else
        {
            animator.SetTrigger("attack");
        }
    }

    public void OnHit()
    {
        animator.SetTrigger("hit");
    }
}
