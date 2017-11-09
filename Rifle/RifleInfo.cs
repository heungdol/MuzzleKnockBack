using UnityEngine;

public class RifleInfo : MonoBehaviour {

	[System.Serializable]
	public class RifleLevelProperties {
		public int rifleBulletRangeLv;
		public int rifleBulletDamageLv;
		public int rifleBundleBulletNumberLv;
		public int rifleMaxReloadBulletNumberLv;

		public RifleLevelProperties (int rifleBulletRangeLv, int rifleBulletDamageLv, int rifleBundleBulletNumberLv, int rifleMaxReloadBulletNumberLv)
		{
			this.rifleBulletRangeLv = rifleBulletRangeLv;
			this.rifleBulletDamageLv = rifleBulletDamageLv;
			this.rifleBundleBulletNumberLv = rifleBundleBulletNumberLv;
			this.rifleMaxReloadBulletNumberLv = rifleMaxReloadBulletNumberLv;
		}
	}

	public void AddLvRandomly () {
		int i = Random.Range (0, 4);

		switch (i) {
		case 0:	// range
			AddRifleBulletRangeLv ();
			uiInformLvUp.StartInforming ("RANGE UP!");
			break;
		case 1:	// damage
			AddRifleBulletDamageLv();
			uiInformLvUp.StartInforming ("DAMAGE UP!");
			break;
		case 2:	// bundle
			AddRifleBundleBulletNumberLv();
			uiInformLvUp.StartInforming ("BUCKSHOT UP!");
			break;
		case 3:	// max reload
			AddRifleReloadBulletNumberLv();
			uiInformLvUp.StartInforming ("MAGAZINE UP!");
			break;
		}
	}

	[System.Serializable]
	public class RifleProperties
	{
		public float rifleBulletRange;
		public float rifleBulletDamage;
		public int rifleBundleBulletNumber;
		public int rifleMaxReloadBulletNumber;
		public int rifleCurrentReloadBulletNumber;
		public float rifleFixedKnockBackForce;
		public float rifleFixedBulletTime;
		public float rifleFixedFireDelayTime;
		public float rifleFixedReloadTime;
		public float rifleFixedAngleBetBullets;

		public RifleProperties (float rifleBulletRange, float rifleBulletDamage, int rifleBundleBulletNumber, int rifleMaxReloadBulletNumber, int rifleCurrentReloadBulletNumber, float rifleFixedKnockBackForce, float rifleFixedBulletTime, float rifleFixedFireDelayTime, float rifleFixedReloadTime, float rifleFixedAngleBetBullets)
		{
			this.rifleBulletRange = rifleBulletRange;
			this.rifleBulletDamage = rifleBulletDamage;
			this.rifleBundleBulletNumber = rifleBundleBulletNumber;
			this.rifleMaxReloadBulletNumber = rifleMaxReloadBulletNumber;
			this.rifleCurrentReloadBulletNumber = rifleCurrentReloadBulletNumber;

			this.rifleFixedKnockBackForce = rifleFixedKnockBackForce;
			this.rifleFixedBulletTime = rifleFixedBulletTime;
			this.rifleFixedFireDelayTime = rifleFixedFireDelayTime;
			this.rifleFixedReloadTime = rifleFixedReloadTime;
			this.rifleFixedAngleBetBullets = rifleFixedAngleBetBullets;
		}
	}

	private RifleLevelProperties rifleLevelProperties;
	public RifleProperties rifleProperties;

	private MouseOutlineSpritesManager mouseOutlineSpriteManager;
	private MouseController mouseController;

	private UIAmmo uiAmmo;
	private UIInformLvUp uiInformLvUp;

	private float currentFireTime;
	private float currentReloadTime;

	void Start () {
		rifleLevelProperties = new RifleLevelProperties (1, 1, 1, 1);
		rifleProperties = new RifleProperties (
			GetRifleBulletRange(rifleLevelProperties.rifleBulletRangeLv)
			, GetRifleBulletDamage(rifleLevelProperties.rifleBulletDamageLv)
			, GetRifleBundleBulletNumber (rifleLevelProperties.rifleBundleBulletNumberLv)
			, GetRifleReloadBulletNumber(rifleLevelProperties.rifleMaxReloadBulletNumberLv)
			, GetRifleReloadBulletNumber(rifleLevelProperties.rifleMaxReloadBulletNumberLv)

			, 7.5f, 0.5f, 0.25f, 0.75f, 5f);

		mouseOutlineSpriteManager = FindObjectOfType<MouseOutlineSpritesManager>();
		mouseOutlineSpriteManager.SetInfo (/*rifleProperties.rifleBulletRange,*/ rifleProperties.rifleBulletDamage, rifleProperties.rifleBundleBulletNumber/*, gameObject*/);
		mouseOutlineSpriteManager.ResetMouseSprites ();

		mouseController = GameObject.FindWithTag ("Mouse").GetComponent<MouseController> ();

		uiAmmo = FindObjectOfType<UIAmmo> ();
		uiAmmo.ChangeImage (rifleProperties.rifleMaxReloadBulletNumber, rifleProperties.rifleCurrentReloadBulletNumber);

		uiInformLvUp = FindObjectOfType<UIInformLvUp> ();

		MouseInfoReset ();
		mouseController.SetCurrentRange (this.rifleProperties.rifleBulletRange);
	}

