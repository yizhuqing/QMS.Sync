# QMS.Sync
QMS 文件同步工具

# 建立bat批处理文件，文件内容
cd /d E:\src_new\QMS\QmsMonitor\QMS.XM
E:\src_new\QMS\QmsMonitor\Sync\bin\Debug\Sync.exe 0

内容详解： 
切换到qms定制项目目录
cd /d E:\src_new\QMS\QmsMonitor\QMS.XM
允许编译后exe，并制定参数为0，0表示同步并监控
E:\src_new\QMS\QmsMonitor\Sync\bin\Debug\Sync.exe 0

# sync.json 文件
{
	// 是否本地同步
    "IsLocal":true,
	// 目标文件地址
    "TargetDir":"E:\\src_new\\QMS\\Qms5\\CTStudyWeb",
	// 源代码文件夹
    "SourceFolder":"src"
}

