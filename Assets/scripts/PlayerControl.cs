using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody2D rb;

    public float speed;

    public float jumpForce = 250;

    public bool Grounded = true;

    public TMP_Text coinText;
    
    public int coinsCollected = 0;
    
    public TMP_Text healthText;
    

    public int currenthealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        healthText.text = currenthealth.ToString();
        

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddForce(Vector2.left * speed);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddForce(Vector2.right * speed);
        }

        if (Input.GetKey(KeyCode.Space) && Grounded)
        {
            rb.AddForce(Vector2.up * jumpForce);
            Grounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Grounded = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Collectable>())
        {
            coinsCollected++;
            coinText.text = coinsCollected.ToString();
        }
        else if (other.GetComponent<Hazard>())
        {
            currenthealth++;
            healthText.text = currenthealth.ToString();
            if (currenthealth <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                SceneManager.LoadScene("SampleScene");
            }
        }
        else if (other.GetCompound<Goall>())
            {
            SceneManager.LoadScene(other.GetComponent<Goall>().NextLevel);
            }
    }
}