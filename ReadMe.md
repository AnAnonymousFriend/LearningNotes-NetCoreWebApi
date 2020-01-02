本项目在https://github.com/anjoy8/Blog.Core基础上搭建

更改部分内容：

​	1.封装多表查询（原作者只封装了单表查询）：

​	ORM 框架采用 SqlSugar 查阅文档可得知，支持动态查询（拼接Lambd或SQL）

![TIM截图20200102142309](C:\Users\kai_wang\Desktop\TIM截图20200102142309.png)

封装后如下图代码所示

![image-20200102142435111](C:\Users\kai_wang\AppData\Roaming\Typora\typora-user-images\image-20200102142435111.png)

DoubleTable 为参数实体类 包含需查询表的名称，外联表及关联参数等