using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    public static CursorController Instance { get; private set; }

    [SerializeField] private List<CursorAnimation> cursorAnimationList;

    private CursorAnimation cursorAnimation;

    private int currentFrame;
    private float frameTimer;
    private int frameCount;


    public enum CursorType
    {
        Solid, Drained
    }

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetActiveCursorType(CursorType.Solid);
    }

    // Update is called once per frame
    void Update()
    {
        FrameTiming();
        if (Input.GetKeyDown(KeyCode.T)) SetActiveCursorAnimation(cursorAnimationList[0]);
        if (Input.GetKeyDown(KeyCode.Y)) SetActiveCursorAnimation(cursorAnimationList[1]);
    }

    public void SetCursorDrained()
    {
        SetActiveCursorType(CursorType.Drained);
    }

    public void SetCursorSolid()
    {
        SetActiveCursorType(CursorType.Solid);
    }

    public void FrameTiming()
    {
        frameTimer -= Time.deltaTime;
        if (frameTimer <= 0f)
        {
            frameTimer += cursorAnimation.frameRate;
            currentFrame = (currentFrame + 1) % frameCount;
            Cursor.SetCursor(cursorAnimation.textureArray[currentFrame], cursorAnimation.offset, CursorMode.Auto);
        }     
    }

    public void SetActiveCursorType(CursorType cursorType)
    {
        SetActiveCursorAnimation(GetCursorAnimation(cursorType));
    }

    private CursorAnimation GetCursorAnimation (CursorType cursorType)
    {
        foreach (CursorAnimation cursorAnimation in cursorAnimationList)
        {
            if (cursorAnimation.cusorType == cursorType)
            {
                return cursorAnimation;
            }
        }
        return null;
    }
     

    private void SetActiveCursorAnimation(CursorAnimation cursorAnimation)
    {
        this.cursorAnimation = cursorAnimation;
        currentFrame = 0;
        frameTimer = cursorAnimation.frameRate;
        frameCount = cursorAnimation.textureArray.Length;
    }

    [System.Serializable]
    public class CursorAnimation
    {
        public CursorType cusorType;
        public Texture2D[] textureArray;
        public float frameRate;
        public Vector2 offset;
    }
}
