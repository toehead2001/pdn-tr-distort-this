using PaintDotNet.Effects;
using System.Drawing;

namespace TRsDistortThis
{
    internal class EffectPluginConfigToken : EffectConfigToken
    {
        internal Point[] Corners { get; set; }
        internal Point[] vCorners { get; set; }
        internal Rectangle sCorners { get; set; }
        internal Rectangle Anchor { get; set; }
        internal Point[] Tweak { get; set; }
        internal bool AlphaTrans { get; set; }
        internal bool Perspective { get; set; }
        internal int UValue { get; set; }
        internal int VValue { get; set; }
        internal bool initialize { get; set; }

        internal EffectPluginConfigToken(Point[] corners, Rectangle scorners, Rectangle anchor, Point[] vcorners, Point[] tweak, bool alphatrans, bool perspective, int uvalue, int vvalue, bool init)
        {
            this.Corners = corners;
            this.vCorners = vcorners;
            this.sCorners = scorners;
            this.Tweak = tweak;
            this.AlphaTrans = alphatrans;
            this.Perspective = perspective;
            this.UValue = uvalue;
            this.VValue = vvalue;
            this.initialize = init;
            this.Anchor = anchor;
        }

        protected EffectPluginConfigToken(EffectPluginConfigToken copyMe) : base(copyMe)
        {
            this.Corners = copyMe.Corners;
            this.sCorners = copyMe.sCorners;
            this.Tweak = copyMe.Tweak;
            this.vCorners = copyMe.vCorners;
            this.AlphaTrans = copyMe.AlphaTrans;
            this.Perspective = copyMe.Perspective;
            this.UValue = copyMe.UValue;
            this.VValue = copyMe.VValue;
            this.initialize = copyMe.initialize;
            this.Anchor = copyMe.Anchor;
        }

        public override object Clone()
        {
            return new EffectPluginConfigToken(this);
        }
    }
}