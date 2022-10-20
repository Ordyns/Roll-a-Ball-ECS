using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public partial class CollectableItemsAnimationSystem : SystemBase
{
    protected override void OnStartRunning(){
        Random random = Random.CreateFromIndex(0);

        Entities.ForEach((ref CollectableAnimationData animationData, ref Translation translation) => {
            animationData.Progress = random.NextFloat();
            animationData.StartPosition = translation.Value;
        }).ScheduleParallel();
    }

    protected override void OnUpdate(){
        float deltaTime = Time.DeltaTime;

        Entities.ForEach((ref CollectableAnimationData animationData, ref Translation translation) => {
            animationData.Progress += deltaTime * animationData.Speed;

            if(animationData.Progress >= math.PI * 2)
                animationData.Progress = 0;
            
            float y = math.sin(animationData.Progress) * (animationData.VeritcalAmplitude / 2);
            translation.Value = animationData.StartPosition + new float3(0, y, 0);
        }).Run();
    }
}
