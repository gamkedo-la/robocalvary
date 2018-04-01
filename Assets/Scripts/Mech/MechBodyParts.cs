﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechBodyParts : MonoBehaviour {
    Transform[] bodyPartsTransforms;


	// Use this for initialization
	void Start () {
        bodyPartsTransforms = GetComponentsInChildren<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void MakeBodyParts (float expForceMin, float expForceMax) {
        for (int i = 0; i < bodyPartsTransforms.Length; i++) {
            GameObject bodyPart = bodyPartsTransforms[i].gameObject;

            SkinnedMeshRenderer bodyPartSkinnedMesh = bodyPart.GetComponent<SkinnedMeshRenderer>();
            MeshRenderer bodyPartMesh = bodyPart.GetComponent<MeshRenderer>();

            if (bodyPartSkinnedMesh == null && bodyPartMesh == null) continue;            

            GameObject bodyPartParent = new GameObject(bodyPart.name);
            bodyPartParent.layer = 12;

            Bounds boundsOfMesh = new Bounds();

            if (bodyPartSkinnedMesh != null) {
                boundsOfMesh.Encapsulate(bodyPartSkinnedMesh.bounds);
            }
            else if (bodyPartMesh != null) {
                boundsOfMesh.Encapsulate(bodyPartMesh.bounds);
            }
            
            bodyPartParent.transform.localPosition = boundsOfMesh.center;
            
            bodyPart.transform.SetParent(bodyPartParent.transform);

            CircleCollider2D bodyPartCollider2D = bodyPartParent.GetComponent<CircleCollider2D>();
            bodyPartCollider2D = bodyPartCollider2D == null ? bodyPartParent.AddComponent<CircleCollider2D>() : bodyPartCollider2D;
            bodyPartCollider2D.enabled = true;            
            bodyPartCollider2D.offset = new Vector2(boundsOfMesh.center.x - bodyPartCollider2D.radius / bodyPart.transform.localScale.x, 
                                                    boundsOfMesh.center.y - bodyPartCollider2D.radius / bodyPart.transform.localScale.y);

            Rigidbody2D bodyPartRb2D = bodyPartParent.GetComponent<Rigidbody2D>();
            bodyPartRb2D = bodyPartRb2D == null ? bodyPartParent.AddComponent<Rigidbody2D>() : bodyPartRb2D;
            bodyPartRb2D.AddForce(Quaternion.Euler(0, 0, Random.Range(0, 360)) * Vector2.left * Random.Range(expForceMin, expForceMax));

            bodyPartParent.transform.SetParent(null);
        }
    }
}
