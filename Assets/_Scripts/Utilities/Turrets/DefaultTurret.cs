namespace TD.Turrets
{
    public class DefaultTurret : TurretBase
    {
        protected override void Fire()
        {

        }

        protected override void PlayFireParticles()
        {
            _particles.Play();
        }
    }
}
