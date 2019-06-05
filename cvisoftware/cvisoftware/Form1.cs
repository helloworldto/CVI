using DataComm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using xCurveControl;

namespace cvisoftware
{
    public partial class Form1 : Form
    {

        private static string dataCommonConfigFileName = "D:\\2019\\cvisoftware\\cvisoftware\\bin\\Debug\\datacommconfig.xml";

        private CurveControl curveControl = new CurveControl();
        private SystemInfo sysInfo;
        private Navigation[] navigation;
        
        private TestUnit[] testUnit;
        private Window[] window;
        private SubWindow[] subWindow;
        private Sensor[] sensor;
        private SensorLimit[] sensorLimit;
        private SensorName[] sensorName;
        private Group[] group;
        private ProdInfoItem[] prodInfoItem;
        private int subWindowNo = 1;

       

        //public ControlTypeEnum controlType; //控制类型变量
       // public ProdInfoItem prodInfoItem = new ProdInfoItem();  //产品的测试条目
        
        

        private int importantId = 21000;//避免冲突的importantId 

        //定义数据组件
        private DataComponent dataComponent = new DataComponent();
        //控制类型
        //定义正在监测 的台位数组
        private TestUnit[] testingTestUnit;
        //监测单元里面含有子窗口、坐标系、传感器信息
        public Form1()
        {
            InitializeComponent();
            sysInfo = new SystemInfo();
            navigation = new Navigation[3];
            testUnit = new TestUnit[10];
            window = new Window[10];
            subWindow = new SubWindow[10];
            sensor = new Sensor[17];
            sensorLimit = new SensorLimit[17];

        }

        /// <summary>
        /// 配置所有信息
        /// </summary>
        public void initAllInfo()
        {
            /// 系统信息配置
            sysInfoMsgInit();
            
            /// 导航信息配置
            naviInfoMsgInit();

            /// 监测单元配置
            tUintInfoMsgInit();

            /// 窗口配置
            windowInfoMsgInit();

            /// 定义子窗口变量
            subWindowInfoMsgInit();

            /// 坐标系配置
            coordinateConfigInfoMsgInit();

            /// 传感器类型配置
            sensorTypeInfoMsgInit();

            /// 传感器配置 sensorConfig
            sensorInfoMsgInit();

            /// 传感器名称配置
            sensorNameMsgInit();
            
            /// 组信息配置
            groupInfoMsgInit();
            
            /// 录入条目信息配置
            prodInfoItemMsgInit();
            
            
        }
       
        /// <summary>
        /// 系统信息配置
        /// 对应systeminfo表
        /// </summary>
        public void sysInfoMsgInit()
        {
            sysInfo.SoftwareName = "黄岛特冰实验室3011";
            sysInfo.CompanyName = "海尔集团";
            sysInfo.TestUnitNum = 10;
            sysInfo.SensorNum = 17;
            sysInfo.Category = 2;
            sysInfo.Language = 0;
            sysInfo.TestUnitNameConfig = "冰箱";
            sysInfo.InputLink = true; ///bool类型
            sysInfo.CommonSensorNum = 6;
            sysInfo.EnSoftwareName = "Huang Dao Special Refrigerator Sampling Laboratory";
            sysInfo.EnTestUnitNameConfig = "Fridge";
            sysInfo.DisplayFlag = 1;
            sysInfo.DisplayTimeLimit = 5000;
            sysInfo.InfoQueryTimeLimit = 24;
            sysInfo.LabName = "黄岛特冰抽样实验室";
            sysInfo.EnLabName = "Huang Dao Special Refrigerator Sampling Laboratory";
            sysInfo.DefaultNoPowerLimit = 5;
            sysInfo.LabCode = "2118021111301"; /////////////每个人学号+“01”,必须不同
            sysInfo.SoftwareName = "黄岛特冰实验室";

            string addInfoUrl = "http://115.28.236.114/RestInterfaceSystem/systemInfoController/addSystemInfo";
            ///string searchInfoUrl = "http://115.28.236.114/RestInterfaceSystem/systemInfoController/getSystemInfoByLabCode/1102";
            ///
            string addData =
                "labCode=" + sysInfo.LabCode+
                "&softwareName="+sysInfo.SoftwareName+
                "&companyName=" + sysInfo.CompanyName +
                "&testUnitNum=" + sysInfo.TestUnitNum +
                "&sensorNum=" + sysInfo.SensorNum +
                "&category=" + sysInfo.Category +
                "&language=" + sysInfo.Language +
                "&testUnitNameConfig=" + sysInfo.TestUnitNameConfig +
                "&inputLink=1" +
                "&commonSensorNum=" + sysInfo.CommonSensorNum +
                "&englishSoftwareName=" + sysInfo.EnSoftwareName +
                "&englishTestUnitNameConfig=" + sysInfo.EnTestUnitNameConfig +
                "&displayFlag=" + sysInfo.DisplayFlag +
                "&diplayTimeLimit=" + sysInfo.DisplayTimeLimit +
                "&infoQueryTimeLimit=" + sysInfo.InfoQueryTimeLimit +
                "&testTable=testdata"+
                "&labName=" + sysInfo.LabName +
                "&englishLabName=" + sysInfo.EnLabName + 
                "&preValue=0"+
                "&cacheValue=10"+
                "&loadable=1"+
                "&reportPassword=123456"+
                "&noPowerLimit=1"+
                "&testCollectionPassword=123456"+
                "&currentTestProdInfoItem=1.0.0"+
                "&currentSensorConfig=2.1.0"+
                "&currentCoorDinateConfig=3.1.0";

            //var json = new JsonUtil<SystemInfo>(); // 这是你项目中原来就有的 json 工具
            //string addData = json.EnCodeObjToJson(sysInfo); 
            //PostData已经添加成功
            string addRus = PostData(addInfoUrl, addData);

            string rus = addRus.Substring(addRus.IndexOf(":") + 1, 3);

            if (rus.Equals("200"))
                MessageBox.Show("系统信息配置成功\n");
            else
                MessageBox.Show("系统信息配置失败，失败编码： " + rus + "\n");
        }

