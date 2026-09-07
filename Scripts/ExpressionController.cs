using UnityEngine;
using UnityEngine.InputSystem;
using Live2D.Cubism.Core;

[DefaultExecutionOrder(11000)]
public class ExpressionController : MonoBehaviour
{
    private CubismParameter eyeLSmile;
    private CubismParameter eyeRSmile;

    private CubismParameter browLForm;
    private CubismParameter browRForm;

    private CubismParameter mouthForm;

    private int currentExpression = 0;

    // =========================
    // UIボタン用
    // =========================

    public void SetNormal()
    {
        currentExpression = 0;
    }

    public void SetSmile()
    {
        currentExpression = 1;
    }

    public void SetTroubled()
    {
        currentExpression = 2;
    }

    public void SetSurprise()
    {
        currentExpression = 3;
    }

    void Start()
    {
        eyeLSmile = FindParameter("ParamEyeLSmile");
        eyeRSmile = FindParameter("ParamEyeRSmile");

        browLForm = FindParameter("ParamBrowLForm");
        browRForm = FindParameter("ParamBrowRForm");

        mouthForm = FindParameter("ParamMouthForm");
    }

    void Update()
    {
        if (Keyboard.current == null)
            return;

        // 0キー：通常
        if (Keyboard.current.digit0Key.wasPressedThisFrame)
        {
            currentExpression = 0;
            Debug.Log("Expression: Normal");
        }

        // 1キー：笑顔
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            currentExpression = 1;
            Debug.Log("Expression: Smile");
        }

        // 2キー：困り・怒り寄り
        if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            currentExpression = 2;
            Debug.Log("Expression: Troubled");
        }

        // 3キー：驚き
        if (Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            currentExpression = 3;
            Debug.Log("Expression: Surprise");
        }
    }

    void LateUpdate()
    {
        switch (currentExpression)
        {
            // =========================
            // 通常
            // =========================
            case 0:
                SetSmileEyes(0f);
                SetBrows(0f);
                SetMouth(0f);
                break;

            // =========================
            // 笑顔
            // =========================
            case 1:
                SetSmileEyes(1f);
                SetBrows(0.3f);
                SetMouth(1f);
                break;

            // =========================
            // 困り・怒り寄り
            // =========================
            case 2:
                SetSmileEyes(0f);
                SetBrows(-1f);
                SetMouth(-0.7f);
                break;

            // =========================
            // 驚き
            // =========================
            case 3:
                SetSmileEyes(0f);
                SetBrows(0.6f);
                SetMouth(0.3f);
                break;
        }
    }

    private void SetSmileEyes(float value)
    {
        if (eyeLSmile != null)
            eyeLSmile.Value = value;

        if (eyeRSmile != null)
            eyeRSmile.Value = value;
    }

    private void SetBrows(float value)
    {
        if (browLForm != null)
            browLForm.Value = value;

        if (browRForm != null)
            browRForm.Value = value;
    }

    private void SetMouth(float value)
    {
        if (mouthForm != null)
            mouthForm.Value = value;
    }

    private CubismParameter FindParameter(string id)
    {
        CubismParameter[] parameters =
            GetComponentsInChildren<CubismParameter>();

        foreach (CubismParameter parameter in parameters)
        {
            if (parameter.Id == id)
                return parameter;
        }

        return null;
    }

}