using API.Core.Services.ModulePlant.Factory;

namespace API.Core.Services.ModulePlant
{

    public  class BoxFactory: AbstractFactory
    {
        public override BInFile CreateBinFile()
        {

            return new BoxBinFile();

        }
    }
}
