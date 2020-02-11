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
            bool result = taskHelper.VerifyAddTask(2, null,
                "Test when task name is null",
                "4",
                "21/12/2019",
                "2/1/2020",
                "1",
                "1");

            Assert.IsFalse(result);

        }

        [TestMethod]
        public void WhenTaskNameIsOver255_ReturnFALSE()
        {

            TaskHelper taskHelper = new TaskHelper();
            bool result = taskHelper.VerifyAddTask(2, "Character Test for Name….................................................................................................….................................................................................................….................................................................................................….................................................................................................….................................................................................................….................................................................................................….................................................................................................….................................................................................................",
                "Test when task name > 255 Characters",
                "4",
                "21/11/2019",
                "12/12/2019",
                "1",
                "1");

            Assert.IsFalse(result);

        }

        [TestMethod]
        public void WhenTaskDescIsNull_ReturnFALSE()
        {

            TaskHelper taskHelper = new TaskHelper();
            bool result = taskHelper.VerifyAddTask(2, "Null Test for Description",
                null,
                "4",
                "21/11/2019",
                "12/12/2019",
                "1",
                "1");

            Assert.IsFalse(result);

        }

        [TestMethod]
        public void WhenTaskDescIsOver255_ReturnFALSE()
        {

            TaskHelper taskHelper = new TaskHelper();
            bool result = taskHelper.VerifyAddTask(2, "Character Test for Description",
                "Test when task description has > 255 Characters….....................................................................................................................................................................................................................",
                "4",
                "21/11/2019",
                "12/12/2019",
                "1",
                "1");

            Assert.IsFalse(result);

        }

        [TestMethod]
        public void WhenMilestoneIsInvalid_ReturnFALSE()
        {

            TaskHelper taskHelper = new TaskHelper();
            bool result = taskHelper.VerifyAddTask(2, "Invalid Milestone Test",
                "Test when user gets past client-side validation for milestone",
                "-2",
                "21/11/2019",
                "12/12/2019",
                "1",
                "1");

            Assert.IsFalse(result);

        }

        [TestMethod]
        public void WhenStartDateIsNull_ReturnFALSE()
        {

            TaskHelper taskHelper = new TaskHelper();
            bool result = taskHelper.VerifyAddTask(2, "Empty Start Date Test",
                "Test when start date is not filled",
                "4",
                "12/12/2019",
                null,
                "1",
                "1");

            Assert.IsFalse(result);

        }

        [TestMethod]
        public void WhenStartDateIsInvalidFormat_ReturnFALSE()
        {

            TaskHelper taskHelper = new TaskHelper();
            bool result = taskHelper.VerifyAddTask(2, "Invalid Start Date Test",
                "Test when an invalid start date is filled",
                "4",
                "21/31/2019",
                "2/1/2019",
                "1",
                "1");

            Assert.IsFalse(result);

        }

        [TestMethod]
        public void WhenEndDateIsNull_ReturnFALSE()
        {

            TaskHelper taskHelper = new TaskHelper();
            bool result = taskHelper.VerifyAddTask(2, "Empty End Date Test",
                "Test when end date is not filled",
                "4",
                "12/12/2019",
                null,
                "1",
                "1");

            Assert.IsFalse(result);

        }

        [TestMethod]
        public void WhenEndDateIsInvalidFormat_ReturnFALSE()
        {

            TaskHelper taskHelper = new TaskHelper();
            bool result = taskHelper.VerifyAddTask(2, "Invalid End Date Test",
                "Test when an invalid end date is filled",
                "4",
                "21/12/2019",
                "12/31/2020",
                "1",
                "1");

            Assert.IsFalse(result);

        }

        [TestMethod]
        public void WhenEndDate_EARILERTHAN_StartDate_ReturnFALSE()
        {

            TaskHelper taskHelper = new TaskHelper();
            bool result = taskHelper.VerifyAddTask(2, "Start Date later than End Date Test",
                "Test when start date is later than end date",
                "4",
                "2/1/2020",
                "121/12/2019",
                "1",
                "1");

            Assert.IsFalse(result);

        }

        [TestMethod]
        public void WhenStatusIdIsInvalid_ReturnFALSE()
        {

            TaskHelper taskHelper = new TaskHelper();
            bool result = taskHelper.VerifyAddTask(2, "Invalid Status Test",
                "Test when an invalid status is filled",
                "4",
                "2/1/2019",
                "21/12/2020",
                "-2",
                "1");

            Assert.IsFalse(result);

        }

        [TestMethod]
        public void WhenStatusIdIsEmpty_ReturnFALSE()
        {

            TaskHelper taskHelper = new TaskHelper();
            bool result = taskHelper.VerifyAddTask(2, "Invalid Status Test",
                "Test when an invalid status is filled",
                "4",
                "20/1/2019",
                "19/12/2020",
                null,
                "1");

            Assert.IsFalse(result);

        }

        [TestMethod]
        public void WhenPriorityIdIsInvalid_ReturnFALSE()
        {

            TaskHelper taskHelper = new TaskHelper();
            bool result = taskHelper.VerifyAddTask(2, "Invalid Priority Test",
                "Test when priority is invalid",
                "4",
                "20/1/2019",
                "19/12/2020",
                "-2",
                "-1");

            Assert.IsFalse(result);

        }

        [TestMethod]
        public void WhenPriorityIdIsEmpty_ReturnFALSE()
        {

            TaskHelper taskHelper = new TaskHelper();
            bool result = taskHelper.VerifyAddTask(2, "Empty Priority Test",
                "Test when priority is empty",
                "4",
                "20/1/2019",
                "19/12/2020",
                "-2",
                "-1");

            Assert.IsFalse(result);

        }

        [TestMethod]
        public void WhenAllFieldsCorrect_ReturnTRUE()
        {

            TaskHelper taskHelper = new TaskHelper();
            bool result = taskHelper.VerifyAddTask(2, "Create UI Wireframe Test",
                "Create UI wireframe for Task Management",
                "4",
                "20/1/2019",
                "19/12/2020",
                "1",
                "1");

            Assert.IsTrue(result);

        }
    }
}
