using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DeathofMainPlayerLogic : MonoBehaviour
{
    public float moveSpeed = 5f;
    public bool isDead = false;
    private Rigidbody rb;

    Vector3 movement, moveHorizontal, moveVertical;
    public Health_Tracking playerStats;
    public Player_Movement player;
    public int health;
    



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Was CharacterController
        health = playerStats.health;
        player = GetComponent<Player_Movement>();
       // isDead = false;
    }



    public void TakeDamage(int damage)
    {
        // health -= damage;
        if (health <= 0)
        {
            {
                Die(); // Was characterController
            }
        }
    }





    
        // Update is called once per frame
        void Update()
        {
            if (isDead)
            {
                return; // Stop all movement if the character is dead
            }

            // MoveCharacter();
        }



        void MoveCharacter()
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
        }



        public void Die()
        {
            isDead = true;
            player.canMove = false;
            // Add any additional logic for when the character dies, like playing death animation
            Debug.Log("DEAD");




        }
    
}
// End of rb death script
