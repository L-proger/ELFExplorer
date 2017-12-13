using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace EmbeddedMemoryExplorer {
    public class FileExtensionMatch : FileTypeMatch {
        public string Extension { get; set; }
        public bool CaseSensitive { get; set; }

        public FileExtensionMatch(string extension)
        {
            Extension = extension;
        }

        public override bool Match(Uri file)
        {
            if(!Path.HasExtension(file.AbsolutePath))
            {
                return false;
            }

            var fileExt = Path.GetExtension(file.AbsolutePath);
            var testExt = Extension;

            if(!CaseSensitive)
            {
                fileExt = fileExt.ToLower();
                testExt = testExt.ToLower();
            }

            return string.Compare(fileExt, testExt,
                CaseSensitive ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal) == 0;
        }
    }
}
