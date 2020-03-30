**API对外接口文档** 

**简要描述：** 

- 添加Bin

**请求URL：** 

- ` http://localhost:5000/api/Test/AddBin `

**请求方式：**

- POST 

**参数：** 

| 参数名        | 必选 | 类型   | 说明      |
| :------------ | :--- | :----- | --------- |
| pn            | 是   | string | 型号名    |
| sn            | 是   | string | 序列号    |
| waveband      | 否   | string | 波段      |
| distance      | 是   | string | 距离      |
| isddm         | 否   | bool   | 是否能DDM |
| binType       | 是   | string | 类型      |
| binMeter      | 是   | string | 米数      |
| compatibility | 是   | string | 兼容      |
| manufacturer  | 是   | string | 厂商      |
| version       | 是   | string | 版本      |
| binByte       | 否   | string | 字节      |
|               |      |        |           |

 **返回示例**

``` 
{
  "success": true,
  "code": 0,
  "msg": "添加成功",
  "response": "1"
}
```

 **返回参数说明** 

| 参数名   | 类型 | 说明               |
| :------- | :--- | ------------------ |
| code     | int  | 0为成功            |
| response | int  | 返回数据库新增的Id |

 **备注** 

- 更多返回错误代码请看首页的错误代码描述
