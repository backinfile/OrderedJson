using Microsoft.VisualStudio.TestTools.UnitTesting;
using OJApp;
using OrderedJson.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OJTest
{
    [TestClass]
    public class OJLogicTest
    {
        private const string Filename = "hello.oj";
        OJParser parser;
        GameContext context;

        [TestInitialize]
        public void Setup()
        {
            context = new GameContext();
            context.hostCard = new Card() { health = 10 };

            Dictionary<string, string> remaps = new Dictionary<string, string>();
            remaps.Add("iftrue", "ifelse:true,$1,$2");

            parser = new OJParser(typeof(CardCommondDefiner), remaps);
        }

        [TestMethod]
        public void ORTest()
        {
            string code = "if:false or true, IncreaseHealth";
            IOJMethod method = parser.Parse(code, Filename);
            method.Invoke(context);
            Assert.AreEqual(15, context.hostCard.health);
        }

        [TestMethod]
        public void ORTest2()
        {
            string code = "if:false or false, IncreaseHealth";
            IOJMethod method = parser.Parse(code, Filename);
            method.Invoke(context);
            Assert.AreEqual(10, context.hostCard.health);
        }
        [TestMethod]
        public void AndTest1()
        {
            string code = "if:true && false, IncreaseHealth";
            IOJMethod method = parser.Parse(code, Filename);
            method.Invoke(context);
            Assert.AreEqual(10, context.hostCard.health);
        }
        [TestMethod]
        public void AndTest2()
        {
            string code = "if:true && true, IncreaseHealth";
            IOJMethod method = parser.Parse(code, Filename);
            method.Invoke(context);
            Assert.AreEqual(15, context.hostCard.health);
        }

        [TestMethod]
        public void CmpTest2()
        {
            string code = "if:2 > 1, IncreaseHealth";
            IOJMethod method = parser.Parse(code, Filename);
            method.Invoke(context);
            Assert.AreEqual(15, context.hostCard.health);
        }

        [TestMethod]
        public void AddTest1()
        {
            string code = "IncreaseHealth: 4+2";
            IOJMethod method = parser.Parse(code, Filename);
            method.Invoke(context);
            Assert.AreEqual(16, context.hostCard.health);
        }
        [TestMethod]
        public void MulTest2()
        {
            string code = "IncreaseHealth: 4*2";
            IOJMethod method = parser.Parse(code, Filename);
            method.Invoke(context);
            Assert.AreEqual(18, context.hostCard.health);
        }

        [TestMethod]
        public void MulTest3()
        {
            string code = "IncreaseHealth: 4+2*3";
            IOJMethod method = parser.Parse(code, Filename);
            method.Invoke(context);
            Assert.AreEqual(20, context.hostCard.health);
        }

        [TestMethod]
        public void MulTest4()
        {
            string code = "IncreaseHealth: 4+2*health";
            IOJMethod method = parser.Parse(code, Filename);
            method.Invoke(context);
            Assert.AreEqual(34, context.hostCard.health);
        }

        [TestMethod]
        public void MulTest5()
        {
            string code = "IncreaseHealth: 4+2*{health+1}";
            IOJMethod method = parser.Parse(code, Filename);
            method.Invoke(context);
            Assert.AreEqual(36, context.hostCard.health);
        }

        [TestMethod]
        public void NotTest1()
        {
            string code = "if:!false, IncreaseHealth: 1";
            IOJMethod method = parser.Parse(code, Filename);
            method.Invoke(context);
            Assert.AreEqual(11, context.hostCard.health);
        }

        [TestMethod]
        public void NegTest1()
        {
            string code = "if:not false, IncreaseHealth: -1";
            IOJMethod method = parser.Parse(code, Filename);
            method.Invoke(context);
            Assert.AreEqual(9, context.hostCard.health);
        }
    }
}
