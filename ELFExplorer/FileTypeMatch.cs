using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmbeddedMemoryExplorer {
    public class FileTypeMatch {
        public virtual bool Match(Uri file)
        {
            return false;
        }
    }
}
