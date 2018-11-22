
using DS.AFP.Framework.Events;
namespace DS.AFP.Framework.WPF
{
    public class OverlayEvent : CompositePresentationEvent<bool>
    {
        public bool ShowOverlay { get; set; }
    }
}
