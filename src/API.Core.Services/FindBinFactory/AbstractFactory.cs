namespace API.Core.Services.ModulePlant.Factory
{
    // 定义抽象工厂类基类
    // 抽象方法 创建bin文件
    public abstract class AbstractFactory
    {
        public abstract BInFile CreateBinFile();
     
    }
}