        /// <summary>
        /// 导航信息配置
        /// 对应testunitnavigationinfo表
        /// </summary>
        public void naviInfoMsgInit()
        {
            ///定义导航信息的变量
            ///表里id是主键，所以id必须为表里没有的，否则插入不进去

            navigation[0] = new Navigation();
            navigation[0].Name = "抽样室";
            navigation[0].Id = importantId;
            navigation[0].Description = "黄岛特冰抽样实验室";
            navigation[0].BelongedId = 0;
            navigation[0].EnName = "SanplingUnit C";

            navigation[1] = new Navigation();
            navigation[1].Name = "A室";
            navigation[1].Id = importantId+1;
            navigation[1].Description = "A室";
            navigation[1].BelongedId = importantId;
            navigation[1].EnName = "A unit C";

            navigation[2] = new Navigation();
            navigation[2].Name = "B室";
            navigation[2].Id = importantId+2;
            navigation[2].Description = "B室";
            navigation[2].BelongedId = importantId;
            navigation[2].EnName = "B unit C";

            ///定义清空导航表的url
            ///

            //string clearUrl = "http://115.28.236.114/RestInterfaceSystem/testUnitNavigationInfoController/deleteAll";

            //string clearRus = GetData(clearUrl);

            ///将导航数据添加到导航表

            int flag = 1;

            for (int i = 0; i < navigation.Length; i++)
            {
                string naviUrl = "http://115.28.236.114/RestInterfaceSystem/testUnitNavigationInfoController/addTestUnitNavigationInfo";
                string naviData = "id=" + navigation[i].Id +
                    "&name=" + navigation[i].Name +
                    "&description=" + navigation[i].Description +
                    "&belongedId=" + navigation[i].BelongedId +
                    "&englishName=" + navigation[i].EnName + 
                    "&labCode=" + sysInfo.LabCode;
                string naviRus = PostData(naviUrl, naviData);

                string rus = naviRus.Substring(naviRus.IndexOf(":") + 1, 3);

                if (!rus.Equals("200"))
                {
                    flag = 0;
                    MessageBox.Show("第：" + i.ToString() + "条导航信息配置失败，失败编码： " + rus+"\n");
                }
                
            }
            if (flag == 1)
                MessageBox.Show("导航信息配置成功\n");
        }

        /// <summary>
        /// 监测单元配置
        /// 对应TestUnitConfig表
        /// </summary>
        public void tUintInfoMsgInit()
        {
            testUnit[0] = new TestUnit();
            testUnit[0].TestUnitNo = importantId;
            testUnit[0].TestUnitName = "";
            testUnit[0].BelongedId = 2;
            testUnit[0].IfBorrow = true;
            testUnit[0].IsGroupInfoDefault = true;
            testUnit[0].EnTestUnitName = "";
            testUnit[0].DiffMode = 1;

            for (int i = 0; i < testUnit.Length; i++)
            {
                if (i != 0)
                    testUnit[i] = new TestUnit();
                testUnit[i].TestUnitNo = testUnit[0].TestUnitNo + i;
                if (i < 5)
                {
                    testUnit[i].TestUnitName = "A0" + (i + 1).ToString();
                    testUnit[i].BelongedId = testUnit[0].TestUnitNo+2;
                    testUnit[i].EnTestUnitName = "EnA0" + (i + 1).ToString();
                }
                else
                {
                    testUnit[i].TestUnitName = "B0" + (i - 4).ToString();
                    testUnit[i].BelongedId = testUnit[0].TestUnitNo+3;
                    testUnit[i].EnTestUnitName = "EnB0" + (i - 4).ToString();
                }
                testUnit[i].IfBorrow = true;
                testUnit[i].IsGroupInfoDefault = true;
                testUnit[i].DiffMode = 1;
            }

            int flag = 1;

            for (int i = 0; i < testUnit.Length; i++)
            {
                string tuUrl = "http://115.28.236.114/RestInterfaceSystem/testUnitConfigController/addTestUnitConfig";

                string tuData = "testUnitNo=" + testUnit[i].TestUnitNo +
                    "&testUnitName=" + testUnit[i].TestUnitName +
                    "&belongedId=" + testUnit[i].BelongedId +
                    "&ifBorrow=1"+
                    "&borrowInfo=1@2@3@4" +
                    "&isGroupInfoDefault=1"+
                    "&groupInfoDefault=1@2@3"+
                    "&dataBufferLength=100"+
                    "&englishName=" + testUnit[i].EnTestUnitName +
                    "&diffMode=" + testUnit[i].DiffMode + 
                    "&labCode="+sysInfo.LabCode+
                    "&testType=1";

                string tuRus = PostData(tuUrl, tuData);
                //MessageBox.Show("监测信息配置：\r\n" + i + " : " + tuRus);
                string rus = tuRus.Substring(tuRus.IndexOf(":") + 1, 3);

                if (!rus.Equals("200"))
                {
                    flag = 0;
                    MessageBox.Show("第：" + i.ToString() + "条监测单元信息配置失败，失败编码： " + rus + "\n");
                }
                
            }
            if (flag == 1)
                MessageBox.Show("监测单元信息配置成功\n");
        }

