using UnityEngine.UI;
using UnityEngine;

namespace Components
{
    [RequireComponent(typeof(Image))]
    public class MonoImage : MonoComponent<Image, Sprite>
    {
        protected override void ProcessSetup()
        {
            Component.sprite = Value;
        }
    }
}