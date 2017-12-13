using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElfReader;
using ElfReader.Elf;
using ElfUtils;

namespace EmbeddedMemoryExplorer {
    public class MemoryBlockDescriptor {
        private DwarfData _dwarfData;
        private string _name;
        private MemoryProfile _memoryProfile;
        private ElfSection[] _sections;

        public MemoryBlockDescriptor(DwarfData dwarfData, MemoryProfile memoryProfile, string memoryName) {
            _dwarfData = dwarfData;
            _name = memoryName;
            _memoryProfile = memoryProfile;
            _sections = _memoryProfile.GetSectionsByMemoryEx(memoryName);

        }
    }
}