        /// <summary>
        /// 窗口配置
        /// 表中有主键，testUnitNo与windowNo不能全一样
        /// 对应WindowConfig表
        /// </summary>
        public void windowInfoMsgInit()
        {
            window[0] = new Window();
            window[0].WindowNo = 1;
            window[0].WindowName = "主窗口1";
            window[0].UpperLimit = 500;
            window[0].LowerLimit = 0;


            for (int i = 0; i < window.Length; i++)
            {
                if (i != 0)
                    window[i] = new Window();
                window[i].WindowNo = window[0].WindowNo+i;
                window[i].WindowName = "主窗口1";
                window[i].UpperLimit = 500;
                window[i].LowerLimit = 0;
            }

            int flag = 1;

            for (int i = 0; i < window.Length; i++)
            {
                string windowUrl = "http://115.28.236.114/RestInterfaceSystem/windowConfigController/addWindowConfig";

                string windowDate = "windowNo=" + window[0].WindowNo +
                    "&testUnitNo=" + testUnit[i].TestUnitNo +
                    "&windowName=" + window[i].WindowName +
                    "&visible=1"+
                    "&upperLimit=" + window[i].UpperLimit +
                    "&lowerLimit=" + window[i].LowerLimit + 
                    "&labCode="+sysInfo.LabCode;
                
                //resultTextBox.Text += tuData + "\n";

                string windowRus = PostData(windowUrl, windowDate);
                //Thread.Sleep(1500);
                string rus = windowRus.Substring(windowRus.IndexOf(":") + 1, 3);
                if (!rus.Equals("200"))
                {
                    flag = 0;
                    MessageBox.Show("第：" + i.ToString() + "条窗口信息配置失败，失败编码： " + rus + "\n");
                }
                
            }
            if (flag == 1)
                MessageBox.Show("窗口信息配置成功\n");
        }

        /// <summary>
        /// 定义子窗口变量
        /// 其中SubWindowInfo[1]数据未知，在这里自己定义，防止主键冲突
        /// 对应SubWindowConfig表
        /// </summary>
        public void subWindowInfoMsgInit()
        {
            //testUnit[0].TestUnitNo = 1;

            for (int i = 0; i < testUnit.Length; i++)
            {
                testUnit[i].SubWindowInfo = new SubWindow[1];

                testUnit[i].SubWindowInfo[0] = new SubWindow();
                testUnit[i].SubWindowInfo[0].SubWindowNo = 1;
                testUnit[i].SubWindowInfo[0].Name = "温度";
                testUnit[i].SubWindowInfo[0].Proportion = 2;
                testUnit[i].SubWindowInfo[0].WindowNo = 1;

            }

            int flag = 1;

            for (int i = 0; i < testUnit.Length; i++)
            {
                for (int j = 0; j < testUnit[i].SubWindowInfo.Length; j++)
                {
                    string subWindowUrl = "http://115.28.236.114/RestInterfaceSystem/subWindowConfigController/addSubWindowConfig";

                    string subWindowData = "subWindowNo=" + testUnit[i].SubWindowInfo[j].SubWindowNo +
                        "&testUnitNo=" + testUnit[i].TestUnitNo +
                        "&name=" + testUnit[i].SubWindowInfo[j].Name +
                        "&visible=1&labCode="+ sysInfo.LabCode +
                        "&proportion=" + testUnit[i].SubWindowInfo[j].Proportion +
                        "&windowNo=" + testUnit[i].SubWindowInfo[j].WindowNo;
                    
                    string subWindowRus =PostData(subWindowUrl, subWindowData);

                    string rus = subWindowRus.Substring(subWindowRus.IndexOf(":") + 1, 3);
                    if (!rus.Equals("200"))
                    {
                        flag = 0;
                        MessageBox.Show("第：" + i.ToString() + " - " + j.ToString() + "条子窗口信息配置失败，失败编码： " + rus + "\n");
                    }
                    
                }
            }
            if (flag == 1)
                MessageBox.Show("子窗口信息配置成功\n");
        }

