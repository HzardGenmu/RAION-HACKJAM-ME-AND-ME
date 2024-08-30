using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public float dashSpeed;
    public float dashCooldown;
    public float dashTime;

    private Rigidbody2D rB; //reference to the Rigidbody2D
    //private Animator anim;

    private bool isJumping = false;
    public bool facingRight = true;
    [SerializeField] private bool grounded;
    private bool isDashing;
    private bool canDash;
    private float moveDirection;

    // Awake is called after all objects are initiallized. Called in a random order.
    private void Awake()
    {
        rB = GetComponent<Rigidbody2D>(); // Will look for the component on this GameObject(What the script is attached to) of type rigidbody.
        //anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update 
    void Start()
    {
        canDash = true;
        LoadPlayerPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing)
        {
            return;
        }

        //Input
        ProcessInput(); //
        //Animate 
        Animation(); //switch character direction
        if (grounded)
            Jump();

        SavePlayerPosition();
    }

    //Better for handling update, can be called multiple times per update
    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        //Move
        Move();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Grabable")
        {
            grounded = true;
        }
    }

    //=====================================================================================================================

    private void Animation()
    {
        if (moveDirection > 0 && !facingRight)
        {
            FlipCharacter();
        }
        else if (moveDirection < 0 && facingRight)
        {
            FlipCharacter();
        }
        /*anim.SetBool("run", moveDirection != 0); // animation for running
        anim.SetBool("Grounded", grounded);// animation for checking whether the character is touching object tagged "Ground"
        anim.SetBool("Dash", isDashing);*/
    }

    private void Move()
    {
        rB.velocity = new Vector2(moveDirection * moveSpeed, rB.velocity.y);
    }

    private void ProcessInput()
    {
        moveDirection = Input.GetAxisRaw("Horizontal"); //scale of (-1) to 1
        Dashing(); //To Dash
    }

    private void FlipCharacter()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && grounded)
        {
            isJumping = true;
            //anim.SetTrigger("Jump");
            rB.AddForce(new Vector2(0f, jumpForce));
            grounded = false;
            isJumping = false;
        }
    }

    private void Dashing()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            Debug.Log("Dash");
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        //anim.SetTrigger("Dash");
        isDashing = true;
        rB.velocity = new Vector2(moveDirection * dashSpeed, rB.velocity.y);

        yield return new WaitForSeconds(dashTime);
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    public bool canAttack()
    {
        return moveDirection == 0;
    }

    public void SavePlayerPosition()
    {
        PlayerPrefs.SetFloat("PlayerPosX", transform.position.x);
        PlayerPrefs.SetFloat("PlayerPosY", transform.position.y);
        PlayerPrefs.SetFloat("PlayerPosZ", transform.position.z);
        PlayerPrefs.Save();
    }

    private void LoadPlayerPosition()
    {
        if (PlayerPrefs.HasKey("PlayerPosX") && PlayerPrefs.HasKey("PlayerPosY") && PlayerPrefs.HasKey("PlayerPosZ"))
        {
            float x = PlayerPrefs.GetFloat("PlayerPosX");
            float y = PlayerPrefs.GetFloat("PlayerPosY");
            float z = PlayerPrefs.GetFloat("PlayerPosZ");
            transform.position = new Vector3(x, y, z);
        }
    }
}
