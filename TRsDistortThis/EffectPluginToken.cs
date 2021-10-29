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
        internal int AaLevel { get; set; }
        internal bool Perspective { get; set; }
        internal int UValue { get; set; }
        internal int VValue { get; set; }
        internal bool initialize { get; set; }

        internal EffectPluginConfigToken()
        {
            this.Corners = new Point[4];
            this.vCorners = new Point[4];
            this.sCorners = Rectangle.Empty;
            this.Tweak = new Point[4];
            this.AlphaTrans = true;
            this.AaLevel = 1;
            this.Perspective = true;
            this.UValue = 100;
            this.VValue = 100;
            this.initialize = true;
            this.Anchor = Rectangle.Empty;
        }

        protected EffectPluginConfigToken(EffectPluginConfigToken copyMe) : base(copyMe)
        {
            this.Corners = copyMe.Corners;
            this.sCorners = copyMe.sCorners;
            this.Tweak = copyMe.Tweak;
            this.vCorners = copyMe.vCorners;
            this.AlphaTrans = copyMe.AlphaTrans;
            this.AaLevel = copyMe.AaLevel;
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