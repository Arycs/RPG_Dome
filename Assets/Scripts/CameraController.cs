using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Tilemap map;

    [SerializeField]
    private float smoothSpeed;
    private float xMax, xMin, yMin, yMax;

    [Header("Camera Shake")]
    private Vector3 shakeActive;
    private float shakeAmplify;

    private void Awake()
    {
        Vector3 minTile = map.CellToWorld(map.cellBounds.min);
        Vector3 maxTile = map.CellToWorld(map.cellBounds.max);
        target.GetComponent<Player>().SetLimits(minTile, maxTile);
        SetLimits(minTile, maxTile);
    }

    private void Update()
    {
        if (shakeAmplify > 0)
        {
            shakeActive = new Vector3(Random.Range(-shakeAmplify, shakeAmplify), Random.Range(-shakeAmplify, shakeAmplify), 0f);
            shakeAmplify -= Time.deltaTime;
        }
        else
        {
            shakeActive = Vector3.zero;
        }

        transform.position += shakeActive;
    }

    public void CameraShake(float _amount)
    {
        shakeAmplify = _amount;
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, smoothSpeed * Time.deltaTime);
        CameraClamp();
    }

    private void CameraClamp()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, xMin, xMax), Mathf.Clamp(transform.position.y, yMin, yMax), -10);
    }

    private void SetLimits(Vector3 minTile, Vector3 maxTile)
    {
        Camera cam = Camera.main;
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;

        xMin = minTile.x + width / 2;
        xMax = maxTile.x - width / 2;

        yMin = minTile.y + height / 2;
        yMax = maxTile.y - height / 2;
    }


    


}