	void MouseInfoReset () {
		mouseOutlineSpriteManager.FixInfo (/*rifleProperties.rifleBulletRange,*/ rifleProperties.rifleBulletDamage, rifleProperties.rifleBundleBulletNumber);
		mouseOutlineSpriteManager.ResetMouseSprites ();
	}

	public void AddRifleBulletRangeLv () {
		rifleLevelProperties.rifleBulletRangeLv++;
		rifleProperties.rifleBulletRange = GetRifleBulletRange (rifleLevelProperties.rifleBulletRangeLv);
		//MouseInfoReset ();
		mouseController.SetCurrentRange (this.rifleProperties.rifleBulletRange);
	}

	float GetRifleBulletRange (int lv) {
		float startRange = 5f;
		float perRange = 2.5f;
		float returnRange = startRange + perRange * (lv - 1);
		return returnRange;
	}

	public void AddRifleBulletDamageLv () {
		rifleLevelProperties.rifleBulletDamageLv++;
		rifleProperties.rifleBulletDamage = GetRifleBulletDamage (rifleLevelProperties.rifleBulletDamageLv);
		MouseInfoReset ();
	}

	float GetRifleBulletDamage (int lv) {
		float startDamage = 10;
		float perDamage = 5;
		float returnDamage = startDamage + perDamage * (lv - 1);
		return returnDamage;
	}

	public void AddRifleBundleBulletNumberLv () {
		rifleLevelProperties.rifleBundleBulletNumberLv++;
		rifleProperties.rifleBundleBulletNumber = GetRifleBundleBulletNumber (rifleLevelProperties.rifleBundleBulletNumberLv);
		MouseInfoReset ();
	}

	int GetRifleBundleBulletNumber (int lv) {
		return lv;
	}

	public void AddRifleReloadBulletNumberLv () {
		rifleLevelProperties.rifleMaxReloadBulletNumberLv++;
		rifleProperties.rifleMaxReloadBulletNumber = GetRifleReloadBulletNumber (rifleLevelProperties.rifleMaxReloadBulletNumberLv);
		uiAmmo.ChangeImage (rifleProperties.rifleMaxReloadBulletNumber, rifleProperties.rifleCurrentReloadBulletNumber);
	}

	int GetRifleReloadBulletNumber (int lv) {
		int startNumber = 5;
		int perNumber = 1;
		int returnNumber = startNumber + perNumber * (lv - 1);
		return returnNumber;
	}

	public bool IsFirable () {
		if (rifleProperties.rifleCurrentReloadBulletNumber > 0 && currentFireTime >= 1) {
			return true;
		} else {
			return false;
		}
	}

	public void FireInfo () {
		currentFireTime = 0;
		currentReloadTime = 0;
		rifleProperties.rifleCurrentReloadBulletNumber--;
		uiAmmo.ChangeImage (rifleProperties.rifleMaxReloadBulletNumber, rifleProperties.rifleCurrentReloadBulletNumber);
	}
		
	void Update () {
		currentFireTime += Time.deltaTime / rifleProperties.rifleFixedFireDelayTime;
		currentFireTime = Mathf.Clamp01 (currentFireTime);

		if (rifleProperties.rifleCurrentReloadBulletNumber == rifleProperties.rifleMaxReloadBulletNumber) {
			currentReloadTime = 0;
		} else {
			currentReloadTime += Time.deltaTime / rifleProperties.rifleFixedReloadTime;
			currentReloadTime = Mathf.Clamp01 (currentReloadTime);

			if (currentReloadTime >= 1) {
				currentReloadTime = 0;
				rifleProperties.rifleCurrentReloadBulletNumber++;
				uiAmmo.ChangeImage (rifleProperties.rifleMaxReloadBulletNumber, rifleProperties.rifleCurrentReloadBulletNumber);
			}
		}

		TestControl ();
	}

	public RifleProperties GetRifleProperties () {
		return rifleProperties;
	}

	void TestControl () {
		// test code
		if (Input.GetKeyDown (KeyCode.U)) {
			AddRifleBulletRangeLv ();
		}

		if (Input.GetKeyDown (KeyCode.I)) {
			AddRifleBulletDamageLv ();
		}

		if (Input.GetKeyDown (KeyCode.O)) {
			AddRifleBundleBulletNumberLv ();
		}

		if (Input.GetKeyDown (KeyCode.P)) {
			AddRifleReloadBulletNumberLv ();
		}
	}
}
