using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    
    
    
    public Transform guideArrow;

    [SerializeField] private float damageMultiplier = 1;//more the multiplier more is the damage applied
    [SerializeField] private GameObject smoke;
    [SerializeField] private GameObject fire;

    private int healthPoints = 100;

    private string CurrentLevel;
    private GameManager gameManager;
    private Transform goalTransform;
    private ControllerRumble rumble;
    private GameObject healthbar;
    private CamShake shake;
    private GameFinished gameFinished;


    // Start is called before the first frame update
    void Start()
    {

        CurrentLevel = Application.loadedLevelName;
        //if(guideArrow == null)
        guideArrow = GameObject.Find("GuideArrow").transform;
        if (gameManager == null)
            gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        if(goalTransform == null)
            goalTransform = GameObject.Find("Goal").transform;
        if(rumble == null)
            rumble = GameObject.FindWithTag("ControllerRumble").GetComponent<ControllerRumble>();
        if (healthbar == null)
            healthbar = GameObject.FindWithTag("Healthbar");
        if (gameFinished == null)
            gameFinished = GameObject.Find("Goal").GetComponent<GameFinished>();

        if (fire != null)
            fire.SetActive(false);

        if (smoke != null)
            smoke.SetActive(false);

        shake = GameObject.FindWithTag("MainCamera").GetComponent<CamShake>();

        //Reset
        rumble.StopVibration();
    }

    // Update is called once per frame
    void Update()
    {
        guideArrow.LookAt(new Vector3(goalTransform.position.x, guideArrow.position.y, goalTransform.position.z));


        
    }

    


    private void OnCollisionEnter(Collision other)
    {
        if(!gameObject.GetComponent<CarController>().enabled)
            return;
        

        if(other.collider.tag == "EnemyCar"){
            LifeDecrement();
        }else if(other.collider.tag != "Ground"){

            int impulsePoint = (int)(other.impulse.magnitude * damageMultiplier )/ 1000;

            //Debug.Log(impulsePoint);//

            //healthPoints--;
            healthPoints -= impulsePoint;

            if (healthPoints <= 25 && !smoke.activeSelf)
                smoke.SetActive(true);

            healthbar.GetComponent<Slider>().value = healthPoints;

            if (healthPoints <= 0){
                healthbar.GetComponent<Slider>().value = 0;

                if (!fire.activeSelf)
                {
                    fire.SetActive(true);
                    //smoke.GetComponent<ParticleSystem>().Stop();// SetActive(false);
                }

                LifeDecrement();
            }else{
                //controller rumble
                rumble.StopVibration();
                rumble.StartVibration(0.25f);
                shake.StartCameraShake(0.25f, 0.2f);
            }
        }
    }


    private void LifeDecrement(){

        //controller rumble
        rumble.StopVibration();
        rumble.StartVibration(0.75f);
        shake.StartCameraShake(1.0f, 0.75f);
        gameFinished.StartFadeOut();


        gameObject.GetComponent<CarController>().StopCar();
        StartCoroutine(WaitForSceneLoad());
    }

    private IEnumerator WaitForSceneLoad() {
        yield return new WaitForSeconds(gameManager.LoadLevelDelay);
        gameManager.DecrementLife(CurrentLevel);
    }
}
