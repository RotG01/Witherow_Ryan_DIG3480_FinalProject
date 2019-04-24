using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float tilt;
    public Boundary boundary;
    public int shields;
    public Text ShieldText;

    public GameObject shot;
    public GameObject shield;
    public Transform shotSpawn;
    public Transform shieldSpawn;
    public float fireRate;
    public float ShieldRate;

    private Rigidbody rb;
    private float nextFire;
    private float nextShield;
    private AudioSource audioSource;

    private void Start()
    {
        shields = 3;

        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        SetText();
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            audioSource.Play();
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.velocity = movement * speed;

        rb.position = new Vector3
        (
             Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
             0.0f,
             Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
        );

        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);

        if (Input.GetButton("Fire2") && shields > 0 && Time.time > nextShield)
        {
            nextShield = Time.time + ShieldRate;
            Instantiate(shield, shieldSpawn.position, Quaternion.Euler(0, 90, 0));
            shields = shields - 1;
            SetText();
        }
    }

    void SetText()
    {
        ShieldText.text = "(Right-Click) Shields:" + shields.ToString();
    }
}