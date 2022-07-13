using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] float speed = 0f;
    [SerializeField] float rotationSpeed = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        movementDirection.Normalize();

        transform.Translate(movementDirection * speed * Time.deltaTime, Space.World);

        if (movementDirection != Vector3.zero){
            Quaternion toRotation = Quaternion.LookRotation( movementDirection, Vector3.up);

            transform.rotation =  Quaternion.RotateTowards( transform.rotation, toRotation, rotationSpeed * Time.deltaTime );
        }
    }
}
