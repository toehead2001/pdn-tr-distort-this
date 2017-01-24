using System;
using System.Drawing;

namespace TRsDistortThis
{
    public class EffectPluginConfigToken : PaintDotNet.Effects.EffectConfigToken
    {

        public Point[] Corners
        {
            get;
            set;
        }
        public Point[] vCorners
        {
            get;
            set;
        }
        public Rectangle sCorners
        {
            get;
            set;
        }
        public Rectangle Anchor
        {
            get;
            set;
        }
        public Point[] Tweak
        {
            get;
            set;
        }
        public bool AlphaTrans
        {
            get;
            set;
        }
        public bool Perspective
        {
            get;
            set;
        }
        public bool RenderFlag
        {
            get;
            set;
        }
        public int UValue
        {
            get;
            set;
        }
        public int VValue
        {
            get;
            set;
        }
        public bool initialize
        {
            get;
            set;
        }

        public EffectPluginConfigToken(Point[] corners, Rectangle scorners,Rectangle anchor, Point[] vcorners,
            Point[] tweak, bool alphatrans, bool perspective, bool renderflag,
            int uvalue, int vvalue, bool init)
            : base()
        {
            this.Corners = corners;
            this.vCorners = vcorners;
            this.sCorners = scorners;
            this.Tweak = tweak;
            this.AlphaTrans = alphatrans;
            this.Perspective = perspective;
            this.RenderFlag=renderflag;
            this.UValue = uvalue;
            this.VValue = vvalue;
            this.initialize = init;
            this.Anchor = anchor;
        }

        protected EffectPluginConfigToken(EffectPluginConfigToken copyMe)
            : base(copyMe)
        {
            this.Corners = copyMe.Corners;
            this.sCorners = copyMe.sCorners;
            this.Tweak = copyMe.Tweak;
            this.vCorners = copyMe.vCorners;
            this.AlphaTrans = copyMe.AlphaTrans;
            this.Perspective = copyMe.Perspective;
            this.RenderFlag = copyMe.RenderFlag;
            this.UValue = copyMe.UValue;
            this.VValue = copyMe.VValue;
            this.initialize= copyMe.initialize;
            this.Anchor = copyMe.Anchor;
        }

        public override object Clone()
        {
            return new EffectPluginConfigToken(this);
        }
    }
}