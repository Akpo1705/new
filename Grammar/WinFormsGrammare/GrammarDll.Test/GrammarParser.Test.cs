using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GrammarDll.Test
{
    [TestClass]
    public class GrammarParserTest
    {



        [TestMethod]
        public void InGrammarParser_xyz_return_xzy()
        {
            string expected = "xyz";

            string actual = GrammarParser.Parse("xyz");

            Assert.AreEqual(expected, actual);
        }




        //���� � �������� - ��� ���������� ������
        [TestMethod]
        public void InGrammarParser_20xy0_return_xyxy()
        {
            string expected = "xyxy";

            string actual = GrammarParser.Parse("2[xy]");

            Assert.AreEqual(expected, actual);
        }




        //���� � �������� - ��� ���������� ������
        [TestMethod]
        public void InGrammarParser_20x02y00_return_xyxy()
        {
            string expected = "xyyxyy";

            string actual = GrammarParser.Parse("2[x2[y]]");

            Assert.AreEqual(expected, actual);
        }




        //���� � �������� - ��� ���������� ������
        [TestMethod]
        public void InGrammarParser_30xyz02za0bc_return_xyxy()
        {
            string expected = "xyzxyzxyzzazabc";

            string actual = GrammarParser.Parse("3[xyz]2[za]bc");

            Assert.AreEqual(expected, actual);
        }




        //���� � �������� - ��� ���������� ������
        [TestMethod]
        [ExpectedException(typeof(FormatException), "����������� ����� ������")]
        public void InGrammarParser_30xyz2za0bc_return_xyxy()
        {
            string actual = GrammarParser.Parse("3[xyz2[za]bc");
        }




        //���� � �������� - ��� ���������� ������
        [TestMethod]
        [ExpectedException(typeof(FormatException), "�� ������ ������ ������, ������ ����� ������ ��������")]
        public void InGrammarParser_30xyz0100020za0bc_return_xyxy()
        {
            string actual = GrammarParser.Parse("3[xyz]10[]2[za]bc");
        }




        //���� � �������� - ��� ���������� ������
        [TestMethod]
        [ExpectedException(typeof(FormatException), "���������� ������ �������")]
        public void InGrammarParser_30xyz0za0bc_return_xyxy()
        {
            string actual = GrammarParser.Parse("3[xyz][za]bc");
        }



        
        //���� � �������� - ��� ���������� ������
        [TestMethod]
        [ExpectedException(typeof(FormatException), "������ �� ������������� ������, ��������� ������������� ������� ������")]
        public void InGrammarParser_030xyz0za0bc0_return_xyxy()
        {
            string actual = GrammarParser.Parse("[3[xyz][za]bc]");
        }
    }
}
