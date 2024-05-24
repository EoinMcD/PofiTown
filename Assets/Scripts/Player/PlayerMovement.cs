using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Basics")]
    [SerializeField] float speed=5f;
    [SerializeField] Transform orientation;
    [SerializeField] Transform[] groundChecks;
    [SerializeField] Transform mesh;
    Rigidbody rb;
    [Space]

    const float playerGravity =3.5f;
    float gravity;
    bool useGravity;

    [Header("Ground Check")]
    [SerializeField] float playerHeight=2;
    public bool grounded;
    [Space]

    [Header("Slope Handling")]
    [SerializeField] float maxSlopeAngle=40f;
    RaycastHit slopeHit;
    bool exitingSlope;
    bool minimiseVelocity=true;


    float horizontalInput;
    float verticalInput;
    Vector3 moveDir;
    float horizRot;
    float vertRot;

    bool canInput = true;

    private void Start() {
        rb=GetComponent<Rigidbody>();
        gravity=playerGravity;
        useGravity=true;
    }

    private void Update()
    {
        MyInput();
        SpeedControl();
        IsGrounded();
        if(useGravity){
            DoGravity();
        }
        RotatePlayer();
    }

    private void FixedUpdate() {
        MovePlayer();
        
    }

     void MyInput() {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
     }

      void MovePlayer() {
        if(!canInput) {return;}
        moveDir = orientation.forward * verticalInput + orientation.right * horizontalInput;
        if(OnSlope() && !exitingSlope) {
            Debug.Log("on slope");
            rb.AddForce(GetSlopeMoveDirection(moveDir) * speed  , ForceMode.Force);

            if(rb.velocity.y > 0) {
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
            }
        }
        
        else if(grounded) {
            //transform.rotation=Quaternion.Euler(horizontalInput,0,verticalInput );
            rb.AddForce(moveDir.normalized * speed , ForceMode.Force);
            
        } 

        UseGravity(!OnSlope());
    }

    void RotatePlayer(){
        if (horizontalInput != 0 && verticalInput != 0) {
            float yRotation = 0;
            if (horizontalInput > 0 && verticalInput > 0) {
                yRotation = 45; // Right and Up
            }
            else if (horizontalInput > 0 && verticalInput < 0) {
                yRotation = 135; // Right and Down
            }
            else if (horizontalInput < 0 && verticalInput > 0) {
                yRotation = -45; // Left and Up
            }
            else if (horizontalInput < 0 && verticalInput < 0) {
                yRotation = -135; // Left and Down
            }
            mesh.localRotation = Quaternion.Euler(0, yRotation, 0);
        }
        else if (horizontalInput != 0) {
            mesh.localRotation = Quaternion.Euler(0, horizontalInput * 90, 0);
        }
        else if (verticalInput > 0) {
            mesh.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (verticalInput != 0) {
            mesh.localRotation = Quaternion.Euler(0, 180, 0);
        }

    }


    void SpeedControl() {
        if(OnSlope() && !exitingSlope && minimiseVelocity) {
            if(rb.velocity.magnitude > speed) {
                rb.velocity= rb.velocity.normalized * speed;
            }
        }
        else if(minimiseVelocity) {
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            if(flatVel.magnitude > speed) {
                Vector3 limitVel= flatVel.normalized * speed;
                rb.velocity = new Vector3(limitVel.x,rb.velocity.y,limitVel.z);
             }
        }
    }

      #region  Slopes
     public bool OnSlope() {
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit , playerHeight * .5f +.3f)) {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle <maxSlopeAngle && angle !=0;
        }

        return false;
    }

    public Vector3 GetSlopeMoveDirection(Vector3 direction) {
        return Vector3.ProjectOnPlane(direction, slopeHit.normal).normalized;

    }
    #endregion

     #region  GroundCheck and Jumping
    bool IsGrounded() {
        for(int i=0;i<groundChecks.Length;i++) {
            if(Physics.Raycast(groundChecks[i].position, Vector3.down, .14f)){
                grounded=true;
                return true;
            }
        }
        grounded=false;
        return grounded;
    }

    public bool isPlayerGrounded(){
        return grounded;
    }

    void UseGravity(bool useGravity){
        this.useGravity=useGravity;
    }

    void DoGravity(){
        rb.AddForce(Vector3.down *gravity, ForceMode.Acceleration);
    }

    public void SetGravityForce(float gravity=playerGravity) {
        this.gravity=gravity;
    }

    #endregion
    

    

}
