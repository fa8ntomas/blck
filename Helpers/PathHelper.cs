﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLEditor
{
    static class PathHelper
    {
        public static string RelativePath(this string candidate, string other)
        {
            if (String.IsNullOrWhiteSpace(candidate))
            {
                return "";
            }

            var isRelative = false;
            List<String> list = new List<String>();
            try
            {
                var candidateInfo = new DirectoryInfo(candidate);
                var otherInfo = new DirectoryInfo(other);

                if (String.Compare(candidateInfo.FullName,otherInfo.FullName, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    isRelative = true;
                }
                else
                {
                    list.Add(candidateInfo.Name);
                    while (candidateInfo.Parent != null)
                    {
                        if (String.Compare(candidateInfo.FullName, otherInfo.FullName, StringComparison.OrdinalIgnoreCase) == 0)
                        {
                            isRelative = true;
                            break;
                        }
                        else
                        {

                            candidateInfo = candidateInfo.Parent;
                            list.Add(candidateInfo.Name);
                        }
                    }
                }
            }
            catch (Exception error)
            {
                var message = String.Format("Unable to check directories {0} and {1}: {2}", candidate, other, error);
                Trace.WriteLine(message);
            }

            String result;
            if (isRelative)
            {
                list.Reverse();
                result= System.IO.Path.Combine(list.ToArray());
            }
            else
            {
                result= candidate;
            }
         
            return result;
        }



        public static string CreateFileWithUniqueName(string folder, string fileName, int maxAttempts = 1024)
        {
            // get filename base and extension
            var fileBase = Path.GetFileNameWithoutExtension(fileName);
            var ext = Path.GetExtension(fileName);
            // build hash set of filenames for performance
            var files = new HashSet<string>(Directory.GetFiles(folder));

            for (var index = 0; index < maxAttempts; index++)
            {
                // first try with the original filename, else try incrementally adding an index
                var name = (index == 0)
                    ? fileName
                    : String.Format("{0}({1}){2}", fileBase, index, ext);

                // check if exists
                var fullPath = Path.Combine(folder, name);
                if (files.Contains(fullPath))
                    continue;

                // try to create the file
                try
                {
                    return fullPath;
                }
                catch (DirectoryNotFoundException) { throw; }
                catch (DriveNotFoundException) { throw; }
                catch (IOException)
                {
                    // Will occur if another thread created a file with this 
                    // name since we created the HashSet. Ignore this and just
                    // try with the next filename.
                }
            }

            throw new Exception("Could not create unique filename in " + maxAttempts + " attempts");
        }
    }
}