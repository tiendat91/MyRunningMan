using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Camera2D : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private bool horizontalFollow = true;
    [SerializeField] private bool verticalFollow = true;

    [Header("Horizontal")]
    [SerializeField][Range(0, 1)] private float horizontalInfluence = 1f;
    [SerializeField] private float horizontalOffset = 0f;
    [SerializeField] private float horizontalSmoothness = 3f;

    [Header("Vertical")]
    [SerializeField][Range(0, 1)] private float verticalInfluence = 1f;
    [SerializeField] private float verticalOffset = 0f;
    [SerializeField] private float verticalSmoothness = 3f;

    public HeroMotor Hero { get; set; }
    public Vector3 TargetPosition { get; set; } //the actual position of hero
    public Vector3 CameraTargetPosition { get; set; } //the position camera know about

    private float _targetHorizontalSmoothFollow;
    private float _targetVerticalSmoothFollow;

    private void Update()
    {
        MoveCamera();
    }

    private void MoveCamera()
    {
        if (Hero == null)
        {
            //stop moving when we dont have a hero/ hero is null
            Debug.Log("Hero is not found");
            return;
        }

        //Calculate position 
        TargetPosition = GetHeroPosition(Hero);
        CameraTargetPosition = new Vector3(TargetPosition.x, TargetPosition.y, 0f);

        //Follow on selected axis (true/false)
        float xPos = horizontalFollow ? CameraTargetPosition.x : transform.localPosition.x;
        float yPos = verticalFollow ? CameraTargetPosition.y : transform.localPosition.y;

        //Set offset (whether by horizontalFollow?)
        CameraTargetPosition += new Vector3(horizontalFollow ? horizontalOffset : 0f
                                            , verticalFollow ? verticalOffset : 0f, 0f);
        //Set smooth value:
        _targetHorizontalSmoothFollow = Mathf.Lerp(_targetHorizontalSmoothFollow,
                                            CameraTargetPosition.x,
                                            horizontalSmoothness * Time.deltaTime);
        _targetVerticalSmoothFollow = Mathf.Lerp(_targetVerticalSmoothFollow,
                                            CameraTargetPosition.y,
                                            verticalSmoothness * Time.deltaTime);
        //Get direction towards target pos
        float xDirection = _targetHorizontalSmoothFollow - transform.localPosition.x;
        float yDirection = _targetVerticalSmoothFollow - transform.localPosition.y;

        Vector3 deltaDirection = new Vector3(xDirection, yDirection, 0f);

        //New position (where the frame at...)
        Vector3 newCameraPosition = transform.localPosition + deltaDirection;

        //Apply new position 
        transform.localPosition = new Vector3(newCameraPosition.x, newCameraPosition.y, transform.localPosition.z);
    }

    //get heros position
    private Vector3 GetHeroPosition(HeroMotor player)
    {
        float xPos = 0f;
        float yPos = 0f;

        xPos += (player.transform.position.x + horizontalOffset) * horizontalInfluence;
        yPos += (player.transform.position.y + verticalOffset) * verticalInfluence;

        Vector3 positionTarget = new Vector3(xPos, yPos, transform.position.z);
        return positionTarget;
    }

    //set camera position towards heros position
    private void CenterOnTarger(HeroMotor player)
    {
        Hero = player;
        Vector3 targetPosition = GetHeroPosition(Hero);
        //set smooth at the begining start
        _targetHorizontalSmoothFollow = targetPosition.x;
        _targetVerticalSmoothFollow = targetPosition.y;

        transform.localPosition = targetPosition; // set camera
    }

    //See camera center on screen 
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 camPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + 2f);
        Gizmos.DrawWireSphere(camPosition, 0.5f);
    }
    private void StopFollow(HeroMotor player)
    {
        Hero = null;
    }

    private void StartFollow(HeroMotor player)
    {
        Hero = player;
        CenterOnTarger(Hero);
    }

    private void OnEnable()
    {
        LevelManager.OnPlayerSpawn += CenterOnTarger;
        Health.OnDeath += StopFollow;
        Health.OnRevive += StartFollow;
    }

    private void OnDisable()
    {
        LevelManager.OnPlayerSpawn -= CenterOnTarger;
        Health.OnDeath -= StopFollow;
        Health.OnRevive -= StartFollow;
    }

}