        /// <summary>
        /// 坐标系配置
        /// 对应CoordinateConfig表
        /// </summary>
        public void coordinateConfigInfoMsgInit()
        {
            string[] tName = { "温度", "频率", "耗电量", "电压", "电流", "功率" };

            int[] highValue = { 40, 200, 30, 400, 30, 300 };

            int[] lowValue = { -40, 0, 0, 0, 0, 0 };

            int[] sensorType = { 1, 4, 7, 3, 5, 6 };

            string[] tEnName = { "Temperature", "Frequency", "Power Consumption", "Voltage", "Current", "Power" };

            for (int i = 0; i < testUnit.Length; i++)
            {
                testUnit[i].CoordinateInfo = new Coordinate[6];

                for (int j = 0; j < testUnit[i].CoordinateInfo.Length; j++)
                {
                    testUnit[i].CoordinateInfo[j] = new Coordinate();

                    testUnit[i].CoordinateInfo[j].CoordinateNo = j+1 ;
                    testUnit[i].CoordinateInfo[j].Name = tName[j];
                    testUnit[i].CoordinateInfo[j].SubWindowNo = 1;
                    testUnit[i].CoordinateInfo[j].HighValue = highValue[j];
                    testUnit[i].CoordinateInfo[j].LowValue = lowValue[j];
                    testUnit[i].CoordinateInfo[j].SensorType = sensorType[j];
                    testUnit[i].CoordinateInfo[j].EnName = tEnName[j];
                }
            }

            int flag = 1;

            for (int i = 0; i < testUnit.Length; i++)
            {
                for (int j = 0; j < testUnit[i].CoordinateInfo.Length; j++)
                {
                    string coordinateConfigUrl = "http://115.28.236.114/RestInterfaceSystem/coordinateConfigController/addCoordinateConfig";

                    string coordinateConfigData = "testUnitNo=" + testUnit[i].TestUnitNo +
                        "&coordinateNo=" + testUnit[i].CoordinateInfo[j].CoordinateNo +
                        "&name=" + testUnit[i].CoordinateInfo[j].Name +
                        "&subWindowNo=" + testUnit[i].CoordinateInfo[j].SubWindowNo +
                        "&highValue=" + testUnit[i].CoordinateInfo[j].HighValue +
                        "&lowValue=" + testUnit[i].CoordinateInfo[j].LowValue +
                        "&visible=1&sensorType=" + (testUnit[i].CoordinateInfo[j].SensorType ) +
                        "&englishName=" + testUnit[i].CoordinateInfo[j].EnName +
                        "&labCode=" + sysInfo.LabCode+
                        "&versionNo=3.1.0";

                    string coordinateConfigRus =PostData(coordinateConfigUrl, coordinateConfigData);

                    string rus = coordinateConfigRus.Substring(coordinateConfigRus.IndexOf(":") + 1, 3);
                    if (!rus.Equals("200"))
                    {
                        flag = 0;
                        MessageBox.Show("第：" + i.ToString() + " - " + j.ToString() + "坐标系信息配置失败，失败编码： " + rus + "\n");
                    }
                    
                }
            }
            if (flag == 1)
                MessageBox.Show("坐标系信息配置成功\n");

        }

        /// <summary>
        /// 传感器类型配置
        /// 对应SensorType表
        /// </summary>
        public void sensorTypeInfoMsgInit()
        {
            for (int i = 0; i < sensor.Length; i++)
            {
                sensor[i] = new Sensor();
                if (i < 12)
                {
                    sensor[i].TypeID = 1;
                    sensor[i].TypeName = "温度";
                    sensor[i].Unit = "℃";
                }
                else if (i >= 12 && i < 13)
                {
                    sensor[i].TypeID = 4;
                    sensor[i].TypeName = "频率";
                    sensor[i].Unit = "Hz";
                }
                else if (i >= 13 && i < 14)
                {
                    sensor[i].TypeID = 3;
                    sensor[i].TypeName = "电压";
                    sensor[i].Unit = "V";
                }
                else if (i >= 14 && i < 15)
                {
                    sensor[i].TypeID = 5;
                    sensor[i].TypeName = "电流";
                    sensor[i].Unit = "A";
                }
                else if (i >= 15 && i < 16)
                {
                    sensor[i].TypeID = 6;
                    sensor[i].TypeName = "功率";
                    sensor[i].Unit = "W";
                }
                else if (i >= 16 && i < 17)
                {
                    sensor[i].TypeID = 7;
                    sensor[i].TypeName = "耗电量";
                    sensor[i].Unit = "kW·h";
                }
            }

            for (int i = 0; i < sensor.Length; i++)
            {
                sensorLimit[i] = new SensorLimit();
                if (i < 12)
                {
                    sensorLimit[i].UpLimit = 200;
                    sensorLimit[i].LowLimit = -100;
                }
                else if (i >= 12 && i < 13)
                {
                    sensorLimit[i].UpLimit = 200;
                    sensorLimit[i].LowLimit = 0;
                }
                else if (i >= 13 && i < 14)
                {
                    sensorLimit[i].UpLimit = 500;
                    sensorLimit[i].LowLimit = 0;
                }
                else if (i >= 14 && i < 15)
                {
                    sensorLimit[i].UpLimit = 100;
                    sensorLimit[i].LowLimit = 0;
                }
                else if (i >= 15 && i < 16)
                {
                    sensorLimit[i].UpLimit = 5000;
                    sensorLimit[i].LowLimit = 0;
                }
                else if (i >= 16 && i < 17)
                {
                    sensorLimit[i].UpLimit = 200;
                    sensorLimit[i].LowLimit = 0;
                }
            }

            int flag = 1;

            for (int i = 0; i < sensor.Length; i++)
            {
                string sensorTypeUrl = "http://115.28.236.114/RestInterfaceSystem/sensorTypeController/addSensorType";
                string sensorTypeData = "sensorTypeId=" + sensor[i].TypeID +
                    "&labCode="+sysInfo.LabCode+
                    "&sensorTypeName=" + sensor[i].TypeName +
                    "&unit=" + sensor[i].Unit +
                    "&upLimit=" + sensorLimit[i].UpLimit +
                    "&lowLimit=" + sensorLimit[i].LowLimit;

                string sensorTypeRus =PostData(sensorTypeUrl, sensorTypeData);

                string rus = sensorTypeRus.Substring(sensorTypeRus.IndexOf(":") + 1, 3);

                if (!rus.Equals("200"))
                {
                    flag = 0;
                    MessageBox.Show("第：" + i.ToString() + "条传感器类型配置失败，失败编码： " + rus + "\n");
                }
                
            }

            if (flag == 1)
                MessageBox.Show("传感器类型配置成功\n");
        }

