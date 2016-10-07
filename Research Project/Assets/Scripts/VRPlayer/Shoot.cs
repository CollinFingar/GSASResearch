using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

    public GameObject shotPrefab;
    public Vector3 shotSpawnOffset = new Vector3(0, 0, .5f);
    public float frequency = .1f;
    public float delay = .5f;
    public bool fullAuto = true;
    public Vector3 shotVelocity = new Vector3(0, 0, 3);
    private Vector3 shootDirectionVelocity;

    private float nextShootTime = 0f;
    private bool shootingAuto = false;
    private bool semiFired = false;
    private bool fireable = false;

	public GameObject[] gunEnergyObjectArray;
	private SpriteRenderer[] gunEnergySpriteArray;
	public Color gunEnergyColor = new Color(1,1,1,1);
	private float alpha = 0f;
	private float alphaRate = 3f;

	public int energyCostBothGuns = 2;
	public VRGUIHandler gui;


    // Use this for initialization
    void Start () {
		InitializeGunEnergy ();
	}
	
	// Update is called once per frame
	void Update () {
        SenseAim();
	}

    void SenseShoot() {
        float rTrig = Input.GetAxis("RightTrigger");
        if (!fullAuto) {
			if (rTrig > .3 && Time.time > nextShootTime) {
				int energyLevel = gui.GetCurrentEnergyLevel ();
				if (energyLevel >= energyCostBothGuns) {
					Fire();
					nextShootTime = Time.time + frequency;
					gui.UpdateEnergyLevel (energyLevel - energyCostBothGuns/2);
				}
            }
        } else {
            if (shootingAuto) {
                if (rTrig > .3 && Time.time > nextShootTime){
                    Fire();
                    nextShootTime = Time.time + frequency;
                } else if(Time.time > nextShootTime){
                    shootingAuto = false;
                }
            } else {
                if (rTrig > .3){
                    nextShootTime = Time.time + delay;
                    shootingAuto = true;
                }
            }
        }   
    }


	void SenseAim() {
		float lTrig = Input.GetAxis("LeftTrigger");
		if (lTrig > .2) {
			fireable = true;
			ChangeColor (true);
		} else if(lTrig < .2){
			if (fireable) {
				fireable = false;
			}
			ChangeColor (false);
		}
		if (fireable) {
			SenseShoot();
		}
	}

    void Fire() {
        shootDirectionVelocity = transform.TransformDirection(shotVelocity);
		GameObject instance = (GameObject)Instantiate(shotPrefab, transform.position + transform.TransformDirection(shotSpawnOffset), transform.rotation);
        instance.GetComponent<Rigidbody>().AddForce(shootDirectionVelocity);
    }

	void InitializeGunEnergy(){
		gunEnergySpriteArray = new SpriteRenderer[gunEnergyObjectArray.Length];
		for (int i = 0; i < gunEnergyObjectArray.Length; i++) {
			SpriteRenderer gunEnergySprite = gunEnergyObjectArray [i].GetComponent<SpriteRenderer> ();
			gunEnergySpriteArray [i] = gunEnergySprite;
			gunEnergySprite.color = new Color (gunEnergyColor.r, gunEnergyColor.g, gunEnergyColor.b, 0f);
		}
	}

	void ChangeColor (bool aiming){
		if (aiming && alpha < 1) {
			float changeAmount = alphaRate * Time.deltaTime;
			if (alpha + changeAmount >= 1) {
				alpha = 1;
			} else {
				alpha += changeAmount;
			}
			UpdateColors ();
		} else if (!aiming && alpha > 0) {
			float changeAmount = alphaRate * Time.deltaTime;
			if (alpha - changeAmount <= 0) {
				alpha = 0;
			} else {
				alpha -= changeAmount;
			}
			UpdateColors ();
		}
	}

	void UpdateColors(){
		for (int i = 0; i < gunEnergySpriteArray.Length; i++) {
			gunEnergySpriteArray[i].color = new Color (gunEnergyColor.r, gunEnergyColor.g, gunEnergyColor.b, alpha);
		}
	}
}


