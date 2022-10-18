using UnityEngine;
using Unity.Entities;
using Unity.Physics;
using Unity.Mathematics;

public partial class PlayerMovementSystem : SystemBase
{
    protected override void OnCreate(){
        base.OnCreate();
    }

    protected override void OnUpdate(){
        float deltaTime = Time.DeltaTime;

        Entities.ForEach((ref PlayerMovementData playerMovementData, ref PhysicsVelocity physicsVelocity) => {
            float2 newVelocity = physicsVelocity.Linear.xz;
            newVelocity += playerMovementData.Direction * playerMovementData.Speed * deltaTime;
            physicsVelocity.Linear.xz = newVelocity;
        }).Run();
    }
}
