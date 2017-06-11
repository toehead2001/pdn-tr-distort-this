using System;
using System.Reflection;
using PaintDotNet;
using PaintDotNet.Effects;
using System.Drawing;

namespace TRsDistortThis
{
    public class PluginSupportInfo : IPluginSupportInfo
    {
        public string Author => ((AssemblyCopyrightAttribute)base.GetType().Assembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false)[0]).Copyright;
        public string Copyright => ((AssemblyDescriptionAttribute)base.GetType().Assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false)[0]).Description;
        public string DisplayName => ((AssemblyProductAttribute)base.GetType().Assembly.GetCustomAttributes(typeof(AssemblyProductAttribute), false)[0]).Product;
        public Version Version => base.GetType().Assembly.GetName().Version;
        public Uri WebsiteUri => new Uri("https://www.getpaint.net/redirect/plugins.html");
    }

    [PluginSupportInfo(typeof(PluginSupportInfo), DisplayName = "Distort This!")]
    public class EffectPlugin : Effect
    {
        internal const string StaticName = "Distort This!";
        private readonly static Bitmap StaticImage = new Bitmap(typeof(EffectPlugin), "DistortThis.png");

        public EffectPlugin()
            : base(StaticName, StaticImage, SubmenuNames.Distort, EffectFlags.Configurable)
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
            AaAmount = token.AaLevel;
            Perspective = token.Perspective;
            Uvalue = (double)token.UValue / 100;
            Vvalue = (double)token.VValue / 100;

            if (src2 == null || src2.Size != sCorners.Size)
            {
                src2?.Dispose();
                src2 = new Surface(sCorners.Size);
                src2.CopySurface(srcArgs.Surface, sCorners);
            }

            A = new PointD(Corners[0].X, Corners[0].Y);
            B = new PointD(Corners[1].X, Corners[1].Y);
            C = new PointD(Corners[2].X, Corners[2].Y);
            D = new PointD(Corners[3].X, Corners[3].Y);

            Antialias = (AaAmount > 1);
            if (Antialias)
                SetupForSubpixels(AaAmount, AaAmount);

            base.OnSetRenderInfo(parameters, dstArgs, srcArgs);
        }

        // Set up antialiasing.
        int ssXSamples, ssYSamples, ssSamples, ssTwiceSamples;
        float ssXStart, ssYStart, ssXStep, ssYStep;
        double ssSamplesRecip;
        void SetupForSubpixels(int xSamples, int ySamples)
        {
            ssXSamples = xSamples;
            ssYSamples = ySamples;
            ssSamples = xSamples * ySamples;
            ssSamplesRecip = 1.0 / (double)ssSamples;
            ssTwiceSamples = ssSamples << 1;
            ssXStep = 1.0f / (float)xSamples;
            ssYStep = 1.0f / (float)ySamples;
            ssXStart = 0.5f * (1.0f - ssXStep);
            ssYStart = 0.5f * (1.0f - ssYStep);
        }

        public override void Render(EffectConfigToken parameters, RenderArgs dstArgs, RenderArgs srcArgs, Rectangle[] rois, int startIndex, int length)
        {
            if (IsCancelRequested) return;
            for (int i = startIndex; i < startIndex + length; ++i)
            {
                if (IsCancelRequested) break;
                myRender(dstArgs.Surface, srcArgs.Surface, rois[i]);
            }
        }


        Point[] Corners = new Point[4];
        Rectangle sCorners = new Rectangle();
        bool AlphaTrans = true; // [0,1] Alpha Transparency
        int AaAmount = 1;
        bool Perspective = true; // [0,1] Perspective
        double Vvalue = 1;
        double Uvalue = 1;
        bool Antialias;
        PointD A, B, C, D;
        Surface src2;

        private void myRender(Surface dst, Surface src, Rectangle rect)
        {
            for (int y = rect.Top; y < rect.Bottom; y++)
            {
                if (IsCancelRequested) return;
                for (int x = rect.Left; x < rect.Right; x++)
                {
                    if (IsCancelRequested) return;
                    dst[x, y] = Antialias ? DistortPointAA(x, y) : DistortPoint(x, y);
                }
            }
        }

        // Antialias version.
        ColorBgra DistortPointAA(float fx, float fy)
        {
            ColorBgra CurrentPixel = ColorBgra.White;

            float bx = fx - ssXStart;
            float y = fy - ssYStart;

            int b = 0, g = 0, r = 0, a = 0;
            for (int i = 0; i < ssYSamples; i++)
            {
                float x = bx;
                for (int j = 0; j < ssXSamples; j++)
                {
                    CurrentPixel = DistortPoint(x, y);
                    int alpha = CurrentPixel.A;
                    if (alpha != 0)
                    {
                        b += alpha * CurrentPixel.B;
                        g += alpha * CurrentPixel.G;
                        r += alpha * CurrentPixel.R;
                        a += alpha;
                    }
                    x += ssXStep;
                }
                y += ssYStep;
            }

            if (a == 0)
            {
                return ColorBgra.FromBgra(0, 0, 0, 0);
            }
            else
            {
                // Compute the (rounded) averages.
                int twiceA = a << 1;
                b = ((b << 1) + a) / twiceA;
                g = ((g << 1) + a) / twiceA;
                r = ((r << 1) + a) / twiceA;
                a = (twiceA + ssSamples) / ssTwiceSamples;
                return ColorBgra.FromBgra((byte)b, (byte)g, (byte)r, (byte)a);
            }
        }

        // Non-antialias version, and version called for antialias subpixels.
        private ColorBgra DistortPoint(float x, float y)
        {
            double uu;
            double vv;
            ColorBgra mix;

            //Clockwise 
            PointD P = new PointD(x, y);

            double x1 = MyUtils.xproduct(A, P, D);
            double x2 = MyUtils.xproduct(B, P, C);
            double y1 = MyUtils.xproduct(A, P, B);
            double y2 = MyUtils.xproduct(D, P, C);

            x1 = Math.Abs(x1);
            x2 = Math.Abs(x2);
            y1 = Math.Abs(y1);
            y2 = Math.Abs(y2);

            //Perspective Skew
            if (Perspective)
            {
                double cx1 = MyUtils.Pythag(A, D);
                double cx2 = MyUtils.Pythag(B, C);
                double cy1 = MyUtils.Pythag(A, B);
                double cy2 = MyUtils.Pythag(D, C);

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
            uu = x1 / (x2 + x1) * sCorners.Width;// +sel.Left;
            vv = y1 / (y2 + y1) * sCorners.Height;// +sel.Top;

            if (!AlphaTrans)
            {
                mix = src2.GetBilinearSampleClamped((float)uu, (float)vv);
            }
            else
            {
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

                    if (hitA || hitB)
                        mix = src2.GetBilinearSampleClamped((float)uu, (float)vv);
                    else
                        mix = ColorBgra.Transparent;
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

                    if (hitA || hitB)
                        mix = src2.GetBilinearSampleClamped((float)uu, (float)vv);
                    else
                        mix = ColorBgra.Transparent;
                }
            }

            return mix;
        }

    }
}