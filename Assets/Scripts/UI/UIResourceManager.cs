﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This is a singleton class component.
public class UIResourceManager : MonoBehaviour {
    public MouseCursor mouseCursor;

    public static MouseCursor MouseCursor;

    private Text enemyCountText;

    private static UIResourceManager _instance;

    public static UIResourceManager Instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<UIResourceManager>();
                // DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }

    void Awake() {
        if (_instance == null) {
            _instance = this;
            // DontDestroyOnLoad(this);
        } else if (this != _instance) {
            Destroy(gameObject);            
        }

        UIResourceManager.MouseCursor = mouseCursor;
        GetEnemyCountText();
        SetEnemyCountText();
    }

    void FixedUpdate() {
        SetEnemyCountText();
    }

    void GetEnemyCountText() {
        GameObject enemyCountTextObject = GameObject.Find("Enemy count text");
        enemyCountText = enemyCountTextObject.GetComponent<Text>();
    }

    public void SetEnemyCountText() {
        int totalEnemies = FindObjectsOfType<AI>().Length;
        enemyCountText.text = "x" + totalEnemies;
    }
}