        /// <summary>
        /// 传感器配置
        /// 对应SensorConfig表
        /// </summary>
        public void sensorInfoMsgInit()
        {
            string[] tName = {"温度1", "温度2", "温度3", "温度4", "温度5", "温度6", "温度7", "温度8", "温度9", "温度10", "干温",
                "湿温", "频率", "电压", "电流", "功率", "耗电量"};

            string[] tEnName = {"Temperature1", "Temperature2", "Temperature3", "Temperature4", "Temperature5", "Temperature6",
                "Temperature7", "Temperature8", "Temperature9", "Temperature10", "Dry Temperature", "Wet Temperature",
                "Frequency", "Voltage", "Current", "Power", "Power Consumption"};

            int[] tSequenceNo = { 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 0, 2, 366, 363, 364, 365, 367 };

            int[] coordinateNo = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 4, 5, 6, 7 };

            int[] groupNo = { 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 1, 1, 5, 5, 5, 5, 5 };

            sensor[0] = new Sensor();
            sensor[0].SensorNo = 1;
            sensor[0].Name = "";
            sensor[0].TotalSequenceNo = 7;
            sensor[0].CoordinateNo = 1;
            sensor[0].GroupNo = 1;
            sensor[0].EnName = "";
            sensor[0].DotNum = "0.0";

            for (int i = 0; i < sensor.Length; i++)
            {
                if (i != 0)
                    sensor[i] = new Sensor();
                sensor[i].SensorNo = i + 1;
                sensor[i].Name = tName[i];
                sensor[i].TotalSequenceNo = tSequenceNo[i];
                sensor[i].CoordinateNo = coordinateNo[i];
                sensor[i].GroupNo = groupNo[i];
                sensor[i].EnName = tEnName[i];
                sensor[i].DotNum = "0.0";
            }

            int flag = 1;

            for (int i = 0; i < testUnit.Length; i++)
            {
                for (int j = 0; j < sensor.Length; j++)
                {
                    string sersorUrl = "http://115.28.236.114/RestInterfaceSystem/sensorConfigController/addSensorConfig";
                    string sensorData = "testUnitNo=" + testUnit[i].TestUnitNo +
                        "&sensorNo=" + sensor[j].SensorNo +
                        "&name=" + sensor[j].Name +
                        "&englishName=" + sensor[j].EnName +
                        "&totalSequenceNo=" + sensor[j].TotalSequenceNo +
                        "&coordinateNo=" + sensor[j].CoordinateNo +
                        "&selected=1"+
                        "&visible=1"+
                        "&dotNum=" + sensor[j].DotNum +
                        "&groupNo=" + sensor[j].GroupNo +
                        "&maxSelect=1"+
                        "&minSelect=1"+
                        "&averageSelect=1"+
                        "&integralAvSelect=1"+
                        "&diffSelect=0"+
                        "&isVirtual=1"+
                        "&state=1"+
                        "&commonSelect=0"+
                        "&commonName=" + sensor[j].EnName +
                        "&englishCommonName=" + sensor[j].EnName +
                        "&labCode="+sysInfo.LabCode+
                        "&versionNo=2.1.0"+
                        "&coordinateNoStr=" + sensor[j].CoordinateNo +
                        "&selectedStr=1"+
                        "&visibleStr=1"+
                        "&assemblyName=func"+
                        "&functionName=funcName"+
                        "&functionParas="+
                        "&functionClass=functionClass"+
                        "&keyParaMeter=1";

                    string sensorRus = PostData(sersorUrl, sensorData);

                    string rus = sensorRus.Substring(sensorRus.IndexOf(":") + 1, 3);
                    if (!rus.Equals("200"))
                    {
                        flag = 0;
                        MessageBox.Show("第：" + i.ToString() + " - " + j.ToString() + "传感器配置失败，失败编码： " + rus + "\n");
                    }
                    
                }
            }

            if (flag == 1)
                MessageBox.Show("传感器配置成功\n");
        }
        
