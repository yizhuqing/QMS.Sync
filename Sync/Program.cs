using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Sync
{
    class Program
    {

        class SyncConfig
        {
            public SyncConfig() { IsLocal = true; }
            public bool IsLocal { get; set; }
            /// <summary>
            /// 目标文件夹
            /// </summary>
            public string TargetDir { get; set; }
            public string SourceFolder { get; set; }
        }
        static void Main(string[] args)
        {
            var configPath=System.Environment.CurrentDirectory + "\\sync.json";
            if (System.IO.File.Exists(configPath))
            {
                var config = Newtonsoft.Json.JsonConvert.DeserializeObject<SyncConfig>(System.IO.File.ReadAllText(configPath));
                var targetDir = config.TargetDir;
                var sourceDir = System.Environment.CurrentDirectory + "\\"+config.SourceFolder;
                var key = string.Empty;
                if (args.Length > 0)
                {
                    key = args[0];
                }
                do
                {
                    if (string.IsNullOrEmpty(key))
                    {
                        key = System.Console.ReadLine();
                    }
                    switch (key)
                    {
                        case "0":
                            Console.WriteLine("正在同步...");
                            FileSync.Sync(sourceDir, targetDir);
                            Console.WriteLine("同步完成");
                            FileSync.Watch(sourceDir, targetDir);
                            break;
                        case "1":
                            Console.WriteLine("正在同步...");
                            FileSync.Sync(sourceDir, targetDir);
                            Console.WriteLine("同步完成");
                            break;
                        case "2":
                            FileSync.Watch(sourceDir, targetDir);
                            break;
                        case "3":
                            Console.Clear();
                            break;
                        default:
                            Console.WriteLine("不支持的命令，请输入“0:同步并监控  1：同步  2:监控  3:清屏”");
                            break;

                    }
                    key = string.Empty;
                } while (true);
            }
            else {
                Console.WriteLine("找不到文件：{0}", configPath);
            }
            Console.ReadLine();
        }
    }
}
