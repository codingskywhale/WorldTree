using Unity.VisualScripting;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public Camera mainCamera; // 이동시킬 카메라
    public float dragSpeed = 3; // 드래그 속도

    private Vector3 dragOrigin;

    public SpriteRenderer bgrenderer;
    private float spriteSizeX;
    private float camAreaX;

    // 실제 맵과의 거리 조절을 위해.
    public float offsetX;

    private void Start()
    {
        setCameraInitialPos();
    }
    void Update()
    {
        HandleMouse();
    }

    void HandleTouch()
    {
        if (Input.touchCount == 1) // 하나의 터치가 있을 때만 처리
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    dragOrigin = mainCamera.ScreenToWorldPoint(touch.position);
                    break;

                case TouchPhase.Moved:
                    Vector3 currentTouchPosition = mainCamera.ScreenToWorldPoint(touch.position);
                    Vector3 direction = dragOrigin - currentTouchPosition;
                    direction.y = 0f; // y좌표 값 고정
                    mainCamera.transform.position += direction * dragSpeed * Time.deltaTime;
                    break;

                case TouchPhase.Ended:
                    // 터치가 끝났을 때 필요한 동작이 있다면 추가
                    break;
            }
        }
    }

    void HandleMouse()
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼 클릭 시작
        {
            dragOrigin = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0)) // 마우스 왼쪽 버튼 드래그
        {
            Vector3 currentMousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = dragOrigin - currentMousePosition;
            direction.y = 0f; // y좌표 값 고정
            mainCamera.transform.position += direction * dragSpeed * Time.deltaTime;
            LimitCameraArea();
        }
    }

    void setCameraInitialPos()
    {
        // bgrenderer.sprite.bounds.size.x // 스프라이트 자체 x 크기
        // bgrenderer.sprite.bounds.size.y // 스프라이트 자체 y 크기
        // bgrenderer.transform.localScale.x // 스프라이트 Scale 값. 
        spriteSizeX = (bgrenderer.sprite.bounds.size.x * bgrenderer.transform.localScale.x) / 2; //스프라이트 사이즈 절반
        camAreaX = Screen.width * mainCamera.orthographicSize / Screen.height; // 카메라 사이즈 반
        mainCamera.transform.position = new Vector3((bgrenderer.sprite.bounds.size.x * bgrenderer.transform.localScale.x) / 2 - (Screen.width * mainCamera.orthographicSize / Screen.height), mainCamera.transform.position.y, -10f);
    }
    void LimitCameraArea()
    {
        // 최대 영역 - 카메라 영역까지만 갈수 있게 설정하면 됨.

        // => 자체 크기 * scale 값 = 실제 크기
        // Debug.Log(Screen.width *mainCamera.orthographicSize / Screen.height); => 카메라 가로 / 2 사이즈
        // 맵 왼쪽 기준 map.min + 가로 / 2 
        // 맵 오른쪽 기준 map.max - 가로 /2 

        float clampX = Mathf.Clamp(mainCamera.transform.position.x, -(spriteSizeX - camAreaX), (spriteSizeX - camAreaX));
        mainCamera.transform.position = new Vector3(clampX, mainCamera.transform.position.y, -10f);
    }
}