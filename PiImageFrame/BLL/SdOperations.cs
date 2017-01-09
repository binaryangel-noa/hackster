using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class SdOperations
    {
        public async Task<IEnumerable<string>> ListFiles(bool webFormatPathes)
        {
            var devices = Windows.Storage.KnownFolders.RemovableDevices;
            var query = devices.CreateFolderQuery(Windows.Storage.Search.CommonFolderQuery.DefaultQuery);
            var folder = query.Folder;

            var folders = await devices.GetFoldersAsync(Windows.Storage.Search.CommonFolderQuery.DefaultQuery);

            var fileList = new List<string>();

            var local = Windows.Storage.ApplicationData.Current.LocalCacheFolder;

            var targetDir = Path.Combine(local.Path, PathConstants.TEMP_SUBDIRECOTRY);

            if (Directory.Exists(targetDir))
            {
                Directory.Delete(targetDir, true);
            }
            Directory.CreateDirectory(targetDir);

            foreach (var storageFolder in folders)
            {
                var files = await storageFolder.GetFilesAsync();
                var myFiles = files.Select(x =>
                {
                    try
                    {
                        var task = Task.Run(() =>
                        {
                            var targetPath = Path.Combine(targetDir, x.Name);

                            System.IO.File.Copy(x.Path, targetPath, true);
                            if (webFormatPathes)
                            {
                                var uri = new System.Uri(targetPath);
                                var converted = uri.AbsoluteUri;
                                return converted;
                            }
                            return x.Path;

                        });
                        Task.WaitAll(task);
                        return task.Result;
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }

                });
                fileList.AddRange(myFiles);
            }
            return fileList;
        }

        public async Task<bool> CopyFiles()
        {
            var task = Task.Run(() =>
            {
                var destDir = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, PathConstants.IMAGES_SUBDIRECTORY);
                if (Directory.Exists(destDir))
                {
                    Directory.Delete(destDir, true);
                }
                Directory.CreateDirectory(destDir);
                var localCacheFolder = Windows.Storage.ApplicationData.Current.LocalCacheFolder;
                var sourceDir = Path.Combine(localCacheFolder.Path, PathConstants.TEMP_SUBDIRECOTRY);
                var files = System.IO.Directory.GetFiles(sourceDir);
                foreach (var file in files)
                {
                    var destPath = Path.Combine(destDir, Path.GetFileName(file));
                    System.IO.File.Copy(file, destPath);
                }
                return true;
            });
            return await task;
        }

        public async Task<IEnumerable<string>> ListCopiedFiles(bool webFormatPathes)
        {
            var task = Task.Run(() =>
            {
                var sourceDir = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, PathConstants.IMAGES_SUBDIRECTORY);
                if (Directory.Exists(sourceDir))
                {
                    sourceDir = sourceDir + @"\";
                    var files = System.IO.Directory.GetFiles(sourceDir);

                    if (webFormatPathes)
                    {
                        files = files.Select(x =>
                        {
                            var uri = new System.Uri(x);
                            var converted = uri.AbsoluteUri;
                            return converted;
                        }).ToArray();
                    }
                    return files.ToList();
                }

                return new List<string>();

            });
            return await task;
        }
    }
}
