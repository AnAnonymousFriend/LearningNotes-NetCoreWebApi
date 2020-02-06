using API.Core.Services.ModulePlant.Factory;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Core.Services.ModulePlant
{
    public class OSFactory: AbstractFactory
    {
        public override BInFile CreateBinFile()
          {
             return new NanChangYaBo();
        }

}
}
