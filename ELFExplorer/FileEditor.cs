using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace EmbeddedMemoryExplorer {
    public class FileEditor {
        public int RowID { get; set; }
        public int ColumnId { get; set; }
        public string Path { get; set; }
        public string UserName { get; set; }

        public List<FileTypeMatch> FileTypes { get; set; } = new List<FileTypeMatch>();

        public string UserPassword {
            get; set;
        }
        public virtual string GetArguments(Uri file)
        {
            return file.AbsolutePath;
        }

        public bool CanEditFile(Uri file) {
            if(FileTypes == null || FileTypes.Count == 0) {
                return false;
            }
            return FileTypes.Any(v => v.Match(file));
        }

        public bool Launch(Uri file) {
            if(string.IsNullOrEmpty(Path)) {
                return false;
            }
            Process.Start(Path, GetArguments(file));
            return true;
        }
    }
}
