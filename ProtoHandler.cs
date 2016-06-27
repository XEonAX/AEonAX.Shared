using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace AEonAX.Shared
{
    public static class ProtoHandler
    {
        public static void Register(string protocol, string protocolName, string executablePath)
        {
            var executableFilename = Path.GetFileName(executablePath);
            RegistryKey protoHandler = Registry.ClassesRoot.CreateSubKey(protocol);
            protoHandler.SetValue("", "URL:" + protocolName);
            protoHandler.SetValue("URL Protocol", "");
            RegistryKey defaultIcon = protoHandler.CreateSubKey("DefaultIcon");
            defaultIcon.SetValue("", executableFilename);
            RegistryKey shell = protoHandler.CreateSubKey("shell");
            RegistryKey open = shell.CreateSubKey("open");
            RegistryKey command = open.CreateSubKey("command");
            command.SetValue("", executablePath + " %1");
        }

        public static void RegisterThisApplication(string protocol, string protocolName)
        {
            Register(protocol, protocolName, System.Reflection.Assembly.GetCallingAssembly().Location);
        }

        public static void UnregisterThisApplication(string v)
        {
            Unregister(v);
        }

        private static void Unregister(string v)
        {
            Registry.ClassesRoot.DeleteSubKeyTree(v,false);
        }
    }
}
