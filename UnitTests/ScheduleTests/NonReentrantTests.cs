namespace FluentScheduler.Tests.UnitTests.ScheduleTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public class NonReentrantTests
    {
        [TestMethod]
        public void Should_Be_True_By_Default()
        {
            // Act
            var schedule = new Schedule(() => { });
            schedule.ToRunNow();

            // Assert
            Assert.IsTrue(schedule.Reentrant);
        }

        [TestMethod]
        public void Should_Default_Reentrent_Parameter_For_Child_Schedules()
        {
            // Act
            var schedule = new Schedule(() => { });
            schedule.ToRunNow().AndEvery(1).Minutes();

            // Assert
            Assert.IsTrue(schedule.Reentrant);
            foreach (var child in schedule.AdditionalSchedules)
                Assert.IsTrue(child.Reentrant);
        }

        [TestMethod]
        public void Should_Set_Reentrent_Parameter_For_Child_Schedules()
        {
            // Act
            var schedule = new Schedule(() => { });
            schedule.NonReentrant().ToRunNow().AndEvery(1).Minutes();

            // Assert
            Assert.IsFalse(schedule.Reentrant);
            foreach (var child in schedule.AdditionalSchedules)
                Assert.IsFalse(child.Reentrant);
        }
    }
}
