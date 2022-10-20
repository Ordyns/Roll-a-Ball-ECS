using UnityEngine;
using Unity.Entities;
using Unity.Transforms;

public partial class DisableSystem : SystemBase
{
    protected override void OnUpdate(){
        EntityCommandBuffer commandBuffer = new EntityCommandBuffer(Unity.Collections.Allocator.TempJob);

        Entities.WithAll<DisableTag>().ForEach((Entity entity) => {
            DisableEntityAndChildrenRecursively(entity, ref commandBuffer);
        }).WithStructuralChanges().Run();

        commandBuffer.Playback(EntityManager);
        commandBuffer.Dispose();
    }

    private void DisableEntityAndChildrenRecursively(Entity entity, ref EntityCommandBuffer commandBuffer){
        if(EntityManager.HasComponent<Child>(entity)){
            DynamicBuffer<Child> buffer = EntityManager.GetBuffer<Child>(entity);
         
            foreach (var child in buffer){
                commandBuffer.DestroyEntity(child.Value);
                DisableEntityAndChildrenRecursively(child.Value, ref commandBuffer);
            }
        }
    }
}
