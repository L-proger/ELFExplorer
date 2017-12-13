using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ElfReader;
using ElfReader.Elf;
using EmbeddedMemoryExplorer.Annotations;

namespace EmbeddedMemoryExplorer {
    public class MemoryTypeAdapter : INotifyPropertyChanged
    {
        private string _name;
        private MemoryProfile _profile;
        private ObservableCollection<ElfSection> _sections = new ObservableCollection<ElfSection>(); 

        public ulong TotalSize {
            get {
                if(_sections == null || _sections.Count == 0) {
                    return 0;
                } else
                {
                    return _sections.Aggregate<ElfSection, ulong>(0, (current, section) => current + section.ItemsTotalSize);
                }
            }
        }

        public MemoryTypeAdapter(MemoryProfile profile, string name)
        {
            _profile = profile;
            foreach (var section in _profile.GetSectionsByMemoryEx(name))
            {
                _sections.Add(section);
            }
            Name = name;
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged();}
        }

        public ObservableCollection<ElfSection> Sections
        {
            get { return _sections; }
            set { _sections = value; OnPropertyChanged();}
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
