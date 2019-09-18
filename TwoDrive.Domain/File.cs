using System;
using System.Collections.Generic;

namespace TwoDrive.Domain
{
    public class File : FolderElement
    {
        public string Content { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastModifiedDate { get; set; }
    }
}
