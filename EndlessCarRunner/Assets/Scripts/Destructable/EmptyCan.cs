public class EmptyCan : Obstacle
{
  // Override abstract method
  public override void PlayDestroyAnimation()
  {
    // Play specific EmptyCan destroy animation here
  }

  public override void PlayDestroyEffects()
  {
    throw new System.NotImplementedException();
  }
}