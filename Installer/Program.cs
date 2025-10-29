using System;
using System.Collections.Generic;
using System.IO;
using WixSharp;
using File = WixSharp.File;

namespace Installer
{
    public class Program
    {
        static void Main()
        {
            //TODO: target the version of Revit matching the build
            //the version of Revit for which to install the add in
            string version = "2026";
            //the GUID for the Pegboard addin for the specified Revit version
            string versionGuid = "6a32a52c-a130-41d0-a522-8e0939c6bf56";
            //the directory where addins will be installed for this version of Revit
            string baseAddinDirPath = @"C:\ProgramData\Autodesk\Revit\Addins\" + version + @"\";
            //the location of the source files
            string pegboardSourceDirPath = @".\source\" + version + @"\";
            //the name of the installation
            string installName = "Pegboard " + version;

            var files = Directory.GetFiles(pegboardSourceDirPath);
            List<File> dllFiles = new List<File>();
            foreach(string f in files) {
                //TODO: exclude unecessary .dlls such as the RevitAPI libraries
                if(Path.GetExtension(f).Equals(".dll"))
                {
                    //GetFiles() will return a in the form ".\source\2025\filename.dll",
                    //so it doesn't need to be prepended with pegboardSourceDirPath
                    dllFiles.Add(new File(f));
                }
            }

            var addInManifestFile = new File(pegboardSourceDirPath + @"Pegboard.addin");
            Dir baseAddinDir = new Dir(
                baseAddinDirPath,
                addInManifestFile,
                new Dir("Pegboard", dllFiles.ToArray()));

            var project = new Project( installName, baseAddinDir);

            project.GUID = new Guid(versionGuid);
            //project.SourceBaseDir = pegboardSourceDirPath;
            //project.OutDir = "<output dir path>";

            project.BuildMsi();
        }
    }
}