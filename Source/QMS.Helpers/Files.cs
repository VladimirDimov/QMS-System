using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QMS.Helpers
{
    public class FilesHelper
    {
        public string GetFileExtension(string fileName)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException("Cannot get file extension of null");
            }

            var lastIndexOfDot = fileName.LastIndexOf('.');

            if (lastIndexOfDot < 0)
            {
                throw new ArgumentException("Invalid file name. File name must contain \".\" !");
            }

            return fileName.Substring(lastIndexOfDot);
        }
    }
}
