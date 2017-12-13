using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ElfReader.Elf;

namespace EmbeddedMemoryExplorer {
    public class MemoryTreeTemplateSelector : DataTemplateSelector
    {
        public DataTemplate MemoryTypeTemplate { get; set; }
        public DataTemplate SectionTemplate {
            get; set;
        }
        public DataTemplate ElfSymbolTemplate {
            get; set;
        }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if(item is MemoryTypeAdapter) {
                return MemoryTypeTemplate;
            }
            if(item is ElfSection) {
                return SectionTemplate;
            }
            if(item is ElfSymbol)
            {
                return ElfSymbolTemplate;
            }
            return base.SelectTemplate(item, container);
        }
    }
}
