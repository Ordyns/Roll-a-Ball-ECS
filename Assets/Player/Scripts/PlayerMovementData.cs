using Unity.Mathematics;
using Unity.Entities;

[GenerateAuthoringComponent]
public struct PlayerMovementData : IComponentData
{
    public float Speed;
    public float2 Direction;
}
