using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskHelper = ProjectFlow.Tasks.TaskHelper;

namespace ProjectFlowUnitTest
{
    [TestClass]
    public class TaskUnitTest
    {

        [TestMethod]
        public void WhenTaskNameIsNull_ReturnFALSE()
        {

            TaskHelper taskHelper = new TaskHelper();
            bool result = taskHelper.VerifyAddTask(null,
                "Verify that task name is null is checked",
                -1,
                "21/12/2019",
                "2/1/2020",
                1);

            Assert.IsFalse(result);

        }

        [TestMethod]
        public void WhenTaskNameIsOver255_ReturnFALSE()
        {

            TaskHelper taskHelper = new TaskHelper();
            bool result = taskHelper.VerifyAddTask("Test is task name checks if the number of character limit is 255 Test is task name checks if the number of character limit is 255 Test is task name checks if the number of character limit is 255 Test is task name checks if the number of character limit is 255 Test is task name checks if the number of character limit is 255 Test is task name checks if the number of character limit is 255 Test is task name checks if the number of character limit is 255 Test is task name checks if the number of character limit is 255 Test is task name checks if the number of character limit is 255 Test is task name checks if the number of character limit is 255 Test is task name checks if the number of character limit is 255 Test is task name checks if the number of character limit is 255 Test is task name checks if the number of character limit is 255 Test is task name checks if the number of character limit is 255 Test is task name checks if the number of character limit is 255 Test is task name checks if the number of character limit is 255 Test is task name checks if the number of character limit is 255 Test is task name checks if the number of character limit is 255 Test is task name checks if the number of character limit is 255 Test is task name checks if the number of character limit is 255 Test is task name checks if the number of character limit is 255 ",
                "Test is task name checks if the number of character limit is 255",
                -1,
                "21/11/2019",
                "12/12/2019",
                1);

            Assert.IsFalse(result);

        }

        [TestMethod]
        public void WhenTaskDescIsNull_ReturnFALSE()
        {

            TaskHelper taskHelper = new TaskHelper();
            bool result = taskHelper.VerifyAddTask("Verify Task Desc",
                null,
                -1,
                "21/11/2019",
                "12/12/2019",
                1);

            Assert.IsFalse(result);

        }

        [TestMethod]
        public void WhenTaskDescIsOver255_ReturnFALSE()
        {

            TaskHelper taskHelper = new TaskHelper();
            bool result = taskHelper.VerifyAddTask("Verify Task Desc",
                "Test is task desc checks if the number of character limit is 255 Test is task desc checks if the number of character limit is 255 Test is task desc checks if the number of character limit is 255 Test is task desc checks if the number of character limit is 255 Test is task desc checks if the number of character limit is 255 Test is task desc checks if the number of character limit is 255 Test is task desc checks if the number of character limit is 255 Test is task desc checks if the number of character limit is 255 Test is task desc checks if the number of character limit is 255 Test is task desc checks if the number of character limit is 255 Test is task desc checks if the number of character limit is 255 Test is task desc checks if the number of character limit is 255 Test is task desc checks if the number of character limit is 255 Test is task desc checks if the number of character limit is 255 Test is task desc checks if the number of character limit is 255 Test is task desc checks if the number of character limit is 255 Test is task desc checks if the number of character limit is 255 Test is task desc checks if the number of character limit is 255 Test is task desc checks if the number of character limit is 255 Test is task desc checks if the number of character limit is 255 Test is task desc checks if the number of character limit is 255 ",
                -1,
                "21/11/2019",
                "12/12/2019",
                1);

            Assert.IsFalse(result);

        }

        [TestMethod]
        public void WhenMilestoneIsInvalid_ReturnFALSE()
        {

            TaskHelper taskHelper = new TaskHelper();
            bool result = taskHelper.VerifyAddTask("Verify Milestone null",
                "Check that verification catches milesone errors",
                -2,
                "21/11/2019",
                "12/12/2019",
                1);

            Assert.IsFalse(result);

        }

        [TestMethod]
        public void WhenStartDateIsNull_ReturnFALSE()
        {

            TaskHelper taskHelper = new TaskHelper();
            bool result = taskHelper.VerifyAddTask("Verify StartDate null",
                "Check that verification catches milesone errors",
                1,
                "12/12/2019",
                null,
                1);

            Assert.IsFalse(result);

        }

        [TestMethod]
        public void WhenStartDateIsInvalidFormat_ReturnFALSE()
        {

            TaskHelper taskHelper = new TaskHelper();
            bool result = taskHelper.VerifyAddTask("Check invalid startDate format",
                "check that verification catches invalid start date",
                1,
                "13/1231/5216",
                "12/12/2019",
                1);

            Assert.IsFalse(result);

        }

        [TestMethod]
        public void WhenEndDateIsNull_ReturnFALSE()
        {

            TaskHelper taskHelper = new TaskHelper();
            bool result = taskHelper.VerifyAddTask("Check null endDate",
                "check that verification catches null end date",
                1,
                "12/12/2019",
                null,
                1);

            Assert.IsFalse(result);

        }

        [TestMethod]
        public void WhenEndDateIsInvalidFormat_ReturnFALSE()
        {

            TaskHelper taskHelper = new TaskHelper();
            bool result = taskHelper.VerifyAddTask("Check invalid endDate format",
                "check that verification catches invalid end date",
                1,
                "12/12/2019",
                "13/1231/5216",
                1);

            Assert.IsFalse(result);

        }

        [TestMethod]
        public void WhenEndDate_EARILERTHAN_StartDate_ReturnFALSE()
        {

            TaskHelper taskHelper = new TaskHelper();
            bool result = taskHelper.VerifyAddTask("Check if endDate can be eariler that start date",
                "Check that verification catches error when end date is eariler that start date",
                1,
                "20/1/2020",
                "19/12/2019",
                1);

            Assert.IsFalse(result);

        }

        [TestMethod]
        public void WhenStatusIdIsInvalid_ReturnFALSE()
        {

            TaskHelper taskHelper = new TaskHelper();
            bool result = taskHelper.VerifyAddTask("Verify StatusId is Invalid",
                "Check that verification catches invalid status index",
                1,
                "20/1/2020",
                "19/12/2019",
                -1);

            Assert.IsFalse(result);

        }
    }
}
