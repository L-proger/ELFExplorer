using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using ElfReader;
using ElfReader.Elf;
using ElfUtils;
using EmbeddedMemoryExplorer.Annotations;

namespace EmbeddedMemoryExplorer {
    public class MemoryExplorerViewModel : ViewModelBase {
        public static MemoryExplorerViewModel Instance { get; } = new MemoryExplorerViewModel();

        public ICollectionView SymbolsView { get; set; }

        private string _title;

        public SymbolsCollection Symbols { get; set; }

        private List<FileEditor> FileEditors { get; } = new List<FileEditor>();
        private Uri _elfUri;

        public ElfFile File
        {
            get { return _elfFile; }
            set { _elfFile = value;  OnPropertyChanged();}
        }

        public MemoryProfile Profile
        {
            get { return _profile; }
            set { _profile = value; OnPropertyChanged();}
        }

        public string Title
        {
            get { return _title; }
            set { _title = value;
                OnPropertyChanged();}
        }

        private ElfFile _elfFile;

        private DwarfData _dwarf;
        private MemoryDescriptor _memoryDescriptor;
        private MemoryProfile _profile;

        private MemoryExplorerViewModel()
        {
            // Cvs.Source = Symbols;
            /* Reload(
                @"D:\MotionParallaxResearch\NCamera\V05\Software\Software\Projects\CooCox\Software\Debug\bin\Software.elf",
                @"C:/Users/Sergey/AppData/Local/VisualGDB/EmbeddedBSPs/arm-eabi/com.sysprogs.arm.stm32/STM32F4xxxx-HAL/LinkerScripts/STM32F439xI_flash.lds");*/
            var executable = NotepadPpEditor.FindExecutablePath();
            if (!string.IsNullOrEmpty(executable))
            {
                FileEditors.Add(new NotepadPpEditor(executable));
            }
            UpdateTitle();

        }

        private void UpdateTitle()
        {
            const string baseTitle = "ELF memory explorer";
            if(_elfUri != null)
            {
                Title = string.Format("{0} [{1}]", baseTitle, Path.GetFileName(_elfUri.AbsolutePath));
            }
            Title = baseTitle;
        }

        public void OnDoubleClick([CanBeNull]SymbolDescriptor target) {
            if(target?.DwarfUnitItem != null) {
                Uri fullSourcePath =  new Uri(_elfUri, target.DwarfUnitItem.FileStr);

                var editor = FindFileEditor(fullSourcePath);
                if(editor != null) {
                    editor.RowID = (int)target.DwarfUnitItem.Line;
                    editor.ColumnId = 0;
                    editor.Launch(fullSourcePath);
                }
            }
        }

        public FileEditor FindFileEditor(Uri file)
        {
            return FileEditors.FirstOrDefault(v => v.CanEditFile(file));
        }

        public void Reload(string elfFilePath, string linkerScrintFilePath) {
            _elfUri = new Uri(elfFilePath);
            try {
                File = new ElfFile(System.IO.File.ReadAllBytes(elfFilePath));
            }
            catch (Exception ex) {
                Debug.WriteLine(ex.Message);
                return;
            }

            _dwarf = new DwarfData(_elfFile);

            _memoryDescriptor = System.IO.File.Exists(linkerScrintFilePath) ? MemoryDescriptor.FromLinkerScript(linkerScrintFilePath) : null;

            Profile = new MemoryProfile(_dwarf, _memoryDescriptor);
            Symbols.Clear();
            foreach (var memoryName in Profile.MemoryNames)
            {
                var adapter = new MemoryTypeAdapter(Profile, memoryName);
                foreach (var section in adapter.Sections)
                {
                    foreach (var symbol in section.Symbols)
                    {
                        if(symbol.Header.Info.SymbolType == Elf32SymbolType.STT_SECTION)
                        {
                            continue;
                        }
                        SymbolDescriptor desc = new SymbolDescriptor();
                        desc.Section = section;
                        desc.Memory = adapter;
                        desc.Symbol = symbol;

                        desc.DwarfUnitItem =
                            _dwarf.globalItems.FirstOrDefault(v => v.Name == symbol.Name);

                        /*Uri address1 = new Uri("http://www.contoso.com/");

                        // Create a new Uri from a string.
                        Uri address2 = new Uri("http://www.contoso.com/index.htm?date=today");

                        // Determine the relative Uri.  
                        Console.WriteLine("The difference is {0}", address1.MakeRelativeUri(address2));*/

                        if(!string.IsNullOrEmpty(desc.DwarfUnitItem?.FileStr)) {
                            if(System.IO.File.Exists(desc.DwarfUnitItem.FileStr)) {
                                Uri elfUri = new Uri(elfFilePath);
                                Uri symbolUri = new Uri(desc.DwarfUnitItem.FileStr);
                                Uri relativePath = elfUri.MakeRelativeUri(symbolUri);
                                desc.DwarfUnitItem.FileStr = relativePath.ToString();
                            }
                        }

                        Symbols.Add(desc);
                    }
                   
                }
            }
            UpdateTitle();

        }


    }
}
