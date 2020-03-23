using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraContral : MonoBehaviour
{
    public float speed = 15f;
    public float zoomSpeed = 15f;
    Vector3 m_Movement;
    Vector3 m_ScrollMovement;
    float m_DeltaTime;
    bool mouseDown;

    private void Start()
    {
        m_DeltaTime = Time.deltaTime;
    }
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        float mouse = Input.GetAxis("Mouse ScrollWheel");
        m_Movement.Set(h, 0f, v);
        //transform.Translate(m_Movement*Time.deltaTime*speed, Space.World);
        m_ScrollMovement.Set(0f, 0f, mouse);
        //transform.Translate(m_ScrollMovement * Time.deltaTime * zoomSpeed, Space.Self);

        bool isMouseKeyDown = Input.GetMouseButtonDown(0);
        bool isMouseKeyUp = Input.GetMouseButtonUp(0);
        if (isMouseKeyDown)
            mouseDown = true;
        else if (isMouseKeyUp)
            mouseDown = false;
        if (mouseDown)
        {
            float mh = Input.GetAxis("Mouse X");
            float mv = Input.GetAxis("Mouse Y");
            m_Movement.Set(mh, 0f, mv);
            //transform.Translate(m_Movement * Time.deltaTime * speed*-1, Space.World);
        }
        if(Time.timeScale==0)
        {
            if (mouseDown)
                transform.Translate(m_Movement * m_DeltaTime * speed * -1, Space.World);
            else
                transform.Translate(m_Movement * m_DeltaTime * speed, Space.World);
            transform.Translate(m_ScrollMovement * m_DeltaTime * zoomSpeed, Space.Self);
        }
    }
    private void FixedUpdate()
    {
        m_DeltaTime = Time.deltaTime;
        if(mouseDown)
            transform.Translate(m_Movement * m_DeltaTime * speed * -1, Space.World);
        else
            transform.Translate(m_Movement * m_DeltaTime * speed, Space.World);
        transform.Translate(m_ScrollMovement * m_DeltaTime * zoomSpeed, Space.Self);
    }
}
