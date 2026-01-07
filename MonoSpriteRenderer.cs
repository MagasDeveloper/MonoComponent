using UnityEngine;

namespace Components
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class MonoSpriteRenderer : MonoComponent<SpriteRenderer, Sprite>
    {
        protected override void ProcessSetup()
        {
            Component.sprite = Value;
        }
    }
}