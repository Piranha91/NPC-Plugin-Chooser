using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mutagen.Bethesda;
using System.IO;
using Mutagen.Bethesda.Archives;
using Mutagen.Bethesda.Plugins;

namespace NPCPluginChooser
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
            string? dirPath = Path.GetDirectoryName(destPath);
            if (dirPath != null)
            {
                if (Directory.Exists(dirPath) == false)
                {
                    try
                    {
                        Directory.CreateDirectory(dirPath);
                    }
                    catch
                    {
                        throw new Exception("Could not create directory at " + dirPath + ". Check path length and permissions.");
                    }
                }
                try
                {
                    var fileStream = File.Create(destPath);
                    file.AsStream().CopyTo(fileStream);
                }
                catch
                {
                    Console.WriteLine("==========================================================================================================");
                    Console.WriteLine("Could not extract file from BSA: " + file.Path + " to " + destPath + ". Check path length and permissions.");
                    Console.WriteLine("==========================================================================================================");
                }
            }
            else
            {
                throw new Exception("Could not create the output directory at " + dirPath);
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

        public static bool HaveFile(string subpath, HashSet<IArchiveReader> bsaReaders, out IArchiveFile? archiveFile)
        {
            foreach (var reader in bsaReaders)
            {
                if (TryGetFile(subpath, reader, out archiveFile))
                {
                    return true;
                }
            }

            archiveFile = null;
            return false;
        }
    }
}