        /// <summary>
        /// 传感器名称配置
        /// 对应SensorName表
        /// </summary>
        public void sensorNameMsgInit()
        {
            ///只示范加入两个传感器名称数据，
            sensorName = new SensorName[2];
            sensorName[0] = new SensorName();
            sensorName[0].Id = 1;                //传感器名称编号
            sensorName[0].Name = "干燥过滤器";   //传感器名称
            sensorName[0].EnName = "Drying filter";

            sensorName[1] = new SensorName();
            sensorName[1].Id = 2;                //传感器名称编号
            sensorName[1].Name = "温度";   //传感器名称
            sensorName[1].EnName = "Tenperature";

            int flag = 1;
            //添加数据到传感器名称表
            for (int i = 0; i < sensorName.Length;i++)
            {
                string sersorNameUrl = "http://115.28.236.114/RestInterfaceSystem/sensorNameController/addSensorName";
                string sensorNameData = "labCode=" +sysInfo.LabCode +
                    "&name=" + sensorName[i].Name +
                    "&id=" + sensorName[i].Id +
                    "&englishName=" + sensorName[i].EnName;

                string sensorNameRus = PostData(sersorNameUrl, sensorNameData);
                string rus = sensorNameRus.Substring(sensorNameRus.IndexOf(":") + 1, 3);
                if (!rus.Equals("200"))
                {
                    flag = 0;
                    MessageBox.Show("第：" + i.ToString() + "个传感器名称配置失败，失败编码： " + rus + "\n");
                }
            }
            if (flag == 1)
                MessageBox.Show("传感器名称配置成功\n");
        }

        /// <summary>
        /// 组信息配置
        /// 对应groupConfig表
        /// </summary>
        public void groupInfoMsgInit()
        {
            ///只示范加入两个组信息配置数据，
            group = new Group[4];
            group[0] = new Group();
            group[0].GroupNo = 1;
            group[0].Name = "环温";
            group[0].EnName = "Enviroment Temperature";

            group[1] = new Group();
            group[1].GroupNo = 2; 
            group[1].Name = "冷藏";
            group[1].EnName = "Refrigeration";

            group[2] = new Group();
            group[2].GroupNo = 3;
            group[2].Name = "冷冻";
            group[2].EnName = "Freeze";

            group[3] = new Group();
            group[3].GroupNo = 5;
            group[3].Name = "电参数";
            group[3].EnName = "Environment Temperature";
            int flag = 1;
            for (int i = 0; i < group.Length; i++)
            {
                string groupUrl = "http://115.28.236.114/RestInterfaceSystem/groupConfigController/addGroupConfig";
                string groupData = "groupNo="+group[i].GroupNo+
                    "&name="+group[i].Name+
                    "&maxSelect=1"+
                    "&minSelect=1"+
                    "&integerAveSelect=1"+
                    "&averageSelect=1"+
                    "&deletable=0"+
                    "&visible=1"+
                    "&sensorType=1"+   //组相对的传感器类型，1是传感器类型为温度
                    "&englishName=" + group[i].EnName+
                    "&labCode="+ sysInfo.LabCode +
                    "&lowTrmUpLimit=1"+
                    "&lowTrmDownLimit=1"+
                    "&lowTriUpLimit=1"+
                    "&lowTriDownLimit=1"+
                    "&highTrmUpLimit=1"+
                    "&highTrmDownLimit=1"+
                    "&highTriUpLimit=1"+
                    "&highTriDownLimit=1";

                string groupRus = PostData(groupUrl, groupData);
                string rus = groupRus.Substring(groupRus.IndexOf(":") + 1, 3);
                if (!rus.Equals("200"))
                {
                    flag = 0;
                    MessageBox.Show("第：" + i.ToString() + "个组信息配置失败，失败编码： " + rus + "\n");
                }
            }
            if (flag == 1)
                MessageBox.Show("组信息配置成功\n");
            }

        /// <summary>
        /// 录入条目信息
        /// 对应testProdInfoItem表
        /// </summary>
        public void prodInfoItemMsgInit()
        {
            ///只示范加入两个录入条目信息配置数据，
            prodInfoItem = new ProdInfoItem[5];

            prodInfoItem[0] = new ProdInfoItem();
            prodInfoItem[0].itemno = importantId;
            prodInfoItem[0].itemname = "试品编号";
            prodInfoItem[0].enitemname = "Product Name";
            prodInfoItem[0].itemtype = 1;
            prodInfoItem[0].inputmode = 1;

            prodInfoItem[1] = new ProdInfoItem();
            prodInfoItem[1].itemno = importantId+1;
            prodInfoItem[1].itemname = "产品型号";
            prodInfoItem[1].enitemname = "Product Model";
            prodInfoItem[1].itemtype = 1;
            prodInfoItem[1].inputmode = 1;

            prodInfoItem[2] = new ProdInfoItem();
            prodInfoItem[2].itemno = importantId + 2;
            prodInfoItem[2].itemname = "测试项目";
            prodInfoItem[2].enitemname = "Test Items";
            prodInfoItem[2].itemtype = 1;
            prodInfoItem[2].inputmode = 2;

            prodInfoItem[3] = new ProdInfoItem();
            prodInfoItem[3].itemno = importantId +3;
            prodInfoItem[3].itemname = "测试时间";
            prodInfoItem[3].enitemname = "Test Time";
            prodInfoItem[3].itemtype = 1;
            prodInfoItem[3].inputmode = 4;


            prodInfoItem[4] = new ProdInfoItem();
            prodInfoItem[4].itemno = importantId + 4;
            prodInfoItem[4].itemname = "测试人";
            prodInfoItem[4].enitemname = "Test Person";
            prodInfoItem[4].itemtype = 1;
            prodInfoItem[4].inputmode = 1;


            int flag = 1;
            for (int i = 0; i < prodInfoItem.Length; i++)
            {
                string prodInfoUrl = "http://115.28.236.114/RestInterfaceSystem/testProdInfoItemController/addTestProdInfoItem";
                string proInfoData =
                    "&itemNo=" + prodInfoItem[i].itemno +
                    "&itemName="+ prodInfoItem[i].itemname+
                    "&defaultContent=/"+
                    "&inputMode="+ prodInfoItem[i].inputmode+
                    "&queryCondition=1" +
                    "&selectItem=耗电量测试@冷却速度测试@/"+
                    "&print=1"+
                    "&display=1"+
                    "&changeable=1"+
                    "&englishName="+ prodInfoItem[i].enitemname +
                    "&englishSelectItem=21321"+
                    "&statusBar=1"+
                    "&englishDefaultContent=dasdas"+
                    "&itemType=1"+
                    "&versionNo=1.1.0"+
                    "&keyItem=dsada"+
                    "&labCode="+sysInfo.LabCode;

                string prodRus = PostData(prodInfoUrl, proInfoData);
                string rus = prodRus.Substring(prodRus.IndexOf(":") + 1, 3);
                if (!rus.Equals("200"))
                {
                    flag = 0;
                    MessageBox.Show("第：" + i.ToString() + "个录入条目信息配置失败，失败编码： " + rus + "\n");
                }
            }
            if (flag == 1)
                MessageBox.Show("录入条目信息配置成功\n");
        }

