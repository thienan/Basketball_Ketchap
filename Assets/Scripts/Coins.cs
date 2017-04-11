﻿using UnityEngine;
using UnityEngine.UI;

public class Coins : MonoBehaviour {

	public Text textField;
	public Image img;
	int _pointsCount = 0;
	bool _isShowAnimation = true;
	float _startScale;
	AudioSource _audioSource;
	AudioClip _sndCoin;

	// Use this for initialization
	void Start ()
	{
	    DefsGame.Coins = this;
		_audioSource = GetComponentInParent<AudioSource> ();
		_sndCoin = Resources.Load<AudioClip>("snd/bonus");
		textField.text = DefsGame.coinsCount.ToString();
		//DefsGame.coinsIcon.UpdatePosition ();
		_pointsCount = DefsGame.coinsCount;
		Color color = textField.color;
		color.a = 0f;
		textField.color = color;
		img.color = new Color(img.color.r, img.color.g, img.color.b, color.a);
		_startScale = img.transform.localScale.x;
	}

	void OnEnable() {
		//Bubble.OnAddCoins += Bubble_OnAddCoins;
		Coin.OnAddCoinsVisual += Coin_OnAddCoinsVisual;
		ScreenSkins.OnAddCoinsVisual += Coin_OnAddCoinsVisual;
		ScreenCoins.OnAddCoinsVisual += Coin_OnAddCoinsVisual;
		//BillingManager.OnAddCoinsVisual += Coin_OnAddCoinsVisual;
		//ScreenMenu.OnAddCoins += Bubble_OnAddCoins;
	}

	void OnDisable() {
		//Bubble.OnAddCoins -= Bubble_OnAddCoins;
		Coin.OnAddCoinsVisual -= Coin_OnAddCoinsVisual;
		ScreenSkins.OnAddCoinsVisual -= Coin_OnAddCoinsVisual;
		ScreenCoins.OnAddCoinsVisual -= Coin_OnAddCoinsVisual;
		//BillingManager.OnAddCoinsVisual -= Coin_OnAddCoinsVisual;
		//ScreenMenu.OnAddCoins -= Bubble_OnAddCoins;
	}

	void Coin_OnAddCoinsVisual (int value)
	{
		AddPointVisual (value);
		_audioSource.PlayOneShot (_sndCoin);
	}

	void Bubble_OnAddCoins (int value)
	{
		AddPoint (value);
	}

	public void ResetCounter() {
		_pointsCount = 0;
		textField.text = "0";
	}

	// Update is called once per frame
	void Update () {
		if (_isShowAnimation) {
			Color _color = textField.color;
			if (textField.color.a < 1f) {
				_color.a += 0.1f;
			} else {
				_isShowAnimation = false;
				_color.a = 1f;
			}
			textField.color = _color;
			img.color = new Color(img.color.r, img.color.g, img.color.b, _color.a);
		}

		if (img.transform.localScale.x > _startScale) {
			img.transform.localScale = new Vector3 (img.transform.localScale.x - 2.0f*Time.deltaTime, img.transform.localScale.y - 2.0f*Time.deltaTime, 1f);
		}
	}

	public void AddPoint(int count)
	{
		_pointsCount += count;
		DefsGame.coinsCount += count;
		PlayerPrefs.SetInt ("coinsCount", DefsGame.coinsCount);
		//DefsGame.coinsIcon.UpdatePosition ();
	}

	void AddPointVisual(int value)
	{
		AddPoint(value);
		textField.text = _pointsCount.ToString ();
		img.transform.localScale = new Vector3 (_startScale*1.4f, _startScale*1.4f, 1f);
	}

	public void UpdateVisual() {
		textField.text = _pointsCount.ToString ();
	}
}
