using System;

namespace API.Core.Services.ModulePlant
{
    public class BoxBinFile:BInFile
    {
        // Fsbox具体显示寻找Bin文件的函数
        public override void CreateBinFile()
        {
            Console.WriteLine("Box 创建bin文件");
        }
    }
}