        /// <summary>
        /// 绘制曲线函数
        /// </summary>
        public void initCurveControl()
        {
            //初始化控制类型，是主控还是远控
            DataComponent.InitApplicationStartPath(Application.StartupPath);
            DataComponent.GetControlType();
            
            //曲线初始化
            int rtn = dataComponent.InitCurve();
            
            //数据组件初始化
            rtn = dataComponent.Init();
           
            //获取正在测试的监测单元
            
            testingTestUnit = dataComponent.GetAllTestUnit();
            //testingTestUnit = dataComponent.GetAllTestingUnit();

            //定义子窗口数、坐标系数、传感器数、组数
            int subWindowNum, coordinateNum, sensorNum, groupNum;
            int i, j;
            subWindowNum = testingTestUnit[0].SubWindowInfo.Length;
            coordinateNum = testingTestUnit[0].CoordinateInfo.Length;
            sensorNum = testingTestUnit[0].SensorInfo.Length + testingTestUnit[0].AverageValueList.Count;
            groupNum = testingTestUnit[0].GroupInfo.Length;
            

            //定义一个曲线组件变量
            CurveControl curveControl = new CurveControl();

            //整个曲线显示组件的窗口初始化
            curveControl.IniParentWindow(subWindowNum, coordinateNum, sensorNum, groupNum);

            //初始化子窗口接口
            for (int k1 = 0; k1 < testingTestUnit[0].SubWindowInfo.Length; k1++)
            {
                curveControl.IniSubWindow(testingTestUnit[0].SubWindowInfo[k1].SubWindowNo,
                    testingTestUnit[0].SubWindowInfo[k1].Name, testingTestUnit[0].SubWindowInfo[k1].Proportion, testingTestUnit[0].SubWindowInfo[k1].Visible);
            }

            //初始化坐标系
            List<int> initAllCoor = new List<int>();
            for (j = 0; j < coordinateNum; j++)
            {
                if (testingTestUnit[0].CoordinateInfo[j].SubWindowNo == subWindowNo)
                {
                    curveControl.InitCoordinate(testingTestUnit[0].CoordinateInfo[j].CoordinateNo,
                                                testingTestUnit[0].CoordinateInfo[j].Name,
                                                1,
                                                testingTestUnit[0].CoordinateInfo[j].Visible,
                                                testingTestUnit[0].CoordinateInfo[j].HighValue,
                                                testingTestUnit[0].CoordinateInfo[j].LowValue,
                                                testingTestUnit[0].CoordinateInfo[j].UpLimit,
                                                testingTestUnit[0].CoordinateInfo[j].LowLimit,
                                                testingTestUnit[0].CoordinateInfo[j].Unit);
                    initAllCoor.Add(testingTestUnit[0].CoordinateInfo[j].CoordinateNo);
                }
            }
            
            //初始化组信息
            for (i = 0; i < testingTestUnit[0].GroupInfo.Count(); i++)
            {
                Group oneGroup = new Group();
                oneGroup = testingTestUnit[0].GroupInfo[i];
                Boolean b = curveControl.SetGroupSelect(oneGroup.GroupNo, true);
                curveControl.SetGroupName(oneGroup.GroupNo, oneGroup.Name);

                curveControl.SetGroupStatisticSelect(oneGroup.GroupNo, 
                                                     oneGroup.MaxSelect, 
                                                     oneGroup.MinSelect, 
                                                     oneGroup.AverageSelect, 
                                                     oneGroup.IntegeraveSelect);
            }

            //确定传感器是否属于在此窗口内的坐标系
            int copyCoordinate = 0;
            //初始化传感器信息
            for (i = 0; i < testingTestUnit[0].SensorInfo.Length; i++)
            {
                if (testingTestUnit[0].SensorInfo[i].BeBorrowed == false && testingTestUnit[0].SensorInfo[i].Selected == true)
                {
                    for (int z1 = 0; z1 < testingTestUnit[0].SensorInfo[i].CoordinateBelong.CoordinateNo.Length; z1++)
                    {
                        if (initAllCoor.Contains(testingTestUnit[0].SensorInfo[i].CoordinateBelong.CoordinateNo[z1]))
                        {
                            copyCoordinate = testingTestUnit[0].SensorInfo[i].CoordinateBelong.CoordinateNo[z1];
                            break;
                        }
                    }

                   curveControl.IniSensorAttri(testingTestUnit[0].SensorInfo[i].SensorNo, 
                                               testingTestUnit[0].SensorInfo[i].Name, 
                                               copyCoordinate, 
                                               testingTestUnit[0].SensorInfo[i].Visible, 
                                               testingTestUnit[0].SensorInfo[i].Selected, 
                                               testingTestUnit[0].SensorInfo[i].TotalSequenceNo, 
                                               testingTestUnit[0].SensorInfo[i].DotNum);

                        //传感器颜色
                    curveControl.SetSensorColorNo(testingTestUnit[0].SensorInfo[i].SensorNo, 
                                                  testingTestUnit[0].SensorInfo[i].ColorNo);

                        //设置传感器组号
                    curveControl.SetSensorGroupNo(testingTestUnit[0].SensorInfo[i].SensorNo, 
                                                  testingTestUnit[0].SensorInfo[i].GroupNo);
                }
            }

            //添加传感器采集时间
            List<float> DotHowLong = new List<float>();
            float startLong = 0;
            for(i=0;i<500;i++){
                startLong = (float)(i * 0.006);
                DotHowLong.Add(startLong);
            }
            curveControl.ReceiveDotHowLong(ref DotHowLong);

            //添加传感器采集数值
            List<float> DotValue = new List<float>();
            List<float> DotValue2 = new List<float>();
            List<float> DotValue3 = new List<float>();
            List<float> DotValue4 = new List<float>();
            for (i = 0; i < 500; i++)
            {
                DotValue.Add((float)(0 + i * 0.05));
                DotValue2.Add((float)(30 - i * 0.05));
                DotValue3.Add((float)(50 - i * 0.5));
                DotValue4.Add((float)(35 - i * 0.01));
            }

            curveControl.ReceiveValue(1, ref DotValue);
            curveControl.ReceiveValue(2, ref DotValue);
            curveControl.ReceiveValue(3, ref DotValue2);
            curveControl.ReceiveValue(4, ref DotValue3);
            curveControl.ReceiveValue(15, ref DotValue4);

            curveControl.EnableScroll(false,this.Height);
            //语言
            curveControl.ChangeLanguage(CurveControl.LanguageType.Chinese);

            //设置控件内的布局
            curveControl.RefreshLayout();
            //画出曲线
            curveControl.DrawPicCurve();
            curveControl.IsFirstListView = false;
            curvePanel.Controls.Add(curveControl);
            //this.Controls.Add(curveControl);
            curveControl.Dock = DockStyle.Fill;
        }

