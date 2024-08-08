using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using stdole;

namespace MiNa.InventorAddInDebugger.Common
{
    public static class PictureDispConverter
    {
        private static Guid _iPictureDispGuid = typeof(IPictureDisp).GUID;
        private static readonly HandleCollector HandleCollector = new HandleCollector("Icon handles", 1000);

        /// <summary>Converts IPictureDisp to Icon if it is possible</summary>
        /// <param name="pictureDisp">Converted icon</param>
        /// <returns>Returns Image if pictureDisp.Type=3. Otherwise returns null</returns>
        /// <remarks></remarks>
        public static System.Drawing.Icon PictureDispToIcon(IPictureDisp pictureDisp)
        {
            System.Drawing.Icon icon = (System.Drawing.Icon)null;
            if (pictureDisp != null && pictureDisp.Type == (short)3)
                icon = System.Drawing.Icon.FromHandle(new IntPtr(pictureDisp.Handle));
            return icon;
        }

        /// <summary>Converts IPictureDisp to Image if it is possible</summary>
        /// <param name="pictureDisp">Converted image</param>
        /// <returns>Returns Image if pictureDisp.Type=1. Otherwise returns null</returns>
        /// <remarks></remarks>
        public static Image PictureDispToImage(IPictureDisp pictureDisp)
        {
            Image image = (Image)null;
            if (pictureDisp != null)
            {
                if (pictureDisp.Type == (short)1)
                {
                    IntPtr hpalette = new IntPtr(pictureDisp.hPal);
                    image = (Image)Image.FromHbitmap(new IntPtr(pictureDisp.Handle), hpalette);
                }

                if (pictureDisp.Type == (short)2)
                    image = (Image)new Metafile(new IntPtr(pictureDisp.Handle), new WmfPlaceableFileHeader());
            }

            return image;
        }

        /// <summary>Converts an Icon into a IPictureDisp</summary>
        /// <param name="icon"></param>
        /// <returns></returns>
        public static IPictureDisp ToIPictureDisp(System.Drawing.Icon icon)
        {
            return PictureDispConverter.OleCreatePictureIndirect((object)new PictureDispConverter.Pictdesc.Icon(icon),
                ref PictureDispConverter._iPictureDispGuid, true);
        }

        /// <summary>Converts an image into a IPictureDisp</summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static IPictureDisp ToIPictureDisp(Image image)
        {
            return PictureDispConverter.OleCreatePictureIndirect(
                (object)new PictureDispConverter.Pictdesc.Bitmap(image is System.Drawing.Bitmap
                    ? (System.Drawing.Bitmap)image
                    : new System.Drawing.Bitmap(image)), ref PictureDispConverter._iPictureDispGuid, true);
        }

        [DllImport("OleAut32.dll", PreserveSig = false)]
        private static extern IPictureDisp OleCreatePictureIndirect(
            [MarshalAs(UnmanagedType.AsAny)] object picdesc,
            ref Guid iid,
            bool fOwn);

        private static class Pictdesc
        {
            public const short PictypeUninitialized = -1;
            public const short PictypeNone = 0;
            public const short PictypeBitmap = 1;
            public const short PictypeMetafile = 2;
            public const short PictypeIcon = 3;
            public const short PictypeEnhmetafile = 4;

            [StructLayout(LayoutKind.Sequential)]
            public class Icon
            {
                internal int cbSizeOfStruct = Marshal.SizeOf(typeof(PictureDispConverter.Pictdesc.Icon));
                internal int picType = 3;
                internal IntPtr hicon = IntPtr.Zero;
                internal int unused1;
                internal int unused2;

                internal Icon(System.Drawing.Icon icon) => this.hicon = icon.ToBitmap().GetHicon();
            }

            [StructLayout(LayoutKind.Sequential)]
            public class Bitmap
            {
                internal int cbSizeOfStruct = Marshal.SizeOf(typeof(PictureDispConverter.Pictdesc.Bitmap));
                internal int picType = 1;
                internal IntPtr hbitmap = IntPtr.Zero;
                internal IntPtr hpal = IntPtr.Zero;
                internal int unused;

                internal Bitmap(System.Drawing.Bitmap bitmap) => this.hbitmap = bitmap.GetHbitmap();
            }
        }
    }
}