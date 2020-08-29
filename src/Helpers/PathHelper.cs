using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace BLEditor
{
    internal static class PathHelper
    {

        public static String Delta(String XMLFilename, String Filename)
        {
            if (String.IsNullOrWhiteSpace(Filename))
            {
                return String.Empty;
            }

            String result = System.IO.Path.Combine(PathHelper.RelativePath(System.IO.Path.GetDirectoryName(Filename), System.IO.Path.GetDirectoryName(XMLFilename)), System.IO.Path.GetFileName(Filename));

            Console.WriteLine(System.IO.Path.GetDirectoryName(Filename) + " -- " + XMLFilename + "--" + PathHelper.RelativePath(System.IO.Path.GetDirectoryName(Filename), System.IO.Path.GetDirectoryName(XMLFilename)));
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

        public static string RelativePath(string pathToRelativize, string pathTarget)
        {
            if (String.IsNullOrWhiteSpace(pathToRelativize))
            {
                return "";
            }

            bool isRelative = false;
            List<String> list = new List<String>();
            try
            {
                DirectoryInfo pathToRelativizeInfo = new DirectoryInfo(pathToRelativize);
                DirectoryInfo pathTargetInfo = new DirectoryInfo(pathTarget);

                if (pathToRelativizeInfo.FullName.Equals(pathTargetInfo.FullName, StringComparison.OrdinalIgnoreCase))
                {
                    isRelative = true;
                }
                else if (pathTargetInfo.FullName.StartsWith(pathToRelativizeInfo.FullName))
                {
                    while (pathTargetInfo.Parent != null)
                    {
                        list.Add("..");

                        if (pathTargetInfo.Parent.FullName.Equals(pathToRelativizeInfo.FullName, StringComparison.OrdinalIgnoreCase))
                        {
                            isRelative = true;
                            break;
                        }
                        else
                        {
                            pathTargetInfo = pathTargetInfo.Parent;
                        }
                    }
                }
                else if (pathToRelativizeInfo.FullName.StartsWith(pathTargetInfo.FullName))
                {
                    while (pathToRelativizeInfo.Parent != null)
                    {
                        list.Add(pathToRelativizeInfo.Name);
                
                        if (pathToRelativizeInfo.Parent.FullName.Equals(pathTargetInfo.FullName, StringComparison.OrdinalIgnoreCase))
                        {
                            isRelative = true;
                            break;
                        }
                        else
                        {
                            pathToRelativizeInfo = pathToRelativizeInfo.Parent;
                        }
                    }
                }
            }
            catch (Exception error)
            {
                var message = String.Format("Unable to check directories {0} and {1}: {2}", pathToRelativize, pathTarget, error);
                Trace.WriteLine(message);
            }

            String result;
            if (isRelative)
            {
                list.Reverse();
                result = System.IO.Path.Combine(list.ToArray());
            }
            else
            {
                result = pathToRelativize;
            }

            return result;
        }

        public static string RelativizePath(string parent, string filename)
        {
            return System.IO.Path.Combine(PathHelper.RelativePath(System.IO.Path.GetDirectoryName(filename), System.IO.Path.GetDirectoryName(parent)), System.IO.Path.GetFileName(filename));
        }

        public static string ShortFileName(string long_name)
        {
            char[] name_chars = new char[1024];
            long length = GetShortPathName(
                long_name, name_chars,
                name_chars.Length);

            string short_name = new string(name_chars);
            return short_name.Substring(0, (int)length);
        }

        // Define GetShortPathName API function.
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern uint GetShortPathName(string lpszLongPath, char[] lpszShortPath, int cchBuffer);
    }
}