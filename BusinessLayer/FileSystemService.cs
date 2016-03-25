using BusinessLayer.Models;
using ServiceHelpers;
using ServiceHelpers.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using System.IO;

namespace BusinessLayer
{
    public class FileSystemService
    {
        private FileSystemManager manager = new FileSystemManager();

        public ExplorerModelVM GetExplorerModel(string path, Uri requestURL)
        {
            ExplorerModel model = manager.GetExplorerModel(path, requestURL);

            ExplorerModelVM vm = new ExplorerModelVM();
            vm.dirModelList = new List<Models.DirModel>();
            foreach (ServiceHelpers.Model.DirModel item in model.dirModelList)
                vm.dirModelList.Add(
                    new BusinessLayer.Models.DirModel()
                    {
                        DirAccessed = item.DirAccessed,
                        DirName = item.DirName,
                    });

            vm.fileModelList = new List<Models.FileModel>();
            foreach (ServiceHelpers.Model.FileModel item in model.fileModelList)
                vm.fileModelList.Add(new BusinessLayer.Models.FileModel()
                    {
                        FileAccessed = item.FileAccessed,
                        FileName = item.FileName,
                        FileSizeText = item.FileSizeText
                    });

            vm.isFile = model.isFile;

            return vm;
        }

        public string GetFileText(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException("File at " + path + " not found!");
            else
            {
                string fileText = File.ReadAllText(path);
                return fileText;
            }
        }
    }
}
