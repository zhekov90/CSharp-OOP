namespace Robots.Tests
{
    using NUnit.Framework;
    using System;

    public class RobotsTests
    {
        private Robot robot;
        private RobotManager manager;
        [SetUp]
        public void Setup()
        {
            robot = new Robot("Test", 10);
            manager = new RobotManager(10);
        }
        [Test]
        public void WhenRobotIsCreated_PropsShoudBeSet()
        {
            Assert.AreEqual("Test", robot.Name);
            Assert.AreEqual(10, robot.Battery);
            Assert.AreEqual(10, robot.MaximumBattery);
        }

        [Test]
        public void WhenRobotManagerIsCreated_CapacityShoudBeSet()
        {
            Assert.AreEqual(10, manager.Capacity);
        }

        [Test]
        public void WhenRobotManagerCapacityIsNegative_CapacityShoudThrowException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                RobotManager roboManager = new RobotManager(-5);
            });
        }

        [Test]
        public void WhenRobotManagerIsCreated_CountShouldBeZero()
        {
            Assert.AreEqual(0, manager.Count);
        }

        [Test]
        public void WhenRobotManagerIsAddedAgain_ShoudThrowException()
        {
            manager.Add(robot);
            Assert.Throws<InvalidOperationException>(() =>
            {

                manager.Add(robot);
            });
        }

        [Test]
        public void WhenRobotManagerIsAddedAndNotEnoughCapacity_ShoudThrowException()
        {
            RobotManager roboManager = new RobotManager(1);
            roboManager.Add(robot);
            Robot robot2 = new Robot("ivan", 3);
            Assert.Throws<InvalidOperationException>(() =>
            {

                roboManager.Add(robot2);
            });
        }

        [Test]
        public void WhenAddWithCorrectData()
        {
            manager.Add(robot);
            Assert.AreEqual(1, manager.Count);
        }

        [Test]
        public void WhenRemoveUnexsistingRobot()
        {
            manager.Add(robot);
            Assert.Throws<InvalidOperationException>(() =>
            {
                manager.Remove("Ivan");
            });
        }


        [Test]
        public void WhenRemoveExsistingRobot()
        {
            manager.Add(robot);
            manager.Remove("Test");
            Assert.AreEqual(0, manager.Count);
        }

        [Test]
        public void WhenRobotUnexistingRobotWorks_ShoudThrowException()
        {
            RobotManager roboManager = new RobotManager(1);
            roboManager.Add(robot);
            Assert.Throws<InvalidOperationException>(() =>
            {

                roboManager.Work("ivan", "raboti", 2);
            });
        }

        [Test]
        public void WhenRobotExistingRobotWorksWithoutEnoughCapacity_ShoudThrowException()
        {
            RobotManager roboManager = new RobotManager(1);
            roboManager.Add(robot);
            Assert.Throws<InvalidOperationException>(() =>
            {

                roboManager.Work("Test", "raboti", 11);
            });
        }

        [Test]
        public void WhenExistingRobotWorksWithEnoughCapacity_ShoudWorkCorrectly()
        {
            RobotManager roboManager = new RobotManager(1);
            roboManager.Add(robot);

            roboManager.Work("Test", "raboti", 2);

            Assert.AreEqual(8, robot.Battery);
        }

        [Test]
        public void WhenUnexistingRobotCharges_ShoudThrowException()
        {
            RobotManager roboManager = new RobotManager(1);
            roboManager.Add(robot);

            Assert.Throws<InvalidOperationException>(() =>
            {
                roboManager.Charge("ivan");
            });
        }

        [Test]
        public void WhenExistingRobotCharges_ShoudChargeCorrectly()
        {
            RobotManager roboManager = new RobotManager(1);
            roboManager.Add(robot);
            roboManager.Work("Test", "raboti", 1);
            roboManager.Work("Test", "raboti", 1);
            roboManager.Charge("Test");
            Assert.AreEqual(robot.MaximumBattery, robot.Battery);
        }
    }
}
