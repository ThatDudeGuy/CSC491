using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Mage_Attacks : MonoBehaviour
{
    // public bool summon_enemies;
    public GameObject skeleton_warrior, spawn_zone, enlarge_target;
    public GameObject[] skeletons;
    public Animator animator;
    public Vector3 initialScale, targetScale = new(2, 2, 2);
    public float duration = 1.0f;  // Duration of the scale
    private float timeElapsed = 0f;
    public int castCycleCount = 0, cycleLimit = 3, counter, attack; //position = 0;
    public bool begin_buff = false;
    // public int enemy_to_spawn = 0;
    // Start is called before the first frame update
    void Start()
    {
        initialScale = new(1,1,1);//lockOn.closestEnemy.transform.localScale;   
        animator = GetComponent<Animator>();
        animator.SetBool("attackComplete", true);
        skeletons = GameObject.FindGameObjectsWithTag("Skeleton");
    }

    // If the mage is not casting, have him reposition for set time, maybe 5-8 seconds,
    // and then being to cast a spell
    void Update()
    {
        if(begin_buff) buff_enemy();
        if(!animator.GetBool("isCasting")) reposition();
    }
    public void buff_enemy(){
        // lockOn.closestEnemy.transform.scaleto
        if (timeElapsed < duration){
            try{
                // print("In buff enemy, target = "+enlarge_target);
                enlarge_target.transform.localScale = Vector3.Lerp(initialScale, targetScale, timeElapsed / duration);
                enlarge_target.GetComponent<States>().angry = true;
                enlarge_target.GetComponent<Ai_Navigation>().agent.speed = 3.5f;
                enlarge_target.GetComponent<Ai_Navigation>().giant = true;
                timeElapsed += Time.deltaTime;
            }
            catch(Exception e) {Debug.Log(e);}
        }
    }

    public void toggleBuff(){
        begin_buff = !begin_buff;
        if(begin_buff == false) timeElapsed = 0;
    }

    public void toggle_isCasting(){
        // animator.SetBool("isCasting", !animator.GetBool("isCasting"));
        animator.SetBool("isCasting", false);
        // animator.SetBool("attackComplete", false);
    }

    void reposition(){
        // Need to write logic that will designate what the mage will do in between cast times
        
        if(counter >= Application.targetFrameRate * 3){
            skeletons = GameObject.FindGameObjectsWithTag("Skeleton");
            counter = 0;
            attack = Random.Range(0,2);//Random.Range(0,3);
            for(int i = 0; i < skeletons.Length; i++) {
                if(skeletons[i] != gameObject && !skeletons[i].GetComponent<States>().angry && !skeletons[i].GetComponent<States>().animator.GetBool("isDead?")){
                    enlarge_target = skeletons[i];
                    break;
                }
                else if(skeletons.Length == 1 && skeletons[0] == gameObject){
                    attack = 1;//Random.Range(1,3);
                }
            }
            animator.SetBool("isCasting", true);
        }
        counter++;
    }

    public void castCycles(){
        // print(castCycleCount);
        if(castCycleCount >= cycleLimit) {
            // animator.SetBool("attackComplete", false);
            castCycleCount = 0;
            print("CastCycle: Attack = "+attack);
            switch(attack){
                case 0:
                    enlarge_enemy();
                    print(attack);
                    return;
                case 1:
                    summon();
                    print(attack);
                    return;
                case 2:
                    print(attack);
                    animator.SetBool("isCasting", false);
                    return;
            }
        }
        castCycleCount += 1;
    }
    public void summon(){
        animator.SetTrigger("summon");
        // animator.SetBool("attackComplete", true);
        // animator.SetBool("isCasting", false);
    }

    public void enlarge_enemy(){
        animator.SetTrigger("castSpell");
    }

    public void summon_enemies(){
        GameObject[] enemies = {
            Instantiate(skeleton_warrior, new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + 1), Quaternion.identity),
            Instantiate(skeleton_warrior, new Vector3(transform.localPosition.x + 1, transform.localPosition.y, transform.localPosition.z), Quaternion.identity),
            Instantiate(skeleton_warrior, new Vector3(transform.localPosition.x - 1, transform.localPosition.y, transform.localPosition.z), Quaternion.identity)
        };
        foreach(GameObject enemy in enemies){
            enemy.GetComponent<Ai_Navigation>().playerFound = true;
        }
    }
}
