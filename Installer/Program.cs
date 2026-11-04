using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using WixSharp;
using WixSharp.Forms;
using File = WixSharp.File;

namespace Installer
{
    public class Program
    {
        static void Main()
        {
            var assemblyConfigurationAttribute = typeof(Program).Assembly.GetCustomAttribute<AssemblyConfigurationAttribute>();
            string buildConfigurationName = assemblyConfigurationAttribute.Configuration;
            Console.WriteLine("Building installer for Pegboard " + buildConfigurationName);

            //TODO: pull this from the Pegboard assembly
            //the revitVersion of Revit for which to install the add in
            string revitVersion;
            //the GUID of the addin for the specified verison of Revit
            string versionGuid;
            //Addin directory path, will vary based on revitVersion
            string baseAddinDirPath;
            //target revitVersion based on revitVersion specific project configuration
#if R2024
            revitVersion = "2024";
            versionGuid = "f0e7c1d5-3b8a-4f2e-9c6b-0f1e2d3c4b5a";
            baseAddinDirPath = @"C:\ProgramData\Autodesk\Revit\Addins\" + revitVersion + @"\";
#elif R2025
      revitVersion = "2025";
            versionGuid = "0663ee30-aa56-4020-8c7b-7d879526efdd";
            baseAddinDirPath = @"C:\ProgramData\Autodesk\Revit\Addins\" + revitVersion + @"\";
#elif R2026
            revitVersion = "2026";
            versionGuid = "6a32a52c-a130-41d0-a522-8e0939c6bf56";
            baseAddinDirPath = @"C:\ProgramData\Autodesk\Revit\Addins\" + revitVersion + @"\";
#elif R2027
            revitVersion = "2027";
            versionGuid = "b289bbca-0224-4517-8d7d-9d3692a12d01";
            baseAddinDirPath = @"C:\Program Files\Autodesk\Revit\Addins\" + revitVersion + @"\";
#else
#endif
            string addinVersion = "1.0.4";

            //the location of the source files
            string pegboardSourceDirPath = @".\source\" + revitVersion + @"\";
            //the name of the installation
            string installName = "Pegboard " + revitVersion;

            var files = Directory.GetFiles(pegboardSourceDirPath);
            List<File> dllFiles = new List<File>();
            foreach(string f in files) {
                //TODO: exclude unecessary .dlls such as the RevitAPI libraries
                if(Path.GetExtension(f).Equals(".dll"))
                {
                    //GetFiles() will return results in the form ".\source\2025\filename.dll",
                    //so it doesn't need to be prepended with pegboardSourceDirPath
                    dllFiles.Add(new File(f));
                }
            }

            var addInManifestFile = new File(pegboardSourceDirPath + @"Pegboard.addin");
            Dir baseAddinDir = new Dir(
                baseAddinDirPath,
                addInManifestFile,
                new Dir("Pegboard", dllFiles.ToArray()));

            ManagedProject project = new ManagedProject(installName, baseAddinDir);
            project.GUID = new Guid(versionGuid);
            project.Version = new Version(addinVersion);
            project.Platform = Platform.x64;
            project.OutFileName = "Pegboard " + revitVersion + " " + addinVersion;

            project.ManagedUI = new ManagedUI();
            project.ManagedUI.InstallDialogs
                .Add(Dialogs.Welcome)
                .Add(Dialogs.Progress)
                .Add(Dialogs.Exit);

            project.ManagedUI.ModifyDialogs
                .Add(Dialogs.MaintenanceType)
                .Add(Dialogs.Progress)
                .Add(Dialogs.Exit);

            project.BuildMsi();
        }
    }
}