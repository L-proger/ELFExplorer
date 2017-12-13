using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using ElfReader;
using ElfReader.Elf;

namespace EmbeddedMemoryExplorer {
    public class MemoryProfileToMemoryArrayConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            MemoryProfile p = value as MemoryProfile;
            return p?.MemoryNames.Select(v => new MemoryTypeAdapter(p, v)).ToArray();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    public class MemoryTypeToNameSizeConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if(!(value is MemoryTypeAdapter)) {
                return "Failed to convert";
            }
            MemoryTypeAdapter p = (MemoryTypeAdapter)value;
            return string.Format("{0} [{1} bytes] ",p.Name, p.TotalSize);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
    }

    public class SectionGroupNameConverterConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if(!(value is ElfSection)) {
                return "Failed to convert";
            }
            ElfSection p = (ElfSection)value;

            if(p.ProgramHeader.HasValue) {
                return string.Format("{0} Bytes: {1}\tVirtual address: 0x{2:x}", p.Name, p.ItemsTotalSize, p.ProgramHeader.Value.PVaddr);
            }
            return string.Format("{0} Bytes: {1}", p.Name, p.ItemsTotalSize);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
    }
}