        private void systemInfoBtn_Click(object sender, EventArgs e)
        {
            initAllInfo();
        }

        private void showCurve_Click(object sender, EventArgs e)
        {
            initCurveControl();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public string PostData(string url, string data)
        {
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.DefaultConnectionLimit = 200;
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
            //request.ContentType = "application/json;charset=UTF-8";
            request.Timeout = 10000;

            UTF8Encoding encoding = new UTF8Encoding();
            byte[] bys = encoding.GetBytes(data); // z
            request.ContentLength = bys.Length; // Length 或者 LongLength 有疑问

            try
            {
                ///获得请求流
                Stream newStream = request.GetRequestStream();
                newStream.Write(bys, 0, bys.Length);
                newStream.Close();

                ///获得响应流
                ///下3行代码按知道修改，查到一般不直接使用WebResponse
                HttpWebResponse webresp = request.GetResponse() as HttpWebResponse;
                Stream responseStream = webresp.GetResponseStream();
                StreamReader sr = new StreamReader(responseStream);

                string str1 = sr.ReadToEnd();
                webresp.Close();
                sr.Close();
                return str1;
            }
            catch (WebException ex)
            {
                return ex.Message;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                //dataCommonConfigFileName
                sysInfo.LabCode = textBox1.Text;
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(dataCommonConfigFileName);
                XmlNode configNode = xmlDoc.SelectSingleNode("config");
                XmlNode labCodeNode = configNode.SelectSingleNode("labcode");
                string labCode = labCodeNode.InnerText;
                if (sysInfo.LabCode!=labCode)
                {
                    labCodeNode.InnerText = sysInfo.LabCode;
                    MessageBox.Show("检测到xml配置文件中labcode和系统配置的LabCode不一致，正在进行修改");
                }
                try
                {
                    xmlDoc.Save(dataCommonConfigFileName);
                    MessageBox.Show("修改成功,完成同步");
                }
                catch (Exception ee)
                {
                    MessageBox.Show("修改失败\n" + ee.Message + "\n");
                    //throw;
                }
                MessageBox.Show("修改成功");

            }
        }


        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }
    }
}
