using DataComm;
using NameplateManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {


        public NameplateManagement.NameplateManagement nameplateCoponent = new NameplateManagement.NameplateManagement();// 录入组件变量
        public DataComponent dataComponent;
        public ControlTypeEnum controlType;
        public DataComm.TestUnit[] testUnit;
        public ProdInfoItem[] prodInfoItem;
        public ProdInfoItem oneProdInfoItem;
        public DataComm.TestUnit oneTestUnit;

        public Form1()
        {
            //InitializeComponent();
            //NameplateCoponent = new NameplateManagement.NameplateManagement();
            //datamangeComponent = new DataComponent();
            //testUnit = new TestUnit[10];
            //prodInfoItem = new ProdInfoItem[10];
            InitializeComponent();
            dataComponent = new DataComponent();
            oneProdInfoItem = new ProdInfoItem();
            oneTestUnit = new DataComm.TestUnit();
        }

        public void loaddata()
        {
            //初始化控制类型，是主控还是远控
            DataComponent.InitApplicationStartPath(Application.StartupPath);//初始化应用程序根目录 
            if (DataComponent.GetControlType() == ControlTypeEnum.RemoteControl) //判断主控还是远控
            {
                controlType = ControlTypeEnum.RemoteControl; //远控赋值
            }
            else
            {
                controlType = ControlTypeEnum.MainControl; //主控赋值
            }
            //数据组件初始化
            int rtn = dataComponent.Init();
            //用于遍历

            //获得所有台位
            testUnit = dataComponent.GetAllTestUnit();
            //获得录入条目
            prodInfoItem = dataComponent.GetProdInfoItem();
            //设置路径
            nameplateCoponent.setstartpath(Application.StartupPath);
            //遍历条目信息
            for (int i = 0; i < prodInfoItem.Length; i++)
            {
                //将条目信息赋值
                oneProdInfoItem = prodInfoItem[i];
                nameplateCoponent.setItemInfo(oneProdInfoItem.itemno, oneProdInfoItem.itemname, oneProdInfoItem.defaultcontent, oneProdInfoItem.inputmode, oneProdInfoItem.selectitem, oneProdInfoItem.changeable, oneProdInfoItem.datalength, oneProdInfoItem.itemtype, oneProdInfoItem.enitemname);
            }
            //  Console.WriteLine(prodInfoItem.Length);
            //遍历所有台位
            for (int j = 0; j < testUnit.Length; j++)
            {
                //台位赋值
                oneTestUnit = testUnit[j];
                nameplateCoponent.setMetaData(oneTestUnit.PrimaryKey, oneTestUnit.BelongedId, oneTestUnit.TestUnitNo, oneTestUnit.TestUnitName, oneTestUnit.TestNow, oneTestUnit.BeginDateTime, oneTestUnit.EndDateTime, ref oneTestUnit.ItemContent, oneTestUnit.ProjectMissionBookId);
            }
            //软件中文名称赋值
            nameplateCoponent.labname = dataComponent.GetSystemInfo().LabName;
            //软件英文名称赋值
            nameplateCoponent.EnlabName = dataComponent.GetSystemInfo().EnLabName;
            //将台位统称和labcode传到录入组件
            nameplateCoponent.SetTestUnitPrefix(dataComponent.GetSystemInfo().TestUnitNameConfig, dataComponent.GetSystemInfo().LabCode);
            //设置行列数
            nameplateCoponent.SetRowAndColumNum(2, 2);
            //控件填充方式，充满窗体
            nameplateCoponent.Dock = DockStyle.Fill;
            //当点击开测触发的事件
            nameplateCoponent.Open_Test += NameplateCoponent_Open_Test;
            //当点击停测触发的事件
            nameplateCoponent.Stop_Test += NameplateCoponent_Stop_Test;
            //当点击保存时触发的事件
            nameplateCoponent.Nameplate_Manage += NameplateCoponent_Nameplate_Manage;
            // 窗体添加上录入组件
            this.Controls.Add(nameplateCoponent);
        }

        private void NameplateCoponent_Nameplate_Manage(MetaData metadata, int testunitid, string labcode)
        {
            dataComponent.ModifyTestInfo(testunitid, metadata.itemcontent);
        }

        private void NameplateCoponent_Stop_Test(MetaData metadata, int testunitid, string labcode)
        {

            //遍历所有台位
            for (int i = 0; i < testUnit.Length - 1; i++)
            {
                if (testUnit[i].TestUnitNo == testunitid) // 是否要停测的台位
                {
                    //将时间+台位标号在数据库中更新
                    dataComponent.UpdateTestUnitReportTime(testUnit[i].PrimaryKey, testUnit[i].ReportTime);
                }
            }
            //停测该台位，数据存库
            dataComponent.StopTest(testunitid, true);
        }

        private void NameplateCoponent_Open_Test(MetaData metadata, int testunitid, string labcode)
        {
            int StartResult = dataComponent.StartTest(testunitid, metadata.itemcontent);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loaddata();
            //DataComponent.InitApplicationStartPath(Application.StartupPath);
            //// if (DataComponent.GetControlType() == ControlTypeEnum.RemoteControl) //判断主控还是远控
            //controlType = ControlTypeEnum.RemoteControl;//远控赋值
            //                                            // else
            //                                            // controlType = ControlTypeEnum.MainControl;//主控赋值
            ////DataComm.DataComponent.InitApplicationStartPath(Application.StartupPath);//初始化应用程序根目录 
            //DataComponent.InitApplicationStartPath(Application.StartupPath);
            //DataComponent.GetControlType();
            //int rtn = datamangeComponent.Init();
            //int i, j;
            //testUnit = datamangeComponent.GetAllTestUnit();//获得所有台位
            //prodInfoItem = datamangeComponent.GetProdInfoItem();//获得录入条目
            //NameplateCoponent.setstartpath(Application.StartupPath);//设置路径

            //for (i = 0; i < prodInfoItem.Length; i++)//遍历条目信息
            //{
            //    oneProdInfoItem = prodInfoItem[i];//将条目信息赋值
            //    NameplateCoponent.setItemInfo(oneProdInfoItem.itemno, oneProdInfoItem.itemname,
            //                                  oneProdInfoItem.defaultcontent, oneProdInfoItem.inputmode,
            //                                  oneProdInfoItem.selectitem, oneProdInfoItem.changeable,
            //                                  oneProdInfoItem.datalength, oneProdInfoItem.itemtype,
            //                                  oneProdInfoItem.enitemname);
            //}

            //for (j = 0; j < testUnit.Length; j++)//遍历所有台位
            //{
            //    onetestUnit = testUnit[j];//台位赋值
            //    NameplateCoponent.setMetaData(onetestUnit.PrimaryKey, onetestUnit.BelongedId,
            //                                  onetestUnit.TestUnitNo, onetestUnit.TestUnitName,
            //                                  onetestUnit.TestNow, onetestUnit.BeginDateTime,
            //                                  onetestUnit.EndDateTime, ref onetestUnit.ItemContent,
            //                                  onetestUnit.ProjectMissionBookId);
            //}

            //NameplateCoponent.labname = datamangeComponent.GetSystemInfo().LabName;//软件中文名称赋值
            //NameplateCoponent.EnlabName = datamangeComponent.GetSystemInfo().EnLabName;//软件英文名称赋值
            //NameplateCoponent.SetTestUnitPrefix(datamangeComponent.GetSystemInfo().TestUnitNameConfig,
            //                                     datamangeComponent.GetSystemInfo().LabCode);//将台位统称和labcode传到录入组件
            //NameplateCoponent.SetRowAndColumNum(2, 2);//设置行列数

            ////AddHandler NameplateCoponent.Open_Test, AddressOf NameplateCoponent_Open_Test//当点击开测触发的事件
            ////AddHandler NameplateCoponent.Stop_Test, AddFressOf NameplateCoponent_Stop_Test//当点击停测触发的事件
            ////AddHandler NameplateCoponent.Nameplate_Manage, AddressOf NameplateCoponent_Nameplate_Manage//当点击保存时触发的事件

            //NameplateCoponent.Dock = DockStyle.Fill;//控件填充方式，充满窗体
            //panel1.Controls.Add(NameplateCoponent);//窗体添加上录入组件
            
          
        }

        //private void panel1_Paint(object sender, PaintEventArgs e)
        //{
        //    Panel.Controls.Add(NameplateCoponent);//窗体添加上录入组件
        //}
    }
}
