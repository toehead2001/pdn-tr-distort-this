using System;
using System.Reflection;
using PaintDotNet;
using PaintDotNet.Effects;
using System.Drawing;

namespace TRsDistortThis
{
    public class PluginSupportInfo : IPluginSupportInfo
    {
        public string Author
        {
            get
            {
                return "TechnoRobbo";
            }
        }
        public string Copyright
        {
            get
            {
                return ((AssemblyCopyrightAttribute)base.GetType().Assembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false)[0]).Copyright;
            }
        }

        public string DisplayName
        {
            get
            {
                return ((AssemblyProductAttribute)base.GetType().Assembly.GetCustomAttributes(typeof(AssemblyProductAttribute), false)[0]).Product;
            }
        }

        public Version Version
        {
            get
            {
                return base.GetType().Assembly.GetName().Version;
            }
        }

        public Uri WebsiteUri
        {
            get
            {
                return new Uri("http://www.technorobbo.com");
            }
        }
    }

    [PluginSupportInfo(typeof(PluginSupportInfo), DisplayName = "TR's DistortThis!")]

    public class EffectPlugin : Effect
    {
        public static string StaticName
        {
            get
            {
                return "TR's DistortThis!";
            }
        }

        public static Bitmap StaticImage
        {
            get
            {
                return Properties.Resources.icon;
            }
        }

        public static string StaticSubMenuName
        {
            get
            {
                return SubmenuNames.Distort;  // Use for existing submenu
            }
        }

        public EffectPlugin()
            : base(StaticName, StaticImage, StaticSubMenuName, EffectFlags.Configurable)
        {

        }

        public override EffectConfigDialog CreateConfigDialog()
        {
            return new EffectPluginConfigDialog();
        }

        protected override void OnSetRenderInfo(EffectConfigToken parameters, RenderArgs dstArgs, RenderArgs srcArgs)
        {
            EffectPluginConfigToken token = (EffectPluginConfigToken)parameters;

            Corners = token.Corners;
            sCorners = token.sCorners;
            AlphaTrans = token.AlphaTrans;
            Perspective = token.Perspective;
            RenderFlag = token.RenderFlag;
            Uvalue = (double)token.UValue / 100;
            Vvalue = (double)token.VValue / 100;
            base.OnSetRenderInfo(parameters, dstArgs, srcArgs);

        }


        public override void Render(EffectConfigToken parameters, RenderArgs dstArgs, RenderArgs srcArgs, Rectangle[] rois, int startIndex, int length)
        {
            if (IsCancelRequested) return;
            passClass.StringID = "1";
            for (int i = startIndex; i < startIndex + length; ++i)
            {
                if (IsCancelRequested) break;
                Rectangle rect = rois[i];
                if (RenderFlag) myRender(dstArgs.Surface, srcArgs.Surface, rect);
            }
            passClass.StringID = "0";
        }

        protected override void OnDispose(bool disposing)
        {
            //NOP
        }


        Point[] Corners = new Point[4];
        Rectangle sCorners = new Rectangle();
        bool AlphaTrans = true; // [0,1] Alpha Transparency
        bool Perspective = true; // [0,1] Perspective
        bool RenderFlag = false;
        double Vvalue = 1;
        double Uvalue = 1;



        private void myRender(Surface dst, Surface src, Rectangle rect)
        {
            if (IsCancelRequested) return;
            PdnRegion selReg = EnvironmentParameters.GetSelection(src.Bounds);

            Rectangle sel = sCorners;


            Surface src2 = new Surface(sel.Width, sel.Height);
            src2.CopySurface(src, sel);

            try
            {
                double CenterX = ((sel.Right - sel.Left) / 2) + sel.Left;
                double CenterY = ((sel.Bottom - sel.Top) / 2) + sel.Top;
                double uu = 0;
                double vv = 0;


                MyUtils.PointD A = new MyUtils.PointD(Corners[0].X, Corners[0].Y);
                MyUtils.PointD B = new MyUtils.PointD(Corners[1].X, Corners[1].Y);
                MyUtils.PointD C = new MyUtils.PointD(Corners[2].X, Corners[2].Y);
                MyUtils.PointD D = new MyUtils.PointD(Corners[3].X, Corners[3].Y);


                for (double y = rect.Top; y < rect.Bottom; y++)
                {
                    if (IsCancelRequested) break;
                    for (double x = rect.Left; x < rect.Right; x++)
                    {
                        if (IsCancelRequested) break;

                        //Clockwise 
                        MyUtils.PointD P = new MyUtils.PointD(x, y);

                        double x1 = MyUtils.xproduct(A, P, D);
                        double x2 = MyUtils.xproduct(B, P, C);
                        double y1 = MyUtils.xproduct(A, P, B);
                        double y2 = MyUtils.xproduct(D, P, C);

                        x1 = Math.Abs(x1);
                        x2 = Math.Abs(x2);
                        y1 = Math.Abs(y1);
                        y2 = Math.Abs(y2);


                        double cx1 = MyUtils.PythagPD(A, D);
                        double cx2 = MyUtils.PythagPD(B, C);
                        double cy1 = MyUtils.PythagPD(A, B);
                        double cy2 = MyUtils.PythagPD(D, C);


                        //Perspective Skew
                        if (Perspective)
                        {
                            double nx1 = x1 * cx2 / cx1;
                            double nx2 = x2 * cx1 / cx2;
                            double ny1 = y1 * cy2 / cy1;
                            double ny2 = y2 * cy1 / cy2;

                            //calculate percentage
                            x1 = nx1 * Uvalue + x1 - x1 * Uvalue;
                            x2 = nx2 * Uvalue + x2 - x2 * Uvalue;
                            y1 = ny1 * Vvalue + y1 - y1 * Vvalue;
                            y2 = ny2 * Vvalue + y2 - y2 * Vvalue;


                        }

                        //map texture
                        uu = x1 / (x2 + x1) * sel.Width;// +sel.Left;
                        vv = y1 / (y2 + y1) * sel.Height;// +sel.Top;

                        Color mix = src2.GetBilinearSampleClamped((float)uu, (float)vv);
                        byte CPA = mix.A;


                        double farAC = (A.X - C.X) * (A.X - C.X) + (A.Y - C.Y) * (A.Y - C.Y);
                        double farBD = (B.X - D.X) * (B.X - D.X) + (B.Y - D.Y) * (B.Y - D.Y);
                        //Tranparency Check
                        //checking hit
                        if (farAC < farBD)
                        {
                            bool hit1 = (MyUtils.xproduct(P, A, B) < 0.0f);
                            bool hit2 = (MyUtils.xproduct(P, B, C) < 0.0f);
                            bool hit3 = (MyUtils.xproduct(P, C, A) < 0.0f);
                            bool hitA = (hit1 == hit2) && (hit2 == hit3);


                            hit1 = (MyUtils.xproduct(P, C, D) < 0.0f);
                            hit2 = (MyUtils.xproduct(P, D, A) < 0.0f);
                            hit3 = (MyUtils.xproduct(P, A, C) < 0.0f);
                            bool hitB = (hit1 == hit2) && (hit2 == hit3);

                            if (AlphaTrans && !hitA && !hitB) { CPA = 0; }
                        }
                        else
                        {
                            bool hit1 = (MyUtils.xproduct(P, B, C) < 0.0f);
                            bool hit2 = (MyUtils.xproduct(P, C, D) < 0.0f);
                            bool hit3 = (MyUtils.xproduct(P, D, B) < 0.0f);
                            bool hitA = (hit1 == hit2) && (hit2 == hit3);


                            hit1 = (MyUtils.xproduct(P, D, A) < 0.0f);
                            hit2 = (MyUtils.xproduct(P, A, B) < 0.0f);
                            hit3 = (MyUtils.xproduct(P, B, D) < 0.0f);
                            bool hitB = (hit1 == hit2) && (hit2 == hit3);

                            if (AlphaTrans && !hitA && !hitB) { CPA = 0; }
                        }
                        dst[(int)x, (int)y] = ColorBgra.FromBgra(mix.B, mix.G, mix.R, CPA);

                    }
                }
            }
            finally
            {
                src2.Dispose();
            }
        }

    }
}