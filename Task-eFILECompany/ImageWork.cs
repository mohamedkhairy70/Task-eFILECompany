using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_eFILECompany
{
    public static class ImageWork
    {
        /// <summary>
        /// Convert Array Byte To Image
        /// </summary>
        /// <returns>Image</returns>
        public static Image ToImage(this byte[] byteArrayIn)
        {
            var ms = new MemoryStream(byteArrayIn);
            var returnImage = Image.FromStream(ms);
            return returnImage;
        }

        /// <summary>
        /// Convert Image To Array Byte
        /// </summary>
        /// <returns>byte[]</returns>
        public static byte[] ToByteArray(this Image imageIn)
        {
            var ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return ms.ToArray();
        }
    }
}
