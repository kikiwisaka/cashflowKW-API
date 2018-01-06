using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Common
{
    public class FileTypConverter
    {
        public static string FileType(string extension)
        {
            int result = 0;
            switch (extension.ToLower())
            {
                case ".pdf":
                    result = 1;
                    break;
                case ".Image":
                    result = 2;
                    break;
                case ".doc":
                    result = 3;
                    break;
                case ".xlsx":
                    result = 4;
                    break;
                case ".docx":
                    result = 5;
                    break;
                case ".jpg":
                    result = 6;
                    break;
                case ".jpeg":
                    result = 7;
                    break;
                case ".png":
                    result = 8;
                    break;
                case ".gif":
                    result = 9;
                    break;
                case ".bmp":
                    result = 10;
                    break;

                default:
                    result = 0;
                    break;
            }
            return result.ToString();
        }
        public static string FileTypeConvert(int index)
        {
            string result = string.Empty;
            switch (index)
            {
                case 1:
                    result = ".pdf";
                    break;
                case 2:
                    result = ".Image";
                    break;
                case 3:
                    result = ".doc";
                    break;
                case 4:
                    result = ".xlsx";
                    break;
                case 5:
                    result = ".docx";
                    break;
                case 6:
                    result = ".jpg";
                    break;
                case 7:
                    result = ".jpeg";
                    break;
                case 8:
                    result = ".png";
                    break;
                case 9:
                    result = ".gif";
                    break;
                case 10:
                    result = ".bmp";
                    break;

                default:
                    result = "empty";
                    break;
            }
            return result;
        }
    }
}
