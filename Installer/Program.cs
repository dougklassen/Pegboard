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

            //the version of Revit for which to install the add in
            string version;
            //the GUID of the addin for the specified verison of Revit
            string versionGuid;
//target version based on version specific project configuration
#if R2025
            version = "2025";
            versionGuid = "0663ee30-aa56-4020-8c7b-7d879526efdd";
#elif R2026
            version = "2026";
            versionGuid = "6a32a52c-a130-41d0-a522-8e0939c6bf56";
#else
#endif

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

            ManagedProject project = new ManagedProject(installName, baseAddinDir);
            project.GUID = new Guid(versionGuid);

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