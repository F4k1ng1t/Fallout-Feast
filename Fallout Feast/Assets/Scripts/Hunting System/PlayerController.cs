using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Stats")]
    public float moveSpeed;
    public float jumpForce;
    [Header("Components")]
    [SerializeField] Rigidbody rig;

    [Header("Gun State")]
    public GameObject bulletPrefab;
    public Transform bulletOrigin;
    public Transform playerCamera;
    public GameObject gun;

    [Header("Deradiator State")]
    public GameObject deradiator;
    public ParticleSystem deradiationParticles;
    private IGunState currentState;

    
    private void Start()
    {
        if(GetComponent<Rigidbody>() != null)
        {
            rig = GetComponent<Rigidbody>();
        }
        SetState(new NoneState());
    }
    public void SetState(IGunState newState)
    {
        if(currentState != null)
        {
            currentState.Exit();
        }
        currentState = newState;
        currentState.Enter(this);
    }
    void Update()
    {
        currentState.HandleInput();
        currentState.Update();

        Move();
        if (Input.GetKeyDown(KeyCode.Space))
            TryJump();
    }
    
    void Move()
    {
        // get the input axis
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        // calculate a direction relative to where we're facing
        Vector3 dir = (transform.forward * z + transform.right * x) * moveSpeed;
        dir.y = rig.linearVelocity.y;
        // set that as our velocity
        rig.linearVelocity = dir;
    }
    void TryJump()
    {
        // create a ray facing down
        Ray ray = new Ray(transform.position, Vector3.down);
        // shoot the raycast
        if (Physics.Raycast(ray, 1.5f))
            rig.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
