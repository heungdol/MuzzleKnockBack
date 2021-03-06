﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class UIInformLvUp : MonoBehaviour {

	public Text informText;

	public UnityEvent InformEvent;

	private Vector3 startLocalPosition;

	private Vector4 startTextColor;

	void Start () {
		startLocalPosition = informText.transform.localPosition;

		startTextColor = informText.color;

		informText.gameObject.SetActive (false);
	}

	public void StartInforming (string str) {
		informText.text = str;
		InformEvent.Invoke ();
		StartCoroutine (Informing ());
	}

	public IEnumerator Informing () {
		informText.gameObject.SetActive (true);
		informText.color = startTextColor;

		float currentAngle = 0;
		float deltaAngle = 45;
		float maxPositionY = 1f;

		currentAngle = 0;

		while (currentAngle <= 90) {
			currentAngle += Time.deltaTime * deltaAngle;
			informText.transform.localPosition = startLocalPosition + Vector3.up * Mathf.Sin (currentAngle * Mathf.Deg2Rad) * maxPositionY;
			yield return null;
		}

		float fadeOutPerSec = 0.5f;

		Vector4 currentTextColor = startTextColor;
		while (currentTextColor.w > 0) {
			currentTextColor.w -= Time.deltaTime / fadeOutPerSec;
			informText.color = currentTextColor;
			//Debug.Log (currentTextColor.w);
			yield return null;
		}

		informText.gameObject.SetActive (false);
	}
	
}
