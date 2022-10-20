using Unity.Collections;
using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Systems;

[UpdateAfter(typeof(EndFramePhysicsSystem))]
public partial class PickupSystem : SystemBase
{
    private BeginInitializationEntityCommandBufferSystem _bufferSystem;
    private BuildPhysicsWorld _buildPhysicsWorld;
    private StepPhysicsWorld _stepPhysicsWorld;

    protected override void OnCreate(){
        _bufferSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
        _buildPhysicsWorld = World.GetOrCreateSystem<BuildPhysicsWorld>();
        _stepPhysicsWorld = World.GetOrCreateSystem<StepPhysicsWorld>();
    }

    protected override void OnUpdate(){
        TriggerJob triggerJob = new TriggerJob() {
            CommandBuffer = _bufferSystem.CreateCommandBuffer(),
            Players = GetComponentDataFromEntity<PlayerMovementData>(true),
            Collectables = GetComponentDataFromEntity<CollectableItemTag>(true)
        };

        Dependency = triggerJob.Schedule(_stepPhysicsWorld.Simulation, Dependency);
    }

    private struct TriggerJob : ITriggerEventsJob
    {
        public EntityCommandBuffer CommandBuffer;

        [ReadOnly] public ComponentDataFromEntity<PlayerMovementData> Players;
        [ReadOnly] public ComponentDataFromEntity<CollectableItemTag> Collectables;

        public void Execute(TriggerEvent triggerEvent){
            if(Collectables.HasComponent(triggerEvent.EntityA) && Collectables.HasComponent(triggerEvent.EntityB))
                return;

            if(Collectables.HasComponent(triggerEvent.EntityA) && Players.HasComponent(triggerEvent.EntityB)){
                CommandBuffer.AddComponent(triggerEvent.EntityA, new DisableTag());
            }
            else if(Players.HasComponent(triggerEvent.EntityA) && Collectables.HasComponent(triggerEvent.EntityB)){
                CommandBuffer.AddComponent(triggerEvent.EntityB, new DisableTag());
            }
        }
    }
}
