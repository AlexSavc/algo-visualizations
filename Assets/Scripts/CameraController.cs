﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour 
{
	
	public float panSpeed = 10f;
	public float panBorderThickness = 10f;
    public Vector2 upperPanLimit;
    Vector3 original;
    public float rotateSpeed = 5f;

    public float scrollSpeed = 200f;
    public float MinOrthSize = 1f;
    public float MaxOrthSize = 50f;

    public float panZoomCoeff = 100;

    public float camzoffset = -10;

    void Start()
    {
        //FindObjectOfType<Map>().OnGenerateMap += SetPanLimitByMap;
    }

    public void SetPanLimitByMap()
    {
    }

	void Update ()
	{
		Vector3 Pos = transform.position;
		

		if (Input.GetKey("w") /*|| Input.mousePosition.y >= Screen.height - panBorderThickness*/)
		{
			Pos.y += panSpeed * Time.deltaTime;
        }

		transform.position = Pos;

		if (Input.GetKey("s") /*|| Input.mousePosition.y <= panBorderThickness*/)
		{
			Pos.y -= panSpeed * Time.deltaTime;
        }

		transform.position = Pos;

		if (Input.GetKey("d") /*|| Input.mousePosition.x >= Screen.width - panBorderThickness*/)
		{
			Pos.x += panSpeed * Time.deltaTime;
        }

		transform.position = Pos;

		if (Input.GetKey("a")/* || Input.mousePosition.x <= panBorderThickness*/)
		{
			Pos.x -= panSpeed * Time.deltaTime;
        }

        if(Input.GetKey(KeyCode.LeftControl))
        {
            float s = Camera.main.orthographicSize;
            float scroll = Input.GetAxis("Mouse ScrollWheel");

            if (s < MinOrthSize) Camera.main.orthographicSize = MinOrthSize;
            if (s > MaxOrthSize) Camera.main.orthographicSize = MaxOrthSize;
            Camera.main.orthographicSize -= scroll * scrollSpeed * s * Time.deltaTime;
            
            panSpeed = panZoomCoeff * Camera.main.orthographicSize;
        }

		Pos.x = Mathf.Clamp (Pos.x, -upperPanLimit.x, upperPanLimit.x);
		Pos.y = Mathf.Clamp (Pos.y, -upperPanLimit.y, upperPanLimit.y);
        
		transform.position = Pos;
    }

    public void CenterOnGameObject(GameObject obj)
    {
        Camera.main.transform.position = obj.transform.position;
    }
}
