using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElfReader.Dwarf;
using ElfReader.Elf;

namespace EmbeddedMemoryExplorer {
    public class SymbolDescriptor
    {
        public MemoryTypeAdapter Memory { get; set; }
        public ElfSection Section { get; set; }
        public ElfSymbol Symbol { get; set; }
        public DwarfCompilationUnitItem DwarfUnitItem { get; set; }
    }
}
