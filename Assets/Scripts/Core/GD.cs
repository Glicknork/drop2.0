﻿using UnityEngine;
using System.Collections;

public class GD : MonoBehaviour {

    public static PlayerController player;
    public static CameraController cameraController;
    public static SoundController soundController;
    public static MainUIController mainUI;

	// Use this for initialization
	void Awake ()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        cameraController = GameObject.Find("Main Camera").GetComponent<CameraController>();
        GetComponent<SoundController>();
        mainUI = GameObject.Find("MainUI").GetComponent<MainUIController>();
	}
	
	
}
