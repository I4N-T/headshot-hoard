using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public Transform playerTransform;
    Rigidbody rb;

    //REFERENCES TO OTHER STUFF
    public GameObject bloodSplatter;
    public GameObject bloodSplatterDeath;
    public GameObject bloodSplatterHead;
    public GameObject helmetSparks;

    //MOVEMENT STUFF
    public float movespeed;
    public float jumpForce;
    bool isGrounded;

    public float gravityLevel;

    //AUDIO
    public AudioSource sfxSource;
    public AudioClip gunShotSound;

    public AudioSource announcerSource;
    public AudioClip quadDamageClip;
    public AudioClip lowGravClip;
    public AudioClip rapidFireClip;
    public AudioClip doubleSpeedClip;


    //STATS
    public static float playerHealth;
    float damage;

    bool isRapidFire;
    float timeElapsed;

    //POWERUPS STUFF
    Coroutine powerCR;
    public Text powerUpTxt;


    void Start()
    {
        playerTransform = this.transform;
        rb = this.GetComponent<Rigidbody>();

        //locks cursor on screen
        Cursor.lockState = CursorLockMode.Locked;

        //Set physics level
        Physics.gravity = new Vector3(Physics.gravity.x, -30f, Physics.gravity.z);

        isGrounded = true;

        //Stat initialization
        playerHealth = 100;
        damage = 1f;
    }

    void Update()
    {
        //timeElapsed -= Time.deltaTime;
        playerHealth = Mathf.Clamp(playerHealth, 0, 100);

        JumpMethod();

        if (!isRapidFire)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();

            }
        }
        else if (isRapidFire)
        {
            if (Input.GetButton("Fire1"))
            {
                ShootRapid();
            }
        }
        
    }
    void FixedUpdate ()
    {
        //Move using WASD
        MovementMethod();
        
        //Physics.gravity = new Vector3(Physics.gravity.x, gravityLevel, Physics.gravity.z);
        //Check Health
        /*if (playerHealth <= 0)
        {
            //Game Over
            Destroy(gameObject);
            SceneManager.LoadScene(3);
        }*/

        
    }


    void MovementMethod()
    {
        float translation = Input.GetAxis("Vertical");
        float strafe = Input.GetAxis("Horizontal");

        Vector3 movement = new Vector3(strafe, 0, translation);
        Vector3 moveDirection = (strafe * transform.right + translation * transform.forward).normalized;

        rb.MovePosition(transform.position + moveDirection * movespeed * Time.deltaTime);
    }

    void JumpMethod()
    {
        if (Input.GetKeyDown("space") && isGrounded)
        {
            rb.AddForce(this.transform.up * jumpForce, ForceMode.Impulse);
        }
    }



    void Shoot()
    {
        //Play sound
        sfxSource.clip = gunShotSound;
        sfxSource.Play();

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
        {
            if (hit.transform.tag == "Zombie")
            {
                ZombieScript _zombieScript = hit.transform.GetComponent<ZombieScript>();
                Collider col = hit.collider.transform.GetComponent<Collider>();

                if (hit.collider.transform.tag == "Zombie")
                {
                    _zombieScript.health -= damage;
                    if (_zombieScript.health > 0)
                    {
                        GameObject _bloodSplatter0 = Instantiate(bloodSplatter, hit.transform.position + hit.transform.forward * .5f, Quaternion.identity) as GameObject;
                        GameObject _bloodSplatter = Instantiate(bloodSplatter, hit.transform.position + hit.transform.forward * 4f, Quaternion.identity) as GameObject;
                    }
                    else if (_zombieScript.health <= 0)
                    {
                        GameObject _bloodSplatter0 = Instantiate(bloodSplatterDeath, hit.transform.position + hit.transform.forward * .5f, Quaternion.identity) as GameObject;
                        GameObject _bloodSplatter = Instantiate(bloodSplatterDeath, hit.transform.position + hit.transform.forward * 4f, Quaternion.identity) as GameObject;
                    }


                }
                else if (hit.collider.transform.name == "Head")
                {
                    GameManager.score += 1;
                    _zombieScript.health -= (damage * 5);
                    GameObject _bloodSplatter0 = Instantiate(bloodSplatterHead, new Vector3(hit.transform.position.x, hit.transform.position.y + 1f, hit.transform.position.z) + hit.transform.forward * .5f, Quaternion.identity) as GameObject;
                    GameObject _bloodSplatter = Instantiate(bloodSplatterHead, new Vector3(hit.transform.position.x, hit.transform.position.y + 1f, hit.transform.position.z) + hit.transform.forward * 4f, Quaternion.identity) as GameObject;
                }
                else if (hit.collider.transform.name == "Helmet")
                {
                    GameObject helmetSparks0 = Instantiate(helmetSparks, new Vector3(hit.transform.position.x, hit.transform.position.y + 1.75f, hit.transform.position.z) + hit.transform.forward * .5f, Quaternion.identity) as GameObject;
                }
            }
            else if (hit.transform.tag == "Teleport")
            {
                ZombieTeleportScript _zombieScript = hit.transform.GetComponent<ZombieTeleportScript>();
                Collider col = hit.collider.transform.GetComponent<Collider>();

                if (hit.collider.transform.tag == "Teleport")
                {
                    _zombieScript.health -= damage;
                    if (_zombieScript.health > 0)
                    {
                        GameObject _bloodSplatter0 = Instantiate(bloodSplatter, hit.transform.position + hit.transform.forward * .5f, Quaternion.identity) as GameObject;
                        GameObject _bloodSplatter = Instantiate(bloodSplatter, hit.transform.position + hit.transform.forward * 4f, Quaternion.identity) as GameObject;
                    }
                    else if (_zombieScript.health <= 0)
                    {
                        GameObject _bloodSplatter0 = Instantiate(bloodSplatterDeath, hit.transform.position + hit.transform.forward * .5f, Quaternion.identity) as GameObject;
                        GameObject _bloodSplatter = Instantiate(bloodSplatterDeath, hit.transform.position + hit.transform.forward * 4f, Quaternion.identity) as GameObject;
                    }


                }
                else if (hit.collider.transform.name == "Head")
                {
                    GameManager.score += 2;
                    _zombieScript.health -= (damage * 5);
                    GameObject _bloodSplatter0 = Instantiate(bloodSplatterHead, new Vector3(hit.transform.position.x, hit.transform.position.y + 1f, hit.transform.position.z) + hit.transform.forward * .5f, Quaternion.identity) as GameObject;
                    GameObject _bloodSplatter = Instantiate(bloodSplatterHead, new Vector3(hit.transform.position.x, hit.transform.position.y + 1f, hit.transform.position.z) + hit.transform.forward * 4f, Quaternion.identity) as GameObject;
                }

            }
        }
    }

    void ShootRapid()
    {   
        if (timeElapsed >= 0.1f)
        {
            Shoot();
            timeElapsed = 0f;
        }
        timeElapsed += Time.deltaTime;
    }

    //PowerUp Routines
    IEnumerator QuadDamage()
    {
        announcerSource.clip = quadDamageClip;
        announcerSource.Play();

        damage *= 4;
        float duration = 10f;
        while (duration > 0)
        {
            duration -= 1f;
            powerUpTxt.text = "Quad Damage";
            yield return new WaitForSeconds(1f);
        }
        powerUpTxt.text = "None";
        damage = 1;
    }

    IEnumerator LowGravity()
    {
        announcerSource.clip = lowGravClip;
        announcerSource.Play();

        Physics.gravity = new Vector3(Physics.gravity.x, -7f, Physics.gravity.z);
        float duration = 10f;
        while (duration > 0)
        {
            duration -= 1f;
            powerUpTxt.text = "Low Gravity";
            yield return new WaitForSeconds(1f);
        }
        powerUpTxt.text = "None";
        Physics.gravity = new Vector3(Physics.gravity.x, -30f, Physics.gravity.z);
    }

    IEnumerator RapidFire()
    {
        announcerSource.clip = rapidFireClip;
        announcerSource.Play();

        isRapidFire = true;
        float duration = 10f;
        while (duration > 0)
        {
            duration -= 1f;
            powerUpTxt.text = "Rapid Fire";
            yield return new WaitForSeconds(1f);
        }
        powerUpTxt.text = "None";
        isRapidFire = false;
    }

    IEnumerator DoubleSpeed()
    {
        announcerSource.clip = doubleSpeedClip;
        announcerSource.Play();

        movespeed *= 1.5f;
        float duration = 10f;
        while (duration > 0)
        {
            duration -= 1f;
            powerUpTxt.text = "Double Speed";
            yield return new WaitForSeconds(1f);
        }
        powerUpTxt.text = "None";
        movespeed /= 1.5f;
    }

    //COLLIDERS

     //Jumping
    void OnCollisionStay(Collision col)
    {
        if (col.gameObject.layer == 8)
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.layer == 8)
        {
            isGrounded = false;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "PowerUp")
        {
            //stop the currently running powerUP
            if (powerCR != null)
            {
                StopCoroutine(powerCR);

                //set stuff back to normal
                damage = 1;
                Physics.gravity = new Vector3(Physics.gravity.x, -30f, Physics.gravity.z);
                isRapidFire = false;
                movespeed = 8;
                powerUpTxt.text = "None";
            }


            //select random powerUp
            int rand = Random.Range(0, 5);
            if (rand == 0)
            {
                powerCR = StartCoroutine(QuadDamage());
            }
            else if (rand == 1)
            {
                powerCR = StartCoroutine(LowGravity());
            }
            else if (rand == 2)
            {
                powerCR = StartCoroutine(RapidFire());
            }
            else if (rand == 3)
            {
                powerCR = StartCoroutine(DoubleSpeed());
            }
            else if (rand == 4)
            {
                int randHealthBonus = Random.Range(5, 20);
                playerHealth += randHealthBonus;
                playerHealth = Mathf.Clamp(playerHealth, 0, 100);
            }

            PowerUpScript puScript = col.gameObject.GetComponent<PowerUpScript>();
            puScript.sfxSource.Play();
            Destroy(col.gameObject, 1f);
        }
    }
}
