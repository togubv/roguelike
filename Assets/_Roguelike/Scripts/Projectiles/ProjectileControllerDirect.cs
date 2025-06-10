using UnityEngine;

namespace Roguelike
{
    public class ProjectileControllerDirect : ProjectileController
    {
        protected override void Move()
        {
            transform.Translate(Vector2.up * movespeed * Time.deltaTime);

            base.Move();
        }
    }
}
