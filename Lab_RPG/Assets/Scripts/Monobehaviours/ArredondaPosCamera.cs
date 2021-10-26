using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ArredondaPosCamera : CinemachineExtension
{
    public float PixelsPerUnit = 32;

    /*
        Na função PostPipelineStageCallback, analisamos se o personagem está dentro do quadrado,
        se estiver, capturamos a posição e arredondamos esse valor.
        Por fim, realizamos a correção através da diferença da posição com o arredondamento dela.
    */
    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, ref CameraState state,
        float deltaTime)
    {
        if(stage == CinemachineCore.Stage.Body){
            Vector3 pos = state.FinalPosition;
            Vector3 pos2 = new Vector3(Round(pos.x), Round(pos.y), pos.z);
            state.PositionCorrection += pos2 - pos;
        }
    }

    /*
        Na função Round, fazemos o arredondamento da posição, multiplicando pelo pixel por unidade
        e dividindo por ele.
    */
    float Round(float x){
        return Mathf.Round(x * PixelsPerUnit) / PixelsPerUnit;
    }
}
