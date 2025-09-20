using DinoFracture;
using System; 
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerMultiplayer : MonoBehaviour
{
    [SerializeField] private float ForwardSpeed = 1f;
    [SerializeField] private float MovementSpeed = 450f;
    [SerializeField] private float AccelerationSpeed = 180f;
    [SerializeField] private float JumpSpeed;
    [SerializeField] private float JumpEndSpeed;
    [SerializeField] private float JumpStartTime;
    private float JumpTime;
    private Rigidbody rb;
    [SerializeField] private bool JumpEnabled;
    private bool PlayerIsGrounded = true;
    private bool HasJumped = false;
    private bool DoubleJump = true;
    ActionMap_1 actionsWrapper;
    private Vector2 move;
    public static event Action OnDeath;


    private void Awake()
    {
        actionsWrapper = new ActionMap_1();
        actionsWrapper.Player.Jump.started += OnJump;
    }

    public void OnDeathTrigger()
    {
        OnDeath?.Invoke();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        //move = actionsWrapper.Player.Jump.ReadValue<Vector2>();// * (PlayerSpeed * Time.deltaTime);
        rb.AddForce(new Vector3(5 * Time.deltaTime, 0, 0), ForceMode.Impulse);
    }

    // Menu OFF
    public void OnEnable()
    {
        actionsWrapper.Player.Enable();
        Menu.OnDisableJump += DisableJump;
        Menu.OnEnableJump += EnableJump;

    }
    // Menu ON
    public void OnDisable()
    {
        actionsWrapper.Player.Disable();
        Menu.OnDisableJump -= DisableJump;
        Menu.OnEnableJump -= EnableJump;

    }

    // Jump
    public void OnJump(InputAction.CallbackContext context)
    {
        
            if (PlayerIsGrounded)
            {
                PlayerIsGrounded = false;
                DoubleJump = true;

                JumpTime = JumpStartTime;
                AudioManager.Instance.PlaySoundEffects(AudioManager.Instance.audioClips.Jump);
                rb.AddForce(new Vector3(0, JumpSpeed, 0), ForceMode.Impulse);
            }
            // DoubleJumps
            else if (PlayerIsGrounded == false && DoubleJump)
            {
                DoubleJump = false;
                PlayerIsGrounded = false;
                rb.AddForce(new Vector3(0, JumpSpeed + 3, 0), ForceMode.Impulse);
                AudioManager.Instance.PlaySoundEffects(AudioManager.Instance.audioClips.JumpDouble);
            }
              
    }

    private void DisableJump()
    {
        actionsWrapper.Player.Jump.Disable();
    }

    private void EnableJump()
    {
        actionsWrapper.Player.Jump.Enable();
    }


    private void OnCollisionEnter(Collision collision)
    { 
        // Ground Check for Jumping
        if (collision.gameObject.tag == "Ground")
        {
            AudioManager.Instance.PlaySoundEffects(AudioManager.Instance.audioClips.JumpReturnToGround);
            PlayerIsGrounded = true;
            if (HasJumped == true)
            {
                HasJumped = false;
                //AudioManager.Instance.PlaySound(ReturnToGroundClip);
            }
        }
    }
}
