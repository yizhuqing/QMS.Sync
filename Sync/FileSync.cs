using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Sync
{
    public class FileSync
    {
        private static string _sourceDir;
        private static string _targetDir;
        public static void Sync(string sourceDir, string targetDir)
        {
        
            try
            {
                DirectoryInfo dir = new DirectoryInfo(sourceDir);
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //获取目录下（不包含子目录）的文件和子目录
                foreach (FileSystemInfo i in fileinfo)
                {
                    if (i is DirectoryInfo)     //判断是否文件夹
                    {
                        if (!Directory.Exists(targetDir + "\\" + i.Name))
                        {
                            Directory.CreateDirectory(targetDir + "\\" + i.Name);   //目标目录下不存在此文件夹即创建子文件夹
                        }
                        Sync(i.FullName, targetDir + "\\" + i.Name);    //递归调用复制子文件夹
                    }
                    else
                    {
                        //File.Copy(i.FullName, targetDir + "\\" + i.Name, true);      //不是文件夹即复制文件，true表示可以覆盖同名文件
                        string sourceFile = sourceDir + "\\" + i.Name;
                        string targetFile = targetDir + "\\" + i.Name;
                        System.IO.File.WriteAllBytes(targetFile, System.IO.File.ReadAllBytes(sourceFile));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("同步错误：" + e.Message);
            }
        }
        public static void Watch(string sourceDir, string targetDir)
        {
            _sourceDir = sourceDir;
            _targetDir = targetDir;
            Console.WriteLine("正在监控变动...");
            if (System.IO.Directory.Exists(sourceDir) && System.IO.Directory.Exists(targetDir))// 文件夹必须存在
            {
                FileSystemWatcher w = new FileSystemWatcher(sourceDir);
                w.Filter = "*.*";
                w.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite |
             NotifyFilters.FileName | NotifyFilters.DirectoryName;
                w.IncludeSubdirectories = true;
                w.EnableRaisingEvents = true;
                w.Created += w_Created;
                w.Deleted += w_Deleted;
                w.Changed += w_Changed;
                w.Renamed += w_Renamed;
            }
        }

        static void w_Deleted(object sender, FileSystemEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(e.ChangeType);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" ");
            Console.Write(e.Name);
            Console.Write(" ");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(e.FullPath);
            try
            {

                if (e.Name.LastIndexOf('.') > -1)// 文件
                {
                    System.IO.File.Delete(_targetDir + "\\" + e.Name);
                }
                else
                {
                    System.IO.Directory.Delete(_targetDir + "\\" + e.Name, true);
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("操作失败：" + ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.WriteLine("同步成功");
        }

        static void w_Created(object sender, FileSystemEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(e.ChangeType);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" ");
            Console.Write(e.Name);
            Console.Write(" ");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(e.FullPath);
            var dir = Path.GetDirectoryName(_targetDir + "\\" + e.Name);
            try
            {

                if (!System.IO.Directory.Exists(dir))
                {
                    System.IO.Directory.CreateDirectory(dir);
                }
                if (e.Name.LastIndexOf('.') > -1)// 文件
                {
                    string sourceFile = _sourceDir + "\\" + e.Name;
                    string targetFile = _targetDir + "\\" + e.Name;
                    System.IO.File.WriteAllBytes(targetFile, System.IO.File.ReadAllBytes(sourceFile));
                }
                else
                {
                    if (!System.IO.Directory.Exists(_targetDir + "\\" + e.Name))
                    {
                        System.IO.Directory.CreateDirectory(_targetDir + "\\" + e.Name);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("操作失败：" + ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.WriteLine("同步成功");
        }


        static void w_Renamed(object sender, RenamedEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(e.ChangeType);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" ");
            Console.Write(e.OldName);
            Console.Write(e.OldFullPath);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(" ");
            Console.Write(e.Name);
            Console.Write(e.FullPath);
            Console.Write(Thread.CurrentThread.Name);
            Console.Write("\r\n");
            string sourceFile = _sourceDir + "\\" + e.Name;
            string targetFile = _targetDir + "\\" + e.Name;
            //System.IO.File.Replace(targetFile, System.IO.File.ReadAllBytes(sourceFile));
        }
        private static volatile object _lock = true;
        static void w_Changed(object sender, FileSystemEventArgs e)
        {
            Thread.Sleep(300);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(e.ChangeType);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" ");
            Console.Write(e.Name);
            Console.Write(" ");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(e.FullPath);
            try
            {
                if (e.Name.LastIndexOf('.') > -1)// 文件
                {
                    //System.IO.File.Copy(_sourceDir + "\\" + e.Name, _targetDir + "\\" + e.Name, true);
                    string sourceFile = _sourceDir + "\\" + e.Name;
                    string targetFile = _targetDir + "\\" + e.Name;
                    System.IO.File.WriteAllBytes(targetFile, System.IO.File.ReadAllBytes(sourceFile));
                }
                else
                { // 文件夹
                    if (!System.IO.Directory.Exists(e.FullPath))
                    {
                        System.IO.Directory.CreateDirectory(_targetDir + "\\" + e.Name);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("操作失败：" + ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.Write("\r\n");
            lock (_lock)
            {

            }
        }
    }
}
