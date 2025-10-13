using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    
    [SerializeField] private float speed = 5f;

    [SerializeField] private Sprite leftSprite;
    [SerializeField] private Sprite rightSprite;
    [SerializeField] protected ChallengeGameHandler cgh;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        int xInput = Keyboard.current.aKey.isPressed ? -1 :
                    Keyboard.current.dKey.isPressed ? 1 : 0;
        int xInput2 = Keyboard.current.leftArrowKey.isPressed ? -1 :
                      Keyboard.current.rightArrowKey.isPressed ? 1 : 0;


        int yInput = Keyboard.current.wKey.isPressed ? 1 :
                     Keyboard.current.sKey.isPressed ? -1 : 0;
        int yInput2 = Keyboard.current.upArrowKey.isPressed ? 1 :
                      Keyboard.current.downArrowKey.isPressed ? -1 : 0;


        rb.linearVelocity = (cgh == null || cgh.gameActive) ? new Vector2((xInput + xInput2) * speed, (yInput + yInput2) * speed) : new Vector2(0f, 0f); 
        if (xInput + xInput2 < 0)
        {
            sr.sprite = leftSprite;
        }
        else if (xInput + xInput2 > 0)
        {
            sr.sprite = rightSprite;
        }

    }
}