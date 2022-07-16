using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] float speed = 0f;
    [SerializeField] float rotationSpeed = 0f;

    [SerializeField] float jumpSpeed = 0f;

    [SerializeField] float jumpButtonGracePeriod = 0.2f;


    private CharacterController characterController = null;
    private float ySpeed;
    private float originalStepOffSet;

    private float? lastGroundedTime;

    private float? jumpButtonPressedTime;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        originalStepOffSet = characterController.stepOffset;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        float magnitude = Mathf.Clamp01(movementDirection.magnitude) * speed;
        movementDirection.Normalize();

        ySpeed += Physics.gravity.y * Time.deltaTime;

        if (characterController.isGrounded)
        {
            lastGroundedTime = Time.time;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpButtonPressedTime = Time.time;
        }

        if (Time.time - lastGroundedTime <= jumpButtonGracePeriod)
        {
            characterController.stepOffset = originalStepOffSet;
            ySpeed = -0.5f;
            if (Time.time - jumpButtonPressedTime <= jumpButtonGracePeriod)
            {

                ySpeed = jumpSpeed;
                lastGroundedTime = null;
                jumpButtonPressedTime = null;
            }

        }
        else
        {
            characterController.stepOffset = 0;
        }

        Vector3 velocity = movementDirection * magnitude;
        velocity.y = ySpeed;
        characterController.Move(velocity * Time.deltaTime);

        if (movementDirection != Vector3.zero)
        {

            animator.SetBool("IsRunning", true); 
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        } else {
            animator.SetBool("IsRunning", false);
        }
    }
}
