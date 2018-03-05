using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace CoreLibrary
{
    public class XMLHelper
    {
        public static string getValue(string name) {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("han.xml");
            XmlNodeList nodeList = xmlDoc.SelectSingleNode("han").ChildNodes;//获取bookstore节点的所有子节点
            foreach (XmlNode xn in nodeList)//遍历根节点的所有子节点
            {
                XmlElement xe = (XmlElement)xn;//将子节点类型转换为XmlElement类型
                if (xe.GetAttribute("name") == name)
                {
                    return xe.InnerText.Trim();
                }
            }
            return "";
        }
        //<?xml version="1.0" encoding="gb2312"?>
        //<han>
        //  <hong name="QCH">Data Source=.;Initial Catalog=QCH;uid=sa;pwd=123;</hong>
        //  <hong name="video1">192.168.100.23</hong>
        //  <hong name="video2">192.168.100.24</hong>
        //  <hong name="video3">192.168.100.20</hong>
        //  <hong name="video4">192.168.100.25</hong>
        //  <hong name="qu">五家沟</hong>
        //  <hong name="Card_Com">COM3</hong>
        //  <hong name="Card_Baudrate">57600</hong>
        //  <hong name="Card_Address">出磅称重点1</hong>
        //  <hong name="flow">出磅</hong>
        //  <hong name="led_IP">192.168.100.32</hong>
        //  <hong name="led_wight">96</hong>
        //  <hong name="led_height">32</hong>
        //  <hong name="Model_Com">COM4</hong>
        //  <hong name="Model_Com_Send_Txt">40 30 31 30 32 0d</hong>
        //  <hong name="Model_Com_Send_Txt_2">40 30 31 30 31 0d</hong>
        //  <hong name="Model_Com_Down_Txt">40 30 31 30 30 0d</hong>
        //  <hong name="Yibiao_Com">COM1</hong>
        //  <hong name="Yibiao_Baute">1200</hong>
        //  <hong name="BandDan">WyyyyMMdd</hong>
        //  <hong name="data_path">D:/images/</hong>
        //  <hong name="no_print"></hong>
        //</han>
    }
}
