using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Кинотеатр;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Фильм()
        {
            Form2 form = new Form2();
            form.Фильм();
        }

        [TestMethod]
        public void Фильм2()
        {
            Form2 form = new Form2();
            form.Фильм2();
        }

        [TestMethod]
        public void Ticket()
        {
            Form2 form = new Form2();
            form.Ticket();
        }

        [TestMethod]
        public void ID()
        {
            Form2 form = new Form2();
            form.ID();
        }
    }
}
