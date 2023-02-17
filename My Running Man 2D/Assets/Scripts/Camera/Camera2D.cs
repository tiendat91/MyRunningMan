using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Camera2D : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private HeroMotor playerToFollow;
    [SerializeField] private bool horizontalFollow = true;
    [SerializeField] private bool verticalFollow = true;

    [Header("Horizontal")]
    [SerializeField][Range(0, 1)] private float horizontalInfluence = 1f;
    [SerializeField] private float horizontalOffset = 0f;
    [SerializeField] private float horizontalSmoothness = 3f;

    [Header("Horizontal")]
    [SerializeField][Range(0, 1)] private float verticalInfluence = 1f;
    [SerializeField] private float verticalOffset = 0f;
    [SerializeField] private float verticalSmoothness = 3f;

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
        //Calculate position 
        TargetPosition = GetHeroPosition(playerToFollow);
        CameraTargetPosition = new Vector3(TargetPosition.x, TargetPosition.y, 0f);

        //Follow on selected axis


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
        Vector3 targetPosition = GetHeroPosition(player);
        transform.localPosition = targetPosition; // set camera
    }

}
