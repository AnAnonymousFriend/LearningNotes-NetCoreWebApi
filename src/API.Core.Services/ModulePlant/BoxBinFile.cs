﻿using System;
using System.Collections.Generic;
using System.Text;

namespace API.Core.Services.ModulePlant
{
    public class BoxBinFile:BInFile
    {
        public override void CreateBinFile()
        {
            Console.WriteLine("Box 创建bin文件");
        }
    }
}
