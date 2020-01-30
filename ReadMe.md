本项目在https://github.com/anjoy8/Blog.Core基础上搭建

更改部分内容：

​	1.封装多表查询（原作者只封装了单表查询）：

​	ORM 框架采用 SqlSugar 查阅文档可得知，支持动态查询（拼接Lambd或SQL）
DoubleTable 为参数实体类 包含需查询表的名称，外联表及关联参数等



发布注意！

如果直接右键属性——发布。将编译过后的文件上传到服务器上，很可能无法启动！

```
![发布图片](./docs/Img/release.png.png)
```



https://github.com/AnAnonymousFriend/LearningNotes-NetCoreWebApi/blob/master/Img/PathGenerated.png

![image-20200130105335359](C:\Users\kai_wang\AppData\Roaming\Typora\typora-user-images\image-20200130105335359.png)

可能原因1：

​				Repository 与 Services 已解耦，并未在项目中依赖引用。所以需要发布时候，手动将API.Core.Services.dll 与 API.Core.Repository.dll 放入bin文件中。

![image-20200130112848146](C:\Users\kai_wang\AppData\Roaming\Typora\typora-user-images\image-20200130112848146.png)

本地Debug 能正常运行是因为设置了生成指定路径

![image-20200130113205154](C:\Users\kai_wang\AppData\Roaming\Typora\typora-user-images\image-20200130113205154.png)





