using UnityEngine.InputSystem;
using UnityEngine;
using Live2D.Cubism.Core;

[DefaultExecutionOrder(10000)]
public class FaceRotationTest : MonoBehaviour
{
    [Header("OpenSeeFace")]
    public OpenSee.OpenSee openSee;

    [Header("Head Sensitivity")]
    [Range(0.1f, 3.0f)]
    public float horizontalSensitivity = 1.0f;

    [Range(0.1f, 3.0f)]
    public float verticalSensitivity = 1.0f;

    [Range(0.1f, 3.0f)]
    public float rollSensitivity = 1.0f;

    private CubismParameter angleX;
    private CubismParameter angleY;
    private CubismParameter angleZ;

    private CubismParameter browL;
    private CubismParameter browR;

    private CubismParameter eyeLOpen;
    private CubismParameter eyeROpen;

    private CubismParameter mouthOpenY;

    private CubismParameter eyeBallX;
    private CubismParameter eyeBallY;
    // ì™ÇÃäÓèÄíl
    private float baseX;
    private float baseY;
    private float baseZ;

    private bool baseSet = false;

    void Start()
    {
        // äÁÇÃäpìx
        angleX = FindParameter("ParamAngleX");
        angleY = FindParameter("ParamAngleY");
        angleZ = FindParameter("ParamAngleZ");

        // î˚
        browL = FindParameter("ParamBrowLY");
        browR = FindParameter("ParamBrowRY");

        // ñ⁄
        eyeLOpen = FindParameter("ParamEyeLOpen");
        eyeROpen = FindParameter("ParamEyeROpen");

        // å˚
        mouthOpenY = FindParameter("ParamMouthOpenY");

        eyeBallX = FindParameter("ParamEyeBallX");
        eyeBallY = FindParameter("ParamEyeBallY");
    }

    void Update()
    {
        if (Keyboard.current != null &&
            Keyboard.current.rKey.wasPressedThisFrame)
        {
            Recalibrate();
        }
    }

    void LateUpdate()
    {
        if (openSee == null)
            return;

        OpenSee.OpenSee.OpenSeeData data =
            openSee.GetOpenSeeData(0);

        if (data == null)
            return;

        // =========================
        // OpenSeeFace ì™ÇÃâÒì]
        // =========================

        float x = data.rotation.x;
        float y = data.rotation.y;
        float z = data.rotation.z;

        // =========================
        // ç≈èâÇÃäÁÇê≥ñ Ç∆ÇµÇƒãLò^
        // =========================

        if (!baseSet)
        {
            baseX = x;
            baseY = y;
            baseZ = z;

            baseSet = true;
        }

        // =========================
        // ê≥ñ Ç©ÇÁÇÃïœâªó 
        // =========================

        float relativeX = x - baseX;
        float relativeY = y - baseY;
        float relativeZ = z - baseZ;

        // =========================
        // éãê¸
        // =========================

        // äÁÇÃå¸Ç´Ç©ÇÁéãê¸ÇçÏÇÈ
        float lookX = Mathf.Clamp(
            relativeY / 30.0f,
            -1.0f,
            1.0f
        );

        float lookY = Mathf.Clamp(
            relativeX / 30.0f,
            -1.0f,
            1.0f
        );

        if (eyeBallX != null)
            eyeBallX.Value = lookX;

        if (eyeBallY != null)
            eyeBallY.Value = lookY;

        // =========================
        // ì™
        // =========================

        if (angleX != null)
        {
            angleX.Value =
                relativeY * horizontalSensitivity;
        }

        if (angleY != null)
        {
            angleY.Value =
                relativeX * verticalSensitivity;
        }

        if (angleZ != null)
        {
            angleZ.Value =
                relativeZ * rollSensitivity;
        }

        if (data.features != null)
        {
            // =========================
            // î˚
            // =========================

            if (browL != null)
            {
                browL.Value =
                    data.features.EyebrowUpDownLeft;
            }

            if (browR != null)
            {
                browR.Value =
                    data.features.EyebrowUpDownRight;
            }

            // =========================
            // Ç‹ÇŒÇΩÇ´
            // =========================

            if (eyeLOpen != null)
            {
                float leftEye =
                    Mathf.InverseLerp(
                        -0.85f,
                        0.25f,
                        data.features.EyeLeft
                    );

                eyeLOpen.Value = leftEye;
            }

            if (eyeROpen != null)
            {
                float rightEye =
                    Mathf.InverseLerp(
                        -0.85f,
                        0.35f,
                        data.features.EyeRight
                    );

                eyeROpen.Value = rightEye;
            }

            // =========================
            // å˚
            // =========================

            if (mouthOpenY != null)
            {
                float mouthOpen =
                    Mathf.Clamp01(
                        data.features.MouthOpen * 2.0f
                    );

                mouthOpenY.Value = mouthOpen;
            }
        }
    }

    // =========================
    // ê≥ñ Ççƒê›íË
    // =========================

    public void Recalibrate()
    {
        baseSet = false;

        Debug.Log("Face recalibrated.");
    }

    // =========================
    // Cubism ParameteréÊìæ
    // =========================

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
    public void SetHorizontalSensitivity(float value)
    {
        horizontalSensitivity = value;
    }

    public void SetVerticalSensitivity(float value)
    {
        verticalSensitivity = value;
    }

    public void SetRollSensitivity(float value)
    {
        rollSensitivity = value;
    }
}