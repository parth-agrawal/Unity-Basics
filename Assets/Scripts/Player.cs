using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// maybe a game where you're flopping around as a blob

public class Player : MonoBehaviour
{

    private bool spacePressed = false;
    private float horizontalInput = 0;
    private Rigidbody rigidbodyComponent;
    [SerializeField] private LayerMask playerMask;


    // create a empty object called GroundCheckTransform and move it to the feet of the capsule body.
    // this becomes our reference for where the foot of the capsule is, and we can
    // point to it to refer to the ground of the capsule

    [SerializeField] private Transform groundCheckTransform = null;




    // Start is called before the first frame update
    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
        Debug.Log("Test");
    }

    // Update is called once per frame
    // Have mousepresses in Update(); in FixedUpdate() you might miss it since it's only called 100 Hz.
    // It's not good practice to have physics behaviors in Update(). The keystroke should be registered in Update(), but physics effects
    // should be applied in FixedUpdate(). I think this is to prevent runaway loops of some sort. 
    void Update()
    {
        // if the key is pressed down - this will not trigger if the spacebar is held down. That would be Input.GetKey() 

        if (Input.GetKeyDown(KeyCode.Space))
        {

            // this class has a component, get the component of type Rigidbody
            // I forget if this is basically calling a constructor? OR it returns 

            // anglebrackets = template. Will support any type. Standard Template Library.

            //Object[] obj = GetComponents<Object>();

            /**
            foreach(Object o in obj){
                Console.WriteLine(o);

            }
            */
            spacePressed = true;


        }

        horizontalInput = Input.GetAxis("Horizontal");

        
       

        
    }

    // FixedUpdate is called once every physics update.
    // by default unity runs at 100 Hz. The physics engine will replace everything at 100 Hz.


    // the player is allowed to jump a maximum of two times
    // if the velocity is 0, reset the jumpmax boolean
    private void FixedUpdate()

    {
        float ypos = rigidbodyComponent.position.y;

        // this returns an array of colliders that the object is colliding with.
        // to check if it's colliding with "anything at all" we just have to ensure this list is greater than 0


        //however, if it's colliding with nothing, that means that it's in the air and we should exit the FixedUpdate

        //the sphere is generated by groundCheckTransform - the Overlap sphere at the very bottom of the capsule. 


        // it's always colliding with its own collider

        // need to research what a Collider and OverlapSphere is in Unity


        // colliding with ground, it's grounded


        //keep the velocity y the same as the original velocity. this makes sure that it retains any momentum. 
        rigidbodyComponent.velocity = new Vector3(horizontalInput * 2, rigidbodyComponent.velocity.y, 0);

        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0) 
        {
            return;

            
        }

        
        if (spacePressed)
        {

            // if the y position of the rigid body is 0,
            // if it's in the air, and the jump count is less than 2, it can jump.
            // it resets when it hits the ground again.
            // else if velocity is 0, jumpcount = 0
            

           
            rigidbodyComponent.AddForce(Vector3.up * 7, ForceMode.VelocityChange);
            spacePressed = false;

            

            

            Debug.Log(ypos);


            
            


        }





    }

    private void OnTriggerEnter(Collider other)
    {
        // we are the player - so the "other" collider will be the coin, or whatever else

        // we had set the Coins to be on the coin layer 9
        if(other.gameObject.layer == 9)
        {
            // other is the collider. so destroy the gameObject associated with that, only.
            Destroy(other.gameObject);
        }
        
    }


}


/*
 * Friction
 * 
 * You can create things called PhysicMaterials that can be given to the collider's
 * PhysicMaterial property. You can set the Combined Friction with another object
 * to be either Average or Minimum or Maximum - based on what other material the 
 * collider is encountering.
 */
