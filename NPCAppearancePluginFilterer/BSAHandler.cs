using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mutagen.Bethesda;
using System.IO;

namespace NPCAppearancePluginFilterer
{
    class BSAHandler
    {
        public static HashSet<IArchiveReader> openBSAArchiveReaders(string currentDataDir, ModKey currentPlugin)
        {
            var readers = new HashSet<IArchiveReader>();

            foreach (var bsaFile in Archive.GetApplicableArchivePaths(GameRelease.SkyrimSE, currentDataDir, currentPlugin))
            {
                try
                {
                    var bsaReader = Archive.CreateReader(GameRelease.SkyrimSE, bsaFile);
                    readers.Add(bsaReader);
                }
                catch
                {
                    throw new Exception("Could not open archive " + bsaFile);
                }
            }
            return readers;
        }

        public static void extractFileFromBSA(IArchiveFile file, string destPath)
        {
            try
            {
                string? dirPath = Path.GetDirectoryName(destPath);
                if (dirPath != null)
                {
                    if (Directory.Exists(dirPath) == false)
                    {
                        Directory.CreateDirectory(dirPath);
                    }
                    var fileStream = File.Create(destPath);
                    file.CopyDataTo(fileStream);
                }
                else
                {
                    throw new Exception("Could not create the output directory at " + dirPath);
                }
            }
            catch
            {
                throw new Exception("Could not extract file from BSA: " + file.Path + " to directory " + destPath);
            }
        }

        

        public static bool TryGetFile(string subpath, IArchiveReader bsaReader, out IArchiveFile? file)
        {
            file = null;
            var files = bsaReader.Files.Where(candidate => candidate.Path.Equals(subpath, StringComparison.OrdinalIgnoreCase));
            if (files.Any())
            {
                file = files.First();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
