using System;
using System.Collections.Generic;

namespace TwoDrive.Domain
{
    public class File : FolderElement, ICloneable
    {
        public string Content { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastModifiedDate { get; set; }

        public Object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
