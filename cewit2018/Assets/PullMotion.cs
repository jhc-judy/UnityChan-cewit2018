using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;
using UnityEngine.UI;


public class PullMotion : MonoBehaviour {

	private enum State { NORMAL, PULLING, HOLDING, PUSHING };

    [SerializeField] public VRInteractiveItem m_InteractiveItem;
    [SerializeField] public Camera camera;
    public float MaxSpeed = 10.0f;
    public float Distance = 30.0f;
    public float ShrinkRatio = 0.2f;

    public float CircularSpeed = 0.5f;
    

    public float RotationSpeed = 20.0f;

    private float radius;
    private Vector3 center;
    private float angle;
    private State state;
    private Vector3 originalPosition;
    private Vector3 originalScale;

    private Text priceTag;

    // Use this for initialization
    void Start ()
    {
        state = State.NORMAL;
        center = camera.transform.position;
        center.y = transform.position.y;
        radius = Mathf.Sqrt(Mathf.Pow(center.x - transform.position.x, 2) + Mathf.Pow(center.z - transform.position.z, 2));
        angle = Vector3.Angle(center, transform.position);
        priceTag = camera.GetComponentInChildren<Text>();
        priceTag.enabled = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (state == State.PULLING)
        {
            Vector3 dest = camera.transform.position;
            if ((transform.position - dest).sqrMagnitude <= Distance)
            {
                state = State.HOLDING;
                priceTag.enabled = true;
                return;
            }
            float ratio = Vector3.Distance(dest, transform.position) / Vector3.Distance(dest, originalPosition);
            float speed = ratio * MaxSpeed;
            transform.position = Vector3.MoveTowards(transform.position, dest, speed * Time.deltaTime);
            if (ratio < ShrinkRatio)
                ratio = ShrinkRatio;
            transform.localScale = originalScale * ratio;
        }
        else if (state == State.PUSHING)
        {
            Vector3 dest = originalPosition;
            if ((transform.position - dest).sqrMagnitude <= 0.5f)
            {
                transform.position = dest;
                transform.localScale = originalScale;
                state = State.NORMAL;
                return;
            }
            float ratio = Vector3.Distance(dest, transform.position) / Vector3.Distance(dest, camera.transform.position);
            float speed = (1 - ratio) * MaxSpeed;
            transform.position = Vector3.MoveTowards(transform.position, dest, speed * Time.deltaTime);
            if (ratio > 1 - ShrinkRatio)
                ratio = 1 - ShrinkRatio;
            transform.localScale = originalScale * (1 - ratio);
    
        }
        else if (state == State.NORMAL)
        {
            angle += CircularSpeed * Time.deltaTime;
            if (angle >= 360.0f)
                angle -= 360.0f;

            var offset = new Vector3(Mathf.Sin(angle), 0.0f, Mathf.Cos(angle)) * radius;
            transform.position = center + offset;
        }
        else
        {
            ApplyRotationEffect();
        }
		
	}

    public void OnEnable()
    {
        m_InteractiveItem.OnClick += HandleClick;
    }

    //Handle the Click event
    private void HandleClick()
    {
        if (state == State.NORMAL)
        {
            state = State.PULLING;
            originalScale = transform.localScale;
            originalPosition = transform.position;
        }
        else if (state == State.HOLDING)
        {
            priceTag.enabled = false;
            state = State.PUSHING;
        }
    }

    // Apply a random rotation effect
    void ApplyRotationEffect()
    {
        transform.Rotate(Vector3.up, Time.deltaTime * RotationSpeed);

    }
}
