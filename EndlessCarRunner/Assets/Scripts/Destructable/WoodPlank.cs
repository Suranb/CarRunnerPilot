using UnityEngine;
using CarRunner.Obstacles;

public class WoodPlank : Obstacle
{
  // Override abstract method
  public override void PlayDestroyAnimation()
  {
    // Play specific WoodPlank destroy animation here
  }

  public override void PlayDestroyEffects()
  {
    // throw new System.NotImplementedException();
  }

  public override void TakeDamage()
  {
    Debug.Log("Doign something else then taking damage...");
  }
}