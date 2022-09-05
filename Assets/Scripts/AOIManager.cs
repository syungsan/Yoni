using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AOIManager : MonoBehaviour
{
    public Rect aoiRect = default;
    private Texture _texture = default;
    private Vector2 mousePosition = default;
    private Vector2 mouseDelta = default;

    // Start is called before the first frame update
    void Start()
    {
        Texture2D texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, Color.white);
        texture.Apply();
        this._texture = texture;
    }

    private void OnGUI()
    {
        float x;
        float y;
        float width;
        float height;

        // フリップ処理
        if (this.mouseDelta.x < 0)
        {
            x = this.mousePosition.x - Mathf.Abs(this.mouseDelta.x);
            width = Mathf.Abs(this.mouseDelta.x);
        }
        else
        {
            x = this.mousePosition.x;
            width = this.mouseDelta.x;
        }

        if (-1.0f * this.mouseDelta.y < 0)
        {
            y = Screen.height - this.mousePosition.y - Mathf.Abs(-1.0f * this.mouseDelta.y);
            height = Mathf.Abs(-1.0f * this.mouseDelta.y);
        }
        else
        {
            y = Screen.height - this.mousePosition.y;
            height = -1.0f * this.mouseDelta.y;
        }

        // 枠抜き矩形の表示
        this.aoiRect = new Rect(x, y, width, height);
        GUI.DrawTexture(this.aoiRect, this._texture, ScaleMode.StretchToFill, true, 0, Color.red, 3, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current != null)
        { 
            // 押した瞬間
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                this.mousePosition = Mouse.current.position.ReadValue();
            }
            // 押してる間
            else if (Mouse.current.leftButton.IsPressed())
            {
                this.mouseDelta = Mouse.current.position.ReadValue() - this.mousePosition;
            }
            // 離した瞬間
            else if (Mouse.current.leftButton.wasReleasedThisFrame && this.mouseDelta != Vector2.zero)
            {
                Debug.Log($"aoiRect => {this.aoiRect} に対するヒートマップ表示処理をせよ！");
            }
        }
    }
}
