# QMS.Sync
QMS 文件同步工具

# 建立bat批处理文件，文件内容
```javascript
echo 切换到qms定制项目目录
cd /d E:\src_new\QMS\QmsMonitor\QMS.XM 
echo 运行编译后exe，并指定参数为0，0表示同步并监控 
C:\Users\billy\Source\Repos\QMS.Sync\Sync\bin\Debug\Sync.exe 0
```
# sync.json 文件
```javascript
{
	// 是否本地同步
    "IsLocal":true,
	// 目标文件地址
    "TargetDir":"E:\\src_new\\QMS\\Qms5\\CTStudyWeb",
	// 源代码文件夹
    "SourceFolder":"src"
}
```

