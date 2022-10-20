using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct CollectableAnimationData : IComponentData
{
    public float3 StartPosition { get; set; }
    public float Progress { get; set; }

    public float Speed;
    public float VeritcalAmplitude;
}
