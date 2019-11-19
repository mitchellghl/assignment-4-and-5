using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class OctopusPlayerControl : MonoBehaviour
{
    enum PLAYER_STATE { S_WALK, S_IDLE, /*S_JUMP*/ }; //player states
    PLAYER_STATE state;

    Rigidbody rb;
    private Vector3 startPosition;
    private Quaternion startRotation;
    Animator anim;
    CharacterController cc;
    float jumpForce = 350f;
    float speed = 15f;
    Ray camRay;
    RaycastHit hit;
    public int ammo;
    public int maxAmmo = 10;
    public GameObject bullet;
    public GameObject bulletSpawn;
    public float reloadTime = 3f;
    public Camera cam;
    public AudioManager audioManager;
    public AmmoManager ammoManager;
    bool reloading = false;

    public LightManager LightManager;
    public Light PumpkinLight;
    public Image Pumkin;
    public TimeCount TimeCount;

    public GameObject TutorialPanel;
    public Text TutorailText;
    Image im;
    Text te;

    public GameObject deathBubbles;

    private bool win = false;

    void Start()
    {
        ammo = maxAmmo;
        ammoManager.updateMaxAmmo(maxAmmo);
        ammoManager.updateAmmo(ammo);
        anim = GetComponent<Animator>();
        cc = gameObject.GetComponent<CharacterController>();
        rb = gameObject.GetComponent<Rigidbody>();
        im = TutorialPanel.GetComponent<Image>();
        te = TutorailText.GetComponent<Text>();
        startPosition = transform.position;
        startRotation = transform.rotation;
        bulletSpawn = GameObject.Find("BulletSpawn");
        cam = Camera.main;
        cam.transform.eulerAngles = new Vector3(70, 0, 0);
        LightManager.light = 20;
        PumpkinLight.intensity = 4;
    }

    private void Update()
    {
        cam.transform.position = new Vector3(transform.position.x, 30, transform.position.z-10);
        var hAxis = Input.GetAxis("Horizontal"); //Get horizontal and vertical axis references
        var vAxis = Input.GetAxis("Vertical");

        aim(); //run aiming function

        if (Input.GetButtonDown("Fire1")) //run shooting function if press mouse button
        {
            shoot();
        }

        if (Input.GetKeyDown(KeyCode.R)) //run reload function
        {
            StartCoroutine(Reload());
        }

        switch (state)
        {
            case PLAYER_STATE.S_IDLE: //idle state
                anim.SetTrigger("stop");
                if (hAxis != 0 || vAxis != 0) //switch to walk if moving
                {
                    state = PLAYER_STATE.S_WALK;
                    audioManager.updateAudio("bloop");
                }
                /*if (Input.GetKeyDown(KeyCode.Space)) //switch to jump if press space
                {
                    state = PLAYER_STATE.S_JUMP;
                }*/
                break;

            case PLAYER_STATE.S_WALK:
                anim.SetTrigger("walk");

                Vector3 forward = transform.TransformDirection(Vector3.forward) * vAxis; //moving around
                Vector3 right = transform.TransformDirection(Vector3.right) * hAxis;
                Vector3 direction = forward + right;
                cc.SimpleMove(direction * speed);

                if (hAxis == 0 && vAxis == 0) //if stop moving, go to idle
                {
                    state = PLAYER_STATE.S_IDLE;
                }

                /*if (Input.GetKeyDown(KeyCode.Space)) //switch to jump if press space
                {
                    state = PLAYER_STATE.S_JUMP;
                }*/
                break;

            /*case PLAYER_STATE.S_JUMP:
                anim.SetTrigger("jump");
                Vector3 jumphight = new Vector3(transform.position.x,transform.position.y+5f,transform.position.z);
                transform.position = jumphight;
                Debug.Log("please jump");
                rb.AddForce(0, jumpForce, 0);
                if (Input.GetKeyUp(KeyCode.Space))
                {
                    state = PLAYER_STATE.S_IDLE;
                    if (Input.GetKeyDown(KeyCode.W))
                    {
                        state = PLAYER_STATE.S_WALK;
                    }
                }
                break;*/

        }
        Pumkin.fillAmount = LightManager.light / 100f;
        StartCoroutine(fadeIn());
        Die();
        MaxLight();
        StartCoroutine(Loadout());
    }

    private void aim()
    {

        camRay = Camera.main.ScreenPointToRay(Input.mousePosition); //shoot ray at mouse
        if (Physics.Raycast(camRay, out hit))
        {
            if(!hit.transform.name.Equals("OctopusPlayer Variant")) //if the ray hits something not called player
            {
                transform.LookAt(hit.point); //look at whatever the ray hit
            }
        }
    }
    
    private void shoot()
    {
        if(ammo > 0 && reloading == false)
        {
            GameObject newBullet = Instantiate(bullet, bulletSpawn.transform.position, Quaternion.identity);
            hit.point = new Vector3(hit.point.x, 2, hit.point.z);
            newBullet.GetComponent<BulletScript>().target = hit.point;
            ammo--;
            audioManager.updateAudio("splash");
            ammoManager.updateAmmo(ammo);
            Debug.Log(ammo);
        } else
        {
            audioManager.updateAudio("gurgle");
            Debug.Log("Empty! Need to reload!");
        }
        
    }
    
    private IEnumerator Reload()
    {
        Debug.Log("Reloading...");
        reloading = true;
        audioManager.updateAudio("reload");
        ammoManager.reloading(ammo);
        yield return new WaitForSeconds(reloadTime);
        ammo = maxAmmo;
        Debug.Log(ammo);
        audioManager.updateAudio("ready");
        reloading = false;
        ammoManager.updateAmmo(ammo);
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Lamp")
        {
            if (LightManager.light < 100)
            {
                LightManager.updateLight(20);
                Debug.Log("Light");
                if (PumpkinLight.intensity < 12)
                {
                    PumpkinLight.intensity += 2;
                    other.gameObject.tag = "Finish";
                }
            }
        }
        if (other.gameObject.tag == "Enemy")
        {
            if (LightManager.light > 0)
            {
                LightManager.updateLight(-10);
                if (PumpkinLight.intensity > 0)
                {
                    PumpkinLight.intensity--;
                }
                audioManager.updateAudio("hurt");
                GameObject bubbles = Instantiate(deathBubbles, other.transform.position, Quaternion.identity);
                Destroy(bubbles, 1);
                Destroy(other.gameObject);
                
            }
        }
    }

    private void Die() //die state
    {
        if (LightManager.light == 0 || TimeCount.TotalTime == 0)
        {

            StartCoroutine(WaitDie());
            Debug.Log("die");
        }

    }
    IEnumerator fadeIn() //tutorial fadein function
    {
        while (im.color.a > 0)
        {
            float newAlpha = im.color.a - 0.5f * Time.deltaTime;
            im.color = new Color(0, 0, 0, newAlpha);
            yield return new WaitForSeconds(10);
            float newteAlpha = te.color.a - 0.5f * Time.deltaTime;
            te.color = new Color(0, 0, 0, newAlpha);
            yield return null;

        }
    }
    private void MaxLight()
    {
        if (LightManager.light >= 100)
        {
            win = true;

        }

    }
    private IEnumerator Loadout()
    {
        if (win == true)
        {
            yield return new WaitForSeconds(3);
            SceneManager.LoadScene("AboutTeamScene");
        }
    }
    private IEnumerator WaitDie()
    {
        
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene("LoseScene");
        
    }
}